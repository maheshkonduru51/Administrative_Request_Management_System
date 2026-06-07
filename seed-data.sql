INSERT INTO Users (Id, FullName, Email, Department, Role) VALUES
('USR-001', 'Mahesh Raju', 'mahesh.raju@example.com', 'Information Technology', 'Employee'),
('USR-002', 'Ananya Sharma', 'ananya.sharma@example.com', 'Administration', 'Admin'),
('USR-003', 'Ravi Kumar', 'ravi.kumar@example.com', 'Operations', 'Manager');

INSERT INTO AdminRequests
(Id, Title, Description, Category, Priority, Status, RequestedByUserId, AssignedToUserId, CreatedAt, UpdatedAt)
VALUES
('REQ-1001', 'Laptop software installation', 'Install approved analytics software for project work.', 'Software Access', 'High', 'InReview', 'USR-001', 'USR-002', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET()),
('REQ-1002', 'ID card access update', 'Add office floor access for new project seating.', 'Facility Access', 'Medium', 'Submitted', 'USR-001', NULL, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET()),
('REQ-1003', 'Document approval workflow', 'Review and approve vendor onboarding document.', 'Document Approval', 'Low', 'Approved', 'USR-003', 'USR-002', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());

INSERT INTO RequestStatusHistory
(Id, RequestId, FromStatus, ToStatus, ChangedByUserId, Reason, ChangedAt)
VALUES
('HIS-1001', 'REQ-1001', 'Draft', 'InReview', 'USR-001', 'Seeded sample workflow.', SYSDATETIMEOFFSET()),
('HIS-1002', 'REQ-1002', 'Draft', 'Submitted', 'USR-001', 'Seeded sample workflow.', SYSDATETIMEOFFSET()),
('HIS-1003', 'REQ-1003', 'Draft', 'Approved', 'USR-003', 'Seeded sample workflow.', SYSDATETIMEOFFSET());

