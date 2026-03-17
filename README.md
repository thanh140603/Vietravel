# Vietravel

## Demo

- Screenshot: `demo.png`

## Structure

- `db/`: SQL scripts
- `backend/`: ASP.NET Core API (Clean Architecture)
- `frontend/`: Next.js app (Tours list page)

## Database

Run `db/tours.sql` on PostgreSQL to create table + index.

## Backend (.NET)

From `backend/`:

```bash
dotnet build Vietravel.sln
dotnet run --project src/Vietravel.Tours.Api
```

Config:

- Connection string: `backend/src/Vietravel.Tours.Api/appsettings.json` → `ConnectionStrings:Default`
  - Recommended: override via environment variable `ConnectionStrings__Default`
  - Example:
    - `Host=localhost;Port=5432;Database=vietravel;Username=postgres;Password=YOUR_PASSWORD`
  - The API also auto-loads `backend/.env` at startup (simple `KEY=VALUE` lines, no overwrite of existing env vars).
- IP whitelist: `IpAllowList:AllowedIps`
- Auth: JWT (demo credentials in `Jwt:DemoUsername` / `Jwt:DemoPassword`)

## Frontend (Next.js)

From `frontend/`:

```bash
npm install
npm run dev
```

Optional env:

- Copy `frontend/.env.example` → `frontend/.env.local`
- Set `NEXT_PUBLIC_API_BASE_URL` (default: `http://localhost:5141`)

