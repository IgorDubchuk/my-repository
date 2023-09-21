BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230831075208_addNewConsumedEventStateError')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Race]') AND [c].[name] = N'TrackId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Race] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Race] DROP COLUMN [TrackId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230831075208_addNewConsumedEventStateError')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[ConsumedEventState]'))
        SET IDENTITY_INSERT [ConsumedEventState] ON;
    EXEC(N'INSERT INTO [ConsumedEventState] ([Id], [Code], [Name])
    VALUES (5, N''Error'', N''При обработке события произошла ошибка'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[ConsumedEventState]'))
        SET IDENTITY_INSERT [ConsumedEventState] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230831075208_addNewConsumedEventStateError')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230831075208_addNewConsumedEventStateError', N'7.0.9');
END;
GO

COMMIT;
GO

