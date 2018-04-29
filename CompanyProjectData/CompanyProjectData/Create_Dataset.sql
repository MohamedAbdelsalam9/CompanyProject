USE [master]
GO
/****** Object:  Database [[CompanyProjectData]    Script Date: 28-Apr-18 9:55:57 PM ******/
CREATE DATABASE [CompanyProjectData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CompanyProjectData', FILENAME = N'E:\Studying\5th Year\2nd Semester\Database\Project\Project\CompanyProject\CompanyProjectData\CompanyProjectData.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CompanyProjectData_log', FILENAME = N'E:\Studying\5th Year\2nd Semester\Database\Project\Project\CompanyProject\CompanyProjectData\CompanyProjectData_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CompanyProjectData] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CompanyProjectData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CompanyProjectData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CompanyProjectData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CompanyProjectData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CompanyProjectData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CompanyProjectData] SET ARITHABORT OFF 
GO
ALTER DATABASE [CompanyProjectData] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CompanyProjectData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CompanyProjectData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CompanyProjectData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CompanyProjectData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CompanyProjectData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CompanyProjectData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CompanyProjectData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CompanyProjectData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CompanyProjectData] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CompanyProjectData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CompanyProjectData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CompanyProjectData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CompanyProjectData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CompanyProjectData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CompanyProjectData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CompanyProjectData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CompanyProjectData] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CompanyProjectData] SET  MULTI_USER 
GO
ALTER DATABASE [CompanyProjectData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CompanyProjectData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CompanyProjectData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CompanyProjectData] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CompanyProjectData] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CompanyProjectData] SET QUERY_STORE = OFF
GO
USE [CompanyProjectData]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [CompanyProjectData]
GO
/****** Object:  Table [dbo].[Assets]    Script Date: 28-Apr-18 9:55:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assets](
	[A_Partid] [int] NOT NULL,
	[state] [nchar](10) NULL,
	[start date] [date] NULL,
	[color] [nvarchar](50) NULL,
	[A_Bnum] [int] NULL,
 CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED 
(
	[A_Partid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 28-Apr-18 9:55:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[Bnum] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[phone] [int] NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[Bnum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 28-Apr-18 9:55:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[CID] [int] NOT NULL,
	[password] [nvarchar](50) NULL,
	[phone] [int] NULL,
	[position] [nvarchar](50) NULL,
	[company] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[fax] [nchar](10) NULL,
	[evaluation] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[CID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 28-Apr-18 9:55:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Dnum] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
	[D_Mgr_EID] [int] NULL,
	[Mgr_start_date] [date] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Dnum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 28-Apr-18 9:55:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EID] [int] NOT NULL,
	[address] [nvarchar](50) NULL,
	[position] [nvarchar](50) NULL,
	[phone] [nchar](10) NULL,
	[evaluation] [nchar](10) NULL,
	[DOB] [date] NULL,
	[sex] [nchar](10) NULL,
	[Email] [nvarchar](50) NULL,
	[start_date] [date] NULL,
	[name] [nvarchar](50) NULL,
	[E_Dnum] [int] NULL,
	[E_D_start_date] [date] NULL,
	[E_Bnum] [int] NULL,
	[password] [int] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Part]    Script Date: 28-Apr-18 9:55:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Part](
	[PartID] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_Part] PRIMARY KEY CLUSTERED 
(
	[PartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 28-Apr-18 9:55:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[P_EID] [int] NOT NULL,
	[P_SID] [int] NOT NULL,
	[P_CID] [int] NOT NULL,
	[status] [nvarchar](50) NULL,
	[startdate] [date] NULL,
	[price] [float] NULL,
	[riskfactor] [float] NULL,
	[end_date] [date] NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[P_EID] ASC,
	[P_SID] ASC,
	[P_CID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 28-Apr-18 9:55:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[SID] [int] NOT NULL,
	[description] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
	[price] [float] NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 28-Apr-18 9:55:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[ST_partid] [int] NOT NULL,
	[ST_WID] [int] NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[ST_partid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppiers]    Script Date: 28-Apr-18 9:55:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppiers](
	[SupID] [int] NOT NULL,
	[phone] [int] NULL,
	[email] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[fax] [nchar](10) NULL,
	[evaluation] [nvarchar](50) NULL,
	[company] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
	[position] [nvarchar](50) NULL,
 CONSTRAINT [PK_Suppiers] PRIMARY KEY CLUSTERED 
(
	[SupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplies]    Script Date: 28-Apr-18 9:55:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplies](
	[S_partID] [int] NOT NULL,
	[Sup_PID] [int] NOT NULL,
	[date] [date] NULL,
	[qtm] [nchar](10) NULL,
 CONSTRAINT [PK_Supplies] PRIMARY KEY CLUSTERED 
(
	[S_partID] ASC,
	[Sup_PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tool]    Script Date: 28-Apr-18 9:55:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tool](
	[T_partid] [int] NOT NULL,
	[state] [nvarchar](50) NULL,
	[start_date] [date] NULL,
	[color] [nchar](10) NULL,
 CONSTRAINT [PK_Tool] PRIMARY KEY CLUSTERED 
(
	[T_partid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warehouse]    Script Date: 28-Apr-18 9:55:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouse](
	[WID] [int] NOT NULL,
	[address] [nvarchar](50) NULL,
	[quantuty] [int] NULL,
 CONSTRAINT [PK_Warehouse] PRIMARY KEY CLUSTERED 
(
	[WID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_Assets_Branch] FOREIGN KEY([A_Bnum])
REFERENCES [dbo].[Branch] ([Bnum])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_Assets_Branch]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_Assets_Part] FOREIGN KEY([A_Partid])
REFERENCES [dbo].[Part] ([PartID])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_Assets_Part]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Branch] FOREIGN KEY([E_Bnum])
REFERENCES [dbo].[Branch] ([Bnum])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Branch]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([E_Dnum])
REFERENCES [dbo].[Department] ([Dnum])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Clients] FOREIGN KEY([P_CID])
REFERENCES [dbo].[Clients] ([CID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Clients]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Employee] FOREIGN KEY([P_EID])
REFERENCES [dbo].[Employee] ([EID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Employee]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Services] FOREIGN KEY([P_SID])
REFERENCES [dbo].[Services] ([SID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Services]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Part] FOREIGN KEY([ST_partid])
REFERENCES [dbo].[Part] ([PartID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Part]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Warehouse] FOREIGN KEY([ST_WID])
REFERENCES [dbo].[Warehouse] ([WID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Warehouse]
GO
ALTER TABLE [dbo].[Suppiers]  WITH CHECK ADD  CONSTRAINT [FK_Suppiers_Part] FOREIGN KEY([SupID])
REFERENCES [dbo].[Part] ([PartID])
GO
ALTER TABLE [dbo].[Suppiers] CHECK CONSTRAINT [FK_Suppiers_Part]
GO
ALTER TABLE [dbo].[Supplies]  WITH CHECK ADD  CONSTRAINT [FK_Supplies_Part] FOREIGN KEY([S_partID])
REFERENCES [dbo].[Part] ([PartID])
GO
ALTER TABLE [dbo].[Supplies] CHECK CONSTRAINT [FK_Supplies_Part]
GO
ALTER TABLE [dbo].[Tool]  WITH CHECK ADD  CONSTRAINT [FK_Tool_Part] FOREIGN KEY([T_partid])
REFERENCES [dbo].[Part] ([PartID])
GO
ALTER TABLE [dbo].[Tool] CHECK CONSTRAINT [FK_Tool_Part]
GO
USE [master]
GO
ALTER DATABASE [CompanyProjectData] SET  READ_WRITE 
GO