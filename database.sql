USE [master]
GO
/****** Object:  Database [AliServanKilinc20241012]    Script Date: 22.10.2024 16:48:21 ******/
CREATE DATABASE [AliServanKilinc20241012]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AliServanKilinc20241012', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AliServanKilinc20241012.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AliServanKilinc20241012_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AliServanKilinc20241012_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [AliServanKilinc20241012] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AliServanKilinc20241012].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AliServanKilinc20241012] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET ARITHABORT OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AliServanKilinc20241012] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AliServanKilinc20241012] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AliServanKilinc20241012] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AliServanKilinc20241012] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [AliServanKilinc20241012] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET RECOVERY FULL 
GO
ALTER DATABASE [AliServanKilinc20241012] SET  MULTI_USER 
GO
ALTER DATABASE [AliServanKilinc20241012] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AliServanKilinc20241012] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AliServanKilinc20241012] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AliServanKilinc20241012] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AliServanKilinc20241012] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AliServanKilinc20241012] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'AliServanKilinc20241012', N'ON'
GO
ALTER DATABASE [AliServanKilinc20241012] SET QUERY_STORE = ON
GO
ALTER DATABASE [AliServanKilinc20241012] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [AliServanKilinc20241012]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [uniqueidentifier] NOT NULL,
	[AccountNo] [nvarchar](450) NOT NULL,
	[AccountTypeId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[VerificationDate] [datetime2](7) NOT NULL,
	[IsVerified] [bit] NOT NULL,
	[Balance] [float] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedOnUtc] [datetime2](7) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountTypes]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedOnUtc] [datetime2](7) NULL,
 CONSTRAINT [PK_AccountTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transfers]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transfers](
	[Id] [uniqueidentifier] NOT NULL,
	[TransferTypeId] [uniqueidentifier] NOT NULL,
	[SenderAccountId] [uniqueidentifier] NOT NULL,
	[RecipientAccountId] [uniqueidentifier] NOT NULL,
	[SenderUserId] [uniqueidentifier] NOT NULL,
	[RecipientUserId] [uniqueidentifier] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Status] [bit] NOT NULL,
	[RejectionDetailDescription] [nvarchar](max) NULL,
	[Amount] [float] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedOnUtc] [datetime2](7) NULL,
 CONSTRAINT [PK_Transfers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransferTypes]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedOnUtc] [datetime2](7) NULL,
 CONSTRAINT [PK_TransferTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[TCKNO] [nvarchar](450) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[RegistrationDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedOnUtc] [datetime2](7) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 22.10.2024 16:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241016190002_InitialCreate', N'8.0.10')
GO
INSERT [dbo].[Accounts] ([Id], [AccountNo], [AccountTypeId], [UserId], [CreatedDate], [VerificationDate], [IsVerified], [Balance], [IsDeleted], [DeletedOnUtc]) VALUES (N'08bd9e40-f258-44c1-fa23-08dcef82a565', N'007735ba358c', N'b1b2455b-2090-44f0-c20f-08dcef8297ba', N'7a26ff0d-6ace-40b0-2bc7-08dcef4d247f', CAST(N'2024-10-18T17:39:17.9456444' AS DateTime2), CAST(N'2024-10-22T12:31:04.4441105' AS DateTime2), 1, 1460, 0, NULL)
INSERT [dbo].[Accounts] ([Id], [AccountNo], [AccountTypeId], [UserId], [CreatedDate], [VerificationDate], [IsVerified], [Balance], [IsDeleted], [DeletedOnUtc]) VALUES (N'27473aaf-9e1c-41a2-b0fc-08dcef847d1e', N'33f676cdf9e9', N'b1b2455b-2090-44f0-c20f-08dcef8297ba', N'5e10af85-58cf-488d-9219-08dcef84597b', CAST(N'2024-10-18T17:52:29.3616656' AS DateTime2), CAST(N'2024-10-18T17:52:29.3999457' AS DateTime2), 1, 400, 0, NULL)
INSERT [dbo].[Accounts] ([Id], [AccountNo], [AccountTypeId], [UserId], [CreatedDate], [VerificationDate], [IsVerified], [Balance], [IsDeleted], [DeletedOnUtc]) VALUES (N'b6ca5263-ceb0-4ec4-cfe3-08dcf0217f1c', N'16f229748d78', N'17cdf46f-fb7d-4096-bfc8-08dcf020c90f', N'7a26ff0d-6ace-40b0-2bc7-08dcef4d247f', CAST(N'2024-10-19T12:36:23.6839835' AS DateTime2), CAST(N'2024-10-19T12:36:23.8191460' AS DateTime2), 1, 530, 0, NULL)
INSERT [dbo].[Accounts] ([Id], [AccountNo], [AccountTypeId], [UserId], [CreatedDate], [VerificationDate], [IsVerified], [Balance], [IsDeleted], [DeletedOnUtc]) VALUES (N'f3258067-f487-4a5a-6094-08dcf07ce6e2', N'eb4a13e3a87b', N'de259f1c-b062-4bba-bfc9-08dcf020c90f', N'b8b97204-22c6-499f-8550-08dcefaf3e9c', CAST(N'2024-10-19T23:30:41.9963305' AS DateTime2), CAST(N'2024-10-19T23:30:42.0096947' AS DateTime2), 1, 190, 0, NULL)
INSERT [dbo].[Accounts] ([Id], [AccountNo], [AccountTypeId], [UserId], [CreatedDate], [VerificationDate], [IsVerified], [Balance], [IsDeleted], [DeletedOnUtc]) VALUES (N'622bd5cf-3cdb-45d0-dd3a-08dcf26e707d', N'33313c80b8ed', N'b1b2455b-2090-44f0-c20f-08dcef8297ba', N'ba75518f-67bb-4a1b-179d-08dcf26e6754', CAST(N'2024-10-22T10:52:12.7349221' AS DateTime2), CAST(N'2024-10-22T12:27:53.8498850' AS DateTime2), 1, 420, 0, NULL)
GO
INSERT [dbo].[AccountTypes] ([Id], [Name], [IsDeleted], [DeletedOnUtc]) VALUES (N'b1b2455b-2090-44f0-c20f-08dcef8297ba', N'Bireysel', 0, NULL)
INSERT [dbo].[AccountTypes] ([Id], [Name], [IsDeleted], [DeletedOnUtc]) VALUES (N'17cdf46f-fb7d-4096-bfc8-08dcf020c90f', N'Ticari Kuruluşlar', 0, NULL)
INSERT [dbo].[AccountTypes] ([Id], [Name], [IsDeleted], [DeletedOnUtc]) VALUES (N'de259f1c-b062-4bba-bfc9-08dcf020c90f', N'Vakıf, Dernek', 0, NULL)
GO
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'5391550d-0b2b-453c-ee9b-08dcef4d24ab', N'User', N'USER', NULL)
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'82a25c19-b893-4bf8-16a6-08dcf1165687', N'Admin', N'ADMIN', NULL)
GO
INSERT [dbo].[Transfers] ([Id], [TransferTypeId], [SenderAccountId], [RecipientAccountId], [SenderUserId], [RecipientUserId], [Date], [Status], [RejectionDetailDescription], [Amount], [Description], [IsDeleted], [DeletedOnUtc]) VALUES (N'4f6dda11-b8f6-4f3b-cb54-08dcef85d69b', N'3ad7b193-8faa-432a-1bf4-08dcef84c891', N'08bd9e40-f258-44c1-fa23-08dcef82a565', N'27473aaf-9e1c-41a2-b0fc-08dcef847d1e', N'7a26ff0d-6ace-40b0-2bc7-08dcef4d247f', N'5e10af85-58cf-488d-9219-08dcef84597b', CAST(N'2024-10-18T18:02:08.9914618' AS DateTime2), 0, N'Bu işlem iptal edilmeli', 50, N'Test Amaçlı', 0, NULL)
INSERT [dbo].[Transfers] ([Id], [TransferTypeId], [SenderAccountId], [RecipientAccountId], [SenderUserId], [RecipientUserId], [Date], [Status], [RejectionDetailDescription], [Amount], [Description], [IsDeleted], [DeletedOnUtc]) VALUES (N'79923f7b-2036-4af4-f80a-08dcf07c956c', N'3ad7b193-8faa-432a-1bf4-08dcef84c891', N'08bd9e40-f258-44c1-fa23-08dcef82a565', N'27473aaf-9e1c-41a2-b0fc-08dcef847d1e', N'7a26ff0d-6ace-40b0-2bc7-08dcef4d247f', N'5e10af85-58cf-488d-9219-08dcef84597b', CAST(N'2024-10-19T23:28:25.3073095' AS DateTime2), 0, NULL, 300, N'Test Amaçlı gönderim', 0, NULL)
INSERT [dbo].[Transfers] ([Id], [TransferTypeId], [SenderAccountId], [RecipientAccountId], [SenderUserId], [RecipientUserId], [Date], [Status], [RejectionDetailDescription], [Amount], [Description], [IsDeleted], [DeletedOnUtc]) VALUES (N'df6bb6ca-30c6-4359-f80b-08dcf07c956c', N'3ad7b193-8faa-432a-1bf4-08dcef84c891', N'08bd9e40-f258-44c1-fa23-08dcef82a565', N'f3258067-f487-4a5a-6094-08dcf07ce6e2', N'7a26ff0d-6ace-40b0-2bc7-08dcef4d247f', N'b8b97204-22c6-499f-8550-08dcefaf3e9c', CAST(N'2024-10-19T23:31:26.0781232' AS DateTime2), 1, NULL, 150, N'Deneme Amaçlıdır', 0, NULL)
INSERT [dbo].[Transfers] ([Id], [TransferTypeId], [SenderAccountId], [RecipientAccountId], [SenderUserId], [RecipientUserId], [Date], [Status], [RejectionDetailDescription], [Amount], [Description], [IsDeleted], [DeletedOnUtc]) VALUES (N'ebd66869-0ea5-4d87-f80c-08dcf07c956c', N'3ad7b193-8faa-432a-1bf4-08dcef84c891', N'27473aaf-9e1c-41a2-b0fc-08dcef847d1e', N'b6ca5263-ceb0-4ec4-cfe3-08dcf0217f1c', N'5e10af85-58cf-488d-9219-08dcef84597b', N'7a26ff0d-6ace-40b0-2bc7-08dcef4d247f', CAST(N'2024-10-19T23:32:18.3373170' AS DateTime2), 1, NULL, 640, N'Test amaçlıdır', 0, NULL)
INSERT [dbo].[Transfers] ([Id], [TransferTypeId], [SenderAccountId], [RecipientAccountId], [SenderUserId], [RecipientUserId], [Date], [Status], [RejectionDetailDescription], [Amount], [Description], [IsDeleted], [DeletedOnUtc]) VALUES (N'd4b01686-3b6a-4588-2648-08dcf07f716b', N'3ad7b193-8faa-432a-1bf4-08dcef84c891', N'27473aaf-9e1c-41a2-b0fc-08dcef847d1e', N'08bd9e40-f258-44c1-fa23-08dcef82a565', N'5e10af85-58cf-488d-9219-08dcef84597b', N'7a26ff0d-6ace-40b0-2bc7-08dcef4d247f', CAST(N'2024-10-19T23:48:53.3873107' AS DateTime2), 1, NULL, 110, NULL, 0, NULL)
INSERT [dbo].[Transfers] ([Id], [TransferTypeId], [SenderAccountId], [RecipientAccountId], [SenderUserId], [RecipientUserId], [Date], [Status], [RejectionDetailDescription], [Amount], [Description], [IsDeleted], [DeletedOnUtc]) VALUES (N'583d69b6-f708-4630-bb21-08dcf26eaba1', N'3ad7b193-8faa-432a-1bf4-08dcef84c891', N'f3258067-f487-4a5a-6094-08dcf07ce6e2', N'622bd5cf-3cdb-45d0-dd3a-08dcf26e707d', N'b8b97204-22c6-499f-8550-08dcefaf3e9c', N'ba75518f-67bb-4a1b-179d-08dcf26e6754', CAST(N'2024-10-22T10:53:51.9519102' AS DateTime2), 1, NULL, 60, N'bireysel ödeme işlem açıklaması...', 0, NULL)
INSERT [dbo].[Transfers] ([Id], [TransferTypeId], [SenderAccountId], [RecipientAccountId], [SenderUserId], [RecipientUserId], [Date], [Status], [RejectionDetailDescription], [Amount], [Description], [IsDeleted], [DeletedOnUtc]) VALUES (N'1a3a8bb7-701c-4752-bb22-08dcf26eaba1', N'3ad7b193-8faa-432a-1bf4-08dcef84c891', N'27473aaf-9e1c-41a2-b0fc-08dcef847d1e', N'622bd5cf-3cdb-45d0-dd3a-08dcf26e707d', N'5e10af85-58cf-488d-9219-08dcef84597b', N'ba75518f-67bb-4a1b-179d-08dcf26e6754', CAST(N'2024-10-22T10:54:25.5792088' AS DateTime2), 1, NULL, 350, N'açıklama ...', 0, NULL)
INSERT [dbo].[Transfers] ([Id], [TransferTypeId], [SenderAccountId], [RecipientAccountId], [SenderUserId], [RecipientUserId], [Date], [Status], [RejectionDetailDescription], [Amount], [Description], [IsDeleted], [DeletedOnUtc]) VALUES (N'fa6390e3-22c6-4d7b-bb23-08dcf26eaba1', N'3ad7b193-8faa-432a-1bf4-08dcef84c891', N'622bd5cf-3cdb-45d0-dd3a-08dcf26e707d', N'f3258067-f487-4a5a-6094-08dcf07ce6e2', N'ba75518f-67bb-4a1b-179d-08dcf26e6754', N'b8b97204-22c6-499f-8550-08dcefaf3e9c', CAST(N'2024-10-22T10:55:44.6799492' AS DateTime2), 1, NULL, 100, N'açıklamam...', 0, NULL)
INSERT [dbo].[Transfers] ([Id], [TransferTypeId], [SenderAccountId], [RecipientAccountId], [SenderUserId], [RecipientUserId], [Date], [Status], [RejectionDetailDescription], [Amount], [Description], [IsDeleted], [DeletedOnUtc]) VALUES (N'1f3db6f8-95ca-433a-26cb-08dcf281123b', N'3ad7b193-8faa-432a-1bf4-08dcef84c891', N'b6ca5263-ceb0-4ec4-cfe3-08dcf0217f1c', N'622bd5cf-3cdb-45d0-dd3a-08dcf26e707d', N'7a26ff0d-6ace-40b0-2bc7-08dcef4d247f', N'ba75518f-67bb-4a1b-179d-08dcf26e6754', CAST(N'2024-10-22T13:05:35.0092203' AS DateTime2), 1, NULL, 110, NULL, 0, NULL)
GO
INSERT [dbo].[TransferTypes] ([Id], [Name], [IsDeleted], [DeletedOnUtc]) VALUES (N'3ad7b193-8faa-432a-1bf4-08dcef84c891', N'Kira Ödemesi', 0, NULL)
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'7a26ff0d-6ace-40b0-2bc7-08dcef4d247f', N'5391550d-0b2b-453c-ee9b-08dcef4d24ab')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'5e10af85-58cf-488d-9219-08dcef84597b', N'5391550d-0b2b-453c-ee9b-08dcef4d24ab')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'b8b97204-22c6-499f-8550-08dcefaf3e9c', N'5391550d-0b2b-453c-ee9b-08dcef4d24ab')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'ba75518f-67bb-4a1b-179d-08dcf26e6754', N'5391550d-0b2b-453c-ee9b-08dcef4d24ab')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'7a26ff0d-6ace-40b0-2bc7-08dcef4d247f', N'82a25c19-b893-4bf8-16a6-08dcf1165687')
GO
INSERT [dbo].[Users] ([Id], [TCKNO], [FirstName], [LastName], [RegistrationDate], [IsDeleted], [DeletedOnUtc], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7a26ff0d-6ace-40b0-2bc7-08dcef4d247f', N'string', N'Serkan', N'Yildirim', CAST(N'2024-10-18T11:16:16.4778598' AS DateTime2), 0, NULL, N'serkan@mail.com_18.10.2024 08:16:16', N'SERKAN@MAIL.COM_18.10.2024 08:16:16', N'serkan@mail.com', N'SERKAN@MAIL.COM', 0, N'AQAAAAIAAYagAAAAENyFdNW63zFVwAcH/zU7EKJqXv+k2sFkdEW27bF8WqfMa1So6XH6itXok+BlHIqPEw==', N'H5YFCXQPOUB3MBGQX6KNREYMXESJFLEJ', N'85f56b86-39c6-47c6-9ca5-36230d9042df', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [TCKNO], [FirstName], [LastName], [RegistrationDate], [IsDeleted], [DeletedOnUtc], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5e10af85-58cf-488d-9219-08dcef84597b', N'10120230344', N'Ayşe', N'Özdemir', CAST(N'2024-10-18T17:51:28.4911490' AS DateTime2), 0, NULL, N'ayse@mail.com_18.10.2024 14:51:28', N'AYSE@MAIL.COM_18.10.2024 14:51:28', N'ayse@mail.com', N'AYSE@MAIL.COM', 0, N'AQAAAAIAAYagAAAAEFi/9lnMSOPC/yi77xBBKYzYLSBB/YI0TBQoxybbKkrbZ2ayALGEpgo//G+ArvJ6YA==', N'FGWK4ZCQGCRLOFMCAFJTD6LHQ2UT7FIB', N'194603a6-0470-461e-928a-5f86f7c4aa4f', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [TCKNO], [FirstName], [LastName], [RegistrationDate], [IsDeleted], [DeletedOnUtc], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b8b97204-22c6-499f-8550-08dcefaf3e9c', N'20230340455', N'Mehmet', N'Korkmaz', CAST(N'2024-10-18T22:58:31.7345538' AS DateTime2), 0, NULL, N'mehmet@mail.com_18.10.2024 19:58:31', N'MEHMET@MAIL.COM_18.10.2024 19:58:31', N'mehmet@mail.com', N'MEHMET@MAIL.COM', 0, N'AQAAAAIAAYagAAAAEMBXZOmiAjeYsAgdb6H69pZlaZj0jFOK3JDmtUGfE3Amt4ZK2oBAZSRJ6fhDD3OWAQ==', N'HTSHYICZFDLNFBVFPXH5EGOY56X7XBWB', N'b95fd0a3-0584-45d0-b3cb-442b6f0c1716', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [TCKNO], [FirstName], [LastName], [RegistrationDate], [IsDeleted], [DeletedOnUtc], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ba75518f-67bb-4a1b-179d-08dcf26e6754', N'40450560677', N'Merve', N'Durmaz', CAST(N'2024-10-22T10:51:56.0893215' AS DateTime2), 0, NULL, N'merve@mail.com_22.10.2024 07:51:56', N'MERVE@MAIL.COM_22.10.2024 07:51:56', N'merve@mail.com', N'MERVE@MAIL.COM', 0, N'AQAAAAIAAYagAAAAEL1Btdv2dlLGeLWkavREA1BM9EshjNFSK3mlwXGZ3HO018ale2dcnqf93hkAew3Zlw==', N'LFE4YRV6KQ5WJGDVAY75OANVUCIPTPIJ', N'6552c2a9-6666-4a96-97ba-92950c25a166', NULL, 0, 0, NULL, 1, 0)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Accounts_AccountNo]    Script Date: 22.10.2024 16:48:21 ******/
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [AK_Accounts_AccountNo] UNIQUE NONCLUSTERED 
(
	[AccountNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Accounts_AccountTypeId]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_AccountTypeId] ON [dbo].[Accounts]
(
	[AccountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Accounts_UserId]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_UserId] ON [dbo].[Accounts]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleClaims_RoleId]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [IX_RoleClaims_RoleId] ON [dbo].[RoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 22.10.2024 16:48:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Roles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transfers_RecipientAccountId]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [IX_Transfers_RecipientAccountId] ON [dbo].[Transfers]
(
	[RecipientAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transfers_RecipientUserId]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [IX_Transfers_RecipientUserId] ON [dbo].[Transfers]
(
	[RecipientUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transfers_SenderAccountId]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [IX_Transfers_SenderAccountId] ON [dbo].[Transfers]
(
	[SenderAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transfers_SenderUserId]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [IX_Transfers_SenderUserId] ON [dbo].[Transfers]
(
	[SenderUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transfers_TransferTypeId]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [IX_Transfers_TransferTypeId] ON [dbo].[Transfers]
(
	[TransferTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserClaims_UserId]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId] ON [dbo].[UserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserLogins_UserId]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId] ON [dbo].[UserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Users_TCKNO]    Script Date: 22.10.2024 16:48:21 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [AK_Users_TCKNO] UNIQUE NONCLUSTERED 
(
	[TCKNO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 22.10.2024 16:48:21 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[Users]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 22.10.2024 16:48:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[Users]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_AccountTypes_AccountTypeId] FOREIGN KEY([AccountTypeId])
REFERENCES [dbo].[AccountTypes] ([Id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_AccountTypes_AccountTypeId]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Users_UserId]
GO
ALTER TABLE [dbo].[RoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleClaims] CHECK CONSTRAINT [FK_RoleClaims_Roles_RoleId]
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD  CONSTRAINT [FK_Transfers_Accounts_RecipientAccountId] FOREIGN KEY([RecipientAccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Transfers] CHECK CONSTRAINT [FK_Transfers_Accounts_RecipientAccountId]
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD  CONSTRAINT [FK_Transfers_Accounts_SenderAccountId] FOREIGN KEY([SenderAccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Transfers] CHECK CONSTRAINT [FK_Transfers_Accounts_SenderAccountId]
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD  CONSTRAINT [FK_Transfers_TransferTypes_TransferTypeId] FOREIGN KEY([TransferTypeId])
REFERENCES [dbo].[TransferTypes] ([Id])
GO
ALTER TABLE [dbo].[Transfers] CHECK CONSTRAINT [FK_Transfers_TransferTypes_TransferTypeId]
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD  CONSTRAINT [FK_Transfers_Users_RecipientUserId] FOREIGN KEY([RecipientUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Transfers] CHECK CONSTRAINT [FK_Transfers_Users_RecipientUserId]
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD  CONSTRAINT [FK_Transfers_Users_SenderUserId] FOREIGN KEY([SenderUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Transfers] CHECK CONSTRAINT [FK_Transfers_Users_SenderUserId]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_UserClaims_Users_UserId]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [AliServanKilinc20241012] SET  READ_WRITE 
GO
