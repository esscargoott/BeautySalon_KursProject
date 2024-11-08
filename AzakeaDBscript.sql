USE [master]
GO
/****** Object:  Database [AzaleaDB]    Script Date: 11.06.2023 9:34:08 ******/
CREATE DATABASE [AzaleaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AzaleaDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\AzaleaDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AzaleaDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\AzaleaDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AzaleaDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AzaleaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AzaleaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AzaleaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AzaleaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AzaleaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AzaleaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AzaleaDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AzaleaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AzaleaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AzaleaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AzaleaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AzaleaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AzaleaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AzaleaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AzaleaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AzaleaDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AzaleaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AzaleaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AzaleaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AzaleaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AzaleaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AzaleaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AzaleaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AzaleaDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AzaleaDB] SET  MULTI_USER 
GO
ALTER DATABASE [AzaleaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AzaleaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AzaleaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AzaleaDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AzaleaDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AzaleaDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AzaleaDB] SET QUERY_STORE = OFF
GO
USE [AzaleaDB]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 11.06.2023 9:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[IdAdmin] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Surname] [nvarchar](20) NOT NULL,
	[Patronymic] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](15) NOT NULL,
	[Login] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[IdAdmin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 11.06.2023 9:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[IdClient] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](60) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[IdClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 11.06.2023 9:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistory](
	[IdHistory] [int] IDENTITY(1,1) NOT NULL,
	[IdAdmin] [int] NOT NULL,
	[DateTime] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
(
	[IdHistory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Masters]    Script Date: 11.06.2023 9:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Masters](
	[IdMaster] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](60) NOT NULL,
	[JobTitle] [nvarchar](40) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Masters] PRIMARY KEY CLUSTERED 
(
	[IdMaster] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 11.06.2023 9:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[IdSchedule] [int] IDENTITY(1,1) NOT NULL,
	[IdMaster] [int] NOT NULL,
	[IdService] [int] NOT NULL,
	[IdClient] [int] NOT NULL,
	[DateTime] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[IdSchedule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 11.06.2023 9:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[IdService] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Price] [money] NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[IdService] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServicesOfMasters]    Script Date: 11.06.2023 9:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicesOfMasters](
	[idService] [int] NOT NULL,
	[idMaster] [int] NOT NULL,
 CONSTRAINT [PK_ServicesOfSchedule] PRIMARY KEY CLUSTERED 
(
	[idService] ASC,
	[idMaster] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admins] ON 

INSERT [dbo].[Admins] ([IdAdmin], [Name], [Surname], [Patronymic], [Password], [Login]) VALUES (17, N'Александра
', N'Смирнова
', N'Владимировна
', N'admin1', N'admin1')
INSERT [dbo].[Admins] ([IdAdmin], [Name], [Surname], [Patronymic], [Password], [Login]) VALUES (19, N'Елизавета
', N'Иванова
', N'Романовна
', N'admin2', N'admin2')
SET IDENTITY_INSERT [dbo].[Admins] OFF
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (1, N'Андреева Валерия Леонидовна', N'89302070793', N'ул. Красная д. 8 кв. 938', N'crafemmapaube@gmail.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (2, N'Олейников Платон Саввич', N'89305637793', N'ул. Космонавтов д. 54', N'crokenuddasse-5176@yopmail.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (3, N'Гончарова Алиса Лукинична', N'89302073839', N'ул. Мира д. 28 кв. 12', N'jugroiwoiffoufe-3356@yopmail.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (5, N'Пухов Дмитрий Иванович', N'89302111193', N'ул. Виноградная д. 5', N'yellowzuby@gmail.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (6, N'Смирнов Александр Леонович', N'89302072222', N'ул. Шевченко д. 1 кв. 32', N'craddetebiyo-6237@yopmail.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (7, N'Воронова Екатерина Кирилловна', N'89302079879', N'ул. Морская д. 39', N'mitori76787@necktai.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (8, N'Иванов Дмитрий Андреевич', N'89334354553', N'ул. Левоневского д. 4 кв. 27', N'mitori6424@necktai.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (9, N'Ковалев Андрей Матвеевич', N'89302035345', N'ул. Кислая д. 4 кв. 33', N'dicens.chet@yahoo.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (10, N'Александрова Елена Артёмовна', N'89302035345', N'ул. Медленная д. 92', N'rippin.raina@yahoo.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (11, N'Ильин Тимофей Елисеевич', N'89302234111', N'ул. Красная д. 9 кв. 43', N'myrtice.wolff@hotmail.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (12, N'Пономарев Даниил Даниилович', N'89302987655', N'ул. Косманавтов д. 61', N'aron.franecki@gmail.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (14, N'Овсянникова Мария Петровна', N'89876545674', N'ул. Пожарная д. 98', N'kenna.heathcote@harris.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (15, N'Андреева Татьяна Романовна', N'89365432435', N'ул. Виноградная д. 24', N'benedict25@hotmail.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (16, N'Золотарева Ульяна Львовна', N'89356434567', N'ул. Шевченко д. 2 кв. 55', N'nola.stoltenberg@witting.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (17, N'Кравец Кристина Романовна', N'89394838457', N'ул. Новороссийская д. 5', N'kristakravech@mail.ru')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (18, N'Жданова Алиса Андреевна', N'89302456554', N'ул. Морская д. 21', N'beulah.wintheiser@hotmail.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (19, N'Голубева Александра Сергеевна', N'89356435645', N'ул. Левоневского д. 35 кв. 245', N'uriel79@turner.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (20, N'Петров Гордей Степанович', N'89301111111', N'ул. Кислая д. 2 кв. 56', N'orland.crist@hotmail.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (21, N'Казанцева Галина Ярославовна', N'85937538475', N'ул. Красных партизан д. 34', N'galyshka@mail.ru')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (22, N'Абрамов Марк Иванович', N'89301111564', N'ул. Медленная д. 123', N'kayden.green@yahoo.com')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (24, N'Лыжнева Анастасия Ивановна', N'98765434567', N'ул. Индустриальная', N'vorona@mail.ru')
INSERT [dbo].[Clients] ([IdClient], [FullName], [Phone], [Address], [Email]) VALUES (25, N'Мазырина Варвара Владимировна', N'89183673042', N'ул. Дымовая д.4 кв.34', N'honkai@gmail.com')
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[LoginHistory] ON 

INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (1, 17, CAST(N'2023-05-14T13:02:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2, 17, CAST(N'2023-05-14T13:03:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3, 19, CAST(N'2023-05-14T13:18:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (4, 17, CAST(N'2023-05-14T13:49:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5, 17, CAST(N'2023-05-14T13:54:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (6, 17, CAST(N'2023-05-14T13:57:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7, 19, CAST(N'2023-05-14T14:00:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (8, 19, CAST(N'2023-05-14T14:12:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (9, 19, CAST(N'2023-05-14T15:53:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (10, 19, CAST(N'2023-05-14T15:54:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (11, 17, CAST(N'2023-05-14T15:56:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (12, 19, CAST(N'2023-05-14T15:59:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (13, 19, CAST(N'2023-05-14T16:19:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (14, 17, CAST(N'2023-05-14T16:22:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (15, 19, CAST(N'2023-05-14T16:24:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (16, 17, CAST(N'2023-05-14T16:25:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (17, 17, CAST(N'2023-05-15T19:52:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (18, 19, CAST(N'2023-05-15T19:59:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (19, 17, CAST(N'2023-05-15T20:02:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (20, 19, CAST(N'2023-05-15T20:05:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (21, 17, CAST(N'2023-05-15T20:10:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (22, 19, CAST(N'2023-05-15T20:12:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (23, 17, CAST(N'2023-05-15T20:27:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (24, 17, CAST(N'2023-05-15T20:28:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (1002, 19, CAST(N'2023-05-19T19:22:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (1003, 19, CAST(N'2023-05-19T19:31:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (1004, 17, CAST(N'2023-05-19T20:18:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2002, 19, CAST(N'2023-05-21T12:26:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2003, 17, CAST(N'2023-05-21T12:32:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2004, 19, CAST(N'2023-05-21T12:34:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2005, 19, CAST(N'2023-05-21T12:37:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2006, 17, CAST(N'2023-05-21T12:38:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2007, 19, CAST(N'2023-05-21T12:57:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2008, 17, CAST(N'2023-05-21T13:08:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2009, 19, CAST(N'2023-05-21T13:09:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2010, 19, CAST(N'2023-05-21T13:11:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2011, 19, CAST(N'2023-05-21T13:12:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2012, 17, CAST(N'2023-05-21T13:13:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2013, 17, CAST(N'2023-05-21T13:17:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2014, 19, CAST(N'2023-05-21T13:21:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2015, 19, CAST(N'2023-05-21T13:22:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2016, 19, CAST(N'2023-05-21T13:25:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2017, 17, CAST(N'2023-05-21T13:28:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2018, 19, CAST(N'2023-05-21T13:28:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2019, 19, CAST(N'2023-05-21T13:47:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2020, 19, CAST(N'2023-05-21T16:22:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2021, 19, CAST(N'2023-05-22T20:08:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2022, 19, CAST(N'2023-05-22T20:09:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2023, 17, CAST(N'2023-05-22T20:11:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2024, 17, CAST(N'2023-05-22T20:17:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2025, 19, CAST(N'2023-05-22T20:21:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2026, 17, CAST(N'2023-05-22T20:23:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2027, 19, CAST(N'2023-05-22T20:34:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2028, 17, CAST(N'2023-05-22T20:38:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2029, 17, CAST(N'2023-05-22T20:44:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2030, 17, CAST(N'2023-05-23T17:47:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2031, 19, CAST(N'2023-05-23T17:59:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2032, 19, CAST(N'2023-05-23T18:01:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2033, 17, CAST(N'2023-05-23T18:16:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2034, 17, CAST(N'2023-05-23T18:18:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (2035, 17, CAST(N'2023-05-23T18:34:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3002, 17, CAST(N'2023-05-24T10:46:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3003, 19, CAST(N'2023-05-24T11:05:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3004, 19, CAST(N'2023-05-24T11:05:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3005, 19, CAST(N'2023-05-24T11:15:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3006, 19, CAST(N'2023-05-24T11:19:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3007, 19, CAST(N'2023-05-24T11:21:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3008, 19, CAST(N'2023-05-24T11:23:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3009, 19, CAST(N'2023-05-24T11:24:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3010, 19, CAST(N'2023-05-24T11:26:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3011, 19, CAST(N'2023-05-24T11:28:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3012, 19, CAST(N'2023-05-24T11:33:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3013, 19, CAST(N'2023-05-24T11:34:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3014, 19, CAST(N'2023-05-24T11:36:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (3015, 19, CAST(N'2023-05-24T11:44:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (4002, 17, CAST(N'2023-05-24T17:26:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (4003, 17, CAST(N'2023-05-24T17:31:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (4004, 19, CAST(N'2023-05-24T17:35:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (4005, 19, CAST(N'2023-05-24T19:00:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (4006, 17, CAST(N'2023-05-24T19:01:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (4007, 17, CAST(N'2023-05-24T19:22:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (4008, 19, CAST(N'2023-05-24T19:26:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (4009, 19, CAST(N'2023-05-24T19:28:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (4010, 19, CAST(N'2023-05-24T19:34:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5002, 19, CAST(N'2023-05-25T17:38:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5003, 17, CAST(N'2023-05-25T17:46:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5004, 19, CAST(N'2023-05-25T17:46:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5005, 17, CAST(N'2023-05-25T17:59:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5006, 17, CAST(N'2023-05-25T18:14:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5007, 17, CAST(N'2023-05-25T20:16:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5008, 17, CAST(N'2023-05-25T20:18:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5009, 17, CAST(N'2023-05-25T20:41:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5010, 19, CAST(N'2023-05-25T20:42:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5011, 17, CAST(N'2023-05-25T20:43:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5012, 17, CAST(N'2023-05-25T20:44:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5013, 19, CAST(N'2023-05-25T20:44:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5014, 17, CAST(N'2023-05-25T20:48:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5015, 19, CAST(N'2023-05-25T20:50:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5016, 19, CAST(N'2023-05-25T20:53:00' AS SmallDateTime))
GO
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5017, 19, CAST(N'2023-05-26T16:28:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5018, 19, CAST(N'2023-05-26T16:39:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5019, 17, CAST(N'2023-05-26T16:43:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5020, 17, CAST(N'2023-05-26T16:52:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5021, 19, CAST(N'2023-05-26T16:54:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5022, 17, CAST(N'2023-05-26T17:09:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5023, 17, CAST(N'2023-05-27T19:13:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5024, 19, CAST(N'2023-05-27T19:14:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5025, 19, CAST(N'2023-05-27T19:26:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5026, 17, CAST(N'2023-05-28T08:41:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5027, 19, CAST(N'2023-05-28T11:46:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5028, 19, CAST(N'2023-05-28T11:58:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5029, 19, CAST(N'2023-05-28T12:04:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5030, 19, CAST(N'2023-05-28T12:06:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5031, 17, CAST(N'2023-05-28T12:08:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5032, 19, CAST(N'2023-05-28T12:17:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5033, 19, CAST(N'2023-05-28T12:20:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5034, 19, CAST(N'2023-05-28T12:21:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5035, 17, CAST(N'2023-05-28T12:22:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5036, 19, CAST(N'2023-05-28T12:48:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5037, 19, CAST(N'2023-05-28T12:58:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5038, 17, CAST(N'2023-05-28T13:04:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5039, 17, CAST(N'2023-06-02T17:52:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5040, 19, CAST(N'2023-06-02T17:58:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5041, 19, CAST(N'2023-06-02T18:14:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5042, 17, CAST(N'2023-06-02T18:23:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5043, 17, CAST(N'2023-06-02T18:43:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5044, 17, CAST(N'2023-06-02T18:45:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5045, 19, CAST(N'2023-06-02T18:59:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5046, 17, CAST(N'2023-06-02T19:00:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5047, 19, CAST(N'2023-06-03T19:33:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5048, 17, CAST(N'2023-06-03T20:20:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5049, 19, CAST(N'2023-06-03T20:22:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5050, 17, CAST(N'2023-06-03T20:28:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5051, 19, CAST(N'2023-06-03T20:36:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5052, 17, CAST(N'2023-06-03T20:37:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5053, 17, CAST(N'2023-06-03T20:50:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5054, 17, CAST(N'2023-06-03T20:54:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5055, 19, CAST(N'2023-06-03T20:58:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5056, 17, CAST(N'2023-06-04T12:11:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5057, 17, CAST(N'2023-06-04T12:16:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5058, 17, CAST(N'2023-06-04T12:41:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5059, 19, CAST(N'2023-06-04T12:46:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5060, 17, CAST(N'2023-06-04T12:48:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5061, 17, CAST(N'2023-06-04T12:50:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5062, 19, CAST(N'2023-06-04T12:56:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5063, 17, CAST(N'2023-06-04T12:59:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5064, 17, CAST(N'2023-06-04T13:20:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5065, 17, CAST(N'2023-06-04T13:29:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5066, 17, CAST(N'2023-06-04T13:44:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5067, 17, CAST(N'2023-06-04T13:46:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5068, 17, CAST(N'2023-06-04T14:14:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5069, 19, CAST(N'2023-06-04T14:18:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5070, 17, CAST(N'2023-06-04T14:20:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5071, 17, CAST(N'2023-06-04T14:34:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5072, 17, CAST(N'2023-06-04T15:15:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5073, 19, CAST(N'2023-06-04T16:33:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5074, 19, CAST(N'2023-06-04T16:36:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5075, 17, CAST(N'2023-06-04T16:39:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5076, 17, CAST(N'2023-06-05T19:21:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5077, 17, CAST(N'2023-06-05T19:21:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5078, 17, CAST(N'2023-06-05T19:21:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5079, 17, CAST(N'2023-06-05T19:22:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5080, 19, CAST(N'2023-06-05T19:24:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5081, 17, CAST(N'2023-06-06T10:16:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (5082, 19, CAST(N'2023-06-06T10:21:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (6039, 19, CAST(N'2023-06-06T16:46:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (6040, 17, CAST(N'2023-06-06T16:52:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (6041, 19, CAST(N'2023-06-06T16:55:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (6042, 17, CAST(N'2023-06-06T17:01:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (6043, 19, CAST(N'2023-06-06T17:03:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (6044, 19, CAST(N'2023-06-06T17:11:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (6045, 17, CAST(N'2023-06-06T17:48:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (6046, 19, CAST(N'2023-06-06T18:40:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7039, 17, CAST(N'2023-06-07T19:39:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7040, 19, CAST(N'2023-06-07T19:42:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7041, 17, CAST(N'2023-06-07T19:44:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7042, 17, CAST(N'2023-06-07T20:05:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7043, 19, CAST(N'2023-06-07T20:07:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7044, 17, CAST(N'2023-06-08T20:23:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7045, 17, CAST(N'2023-06-08T20:26:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7046, 19, CAST(N'2023-06-08T20:28:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7047, 19, CAST(N'2023-06-08T20:33:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7048, 19, CAST(N'2023-06-08T20:34:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7049, 19, CAST(N'2023-06-08T20:46:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7050, 19, CAST(N'2023-06-08T20:48:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7051, 19, CAST(N'2023-06-08T21:00:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7052, 17, CAST(N'2023-06-08T21:10:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7053, 19, CAST(N'2023-06-08T21:12:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7054, 17, CAST(N'2023-06-08T21:17:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7055, 19, CAST(N'2023-06-08T21:18:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7056, 17, CAST(N'2023-06-09T19:42:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7057, 19, CAST(N'2023-06-09T19:46:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7058, 19, CAST(N'2023-06-09T19:59:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7059, 17, CAST(N'2023-06-09T20:00:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7060, 19, CAST(N'2023-06-09T20:01:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7061, 17, CAST(N'2023-06-09T20:16:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7062, 19, CAST(N'2023-06-09T20:23:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7063, 17, CAST(N'2023-06-09T20:27:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7064, 17, CAST(N'2023-06-09T20:41:00' AS SmallDateTime))
GO
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7065, 17, CAST(N'2023-06-09T20:50:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7066, 19, CAST(N'2023-06-09T20:54:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7067, 19, CAST(N'2023-06-09T20:57:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7068, 19, CAST(N'2023-06-09T21:08:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7069, 17, CAST(N'2023-06-10T08:29:00' AS SmallDateTime))
INSERT [dbo].[LoginHistory] ([IdHistory], [IdAdmin], [DateTime]) VALUES (7070, 17, CAST(N'2023-06-10T08:35:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[LoginHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[Masters] ON 

INSERT [dbo].[Masters] ([IdMaster], [FullName], [JobTitle], [Address], [Phone]) VALUES (1, N'Мишутина Алёна Ивановна', N'Мастер маникюра', N'ул. Железнодорожная д. 4', N'89367485231')
INSERT [dbo].[Masters] ([IdMaster], [FullName], [JobTitle], [Address], [Phone]) VALUES (2, N'Перов Никита Николаевич', N'Колорист', N'ул. Московская д. 9 кв. 93', N'89238475647')
INSERT [dbo].[Masters] ([IdMaster], [FullName], [JobTitle], [Address], [Phone]) VALUES (3, N'Белякова Амина Дамировна', N'Визажист', N'ул. Горького д. 43', N'89284756471')
INSERT [dbo].[Masters] ([IdMaster], [FullName], [JobTitle], [Address], [Phone]) VALUES (4, N'Моисеева Александра Павловна', N'Парикмахер-стилист', N'ул. Красная д. 9 кв 123', N'89127463749')
INSERT [dbo].[Masters] ([IdMaster], [FullName], [JobTitle], [Address], [Phone]) VALUES (5, N'Зайцева Изольда Богдановна', N'Массажист', N'ул. Космонавтов д. 98', N'89123456473')
INSERT [dbo].[Masters] ([IdMaster], [FullName], [JobTitle], [Address], [Phone]) VALUES (6, N'Мирный Мирослав Васильевич', N'Косметолог', N'ул. Морская д. 29 кв. 83', N'89123847573')
INSERT [dbo].[Masters] ([IdMaster], [FullName], [JobTitle], [Address], [Phone]) VALUES (7, N'Никифоров Ярослав Тихонович', N'Парикмахер-стилист', N'ул. Дальняя д. 76', N'89287765432')
INSERT [dbo].[Masters] ([IdMaster], [FullName], [JobTitle], [Address], [Phone]) VALUES (8, N'Соловьёв Юрий Васильевич', N'Мастер педикюра', N'ул. Виноградная д. 5', N'89283745857')
INSERT [dbo].[Masters] ([IdMaster], [FullName], [JobTitle], [Address], [Phone]) VALUES (2009, N'Юльцов Валерий Генадиевич', N'Тату мастер', N'ул. Красная д. 8 кв. 11 ', N'88005553555')
SET IDENTITY_INSERT [dbo].[Masters] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (4, 7, 5, 14, CAST(N'2023-05-05T16:40:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (5, 1, 2, 11, CAST(N'2023-04-16T12:00:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (9, 8, 3, 7, CAST(N'2023-05-09T10:40:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (10, 4, 5, 9, CAST(N'2023-05-20T12:30:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (11, 7, 5, 10, CAST(N'2023-05-20T13:00:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (16, 3, 7, 5, CAST(N'2023-03-27T19:00:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (17, 4, 5, 10, CAST(N'2023-04-23T16:45:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (18, 4, 4, 12, CAST(N'2023-04-23T16:40:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (20, 1, 2, 11, CAST(N'2023-02-14T17:00:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (23, 1, 1, 1, CAST(N'2023-05-23T13:40:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (24, 6, 6, 6, CAST(N'2023-06-23T17:00:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (28, 1, 1, 2, CAST(N'2023-05-28T13:45:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (32, 1, 1, 1, CAST(N'2023-01-01T13:00:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (1002, 1, 1, 25, CAST(N'2023-06-15T10:15:00' AS SmallDateTime))
INSERT [dbo].[Schedules] ([IdSchedule], [IdMaster], [IdService], [IdClient], [DateTime]) VALUES (2002, 2009, 18, 5, CAST(N'2023-06-09T10:30:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (1, N'Маникюр базовый', 2000.0000, N'Уход и однотонный цвет')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (2, N'Маникюр с дизайном', 4000.0000, N'Уход и дизайн по пожеланию клиента')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (3, N'Педикюр', 2500.0000, N'Уход и окрашивание')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (4, N'Стрижка', 600.0000, N'Стрижка ножницами по пожеланию клиента')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (5, N'Стрижка с укладкой', 750.0000, N'Стрижка ножницами по пожеланию клиента и укладка')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (6, N'Чистка лица', 2500.0000, N'Механическая и ультрозвуковая')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (7, N'Простой макияж', 2000.0000, N'Нюдовый или легкий макияж')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (8, N'Праздничный макияж', 3000.0000, N'Вечерний или яркий макияж')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (9, N'Массаж 30 мин', 1400.0000, N'Массаж в ускоренном темпе')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (10, N'Массаж 60 мин', 2500.0000, N'Массаж в обычном темпе')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (11, N'Окрашивание волос', 2670.0000, N'Окрашивание волос в яркие и обычные цвета')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (12, N'Укладка волос', 1680.0000, N'Создание прически и укладки')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (13, N'Уход за лицом', 1000.0000, N'Проведение действий для увлажнения кожи лица')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (14, N'Пилинг-массаж', 2800.0000, N'Массаж для очистки тела')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (15, N'Стрижка горячими ножницами', 2300.0000, N'Стрижка горячими ножницами которые запаивают кончики')
INSERT [dbo].[Services] ([IdService], [Name], [Price], [Description]) VALUES (18, N'Тату', 10000.0000, N'Набитие татуировки любой сложности')
SET IDENTITY_INSERT [dbo].[Services] OFF
GO
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (1, 1)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (2, 1)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (3, 8)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (4, 4)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (4, 7)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (5, 4)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (5, 7)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (6, 6)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (7, 3)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (8, 3)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (9, 5)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (10, 5)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (11, 2)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (12, 7)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (13, 6)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (14, 5)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (15, 4)
INSERT [dbo].[ServicesOfMasters] ([idService], [idMaster]) VALUES (18, 2009)
GO
ALTER TABLE [dbo].[LoginHistory]  WITH CHECK ADD  CONSTRAINT [FK_LoginHistory_Admins] FOREIGN KEY([IdAdmin])
REFERENCES [dbo].[Admins] ([IdAdmin])
GO
ALTER TABLE [dbo].[LoginHistory] CHECK CONSTRAINT [FK_LoginHistory_Admins]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Clients] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Clients] ([IdClient])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Clients]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Masters] FOREIGN KEY([IdMaster])
REFERENCES [dbo].[Masters] ([IdMaster])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Masters]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Services] FOREIGN KEY([IdService])
REFERENCES [dbo].[Services] ([IdService])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Services]
GO
ALTER TABLE [dbo].[ServicesOfMasters]  WITH CHECK ADD  CONSTRAINT [FK_ServicesOfMasters_Masters] FOREIGN KEY([idMaster])
REFERENCES [dbo].[Masters] ([IdMaster])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServicesOfMasters] CHECK CONSTRAINT [FK_ServicesOfMasters_Masters]
GO
ALTER TABLE [dbo].[ServicesOfMasters]  WITH CHECK ADD  CONSTRAINT [FK_ServicesOfMasters_Services] FOREIGN KEY([idService])
REFERENCES [dbo].[Services] ([IdService])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServicesOfMasters] CHECK CONSTRAINT [FK_ServicesOfMasters_Services]
GO
USE [master]
GO
ALTER DATABASE [AzaleaDB] SET  READ_WRITE 
GO
