BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230825102359_addNewConsumedEventStateProcessing')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[ConsumedEventState]'))
        SET IDENTITY_INSERT [ConsumedEventState] ON;
    EXEC(N'INSERT INTO [ConsumedEventState] ([Id], [Code], [Name])
    VALUES (4, N''Processing'', N''Событие обрабатывается'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[ConsumedEventState]'))
        SET IDENTITY_INSERT [ConsumedEventState] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230825102359_addNewConsumedEventStateProcessing')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230825102359_addNewConsumedEventStateProcessing', N'7.0.9');
END;
GO

COMMIT;
GO

