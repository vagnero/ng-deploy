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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230808190747_DepartamentoXFuncionario')
BEGIN
    CREATE TABLE [Departamentos] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(128) NOT NULL,
        [Sigla] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_Departamentos] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230808190747_DepartamentoXFuncionario')
BEGIN
    CREATE TABLE [Funcionarios] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(128) NOT NULL,
        [Foto] nvarchar(1000) NULL,
        [Rg] nvarchar(15) NOT NULL,
        [DepartamentoId] int NOT NULL,
        CONSTRAINT [PK_Funcionarios] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Funcionarios_Departamentos_DepartamentoId] FOREIGN KEY ([DepartamentoId]) REFERENCES [Departamentos] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230808190747_DepartamentoXFuncionario')
BEGIN
    CREATE INDEX [IX_Funcionarios_DepartamentoId] ON [Funcionarios] ([DepartamentoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230808190747_DepartamentoXFuncionario')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230808190747_DepartamentoXFuncionario', N'6.0.0');
END;
GO

COMMIT;
GO

