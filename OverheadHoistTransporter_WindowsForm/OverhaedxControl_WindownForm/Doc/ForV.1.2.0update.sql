USE [OHTC_CSOT_T4]
GO
DROP TABLE [dbo].[UASFNC]
GO
DROP TABLE [dbo].[UASUFNC]
GO
/****** Object:  Table [dbo].[UASFNC]    Script Date: 2019/11/21 上午 11:46:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UASFNC](
	[FUNC_CODE] [char](60) NOT NULL,
	[FUNC_NAME] [char](80) NULL,
PRIMARY KEY CLUSTERED 
(
	[FUNC_CODE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UASUFNC]    Script Date: 2019/11/21 上午 11:46:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UASUFNC](
	[USER_GRP] [char](20) NOT NULL,
	[FUNC_CODE] [char](60) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[USER_GRP] ASC,
	[FUNC_CODE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[UASFNC] ([FUNC_CODE], [FUNC_NAME]) VALUES (N'FUNC_ACCOUNT_MANAGEMENT                                     ', N'User Account Management                                                         ')
GO
INSERT [dbo].[UASFNC] ([FUNC_CODE], [FUNC_NAME]) VALUES (N'FUNC_CLOSE_SYSTEM                                           ', N'System Shutdown                                                                 ')
GO
INSERT [dbo].[UASFNC] ([FUNC_CODE], [FUNC_NAME]) VALUES (N'FUNC_DEBUG                                                  ', N'Debug                                                                           ')
GO
INSERT [dbo].[UASFNC] ([FUNC_CODE], [FUNC_NAME]) VALUES (N'FUNC_LOGIN                                                  ', N'User Login                                                                      ')
GO
INSERT [dbo].[UASFNC] ([FUNC_CODE], [FUNC_NAME]) VALUES (N'FUNC_MTS_MTL_MAINTENANCE                                    ', N'MTS/MTL Maintenance                                                             ')
GO
INSERT [dbo].[UASFNC] ([FUNC_CODE], [FUNC_NAME]) VALUES (N'FUNC_PORT_MAINTENANCE                                       ', N'Port Maintenance                                                                ')
GO
INSERT [dbo].[UASFNC] ([FUNC_CODE], [FUNC_NAME]) VALUES (N'FUNC_SYSTEM_CONCROL_MODE                                    ', N'System Control                                                                  ')
GO
INSERT [dbo].[UASFNC] ([FUNC_CODE], [FUNC_NAME]) VALUES (N'FUNC_TRANSFER_MANAGEMENT                                    ', N'Transfer Management                                                             ')
GO
INSERT [dbo].[UASFNC] ([FUNC_CODE], [FUNC_NAME]) VALUES (N'FUNC_VEHICLE_MANAGEMENT                                     ', N'Vehicle Management                                                              ')
GO
INSERT [dbo].[UASFNC] ([FUNC_CODE], [FUNC_NAME]) VALUES (N'FUNC_ADVANCED_SETTINGS                                      ', N'Advanced Settings                                                               ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ADMIN               ', N'FUNC_ACCOUNT_MANAGEMENT                                     ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ADMIN               ', N'FUNC_CLOSE_SYSTEM                                           ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ADMIN               ', N'FUNC_DEBUG                                                  ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ADMIN               ', N'FUNC_LOGIN                                                  ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ADMIN               ', N'FUNC_MTS_MTL_MAINTENANCE                                    ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ADMIN               ', N'FUNC_PORT_MAINTENANCE                                       ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ADMIN               ', N'FUNC_SYSTEM_CONCROL_MODE                                    ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ADMIN               ', N'FUNC_TRANSFER_MANAGEMENT                                    ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ADMIN               ', N'FUNC_VEHICLE_MANAGEMENT                                     ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ENG                 ', N'FUNC_ACCOUNT_MANAGEMENT                                     ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ENG                 ', N'FUNC_CLOSE_SYSTEM                                           ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ENG                 ', N'FUNC_DEBUG                                                  ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ENG                 ', N'FUNC_LOGIN                                                  ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ENG                 ', N'FUNC_MTS_MTL_MAINTENANCE                                    ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ENG                 ', N'FUNC_PORT_MAINTENANCE                                       ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ENG                 ', N'FUNC_SYSTEM_CONCROL_MODE                                    ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ENG                 ', N'FUNC_TRANSFER_MANAGEMENT                                    ')
GO
INSERT [dbo].[UASUFNC] ([USER_GRP], [FUNC_CODE]) VALUES (N'ENG                 ', N'FUNC_VEHICLE_MANAGEMENT                                     ')
GO

update AADDRESS set [ZOOM_LV]=15