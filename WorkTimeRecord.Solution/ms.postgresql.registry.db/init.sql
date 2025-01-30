CREATE TABLE IF NOT EXISTS user_worktime_record (
    user_name VARCHAR(50) PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(100),
    last_record TIMESTAMP,
    mode VARCHAR(20)
);

INSERT INTO user_worktime_record (user_name, first_name, last_name, last_record, mode)
VALUES ('rserrano', 'Ramón', 'Serrano Valero', '2024-05-01 08:00:00', 'Entrada');



