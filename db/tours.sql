DROP TABLE IF EXISTS "Tours";

CREATE TABLE "Tours"
(
    "Id"        SERIAL PRIMARY KEY,
    "Name"      VARCHAR(200) NOT NULL,
    "Price"     NUMERIC(18, 2) NOT NULL CHECK ("Price" >= 0),
    "City"      VARCHAR(100) NOT NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT (NOW())
);

SELECT
    "Id",
    "Name",
    "Price",
    "City",
    "CreatedAt"
FROM "Tours"
ORDER BY "CreatedAt" DESC, "Id" DESC
LIMIT 10;

CREATE INDEX IF NOT EXISTS "IX_Tours_City" ON "Tours" ("City");

