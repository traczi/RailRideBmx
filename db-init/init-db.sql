USE master;
GO

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = N'RailRideBmxDB')
    BEGIN
        CREATE DATABASE RailRideBmxDB;
    END
GO

USE RailRideBmxDB;
GO


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '__EFMigrationsHistory')
    BEGIN
        CREATE TABLE [__EFMigrationsHistory] (
            [MigrationId] nvarchar(150) NOT NULL,
            [ProductVersion] nvarchar(32) NOT NULL,
            CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
        );
    END
GO


BEGIN TRANSACTION;
GO

CREATE TABLE [Brands] (
    [BrandId] uniqueidentifier NOT NULL,
    [BrandName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Brands] PRIMARY KEY ([BrandId])
);
GO

CREATE TABLE [Carts] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] nvarchar(max) NULL,
    [SessionId] nvarchar(max) NULL,
    [IsPayd] bit NOT NULL,
    CONSTRAINT [PK_Carts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Categories] (
    [CategoryId] uniqueidentifier NOT NULL,
    [CategoryName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
);
GO

CREATE TABLE [Colors] (
    [ColorId] uniqueidentifier NOT NULL,
    [ColorName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Colors] PRIMARY KEY ([ColorId])
);
GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [Firstname] nvarchar(max) NOT NULL,
    [Lastname] nvarchar(max) NOT NULL,
    [Email] nvarchar(450) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Role] int NULL,
    [ResetPassWordToken] nvarchar(max) NULL,
    [ResetPasswordTokenExpiration] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Addresses] (
    [AddressId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Line1] nvarchar(max) NOT NULL,
    [Line2] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [Country] nvarchar(max) NOT NULL,
    [PostalCode] nvarchar(max) NOT NULL,
    [CartId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([AddressId]),
    CONSTRAINT [FK_Addresses_Carts_CartId] FOREIGN KEY ([CartId]) REFERENCES [Carts] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Image] nvarchar(max) NOT NULL,
    [Price] real NOT NULL,
    [Quantity] int NOT NULL,
    [CategoryId] uniqueidentifier NOT NULL,
    [ColorId] uniqueidentifier NOT NULL,
    [BrandId] uniqueidentifier NOT NULL,
    [SubCategory] nvarchar(max) NULL,
    [ConfigCategory] nvarchar(max) NULL,
    [Geometry] nvarchar(max) NULL,
    [FrameSize] real NULL,
    [HandlebarSize] real NULL,
    [WheelSize] real NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Brands_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Brands] ([BrandId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Products_Colors_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [Colors] ([ColorId]) ON DELETE CASCADE
);
GO

CREATE TABLE [ConfigurationsBMX] (
    [ConfigurationId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [NameConfiguration] nvarchar(max) NOT NULL,
    [FrameId] uniqueidentifier NULL,
    [HandlebarId] uniqueidentifier NULL,
    [HandlebarCuffId] uniqueidentifier NULL,
    [HandlebarCapId] uniqueidentifier NULL,
    [ForkId] uniqueidentifier NULL,
    [GallowsId] uniqueidentifier NULL,
    [HeadsetId] uniqueidentifier NULL,
    [RotorId] uniqueidentifier NULL,
    [SaddleId] uniqueidentifier NULL,
    [SaddleStemId] uniqueidentifier NULL,
    [SaddleClampId] uniqueidentifier NULL,
    [WheelId] uniqueidentifier NULL,
    [TireId] uniqueidentifier NULL,
    [RimId] uniqueidentifier NULL,
    [SpokesId] uniqueidentifier NULL,
    [HubsId] uniqueidentifier NULL,
    [ChainsId] uniqueidentifier NULL,
    [FrontBrakesId] uniqueidentifier NULL,
    [RearBrakesId] uniqueidentifier NULL,
    [AssemblyId] uniqueidentifier NULL,
    [PedalId] uniqueidentifier NULL,
    [PedalArmsId] uniqueidentifier NULL,
    [DiskId] uniqueidentifier NULL,
    [CrankSetId] uniqueidentifier NULL,
    [PegsId] uniqueidentifier NULL,
    [CreateAt] datetime2 NOT NULL,
    [UpdateAt] datetime2 NOT NULL,
    CONSTRAINT [PK_ConfigurationsBMX] PRIMARY KEY ([ConfigurationId]),
    CONSTRAINT [FK_ConfigurationsBMX_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Comments] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [Rating] int NOT NULL,
    [CommentText] nvarchar(max) NOT NULL,
    [IsReported] bit NOT NULL,
    [DatePosted] datetime2 NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Likes] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Likes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Likes_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Likes_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductCarts] (
    [ProductCartId] uniqueidentifier NOT NULL,
    [CartId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_ProductCarts] PRIMARY KEY ([ProductCartId]),
    CONSTRAINT [FK_ProductCarts_Carts_CartId] FOREIGN KEY ([CartId]) REFERENCES [Carts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductCarts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Addresses_CartId] ON [Addresses] ([CartId]);
GO

CREATE INDEX [IX_Comments_ProductId] ON [Comments] ([ProductId]);
GO

CREATE UNIQUE INDEX [IX_Comments_UserId_ProductId] ON [Comments] ([UserId], [ProductId]);
GO

CREATE INDEX [IX_ConfigurationsBMX_UserId] ON [ConfigurationsBMX] ([UserId]);
GO

CREATE INDEX [IX_Likes_ProductId] ON [Likes] ([ProductId]);
GO

CREATE INDEX [IX_Likes_UserId] ON [Likes] ([UserId]);
GO

CREATE INDEX [IX_ProductCarts_CartId] ON [ProductCarts] ([CartId]);
GO

CREATE INDEX [IX_ProductCarts_ProductId] ON [ProductCarts] ([ProductId]);
GO

CREATE INDEX [IX_Products_BrandId] ON [Products] ([BrandId]);
GO

CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
GO

CREATE INDEX [IX_Products_ColorId] ON [Products] ([ColorId]);
GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240429085500_initialMigration', N'6.0.16');
GO

COMMIT;
GO

