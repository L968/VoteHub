using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text;
using VoteHub;
using VoteHub.Mapping;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
var jwtKey = builder.Configuration.GetValue<string>("Jwt:Key") ?? throw new ArgumentNullException("Jwt:Key");
var cassandraContactPoint = builder.Configuration.GetValue<string>("Cassandra:ContactPoint") ?? throw new ArgumentNullException("Cassandra:ContactPoint");
var cassandraKeyspace = builder.Configuration.GetValue<string>("Cassandra:Keyspace") ?? throw new ArgumentNullException("Cassandra:Keyspace");
var openTelemetryKey = builder.Configuration.GetValue<string>("OpenTelemetry:Key") ?? throw new ArgumentNullException("OpenTelemetry:Key");

builder.Services.AddSingleton(sp =>
{
    var cluster = Cluster.Builder()
                        .AddContactPoint(cassandraContactPoint)
                        .Build();

    return cluster.Connect(cassandraKeyspace);
});

builder.Services.AddSingleton<IMapper>(sp =>
{
    var session = sp.GetRequiredService<Cassandra.ISession>();

    var mappingConfiguration = new MappingConfiguration();
    mappingConfiguration.Define<VoteMapping>();
    mappingConfiguration.Define<ParticipantMapping>();
    mappingConfiguration.Define<VotingSessionMapping>();

    return new Mapper(session, mappingConfiguration);
});

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(token =>
{
    token.RequireHttpsMetadata = false;
    token.SaveToken = true;
    token.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(resourceBuilder => resourceBuilder.AddService(DiagnosticsConfig.ServiceName))
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();

        metrics.AddMeter(DiagnosticsConfig.Meter.Name);

        metrics.AddOtlpExporter();
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();

        tracing.AddOtlpExporter();
    });

builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());

builder.Services.Configure<OtlpExporterOptions>(o => o.Headers = $"x-otlp-api-key={openTelemetryKey}");

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddCarter();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapCarter();

app.UseAuthorization();

app.MapControllers();

app.Run();
