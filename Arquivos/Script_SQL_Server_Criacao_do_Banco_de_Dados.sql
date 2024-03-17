USE [DesafioS4EDb]
GO
/****** Object:  Table [dbo].[Empresas]    Script Date: 03/17/2024 01:06:10 ******/
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
/****** Object:  Table [dbo].[Associados]    Script Date: 03/17/2024 01:06:10 ******/
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
/****** Object:  Table [dbo].[EmpresasAssociados]    Script Date: 03/17/2024 01:06:10 ******/
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
/****** Object:  ForeignKey [FK_EmpresasAssociados_Associados]    Script Date: 03/17/2024 01:06:10 ******/
ALTER TABLE [dbo].[EmpresasAssociados]  WITH CHECK ADD  CONSTRAINT [FK_EmpresasAssociados_Associados] FOREIGN KEY([IdAssociado])
REFERENCES [dbo].[Associados] ([Id])
GO
ALTER TABLE [dbo].[EmpresasAssociados] CHECK CONSTRAINT [FK_EmpresasAssociados_Associados]
GO
/****** Object:  ForeignKey [FK_EmpresasAssociados_Empresas]    Script Date: 03/17/2024 01:06:10 ******/
ALTER TABLE [dbo].[EmpresasAssociados]  WITH CHECK ADD  CONSTRAINT [FK_EmpresasAssociados_Empresas] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresas] ([Id])
GO
ALTER TABLE [dbo].[EmpresasAssociados] CHECK CONSTRAINT [FK_EmpresasAssociados_Empresas]
GO
