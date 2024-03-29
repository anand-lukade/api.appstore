
GO
CREATE TABLE [mus].[AppMaster](
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
/****** Object:  Table [mus].[Category]    Script Date: 16/06/2019 6.17.10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [mus].[Category](
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
/****** Object:  Table [mus].[Comment]    Script Date: 16/06/2019 6.17.10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mus].[Comment](
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
/****** Object:  Table [mus].[DocumentMaster]    Script Date: 16/06/2019 6.17.11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [mus].[DocumentMaster](
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
/****** Object:  Table [mus].[LogMaster]    Script Date: 16/06/2019 6.17.11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [mus].[LogMaster](
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
/****** Object:  Table [mus].[Post]    Script Date: 16/06/2019 6.17.11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mus].[Post](
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
/****** Object:  Table [mus].[Rating]    Script Date: 16/06/2019 6.17.12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mus].[Rating](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppId] [uniqueidentifier] NOT NULL,
	[Point] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [mus].[ThirdParty]    Script Date: 16/06/2019 6.17.12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [mus].[ThirdParty](
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
/****** Object:  Table [mus].[UserInfo]    Script Date: 16/06/2019 6.17.12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [mus].[UserInfo](
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
/****** Object:  Table [mus].[WebPageUrls]    Script Date: 16/06/2019 6.17.12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [mus].[WebPageUrls](
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
INSERT [mus].[AppMaster] ([Id], [CategoryId], [Title], [Description], [Version], [Icon], [AndriodSmartPhoneBuild], [AndriodTabletBuild], [IphoneBuild], [IphonePackageName], [IpadBuild], [IpadPackageName], [ScreenShots], [Documents], [Published], [IsDeleted], [CreatedTime], [DeletedTime], [Download]) VALUES (N'b8192fa2-88dd-4a18-a367-bbf39a72ca5e', 1, N'FIrst HOster App', N'Let me test first', N'0.0.1', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_favicon.ico', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_favicon.ico', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_favicon.ico', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_favicon.ico', N'First', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_main.977e40af1dc89bed0cf8.js', N'FIrst', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_favicon.ico', N'https://apimorrisonstore.azurewebsites.net/UploadBuckets/b8192fa2-88dd-4a18-a367-bbf39a72ca5e_3rdpartylicenses.txt', 1, 0, CAST(0x0000AA6E00372BF5 AS DateTime), NULL, 2)
SET IDENTITY_INSERT [mus].[Category] ON 

INSERT [mus].[Category] ([Id], [Name]) VALUES (1, N'Demo')
INSERT [mus].[Category] ([Id], [Name]) VALUES (2, NULL)
INSERT [mus].[Category] ([Id], [Name]) VALUES (3, NULL)
INSERT [mus].[Category] ([Id], [Name]) VALUES (4, NULL)
INSERT [mus].[Category] ([Id], [Name]) VALUES (5, N'rererer')
INSERT [mus].[Category] ([Id], [Name]) VALUES (6, N'Game')
SET IDENTITY_INSERT [mus].[Category] OFF
SET IDENTITY_INSERT [mus].[Rating] ON 

INSERT [mus].[Rating] ([Id], [AppId], [Point]) VALUES (1, N'b8192fa2-88dd-4a18-a367-bbf39a72ca5e', 4)
SET IDENTITY_INSERT [mus].[Rating] OFF
SET IDENTITY_INSERT [mus].[UserInfo] ON 

INSERT [mus].[UserInfo] ([Id], [FirstName], [LastName], [UserName], [Password], [Email], [IsActive], [IsAdmin]) VALUES (1, N'Anand', N'Lukade', N'Anand.Lukade', N'Password@123', N'anand.lukade@zensar.com', 1, 1)
SET IDENTITY_INSERT [mus].[UserInfo] OFF
USE [master]
GO
ALTER DATABASE [mususapp] SET  READ_WRITE 
GO
