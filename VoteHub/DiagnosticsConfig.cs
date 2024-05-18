using System.Diagnostics.Metrics;

namespace VoteHub;

public static class DiagnosticsConfig
{
    public const string ServiceName = "VoteHub";
    public static Meter Meter = new Meter(ServiceName);

    public static Counter<int> VotesCounter = Meter.CreateCounter<int>("voting.count");
}
