\c users_management_db

CREATE TABLE "users"
(
    id serial PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE
);

INSERT INTO users(username, email)
VALUES ('User1', 'Email1'), ('User2', 'Email2');