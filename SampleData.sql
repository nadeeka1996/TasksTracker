-- Insert a sample user with password hash and salt
-- craeted user (username = john.doe@example.com / password = 123456)
DECLARE @UserId UNIQUEIDENTIFIER = NEWID();

INSERT INTO [Users] ([Id], [Name], [Email], [PasswordHash], [PasswordSalt])
VALUES (
    @UserId,
    N'John Doe',
    N'john.doe@example.com',
    N'+vnIIWE8gxhbEG05T1qp2DuOpQtOq91X3qtByP7VIGY=',
    N'mrz4YVzkeoVrW5+4DOG+X1iAKZcdJvs8a5kQqF71y5HUJRcN5zCpzoasFhxjU8Fc+LtJDvQnHDOP8e7NacEo/w=='
);

-- Insert a sample task for the user
DECLARE @TaskId UNIQUEIDENTIFIER = NEWID();

INSERT INTO [TaskItems] (
    [Id], [Title], [Description], [Status], [Created], [Updated], [IsDeleted], [UserId]
)
VALUES (
    @TaskId,
    N'Initial Task',
    N'This is a sample task description.',
    1, 
    GETDATE(),
    GETDATE(),
    0,
    @UserId
);

-- Insert a task history entry for that task
INSERT INTO [TaskItemHistories] (
    [Id], [TaskItemId], [Title], [Description], [Status], [ChangedAt], [ChangedBy], [IsDeleted]
)
VALUES (
    NEWID(),
    @TaskId,
    N'Initial Task',
    N'This is a historical record of the task.',
    1,
    GETDATE(),
    @UserId,
    0
);
