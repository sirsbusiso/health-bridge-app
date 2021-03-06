USE [master]
GO
/****** Object:  Database [HealthBridgeDB]    Script Date: 2022/01/31 12:41:55 ******/
CREATE DATABASE [HealthBridgeDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HealthBridgeDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER2022\MSSQL\DATA\HealthBridgeDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HealthBridgeDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER2022\MSSQL\DATA\HealthBridgeDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [HealthBridgeDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HealthBridgeDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HealthBridgeDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [HealthBridgeDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HealthBridgeDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HealthBridgeDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HealthBridgeDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HealthBridgeDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HealthBridgeDB] SET  MULTI_USER 
GO
ALTER DATABASE [HealthBridgeDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HealthBridgeDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HealthBridgeDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HealthBridgeDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HealthBridgeDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HealthBridgeDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [HealthBridgeDB] SET QUERY_STORE = OFF
GO
USE [HealthBridgeDB]
GO
/****** Object:  Table [dbo].[tb_Invoice]    Script Date: 2022/01/31 12:41:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Invoice](
	[InvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceDateTime] [datetime] NOT NULL,
	[PatientId] [bigint] NULL,
	[InvoiceTotal] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Invoice_Line]    Script Date: 2022/01/31 12:41:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Invoice_Line](
	[InvoiceLineId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NULL,
	[Qty] [float] NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[LineTotal] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceLineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Patient]    Script Date: 2022/01/31 12:41:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Patient](
	[PatientId] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[IdNumber] [varchar](13) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_Invoice]  WITH CHECK ADD FOREIGN KEY([PatientId])
REFERENCES [dbo].[tb_Patient] ([PatientId])
GO
ALTER TABLE [dbo].[tb_Invoice_Line]  WITH CHECK ADD FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[tb_Invoice] ([InvoiceId])
GO
USE [master]
GO
ALTER DATABASE [HealthBridgeDB] SET  READ_WRITE 
GO
