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
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Surname] nvarchar(max) NULL,
        [Patronymic] nvarchar(max) NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [Department] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Department] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [Project] (
        [Id] nvarchar(450) NOT NULL,
        [IdDirector] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_Project] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [Role] (
        [Id] int NOT NULL IDENTITY,
        [NameRole] nvarchar(max) NOT NULL,
        [TypeRole] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [Team] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Team] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [TypeTask] (
        [Id] int NOT NULL IDENTITY,
        [NameType] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TypeTask] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [DepartmentModelProjectModel] (
        [DepartmentModelId] nvarchar(450) NOT NULL,
        [ProjectModelId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_DepartmentModelProjectModel] PRIMARY KEY ([DepartmentModelId], [ProjectModelId]),
        CONSTRAINT [FK_DepartmentModelProjectModel_Department_DepartmentModelId] FOREIGN KEY ([DepartmentModelId]) REFERENCES [Department] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DepartmentModelProjectModel_Project_ProjectModelId] FOREIGN KEY ([ProjectModelId]) REFERENCES [Project] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [DepartmentMembers] (
        [Id] int NOT NULL IDENTITY,
        [DepartmentID] nvarchar(450) NOT NULL,
        [UserContextID] nvarchar(450) NOT NULL,
        [RoleID] int NOT NULL,
        CONSTRAINT [PK_DepartmentMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DepartmentMembers_AspNetUsers_UserContextID] FOREIGN KEY ([UserContextID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DepartmentMembers_Department_DepartmentID] FOREIGN KEY ([DepartmentID]) REFERENCES [Department] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DepartmentMembers_Role_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [Role] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [DepartmentModelTeam] (
        [DepartmentModelsId] nvarchar(450) NOT NULL,
        [TeamId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_DepartmentModelTeam] PRIMARY KEY ([DepartmentModelsId], [TeamId]),
        CONSTRAINT [FK_DepartmentModelTeam_Department_DepartmentModelsId] FOREIGN KEY ([DepartmentModelsId]) REFERENCES [Department] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DepartmentModelTeam_Team_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Team] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [TeamMembers] (
        [Id] int NOT NULL IDENTITY,
        [TeamID] nvarchar(450) NOT NULL,
        [UserContextID] nvarchar(450) NOT NULL,
        [RoleID] int NOT NULL,
        CONSTRAINT [PK_TeamMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TeamMembers_AspNetUsers_UserContextID] FOREIGN KEY ([UserContextID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TeamMembers_Role_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [Role] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TeamMembers_Team_TeamID] FOREIGN KEY ([TeamID]) REFERENCES [Team] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [Task] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [File] varbinary(max) NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        [Status] int NULL,
        [TypeId] int NULL,
        [ProjectId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Task] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Task_Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Task_TypeTask_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [TypeTask] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [ExecutorTask] (
        [id] nvarchar(450) NOT NULL,
        [userId] nvarchar(450) NULL,
        [TaskId] nvarchar(450) NULL,
        CONSTRAINT [PK_ExecutorTask] PRIMARY KEY ([id]),
        CONSTRAINT [FK_ExecutorTask_AspNetUsers_userId] FOREIGN KEY ([userId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_ExecutorTask_Task_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Task] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE TABLE [Subtasks] (
        [Id] nvarchar(450) NOT NULL,
        [Description] nvarchar(max) NULL,
        [File] varbinary(max) NULL,
        [TaskId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Subtasks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Subtasks_Task_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Task] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_DepartmentMembers_DepartmentID] ON [DepartmentMembers] ([DepartmentID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_DepartmentMembers_RoleID] ON [DepartmentMembers] ([RoleID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_DepartmentMembers_UserContextID] ON [DepartmentMembers] ([UserContextID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_DepartmentModelProjectModel_ProjectModelId] ON [DepartmentModelProjectModel] ([ProjectModelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_DepartmentModelTeam_TeamId] ON [DepartmentModelTeam] ([TeamId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_ExecutorTask_TaskId] ON [ExecutorTask] ([TaskId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_ExecutorTask_userId] ON [ExecutorTask] ([userId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_Subtasks_TaskId] ON [Subtasks] ([TaskId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_Task_ProjectId] ON [Task] ([ProjectId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Task_TypeId] ON [Task] ([TypeId]) WHERE [TypeId] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_TeamMembers_RoleID] ON [TeamMembers] ([RoleID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_TeamMembers_TeamID] ON [TeamMembers] ([TeamID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    CREATE INDEX [IX_TeamMembers_UserContextID] ON [TeamMembers] ([UserContextID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529161416_MigrateDBv1'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240529161416_MigrateDBv1', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529163152_MigrateDBv2'
)
BEGIN
    ALTER TABLE [Task] DROP CONSTRAINT [FK_Task_TypeTask_TypeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529163152_MigrateDBv2'
)
BEGIN
    DROP INDEX [IX_Task_TypeId] ON [Task];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529163152_MigrateDBv2'
)
BEGIN
    ALTER TABLE [TypeTask] ADD [taskId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529163152_MigrateDBv2'
)
BEGIN
    CREATE INDEX [IX_TypeTask_taskId] ON [TypeTask] ([taskId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529163152_MigrateDBv2'
)
BEGIN
    ALTER TABLE [TypeTask] ADD CONSTRAINT [FK_TypeTask_Task_taskId] FOREIGN KEY ([taskId]) REFERENCES [Task] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529163152_MigrateDBv2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240529163152_MigrateDBv2', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529164134_MigrateDBv3'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240529164134_MigrateDBv3', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529164230_MigrateDBv4'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240529164230_MigrateDBv4', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529171621_MigrateDBv5'
)
BEGIN
    ALTER TABLE [TypeTask] DROP CONSTRAINT [FK_TypeTask_Task_taskId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529171621_MigrateDBv5'
)
BEGIN
    ALTER TABLE [TypeTask] DROP CONSTRAINT [PK_TypeTask];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529171621_MigrateDBv5'
)
BEGIN
    DROP INDEX [IX_TypeTask_taskId] ON [TypeTask];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529171621_MigrateDBv5'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TypeTask]') AND [c].[name] = N'taskId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [TypeTask] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [TypeTask] DROP COLUMN [taskId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529171621_MigrateDBv5'
)
BEGIN
    EXEC sp_rename N'[TypeTask]', N'Type';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529171621_MigrateDBv5'
)
BEGIN
    ALTER TABLE [Type] ADD CONSTRAINT [PK_Type] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529171621_MigrateDBv5'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Task_TypeId] ON [Task] ([TypeId]) WHERE [TypeId] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529171621_MigrateDBv5'
)
BEGIN
    ALTER TABLE [Task] ADD CONSTRAINT [FK_Task_Type_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [Type] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529171621_MigrateDBv5'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240529171621_MigrateDBv5', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190444_MigrateDBv6'
)
BEGIN
    ALTER TABLE [Task] DROP CONSTRAINT [FK_Task_Type_TypeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190444_MigrateDBv6'
)
BEGIN
    ALTER TABLE [Type] DROP CONSTRAINT [PK_Type];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190444_MigrateDBv6'
)
BEGIN
    EXEC sp_rename N'[Type]', N'TypeTask';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190444_MigrateDBv6'
)
BEGIN
    ALTER TABLE [TypeTask] ADD CONSTRAINT [PK_TypeTask] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190444_MigrateDBv6'
)
BEGIN
    ALTER TABLE [Task] ADD CONSTRAINT [FK_Task_TypeTask_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [TypeTask] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190444_MigrateDBv6'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240529190444_MigrateDBv6', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190701_MigrateDBv7'
)
BEGIN
    ALTER TABLE [Task] DROP CONSTRAINT [FK_Task_TypeTask_TypeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190701_MigrateDBv7'
)
BEGIN
    DROP INDEX [IX_Task_TypeId] ON [Task];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190701_MigrateDBv7'
)
BEGIN
    ALTER TABLE [TypeTask] ADD [taskId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190701_MigrateDBv7'
)
BEGIN
    CREATE INDEX [IX_TypeTask_taskId] ON [TypeTask] ([taskId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190701_MigrateDBv7'
)
BEGIN
    ALTER TABLE [TypeTask] ADD CONSTRAINT [FK_TypeTask_Task_taskId] FOREIGN KEY ([taskId]) REFERENCES [Task] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190701_MigrateDBv7'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240529190701_MigrateDBv7', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190813_MigrateDBv8'
)
BEGIN
    ALTER TABLE [TypeTask] DROP CONSTRAINT [FK_TypeTask_Task_taskId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190813_MigrateDBv8'
)
BEGIN
    DROP INDEX [IX_TypeTask_taskId] ON [TypeTask];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190813_MigrateDBv8'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TypeTask]') AND [c].[name] = N'taskId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [TypeTask] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [TypeTask] DROP COLUMN [taskId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190813_MigrateDBv8'
)
BEGIN
    CREATE TABLE [TaskModelTypeTask] (
        [TypeId] int NOT NULL,
        [taskId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_TaskModelTypeTask] PRIMARY KEY ([TypeId], [taskId]),
        CONSTRAINT [FK_TaskModelTypeTask_Task_taskId] FOREIGN KEY ([taskId]) REFERENCES [Task] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TaskModelTypeTask_TypeTask_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [TypeTask] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190813_MigrateDBv8'
)
BEGIN
    CREATE INDEX [IX_TaskModelTypeTask_taskId] ON [TaskModelTypeTask] ([taskId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190813_MigrateDBv8'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240529190813_MigrateDBv8', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529190906_MigrateDBv9'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240529190906_MigrateDBv9', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529191041_MigrateDBv9o'
)
BEGIN
    DROP TABLE [TaskModelTypeTask];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529191041_MigrateDBv9o'
)
BEGIN
    DROP TABLE [TypeTask];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240529191041_MigrateDBv9o'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240529191041_MigrateDBv9o', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602115310_MigrateDB10'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Subtasks]') AND [c].[name] = N'File');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Subtasks] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Subtasks] DROP COLUMN [File];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602115310_MigrateDB10'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240602115310_MigrateDB10', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240608133201_MigrateDBv11'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Task]') AND [c].[name] = N'File');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Task] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Task] DROP COLUMN [File];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240608133201_MigrateDBv11'
)
BEGIN
    ALTER TABLE [Task] ADD [FileId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240608133201_MigrateDBv11'
)
BEGIN
    CREATE TABLE [File] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Expansion] nvarchar(max) NOT NULL,
        [bytes] varbinary(max) NULL,
        CONSTRAINT [PK_File] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240608133201_MigrateDBv11'
)
BEGIN
    CREATE INDEX [IX_Task_FileId] ON [Task] ([FileId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240608133201_MigrateDBv11'
)
BEGIN
    ALTER TABLE [Task] ADD CONSTRAINT [FK_Task_File_FileId] FOREIGN KEY ([FileId]) REFERENCES [File] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240608133201_MigrateDBv11'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240608133201_MigrateDBv11', N'8.0.4');
END;
GO

COMMIT;
GO

