CREATE KEYSPACE IF NOT EXISTS votehub
WITH replication = { 'class': 'SimpleStrategy', 'replication_factor': 1 };

USE votehub;

CREATE TABLE IF NOT EXISTS voting_session (
    session_id UUID,
    start_time TIMESTAMP,
    end_time TIMESTAMP,
    PRIMARY KEY (session_id)
);

INSERT INTO voting_session (session_id, start_time, end_time) VALUES (b1896049-9f28-492f-84fc-f1682ad39918, toTimestamp(now()), toTimestamp(now()) + 10d);

CREATE TABLE IF NOT EXISTS participant (
    session_id UUID,
    participant_id UUID,
    name TEXT,
    image_path TEXT,
    PRIMARY KEY (session_id, participant_id)
);

INSERT INTO participant (participant_id, session_id, name, image_path) VALUES (e8b9e6e0-b1c5-4e6c-b60d-6947aefa7a5e, b1896049-9f28-492f-84fc-f1682ad39918, 'Participant 1', 'image1.jpg');
INSERT INTO participant (participant_id, session_id, name, image_path) VALUES (5e92b7c0-59c8-4a36-b547-6d10b06de726, b1896049-9f28-492f-84fc-f1682ad39918, 'Participant 2', 'image2.jpg');
INSERT INTO participant (participant_id, session_id, name, image_path) VALUES (01b7e47d-34b4-4634-9b77-342470ee14db, b1896049-9f28-492f-84fc-f1682ad39918, 'Participant 3', 'image3.jpg');

CREATE TABLE IF NOT EXISTS vote (
    vote_id UUID,
    session_id UUID,
    participant_id UUID,
    user_id INT,
    timestamp TIMESTAMP,
    PRIMARY KEY (session_id, participant_id, vote_id)
);

INSERT INTO vote (vote_id, session_id, participant_id, user_id, timestamp) VALUES (uuid(), b1896049-9f28-492f-84fc-f1682ad39918, 5e92b7c0-59c8-4a36-b547-6d10b06de726, 2, toTimestamp(now()));
INSERT INTO vote (vote_id, session_id, participant_id, user_id, timestamp) VALUES (uuid(), b1896049-9f28-492f-84fc-f1682ad39918, 5e92b7c0-59c8-4a36-b547-6d10b06de726, 2, toTimestamp(now()));

INSERT INTO vote (vote_id, session_id, participant_id, user_id, timestamp) VALUES (uuid(), b1896049-9f28-492f-84fc-f1682ad39918, e8b9e6e0-b1c5-4e6c-b60d-6947aefa7a5e, 2, toTimestamp(now()));
INSERT INTO vote (vote_id, session_id, participant_id, user_id, timestamp) VALUES (uuid(), b1896049-9f28-492f-84fc-f1682ad39918, e8b9e6e0-b1c5-4e6c-b60d-6947aefa7a5e, 2, toTimestamp(now()));
INSERT INTO vote (vote_id, session_id, participant_id, user_id, timestamp) VALUES (uuid(), b1896049-9f28-492f-84fc-f1682ad39918, e8b9e6e0-b1c5-4e6c-b60d-6947aefa7a5e, 2, toTimestamp(now()));
