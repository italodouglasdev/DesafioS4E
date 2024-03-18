USE [master]
GO
/****** Object:  Database [DesafioS4EDb]    Script Date: 03/18/2024 18:16:51 ******/
CREATE DATABASE [DesafioS4EDb] ON  PRIMARY 
( NAME = N'DesafioS4EDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\DesafioS4EDb.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DesafioS4EDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\DesafioS4EDb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DesafioS4EDb] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DesafioS4EDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DesafioS4EDb] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [DesafioS4EDb] SET ANSI_NULLS OFF
GO
ALTER DATABASE [DesafioS4EDb] SET ANSI_PADDING OFF
GO
ALTER DATABASE [DesafioS4EDb] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [DesafioS4EDb] SET ARITHABORT OFF
GO
ALTER DATABASE [DesafioS4EDb] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [DesafioS4EDb] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [DesafioS4EDb] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [DesafioS4EDb] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [DesafioS4EDb] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [DesafioS4EDb] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [DesafioS4EDb] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [DesafioS4EDb] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [DesafioS4EDb] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [DesafioS4EDb] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [DesafioS4EDb] SET  DISABLE_BROKER
GO
ALTER DATABASE [DesafioS4EDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [DesafioS4EDb] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [DesafioS4EDb] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [DesafioS4EDb] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [DesafioS4EDb] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [DesafioS4EDb] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [DesafioS4EDb] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [DesafioS4EDb] SET  READ_WRITE
GO
ALTER DATABASE [DesafioS4EDb] SET RECOVERY FULL
GO
ALTER DATABASE [DesafioS4EDb] SET  MULTI_USER
GO
ALTER DATABASE [DesafioS4EDb] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [DesafioS4EDb] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'DesafioS4EDb', N'ON'
GO
USE [DesafioS4EDb]
GO
/****** Object:  Table [dbo].[Empresas]    Script Date: 03/18/2024 18:16:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Empresas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](200) NOT NULL,
	[Cnpj] [varchar](14) NOT NULL,
 CONSTRAINT [PK_Empresas_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_Empresas_Cnpj] UNIQUE NONCLUSTERED 
(
	[Cnpj] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Associados]    Script Date: 03/18/2024 18:16:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Associados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](200) NOT NULL,
	[Cpf] [varchar](11) NOT NULL,
	[DataNascimento] [datetime] NULL,
 CONSTRAINT [PK_Associados_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_Associados_Cpf] UNIQUE NONCLUSTERED 
(
	[Cpf] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmpresasAssociados]    Script Date: 03/18/2024 18:16:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmpresasAssociados](
	[IdEmpresa] [int] NOT NULL,
	[IdAssociado] [int] NOT NULL,
 CONSTRAINT [PK_EmpresasAssociados] PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC,
	[IdAssociado] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_EmpresasAssociados_Associados]    Script Date: 03/18/2024 18:16:52 ******/
ALTER TABLE [dbo].[EmpresasAssociados]  WITH CHECK ADD  CONSTRAINT [FK_EmpresasAssociados_Associados] FOREIGN KEY([IdAssociado])
REFERENCES [dbo].[Associados] ([Id])
GO
ALTER TABLE [dbo].[EmpresasAssociados] CHECK CONSTRAINT [FK_EmpresasAssociados_Associados]
GO
/****** Object:  ForeignKey [FK_EmpresasAssociados_Empresas]    Script Date: 03/18/2024 18:16:52 ******/
ALTER TABLE [dbo].[EmpresasAssociados]  WITH CHECK ADD  CONSTRAINT [FK_EmpresasAssociados_Empresas] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresas] ([Id])
GO
ALTER TABLE [dbo].[EmpresasAssociados] CHECK CONSTRAINT [FK_EmpresasAssociados_Empresas]
GO
