BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    CREATE TABLE [ConsumedEventState] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ConsumedEventState] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    CREATE TABLE [ConsumedEventType] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ConsumedEventType] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    CREATE TABLE [PublishedEventType] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_PublishedEventType] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    CREATE TABLE [ConsumedEvent] (
        [Id] int NOT NULL IDENTITY,
        [TypeId] int NOT NULL,
        [EventDateTime] datetime2 NOT NULL,
        [RecieveDateTime] datetime2 NOT NULL,
        [StateId] int NOT NULL,
        [ProcessedDateTime] datetime2 NULL,
        [Data] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ConsumedEvent] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ConsumedEvent_ConsumedEventState_StateId] FOREIGN KEY ([StateId]) REFERENCES [ConsumedEventState] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ConsumedEvent_ConsumedEventType_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [ConsumedEventType] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    CREATE TABLE [PublishedEvent] (
        [Id] int NOT NULL IDENTITY,
        [TypeId] int NOT NULL,
        [CreateDateTime] datetime2 NOT NULL,
        [Data] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_PublishedEvent] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PublishedEvent_PublishedEventType_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [PublishedEventType] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[ConsumedEventState]'))
        SET IDENTITY_INSERT [ConsumedEventState] ON;
    EXEC(N'INSERT INTO [ConsumedEventState] ([Id], [Code], [Name])
    VALUES (1, N''Recieved'', N''Событие получено''),
    (2, N''Processed'', N''Событие обработано''),
    (3, N''ToRepeatProcess'', N''Событие должно быть обработано повторно'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[ConsumedEventState]'))
        SET IDENTITY_INSERT [ConsumedEventState] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[ConsumedEventType]'))
        SET IDENTITY_INSERT [ConsumedEventType] ON;
    EXEC(N'INSERT INTO [ConsumedEventType] ([Id], [Code], [Name])
    VALUES (1, N''SeasonCalendarPublished'', N''Опубликован календарь сезона''),
    (2, N''SeasonParticipantsPublished'', N''Опубликован состав команд-участников сезона''),
    (3, N''DriverContractSigned'', N''Заключен контракт с гонщиком''),
    (4, N''RaceFinished'', N''Гонка завершилась'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[ConsumedEventType]'))
        SET IDENTITY_INSERT [ConsumedEventType] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PublishedEventType]'))
        SET IDENTITY_INSERT [PublishedEventType] ON;
    EXEC(N'INSERT INTO [PublishedEventType] ([Id], [Code], [Name])
    VALUES (1, N''AfterRaceDriverStandings'', N''Позиции в чемпионате мира по итогам гонки''),
    (2, N''AfterRaceCunstructorStandings'', N''Позиции в кубке конструкторов по итогам гонки''),
    (3, N''DriverChampionDetermined'', N''Определен чемпион мира''),
    (4, N''ConstructorChampionDetermined'', N''Определен обладатель кубка конструкторов'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PublishedEventType]'))
        SET IDENTITY_INSERT [PublishedEventType] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    CREATE INDEX [IX_ConsumedEvent_StateId] ON [ConsumedEvent] ([StateId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    CREATE INDEX [IX_ConsumedEvent_TypeId] ON [ConsumedEvent] ([TypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    CREATE INDEX [IX_PublishedEvent_TypeId] ON [PublishedEvent] ([TypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230801202308_addEventModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230801202308_addEventModel', N'7.0.9');
END;
GO

COMMIT;
GO

