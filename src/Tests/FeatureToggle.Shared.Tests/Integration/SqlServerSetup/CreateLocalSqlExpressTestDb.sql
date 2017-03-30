USE [master]
GO

/****** Object:  Database [FeatureToggleIntegrationTests]    Script Date: 22/04/2014 1:27:45 PM ******/
CREATE DATABASE [FeatureToggleIntegrationTests]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FeatureToggleIntegrationTests', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\FeatureToggleIntegrationTests.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FeatureToggleIntegrationTests_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\FeatureToggleIntegrationTests.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FeatureToggleIntegrationTests].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET ANSI_NULL_DEFAULT ON 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET ANSI_NULLS ON 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET ANSI_PADDING ON 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET ANSI_WARNINGS ON 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET ARITHABORT ON 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET CURSOR_DEFAULT  LOCAL 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET CONCAT_NULL_YIELDS_NULL ON 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET QUOTED_IDENTIFIER ON 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET  DISABLE_BROKER 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET RECOVERY FULL 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET  MULTI_USER 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET DB_CHAINING OFF 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [FeatureToggleIntegrationTests] SET  READ_WRITE 
GO


USE [FeatureToggleIntegrationTests]
GO

/****** Object:  Table [dbo].[Toggles]    Script Date: 22/04/2014 1:28:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Toggles](
	[ToggleName] [nvarchar](100) NOT NULL,
	[Value] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ToggleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [FeatureToggleIntegrationTests]
GO

INSERT INTO [dbo].[Toggles]
           ([ToggleName],[Value])
     VALUES
           ('MySqlServerToggleTrue',1),
		   ('MySqlServerToggleFalse',0)
GO