# Administrative Request Management System

An end-to-end internal administrative workflow application built for an entry-level Software Engineer portfolio. The project demonstrates C#, ASP.NET Core Web API, REST Webservices, GraphQL-style querying, SQL database design, requirements documentation, validation, test cases, Git-ready structure, and an Angular frontend source layout.

## What This Project Does

Employees can submit administrative requests such as access requests, device support, ID card updates, software installation, travel support, or document approvals. Admin users can review requests, add comments, change status, and track the full audit history.

## Why This Project Is Useful

This project matches common enterprise software work:

- Collecting and documenting user requirements
- Designing and developing administrative software applications
- Building REST API Webservices with C# and ASP.NET Core
- Modeling relational database tables with SQL
- Practicing Entity Framework-style entities and repository patterns
- Supporting validation, test cases, release notes, and maintenance
- Practicing Angular component structure and frontend API integration
- Adding GraphQL-style query access for flexible dashboard/search use cases

## Tech Stack

- Backend: C#, ASP.NET Core Web API, .NET 8
- API styles: REST Webservices and GraphQL-style endpoint
- Database design: SQL Server schema included
- Frontend: Angular source structure plus a runnable static demo
- Tools: Git, GitHub, Azure DevOps-style documentation, CI/CD-ready layout

## Folder Structure

```text
Administrative_Request_Management_System/
  backend/AdminRequest.Api/          C# ASP.NET Core API
  database/                          SQL Server schema and seed data
  frontend/angular-admin-request-system/ Angular source files
  docs/                              Requirements, SDLC notes, test cases
```

## Run The Backend

Install the **.NET 8 SDK** first. The .NET Runtime alone is not enough for `dotnet run`.

```bash
cd backend/AdminRequest.Api
dotnet run
```

Open:

- API health: `http://localhost:5084/api/health`
- Static demo UI: `http://localhost:5084`
- Requests API: `http://localhost:5084/api/requests`
- Dashboard API: `http://localhost:5084/api/dashboard`

## GraphQL-Style Query Examples

POST to `/graphql`:

```json
{
  "query": "requests"
}
```

```json
{
  "query": "dashboard"
}
```

```json
{
  "query": "request",
  "variables": {
    "id": "REQ-1001"
  }
}
```


Built an Administrative Request Management System using C#, ASP.NET Core, Angular, SQL Server schema design, REST Webservices, GraphQL-style querying, Git, validation rules, test cases, and SDLC documentation to manage employee admin requests, approvals, comments, and audit history.
