# SDLC Notes

## 1. Requirement Gathering

The system was designed for an internal administration team that needs to track employee support requests, approvals, comments, and audit history.

Key requirements:

- Employees need a simple request form.
- Admin users need a request queue.
- Managers/admins need status updates and comments.
- The business needs audit history for status changes.
- The system needs JSON APIs for frontend integration.

## 2. Design

The backend follows a simple layered structure:

- Models: domain entities such as request, user, comment, and history.
- DTOs: request and response objects used by API endpoints.
- Data: repository interface and in-memory implementation.
- Program: REST Webservices and GraphQL-style endpoint definitions.

The SQL Server schema supports:

- Users
- AdminRequests
- RequestComments
- RequestStatusHistory

## 3. Development

Development focused on:

- C# and ASP.NET Core API endpoints
- Validation rules for request creation
- Request status workflow
- Audit history tracking
- Search and dashboard aggregation
- Static UI for quick demonstration
- Angular source layout for frontend learning

## 4. Testing

Manual test cases are documented in `docs/test-cases.md`.

Important validation checks:

- Title is required.
- Description is required.
- Requester must exist.
- Status update requires a user id.
- Comment requires user id and message.

## 5. Release

The project is GitHub-ready. Recommended release steps:

1. Build backend using `dotnet build`.
2. Run backend using `dotnet run`.
3. Validate API health endpoint.
4. Test request creation and status update.
5. Upload full project folder to GitHub.

## 6. Maintenance

Future improvements:

- Replace in-memory repository with Entity Framework Core.
- Connect SQL Server database.
- Add login and role-based authorization.
- Add real GraphQL package such as Hot Chocolate.
- Add automated unit tests.
- Deploy backend to Azure App Service.
- Deploy database to Azure SQL.

