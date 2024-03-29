USE [master]
GO
/****** Object:  Database [CompanyProjectData2]    Script Date: 5/7/2018 4:11:20 AM ******/
CREATE DATABASE [CompanyProjectData2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CompanyProjectData2', FILENAME = N'E:\Studying\5th Year\2nd Semester\Database\Project\Project\CompanyProject\CompanyProjectData2\CompanyProjectData2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CompanyProjectData2_log', FILENAME = N'E:\Studying\5th Year\2nd Semester\Database\Project\Project\CompanyProject\CompanyProjectData2\CompanyProjectData2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CompanyProjectData2] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CompanyProjectData2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CompanyProjectData2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET ARITHABORT OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CompanyProjectData2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CompanyProjectData2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CompanyProjectData2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CompanyProjectData2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CompanyProjectData2] SET  MULTI_USER 
GO
ALTER DATABASE [CompanyProjectData2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CompanyProjectData2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CompanyProjectData2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CompanyProjectData2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CompanyProjectData2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CompanyProjectData2] SET QUERY_STORE = OFF
GO
USE [CompanyProjectData2]
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
USE [CompanyProjectData2]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 5/7/2018 4:11:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[ST_partid] [int] NOT NULL,
	[ST_WID] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[ST_partid] ASC,
	[ST_WID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 5/7/2018 4:11:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupID] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[company] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[phone] [nchar](20) NOT NULL,
	[fax] [nchar](10) NULL,
	[address] [nvarchar](50) NULL,
	[position] [nvarchar](50) NULL,
	[evaluation] [nvarchar](50) NULL,
 CONSTRAINT [PK_Suppiers] PRIMARY KEY CLUSTERED 
(
	[SupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Part]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Part](
	[PartID] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Part] PRIMARY KEY CLUSTERED 
(
	[PartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supply]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supply](
	[S_partID] [int] NOT NULL,
	[Sup_PID] [int] NOT NULL,
	[date] [date] NULL,
	[qtm] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Supplies] PRIMARY KEY CLUSTERED 
(
	[S_partID] ASC,
	[Sup_PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PartSupplies]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PartSupplies]
AS
SELECT        dbo.Part.PartID, dbo.Part.name AS PartName, derivedtbl_1.total_quantity AS AvailableQuantity, dbo.Supply.date AS SupplyDate, dbo.Supply.qtm AS SupplyQuantity, dbo.Supplier.SupID AS SupplierID, 
                         dbo.Supplier.name AS SupplierName, dbo.Supplier.company AS SupplierCompany
FROM            dbo.Supplier RIGHT OUTER JOIN
                         dbo.Supply ON dbo.Supplier.SupID = dbo.Supply.Sup_PID INNER JOIN
                         dbo.Part ON dbo.Supply.S_partID = dbo.Part.PartID AND dbo.Supply.S_partID = dbo.Part.PartID LEFT OUTER JOIN
                             (SELECT        ST_partid, SUM(quantity) AS total_quantity
                               FROM            dbo.Stock
                               GROUP BY ST_partid) AS derivedtbl_1 ON dbo.Part.PartID = derivedtbl_1.ST_partid
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](20) NOT NULL,
	[EID] [bigint] NULL,
	[CID] [int] NULL,
	[role] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Asset]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asset](
	[A_Partid] [int] NOT NULL,
	[A_Bnum] [int] NULL,
	[state] [nchar](10) NULL,
	[start_date] [date] NULL,
	[color] [nvarchar](50) NULL,
 CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED 
(
	[A_Partid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[Bnum] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[phone] [nchar](20) NOT NULL,
	[address] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[Bnum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[CID] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](50) NOT NULL,
	[last_name] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NULL,
	[phone] [nchar](20) NULL,
	[fax] [nchar](10) NULL,
	[sex] [nchar](1) NULL,
	[address] [nvarchar](50) NULL,
	[company] [nvarchar](50) NULL,
	[position] [nvarchar](50) NULL,
	[evaluation] [nvarchar](50) NULL,
	[purchases] [float] NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[CID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Dnum] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[D_Mgr_EID] [int] NULL,
	[Mgr_start_date] [date] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Dnum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EID] [bigint] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](50) NOT NULL,
	[last_name] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[phone] [varchar](20) NOT NULL,
	[position] [nvarchar](50) NOT NULL,
	[salary] [int] NOT NULL,
	[sex] [nchar](1) NULL,
	[DOB] [date] NULL,
	[address] [nvarchar](50) NULL,
	[Super_EID] [bigint] NULL,
	[evaluation] [nchar](10) NULL,
	[start_date] [date] NULL,
	[E_Bnum] [int] NULL,
	[E_Dnum] [int] NULL,
	[E_D_start_date] [date] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[P_EID] [bigint] NOT NULL,
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
/****** Object:  Table [dbo].[Service]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[SID] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tool]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tool](
	[T_partID] [int] NOT NULL,
	[state] [nvarchar](50) NULL,
	[start_date] [date] NULL,
	[color] [nchar](10) NULL,
 CONSTRAINT [PK_Tool] PRIMARY KEY CLUSTERED 
(
	[T_partID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warehouse]    Script Date: 5/7/2018 4:11:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouse](
	[WID] [int] NOT NULL,
	[address] [nvarchar](50) NULL,
 CONSTRAINT [PK_Warehouse] PRIMARY KEY CLUSTERED 
(
	[WID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [FK_AppUser_Client] FOREIGN KEY([CID])
REFERENCES [dbo].[Client] ([CID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [FK_AppUser_Client]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [FK_AppUser_Employee] FOREIGN KEY([EID])
REFERENCES [dbo].[Employee] ([EID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [FK_AppUser_Employee]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_Branch] FOREIGN KEY([A_Bnum])
REFERENCES [dbo].[Branch] ([Bnum])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_Branch]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_Part] FOREIGN KEY([A_Partid])
REFERENCES [dbo].[Part] ([PartID])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_Part]
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
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Client] FOREIGN KEY([P_CID])
REFERENCES [dbo].[Client] ([CID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Client]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Employee] FOREIGN KEY([P_EID])
REFERENCES [dbo].[Employee] ([EID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Employee]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Service] FOREIGN KEY([P_SID])
REFERENCES [dbo].[Service] ([SID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Service]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Part] FOREIGN KEY([ST_partid])
REFERENCES [dbo].[Part] ([PartID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Part]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Warehouse] FOREIGN KEY([ST_WID])
REFERENCES [dbo].[Warehouse] ([WID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Warehouse]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Part] FOREIGN KEY([SupID])
REFERENCES [dbo].[Part] ([PartID])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Part]
GO
ALTER TABLE [dbo].[Supply]  WITH CHECK ADD  CONSTRAINT [FK_Supply_Part] FOREIGN KEY([S_partID])
REFERENCES [dbo].[Part] ([PartID])
GO
ALTER TABLE [dbo].[Supply] CHECK CONSTRAINT [FK_Supply_Part]
GO
ALTER TABLE [dbo].[Tool]  WITH CHECK ADD  CONSTRAINT [FK_Tool_Part] FOREIGN KEY([T_partID])
REFERENCES [dbo].[Part] ([PartID])
GO
ALTER TABLE [dbo].[Tool] CHECK CONSTRAINT [FK_Tool_Part]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [CID_OR_EID] CHECK  (([CID] IS NULL AND [EID] IS NOT NULL OR [CID] IS NOT NULL AND [EID] IS NULL))
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [CID_OR_EID]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [CK_AppUser_role] CHECK  ((([role]='admin' OR [role]='employee' OR [role]='client') AND ([role]='admin' AND [EID] IS NOT NULL OR [role]='employee' AND [EID] IS NOT NULL OR [role]='client' AND [CID] IS NOT NULL)))
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [CK_AppUser_role]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [Pass_less_than_8] CHECK  ((len([password])>=(8)))
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [Pass_less_than_8]
GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD  CONSTRAINT [CK_Branch_phone] CHECK  ((concat('',CONVERT([bigint],[phone])*(1))=CONVERT([bigint],[phone])*(1)))
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [CK_Branch_phone]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [CK_Client_phone] CHECK  ((concat('',CONVERT([bigint],[phone])*(1))=CONVERT([bigint],[phone])*(1)))
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [CK_Client_phone]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [CK_Client_sex] CHECK  (([sex]='M' OR [sex]='F'))
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [CK_Client_sex]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [CK_Employee_phone] CHECK  ((concat('',CONVERT([bigint],[phone])*(1))=CONVERT([bigint],[phone])*(1)))
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [CK_Employee_phone]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [CK_Employee_sex] CHECK  (([sex]='M' OR [sex]='F'))
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [CK_Employee_sex]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [CK_Supplier_phone] CHECK  ((concat('',CONVERT([bigint],[phone])*(1))=CONVERT([bigint],[phone])*(1)))
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [CK_Supplier_phone]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Supplier"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Supply"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Part"
            Begin Extent = 
               Top = 6
               Left = 662
               Bottom = 102
               Right = 832
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "derivedtbl_1"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 102
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PartSupplies'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PartSupplies'
GO
USE [master]
GO
ALTER DATABASE [CompanyProjectData2] SET  READ_WRITE 
GO
