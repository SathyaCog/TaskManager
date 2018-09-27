
CREATE DATABASE [TaskManager]

USE [TaskManager]
GO

/****** Object:  Table [dbo].[tblTask]    Script Date: 27-09-2018 19:27:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblTask](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[Task] [varchar](100) NOT NULL,
	[ParentTask] [varchar](100) NULL,
	[Priority] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_tblTask] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


