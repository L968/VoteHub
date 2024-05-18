CREATE KEYSPACE IF NOT EXISTS votehub
WITH replication = { 'class': 'SimpleStrategy', 'replication_factor': 1 };

USE votehub;

CREATE TABLE IF NOT EXISTS voting_session_count (
    details_id UUID,
    vote_count COUNTER,
    PRIMARY KEY (details_id)
);

UPDATE voting_session_count SET vote_count = vote_count + 12 WHERE details_id = e8b9e6e0-b1c5-4e6c-b60d-6947aefa7a5e;
UPDATE voting_session_count SET vote_count = vote_count + 0 WHERE details_id = 5e92b7c0-59c8-4a36-b547-6d10b06de726;
UPDATE voting_session_count SET vote_count = vote_count + 85 WHERE details_id = 01b7e47d-34b4-4634-9b77-342470ee14db;

CREATE TABLE IF NOT EXISTS voting_session_details (
    details_id UUID,
    session_id UUID,
    participant_name TEXT,
    participant_image_path TEXT,
    start_time TIMESTAMP,
    end_time TIMESTAMP,
    PRIMARY KEY (details_id, session_id)
);

INSERT INTO voting_session_details (details_id, session_id, participant_name, participant_image_path, start_time, end_time) VALUES (e8b9e6e0-b1c5-4e6c-b60d-6947aefa7a5e, b1896049-9f28-492f-84fc-f1682ad39918, 'Participant 1', 'image1.jpg', toTimestamp(now()), toTimestamp(now()) + 10d);
INSERT INTO voting_session_details (details_id, session_id, participant_name, participant_image_path, start_time, end_time) VALUES (5e92b7c0-59c8-4a36-b547-6d10b06de726, b1896049-9f28-492f-84fc-f1682ad39918, 'Participant 2', 'image2.jpg', toTimestamp(now()), toTimestamp(now()) + 10d);
INSERT INTO voting_session_details (details_id, session_id, participant_name, participant_image_path, start_time, end_time) VALUES (01b7e47d-34b4-4634-9b77-342470ee14db, b1896049-9f28-492f-84fc-f1682ad39918, 'Participant 3', 'image3.jpg', toTimestamp(now()), toTimestamp(now()) + 10d);
