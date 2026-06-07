# Test Cases

| Test ID | Scenario | Steps | Expected Result |
|---|---|---|---|
| TC-001 | API health check | Call `GET /api/health` | API returns `Healthy` status |
| TC-002 | View request list | Call `GET /api/requests` | API returns seeded requests |
| TC-003 | Search requests | Call `GET /api/requests?search=software` | API returns matching software access request |
| TC-004 | View request detail | Call `GET /api/requests/REQ-1001` | API returns request detail, comments, and history |
| TC-005 | Create valid request | Submit title, description, category, priority, requester | API creates request with `Submitted` status |
| TC-006 | Create invalid request | Submit empty title | API returns validation error |
| TC-007 | Invalid requester | Submit unknown requester id | API returns validation error |
| TC-008 | Update status | Patch request status to `Approved` | API updates status and adds audit history |
| TC-009 | Add comment | Add admin comment to request | API stores comment and updates timestamp |
| TC-010 | Dashboard counts | Create request, then call dashboard | Total and submitted counts update |
| TC-011 | GraphQL dashboard | POST query `dashboard` to `/graphql` | API returns dashboard data object |
| TC-012 | GraphQL request detail | POST query `request` with request id | API returns matching request detail |
| TC-013 | Static UI load | Open root URL | UI loads dashboard, form, and request queue |
| TC-014 | UI create request | Submit form from UI | New request appears in queue |
| TC-015 | UI approve request | Click approve on a request | Request status changes to `Approved` |

