\connect users_management_db

CREATE TABLE users
(
    id serial PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL
);

ALTER TABLE "users" ;

INSERT INTO users(username, email) VALUES ("User1", "Email1");
INSERT INTO users(username, email) VALUES ("User2", "Email2");