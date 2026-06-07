# Requirements

## Project Objective

Build an internal administrative request system that allows employees to submit requests and allows administration users to review, update, approve, reject, and close those requests.

## User Roles

- Employee: creates requests and views status.
- Manager: reviews operational requests and adds comments.
- Admin: changes status, approves or rejects requests, and monitors queue health.

## Functional Requirements

1. Users can view all administrative requests.
2. Users can search requests by title, description, or category.
3. Users can create a new request with title, description, category, priority, and requester.
4. System validates required fields before creating a request.
5. Admin users can update request status.
6. System stores request status history for audit tracking.
7. Users can add comments to a request.
8. Dashboard shows request counts by status and priority.
9. API supports REST Webservices for normal application use.
10. API supports GraphQL-style query endpoint for flexible dashboard and search scenarios.

## Non-Functional Requirements

- The system should be simple to run locally.
- API responses should use JSON.
- Request validation should be clear and predictable.
- Code should be organized by models, DTOs, data access, and API endpoints.
- SQL schema should support relational database design and future Entity Framework migration.
- Documentation should include SDLC notes and test cases.

## Learning Stack Note

This project is designed as a practical learning project for C#, ASP.NET Core, Angular, Webservices, GraphQL-style querying, relational databases, and SDLC. The runnable backend uses an in-memory repository to avoid dependency issues, while the database folder contains a SQL Server schema for relational implementation.

