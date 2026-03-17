DROP TABLE IF EXISTS "Users";

CREATE TABLE "Users"
(
    "Id"           SERIAL PRIMARY KEY,
    "Username"     VARCHAR(100) NOT NULL,
    "Password"     TEXT NOT NULL,
    "CreatedAt"    TIMESTAMPTZ  NOT NULL DEFAULT (NOW())
);

CREATE UNIQUE INDEX IF NOT EXISTS "UX_Users_Username" ON "Users" ("Username");

