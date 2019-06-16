USE [master]
GO
/****** Object:  Database [mususapp]    Script Date: 16/06/2019 6.17.10 PM ******/
CREATE DATABASE [mususapp]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [mususapp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [mususapp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [mususapp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [mususapp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [mususapp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [mususapp] SET ARITHABORT OFF 
GO
ALTER DATABASE [mususapp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [mususapp] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [mususapp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [mususapp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [mususapp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [mususapp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [mususapp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [mususapp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [mususapp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [mususapp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [mususapp] SET  ENABLE_BROKER 
GO
ALTER DATABASE [mususapp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [mususapp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [mususapp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [mususapp] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [mususapp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [mususapp] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [mususapp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [mususapp] SET RECOVERY FULL 
GO
ALTER DATABASE [mususapp] SET  MULTI_USER 
GO
ALTER DATABASE [mususapp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [mususapp] SET DB_CHAINING OFF 
GO
USE [mususapp]
GO
/****** Object:  Table [dbo].[AppMaster]    Script Date: 16/06/2019 6.17.10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AppMaster](
	[Id] [uniqueidentifier] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Title] [varchar](max) NULL,
	[Description] [varchar](max) NULL,
	[Version] [varchar](50) NULL,
	[Icon] [varchar](max) NULL,
	[AndriodSmartPhoneBuild] [varchar](max) NULL,
	[AndriodTabletBuild] [varchar](max) NULL,
	[IphoneBuild] [varchar](max) NULL,
	[IphonePackageName] [varchar](max) NULL,
	[IpadBuild] [varchar](max) NULL,
	[IpadPackageName] [varchar](max) NULL,
	[ScreenShots] [varchar](max) NULL,
	[Documents] [varchar](max) NULL,
	[Published] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[DeletedTime] [datetime] NULL,
	[Download] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 16/06/2019 6.17.10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 16/06/2019 6.17.10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Msg] [text] NULL,
	[CreateTime] [datetime] NULL,
	[UserName] [text] NULL,
	[PostId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[DocumentMaster]    Script Date: 16/06/2019 6.17.11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DocumentMaster](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [varchar](max) NULL,
	[Description] [varchar](max) NULL,
	[Documents] [varchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[DeletedTime] [datetime] NULL,
	[Download] [int] NULL,
 CONSTRAINT [PK_DocumentMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LogMaster]    Script Date: 16/06/2019 6.17.11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LogMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[logtime] [datetime] NULL,
	[logtext] [varchar](max) NULL,
 CONSTRAINT [PK_LogMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Post]    Script Date: 16/06/2019 6.17.11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Txt] [text] NULL,
	[CreateTime] [datetime] NULL,
	[UserName] [text] NULL,
	[AppId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Rating]    Script Date: 16/06/2019 6.17.12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppId] [uniqueidentifier] NOT NULL,
	[Point] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[ThirdParty]    Script Date: 16/06/2019 6.17.12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ThirdParty](
	[Id] [uniqueidentifier] NOT NULL,
	[CategoryId] [int] NULL,
	[Title] [varchar](max) NULL,
	[Description] [varchar](max) NULL,
	[Version] [varchar](max) NULL,
	[ThirdPartyAppUrl] [varchar](max) NULL,
	[Documents] [varchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[DeletedTime] [datetime] NULL,
	[Download] [int] NULL,
 CONSTRAINT [PK_ThirdParty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 16/06/2019 6.17.12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](max) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](150) NOT NULL,
	[Email] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WebPageUrls]    Script Date: 16/06/2019 6.17.12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WebPageUrls](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [varchar](max) NULL,
	[Description] [varchar](max) NULL,
	[WebPageUrl] [varchar](max) NULL,
	[Documents] [varchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[DeletedTime] [datetime] NULL,
	[Download] [int] NULL,
 CONSTRAINT [PK_WebPageUrls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[AppMaster] ([Id], [CategoryId], [Title], [Description], [Version], [Icon], [AndriodSmartPhoneBuild], [AndriodTabletBuild], [IphoneBuild], [IphonePackageName], [IpadBuild], [IpadPackageName], [ScreenShots], [Documents], [Published], [IsDeleted], [CreatedTime], [DeletedTime], [Download]) VALUES (N'b8192fa2-88dd-4a18-a367-bbf39a72ca5e', 1, N'FIrst HOster App', N'Let me test first', N'0.0.1', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_favicon.ico', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_favicon.ico', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_favicon.ico', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_favicon.ico', N'First', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_main.977e40af1dc89bed0cf8.js', N'FIrst', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_favicon.ico', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_3rdpartylicenses.txt', 1, 0, CAST(0x0000AA6E00372BF5 AS DateTime), NULL, 2)
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Demo')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, NULL)
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, NULL)
INSERT [dbo].[Category] ([Id], [Name]) VALUES (4, NULL)
INSERT [dbo].[Category] ([Id], [Name]) VALUES (5, N'rererer')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (6, N'Game')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Rating] ON 

INSERT [dbo].[Rating] ([Id], [AppId], [Point]) VALUES (1, N'b8192fa2-88dd-4a18-a367-bbf39a72ca5e', 4)
SET IDENTITY_INSERT [dbo].[Rating] OFF
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([Id], [FirstName], [LastName], [UserName], [Password], [Email], [IsActive], [IsAdmin]) VALUES (1, N'Anand', N'Lukade', N'Anand.Lukade', N'Password@123', N'anand.lukade@zensar.com', 1, 1)
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
USE [master]
GO
ALTER DATABASE [mususapp] SET  READ_WRITE 
GO
