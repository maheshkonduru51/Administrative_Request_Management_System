CREATE TABLE Users (
    Id NVARCHAR(20) NOT NULL PRIMARY KEY,
    FullName NVARCHAR(120) NOT NULL,
    Email NVARCHAR(160) NOT NULL UNIQUE,
    Department NVARCHAR(100) NOT NULL,
    Role NVARCHAR(30) NOT NULL CHECK (Role IN ('Employee', 'Manager', 'Admin'))
);

CREATE TABLE AdminRequests (
    Id NVARCHAR(20) NOT NULL PRIMARY KEY,
    Title NVARCHAR(120) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Category NVARCHAR(80) NOT NULL,
    Priority NVARCHAR(20) NOT NULL CHECK (Priority IN ('Low', 'Medium', 'High')),
    Status NVARCHAR(30) NOT NULL CHECK (Status IN ('Draft', 'Submitted', 'InReview', 'Approved', 'Rejected', 'Closed')),
    RequestedByUserId NVARCHAR(20) NOT NULL,
    AssignedToUserId NVARCHAR(20) NULL,
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    CONSTRAINT FK_AdminRequests_RequestedBy FOREIGN KEY (RequestedByUserId) REFERENCES Users(Id),
    CONSTRAINT FK_AdminRequests_AssignedTo FOREIGN KEY (AssignedToUserId) REFERENCES Users(Id)
);

CREATE TABLE RequestComments (
    Id NVARCHAR(60) NOT NULL PRIMARY KEY,
    RequestId NVARCHAR(20) NOT NULL,
    UserId NVARCHAR(20) NOT NULL,
    Message NVARCHAR(1000) NOT NULL,
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    CONSTRAINT FK_RequestComments_Request FOREIGN KEY (RequestId) REFERENCES AdminRequests(Id),
    CONSTRAINT FK_RequestComments_User FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE RequestStatusHistory (
    Id NVARCHAR(60) NOT NULL PRIMARY KEY,
    RequestId NVARCHAR(20) NOT NULL,
    FromStatus NVARCHAR(30) NOT NULL,
    ToStatus NVARCHAR(30) NOT NULL,
    ChangedByUserId NVARCHAR(20) NOT NULL,
    Reason NVARCHAR(500) NULL,
    ChangedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    CONSTRAINT FK_RequestStatusHistory_Request FOREIGN KEY (RequestId) REFERENCES AdminRequests(Id),
    CONSTRAINT FK_RequestStatusHistory_User FOREIGN KEY (ChangedByUserId) REFERENCES Users(Id)
);

CREATE INDEX IX_AdminRequests_Status ON AdminRequests(Status);
CREATE INDEX IX_AdminRequests_Category ON AdminRequests(Category);
CREATE INDEX IX_AdminRequests_RequestedBy ON AdminRequests(RequestedByUserId);
CREATE INDEX IX_RequestStatusHistory_RequestId ON RequestStatusHistory(RequestId);

