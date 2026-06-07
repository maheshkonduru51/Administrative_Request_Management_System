using AdminRequest.Api.Data;
using AdminRequest.Api.DTOs;
using AdminRequest.Api.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRequestRepository, InMemoryRequestRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalFrontend", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseCors("LocalFrontend");
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/health", () => Results.Ok(new
{
    service = "Administrative Request Management API",
    status = "Healthy",
    timestamp = DateTimeOffset.UtcNow
}));

app.MapGet("/api/users", (IRequestRepository repository) =>
    Results.Ok(repository.GetUsers()));

app.MapGet("/api/dashboard", (IRequestRepository repository) =>
{
    var requests = repository.GetRequests(null, null).ToList();
    var response = new DashboardDto(
        Total: requests.Count,
        Submitted: requests.Count(r => r.Status == RequestStatus.Submitted),
        InReview: requests.Count(r => r.Status == RequestStatus.InReview),
        Approved: requests.Count(r => r.Status == RequestStatus.Approved),
        Rejected: requests.Count(r => r.Status == RequestStatus.Rejected),
        Closed: requests.Count(r => r.Status == RequestStatus.Closed),
        HighPriority: requests.Count(r => r.Priority == RequestPriority.High),
        LatestRequests: requests
            .OrderByDescending(r => r.UpdatedAt)
            .Take(5)
            .Select(RequestDto.FromModel)
            .ToList());

    return Results.Ok(response);
});

app.MapGet("/api/requests", (string? status, string? search, IRequestRepository repository) =>
{
    RequestStatus? parsedStatus = null;
    if (!string.IsNullOrWhiteSpace(status) &&
        Enum.TryParse<RequestStatus>(status, ignoreCase: true, out var value))
    {
        parsedStatus = value;
    }

    var requests = repository.GetRequests(parsedStatus, search)
        .OrderByDescending(r => r.UpdatedAt)
        .Select(RequestDto.FromModel);

    return Results.Ok(requests);
});

app.MapGet("/api/requests/{id}", (string id, IRequestRepository repository) =>
{
    var request = repository.GetRequest(id);
    return request is null ? Results.NotFound(new { message = "Request not found." }) : Results.Ok(RequestDetailDto.FromModel(request));
});

app.MapPost("/api/requests", (CreateRequestDto dto, IRequestRepository repository) =>
{
    var errors = ValidateCreateRequest(dto, repository);
    if (errors.Count > 0)
    {
        return Results.BadRequest(new { errors });
    }

    var request = repository.CreateRequest(dto);
    return Results.Created($"/api/requests/{request.Id}", RequestDetailDto.FromModel(request));
});

app.MapPatch("/api/requests/{id}/status", (string id, UpdateRequestStatusDto dto, IRequestRepository repository) =>
{
    if (string.IsNullOrWhiteSpace(dto.ChangedByUserId))
    {
        return Results.BadRequest(new { errors = new[] { "ChangedByUserId is required." } });
    }

    var request = repository.UpdateStatus(id, dto);
    return request is null ? Results.NotFound(new { message = "Request not found." }) : Results.Ok(RequestDetailDto.FromModel(request));
});

app.MapPost("/api/requests/{id}/comments", (string id, AddCommentDto dto, IRequestRepository repository) =>
{
    if (string.IsNullOrWhiteSpace(dto.UserId) || string.IsNullOrWhiteSpace(dto.Message))
    {
        return Results.BadRequest(new { errors = new[] { "UserId and Message are required." } });
    }

    var request = repository.AddComment(id, dto);
    return request is null ? Results.NotFound(new { message = "Request not found." }) : Results.Ok(RequestDetailDto.FromModel(request));
});

app.MapPost("/graphql", (GraphQlRequestDto dto, IRequestRepository repository) =>
{
    var query = dto.Query.Trim().ToLowerInvariant();
    var variables = dto.Variables ?? [];

    if (query.Contains("dashboard"))
    {
        var requests = repository.GetRequests(null, null).ToList();
        return Results.Ok(new
        {
            data = new
            {
                dashboard = new
                {
                    total = requests.Count,
                    submitted = requests.Count(r => r.Status == RequestStatus.Submitted),
                    inReview = requests.Count(r => r.Status == RequestStatus.InReview),
                    approved = requests.Count(r => r.Status == RequestStatus.Approved),
                    rejected = requests.Count(r => r.Status == RequestStatus.Rejected),
                    closed = requests.Count(r => r.Status == RequestStatus.Closed)
                }
            }
        });
    }

    if (query.Contains("request") && variables.TryGetValue("id", out var idValue))
    {
        var request = repository.GetRequest(idValue?.ToString() ?? string.Empty);
        return request is null
            ? Results.NotFound(new { errors = new[] { "Request not found." } })
            : Results.Ok(new { data = new { request = RequestDetailDto.FromModel(request) } });
    }

    if (query.Contains("users"))
    {
        return Results.Ok(new { data = new { users = repository.GetUsers() } });
    }

    return Results.Ok(new
    {
        data = new
        {
            requests = repository.GetRequests(null, null)
                .OrderByDescending(r => r.UpdatedAt)
                .Select(RequestDto.FromModel)
        }
    });
});

app.Run();

static List<string> ValidateCreateRequest(CreateRequestDto dto, IRequestRepository repository)
{
    var errors = new List<string>();

    if (string.IsNullOrWhiteSpace(dto.Title))
    {
        errors.Add("Title is required.");
    }

    if (string.IsNullOrWhiteSpace(dto.Description))
    {
        errors.Add("Description is required.");
    }

    if (string.IsNullOrWhiteSpace(dto.RequestedByUserId))
    {
        errors.Add("RequestedByUserId is required.");
    }
    else if (repository.GetUser(dto.RequestedByUserId) is null)
    {
        errors.Add("RequestedByUserId must match an existing user.");
    }

    if (dto.Title?.Length > 120)
    {
        errors.Add("Title must be 120 characters or fewer.");
    }

    return errors;
}
