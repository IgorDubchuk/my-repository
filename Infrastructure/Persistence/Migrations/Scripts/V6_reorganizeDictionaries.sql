BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231024090717_reorganizeDictionaries')
BEGIN
    ALTER TABLE [Race] DROP CONSTRAINT [FK_Race_Season_SeasonId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231024090717_reorganizeDictionaries')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Race]') AND [c].[name] = N'SeasonId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Race] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Race] ALTER COLUMN [SeasonId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231024090717_reorganizeDictionaries')
BEGIN
    ALTER TABLE [Race] ADD CONSTRAINT [FK_Race_Season_SeasonId] FOREIGN KEY ([SeasonId]) REFERENCES [Season] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231024090717_reorganizeDictionaries')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231024090717_reorganizeDictionaries', N'7.0.9');
END;
GO

COMMIT;
GO

