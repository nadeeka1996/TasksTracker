IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [PasswordSalt] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

CREATE TABLE [TaskItems] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Status] int NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_TaskItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TaskItems_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [TaskItemHistories] (
    [Id] uniqueidentifier NOT NULL,
    [TaskItemId] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Status] int NOT NULL,
    [ChangedAt] datetime2 NOT NULL,
    [ChangedBy] uniqueidentifier NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TaskItemHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TaskItemHistories_TaskItems_TaskItemId] FOREIGN KEY ([TaskItemId]) REFERENCES [TaskItems] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_TaskItemHistories_TaskItemId] ON [TaskItemHistories] ([TaskItemId]);

CREATE INDEX [IX_TaskItems_UserId] ON [TaskItems] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250503073719_Initial', N'9.0.4');

COMMIT;
GO

