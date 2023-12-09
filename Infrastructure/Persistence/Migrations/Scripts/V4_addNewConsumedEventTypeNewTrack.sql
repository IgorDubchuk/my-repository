BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803172314_addNewConsumedEventTypeNewTrack')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[ConsumedEventType]'))
        SET IDENTITY_INSERT [ConsumedEventType] ON;
    EXEC(N'INSERT INTO [ConsumedEventType] ([Id], [Code], [Name])
    VALUES (5, N''NewTrack'', N''Новый автодром'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[ConsumedEventType]'))
        SET IDENTITY_INSERT [ConsumedEventType] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803172314_addNewConsumedEventTypeNewTrack')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230803172314_addNewConsumedEventTypeNewTrack', N'7.0.9');
END;
GO

COMMIT;
GO

