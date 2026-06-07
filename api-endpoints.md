# API Endpoints

Base URL:

```text
http://localhost:5084
```

## Health

```http
GET /api/health
```

Checks whether the API is running.

## Users

```http
GET /api/users
```

Returns seeded users.

## Dashboard

```http
GET /api/dashboard
```

Returns request counts by status and priority.

## Requests

```http
GET /api/requests
GET /api/requests?status=Submitted
GET /api/requests?search=software
GET /api/requests/{id}
```

Returns request list and request detail.

## Create Request

```http
POST /api/requests
```

```json
{
  "title": "Azure DevOps access request",
  "description": "Need Azure DevOps access for sprint tasks.",
  "category": "Software Access",
  "priority": "Medium",
  "requestedByUserId": "USR-001"
}
```

## Update Status

```http
PATCH /api/requests/{id}/status
```

```json
{
  "status": "Approved",
  "changedByUserId": "USR-002",
  "reason": "Approved after admin review."
}
```

## Add Comment

```http
POST /api/requests/{id}/comments
```

```json
{
  "userId": "USR-002",
  "message": "Please attach manager approval."
}
```

## GraphQL-Style Endpoint

```http
POST /graphql
```

Dashboard query:

```json
{
  "query": "dashboard",
  "variables": {}
}
```

Request detail query:

```json
{
  "query": "request",
  "variables": {
    "id": "REQ-1001"
  }
}
```

