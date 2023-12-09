BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE TABLE [Season] (
        [Id] int NOT NULL IDENTITY,
        [Year] smallint NOT NULL,
        CONSTRAINT [PK_Season] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE TABLE [Team] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Team] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE TABLE [Track] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Track] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE TABLE [DriverSeasonParticipation] (
        [Id] int NOT NULL IDENTITY,
        [DriverId] int NOT NULL,
        [SeasonId] int NOT NULL,
        [Position] tinyint NULL,
        [Score] tinyint NULL,
        CONSTRAINT [PK_DriverSeasonParticipation] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DriverSeasonParticipation_Driver_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [Driver] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DriverSeasonParticipation_Season_SeasonId] FOREIGN KEY ([SeasonId]) REFERENCES [Season] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE TABLE [DriverTeamContract] (
        [Id] int NOT NULL IDENTITY,
        [DriverId] int NOT NULL,
        [TeamId] int NOT NULL,
        [DateFrom] datetime2 NOT NULL,
        [DateTo] datetime2 NOT NULL,
        CONSTRAINT [PK_DriverTeamContract] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DriverTeamContract_Driver_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [Driver] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DriverTeamContract_Team_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Team] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE TABLE [TeamSeasonParticipation] (
        [Id] int NOT NULL IDENTITY,
        [TeamId] int NOT NULL,
        [Position] tinyint NULL,
        [Score] tinyint NULL,
        [DriverId] int NOT NULL,
        [SeasonId] int NULL,
        CONSTRAINT [PK_TeamSeasonParticipation] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TeamSeasonParticipation_Season_SeasonId] FOREIGN KEY ([SeasonId]) REFERENCES [Season] ([Id]),
        CONSTRAINT [FK_TeamSeasonParticipation_Team_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [Team] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE TABLE [Race] (
        [Id] int NOT NULL IDENTITY,
        [SeasonId] int NOT NULL,
        [TrackId] int NOT NULL,
        [NumberInSeason] tinyint NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Date] Date NOT NULL,
        [RaceId] int NOT NULL,
        CONSTRAINT [PK_Race] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Race_Season_SeasonId] FOREIGN KEY ([SeasonId]) REFERENCES [Season] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Race_Track_RaceId] FOREIGN KEY ([RaceId]) REFERENCES [Track] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE TABLE [DriverRaceParticipation] (
        [Id] int NOT NULL IDENTITY,
        [DriverId] int NOT NULL,
        [TeamId] int NOT NULL,
        [RaceId] int NOT NULL,
        [Position] tinyint NULL,
        [ScoreForRace] tinyint NULL,
        [ScoreInSeasonAfterRace] tinyint NULL,
        CONSTRAINT [PK_DriverRaceParticipation] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DriverRaceParticipation_Driver_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [Driver] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DriverRaceParticipation_Race_RaceId] FOREIGN KEY ([RaceId]) REFERENCES [Race] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DriverRaceParticipation_Team_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Team] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE TABLE [TeamRaceParticipation] (
        [Id] int NOT NULL IDENTITY,
        [TeamId] int NOT NULL,
        [RaceId] int NOT NULL,
        [ScoreForRace] tinyint NULL,
        [ScoreInSeasonAfterRace] tinyint NULL,
        CONSTRAINT [PK_TeamRaceParticipation] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TeamRaceParticipation_Race_RaceId] FOREIGN KEY ([RaceId]) REFERENCES [Race] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TeamRaceParticipation_Team_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Team] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_DriverRaceParticipation_DriverId] ON [DriverRaceParticipation] ([DriverId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_DriverRaceParticipation_RaceId] ON [DriverRaceParticipation] ([RaceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_DriverRaceParticipation_TeamId] ON [DriverRaceParticipation] ([TeamId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_DriverSeasonParticipation_DriverId] ON [DriverSeasonParticipation] ([DriverId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_DriverSeasonParticipation_SeasonId] ON [DriverSeasonParticipation] ([SeasonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_DriverTeamContract_DriverId] ON [DriverTeamContract] ([DriverId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_DriverTeamContract_TeamId] ON [DriverTeamContract] ([TeamId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_Race_RaceId] ON [Race] ([RaceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_Race_SeasonId] ON [Race] ([SeasonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_TeamRaceParticipation_RaceId] ON [TeamRaceParticipation] ([RaceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_TeamRaceParticipation_TeamId] ON [TeamRaceParticipation] ([TeamId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_TeamSeasonParticipation_DriverId] ON [TeamSeasonParticipation] ([DriverId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    CREATE INDEX [IX_TeamSeasonParticipation_SeasonId] ON [TeamSeasonParticipation] ([SeasonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720065837_addMainModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230720065837_addMainModel', N'7.0.9');
END;
GO

COMMIT;
GO

