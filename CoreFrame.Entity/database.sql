USE [master]
GO
/****** Object:  Database [CoreFrame.Admin]    Script Date: 11/21/2018 11:09:15 ******/
CREATE DATABASE [CoreFrame.Admin] ON  PRIMARY 
( NAME = N'CoreFrame.Admin', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\CoreFrame.Admin.ndf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CoreFrame.Admin_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\CoreFrame.Admin.ldf' , SIZE = 1792KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CoreFrame.Admin] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CoreFrame.Admin].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CoreFrame.Admin] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET ANSI_NULLS OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET ANSI_PADDING OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET ARITHABORT OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [CoreFrame.Admin] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [CoreFrame.Admin] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [CoreFrame.Admin] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET  DISABLE_BROKER
GO
ALTER DATABASE [CoreFrame.Admin] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [CoreFrame.Admin] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [CoreFrame.Admin] SET  READ_WRITE
GO
ALTER DATABASE [CoreFrame.Admin] SET RECOVERY FULL
GO
ALTER DATABASE [CoreFrame.Admin] SET  MULTI_USER
GO
ALTER DATABASE [CoreFrame.Admin] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [CoreFrame.Admin] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'CoreFrame.Admin', N'ON'
GO
USE [CoreFrame.Admin]
GO
/****** Object:  Table [dbo].[Dev_Project]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dev_Project](
	[Id] [varchar](50) NOT NULL,
	[ProjectId] [varchar](50) NULL,
	[ProjectName] [varchar](255) NULL,
 CONSTRAINT [PK__Dev_Proj__3214EC072B3F6F97] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自然主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dev_Project', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dev_Project', @level2type=N'COLUMN',@level2name=N'ProjectId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dev_Project', @level2type=N'COLUMN',@level2name=N'ProjectName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dev_Project'
GO
INSERT [dbo].[Dev_Project] ([Id], [ProjectId], [ProjectName]) VALUES (N'1', N'1', N'无人驾驶')
/****** Object:  Table [dbo].[Base_UserRoleMap]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Base_UserRoleMap](
	[Id] [varchar](50) NOT NULL,
	[UserId] [varchar](50) NULL,
	[RoleId] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Base_UserRoleMap] ([Id], [UserId], [RoleId]) VALUES (N'039E7867D8D17-DDF7060D-10BA-4641-8605-24FB4DE4F446', N'Admin', N'039e3de2a3a7c-b3a0d714-922a-4cb7-a412-69eefd4d3ac7')
INSERT [dbo].[Base_UserRoleMap] ([Id], [UserId], [RoleId]) VALUES (N'039E9BDDCCB55-DB6299A3-7E47-4F24-87B7-5003A5543520', N'039e3e3891eea-a67a61c4-adae-49fb-9fa3-50f3d54eabc3', N'039e9b41c04fd-215eac36-aa67-4f11-87f7-2c6957ce0440')
/****** Object:  Table [dbo].[Base_User]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Base_User](
	[Id] [varchar](50) NOT NULL,
	[UserId] [varchar](50) NULL,
	[UserName] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[RealName] [varchar](50) NULL,
	[Sex] [int] NULL,
	[Birthday] [date] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE UNIQUE CLUSTERED INDEX [UserId] ON [dbo].[Base_User] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = ON, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_User', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户Id,逻辑主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_User', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_User', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_User', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真实姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_User', @level2type=N'COLUMN',@level2name=N'RealName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别(1为男，0为女)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_User', @level2type=N'COLUMN',@level2name=N'Sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出生日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_User', @level2type=N'COLUMN',@level2name=N'Birthday'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统，用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_User'
GO
INSERT [dbo].[Base_User] ([Id], [UserId], [UserName], [Password], [RealName], [Sex], [Birthday]) VALUES (N'039e3e3891eea-1e885eb2-75a3-4f82-bb9a-570d717f2af4', N'039e3e3891eea-a67a61c4-adae-49fb-9fa3-50f3d54eabc3', N'llj', N'e10adc3949ba59abbe56e057f20f883e', N'小明', 1, CAST(0xB73D0B00 AS Date))
INSERT [dbo].[Base_User] ([Id], [UserId], [UserName], [Password], [RealName], [Sex], [Birthday]) VALUES (N'039e40811175f-af4ebd3b-4908-4f9e-8075-31859b8aede0', N'039e40811175f-dbed6ef5-f06a-4512-9b40-ecfa2c7925f9', N'xiaohua', N'e10adc3949ba59abbe56e057f20f883e', N'小花', 0, CAST(0xC33D0B00 AS Date))
INSERT [dbo].[Base_User] ([Id], [UserId], [UserName], [Password], [RealName], [Sex], [Birthday]) VALUES (N'2e9a9876-469e-42b3-b631-48e866723eeb', N'Admin', N'Admin', N'e10adc3949ba59abbe56e057f20f883e', N'超级管理员', 1, CAST(0xA53D0B00 AS Date))
/****** Object:  Table [dbo].[Base_UnitTest]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Base_UnitTest](
	[Id] [varchar](50) NOT NULL,
	[UserId] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[Age] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_UnitTest', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单元测试表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_UnitTest'
GO
INSERT [dbo].[Base_UnitTest] ([Id], [UserId], [UserName], [Age]) VALUES (N'10', NULL, NULL, NULL)
INSERT [dbo].[Base_UnitTest] ([Id], [UserId], [UserName], [Age]) VALUES (N'143b8b04-d389-43b3-b606-2c211c4d5886', N'1', N'a821dc69-660e-4ccc-bb5f-1a017d5ba04a', NULL)
INSERT [dbo].[Base_UnitTest] ([Id], [UserId], [UserName], [Age]) VALUES (N'6ff20fd0-675c-436a-aa4f-ef4964fcf191', N'2', N'cd099bdc-95cc-445b-9a01-5f1c97b65721', NULL)
/****** Object:  Table [dbo].[Base_SysRole]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Base_SysRole](
	[Id] [varchar](50) NOT NULL,
	[RoleId] [varchar](50) NULL,
	[RoleName] [nvarchar](50) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE CLUSTERED INDEX [RoleId] ON [dbo].[Base_SysRole] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysRole', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'逻辑主键，角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysRole', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysRole', @level2type=N'COLUMN',@level2name=N'RoleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysRole'
GO
INSERT [dbo].[Base_SysRole] ([Id], [RoleId], [RoleName]) VALUES (N'039e3de2a3a7c-e35bc1a8-7e42-44a2-9881-7d1d6768ab72', N'039e3de2a3a7c-b3a0d714-922a-4cb7-a412-69eefd4d3ac7', N'超级管理员')
INSERT [dbo].[Base_SysRole] ([Id], [RoleId], [RoleName]) VALUES (N'039e4630ba5a1-31320968-d1dd-4275-ae71-a06da0731a2b', N'039e4630ba5a1-72a1d684-e3e8-4391-b38a-ad393e85b5b8', N'管理员')
INSERT [dbo].[Base_SysRole] ([Id], [RoleId], [RoleName]) VALUES (N'039e9b41c04fc-c4ff2bd4-e086-47b6-b1b4-b93d84669f84', N'039e9b41c04fd-215eac36-aa67-4f11-87f7-2c6957ce0440', N' 教师')
/****** Object:  Table [dbo].[Base_SysLog]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Base_SysLog](
	[Id] [varchar](50) NOT NULL,
	[LogType] [varchar](255) NULL,
	[LogContent] [varchar](max) NULL,
	[OpUserName] [varchar](255) NULL,
	[OpTime] [datetime] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE CLUSTERED INDEX [OpTime] ON [dbo].[Base_SysLog] 
(
	[OpTime] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [LogType] ON [dbo].[Base_SysLog] 
(
	[LogType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog', @level2type=N'COLUMN',@level2name=N'LogType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog', @level2type=N'COLUMN',@level2name=N'LogContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog', @level2type=N'COLUMN',@level2name=N'OpUserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志记录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog', @level2type=N'COLUMN',@level2name=N'OpTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog'
GO
/****** Object:  Table [dbo].[Base_PermissionUser]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Base_PermissionUser](
	[Id] [varchar](50) NOT NULL,
	[UserId] [varchar](50) NULL,
	[PermissionValue] [varchar](50) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE CLUSTERED INDEX [UserId] ON [dbo].[Base_PermissionUser] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionUser', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户主键Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionUser', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionUser', @level2type=N'COLUMN',@level2name=N'PermissionValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户权限表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionUser'
GO
INSERT [dbo].[Base_PermissionUser] ([Id], [UserId], [PermissionValue]) VALUES (N'039e9b3fbcd24-1e70ba12-9b64-4bb2-8825-ff2b44cc29e3', N'039e40811175f-dbed6ef5-f06a-4512-9b40-ecfa2c7925f9', N'sysuser.search')
INSERT [dbo].[Base_PermissionUser] ([Id], [UserId], [PermissionValue]) VALUES (N'039e9b3fbcd26-855f7b7a-da8b-4d1b-8264-f7b5b31b8154', N'039e40811175f-dbed6ef5-f06a-4512-9b40-ecfa2c7925f9', N'sysrole.search')
/****** Object:  Table [dbo].[Base_PermissionRole]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Base_PermissionRole](
	[Id] [varchar](50) NOT NULL,
	[RoleId] [varchar](50) NULL,
	[PermissionValue] [varchar](50) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE CLUSTERED INDEX [RoleId] ON [dbo].[Base_PermissionRole] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionRole', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色主键Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionRole', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionRole', @level2type=N'COLUMN',@level2name=N'PermissionValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色权限表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionRole'
GO
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9d340dbe9-c5789c86-a0a0-4409-a547-0cd546dac932', N'039e3de2a3a7c-b3a0d714-922a-4cb7-a412-69eefd4d3ac7', N'sysrole.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9d340dbea-a174bf3f-da9a-46c8-8553-a67cd13f5bd3', N'039e3de2a3a7c-b3a0d714-922a-4cb7-a412-69eefd4d3ac7', N'sysrole.manage')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9d340dbea-340325a5-06db-4832-9109-25e6aeb8a641', N'039e3de2a3a7c-b3a0d714-922a-4cb7-a412-69eefd4d3ac7', N'sysrole.manageSysPermission')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9d340dbea-4072f24a-e11b-4188-82c2-fb5371de76b3', N'039e3de2a3a7c-b3a0d714-922a-4cb7-a412-69eefd4d3ac7', N'appsecret.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9d340dbea-2169852d-8c9d-4920-92b6-3998a75fe554', N'039e3de2a3a7c-b3a0d714-922a-4cb7-a412-69eefd4d3ac7', N'appsecret.manage')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9d340dbea-6512261a-618a-441b-b642-c4b7325f8b59', N'039e3de2a3a7c-b3a0d714-922a-4cb7-a412-69eefd4d3ac7', N'sysLog.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e4082682ad-73a3c09d-02bc-4976-9261-bc54d503d4b3', N'039e3de2a5724-492ec4b1-848f-4214-8df5-05ddeffa3e3e', N'sysrole.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e4082682ad-b69b58c3-ece5-4798-9af4-67d87f6fec57', N'039e3de2a5724-492ec4b1-848f-4214-8df5-05ddeffa3e3e', N'sysuser.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e40813d768-8f112ee0-ab06-40de-bea6-84d08585929a', N'039e3de2a728b-95a8888c-5b3c-42e7-a983-379e7f0cefc4', N'sysrole.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e40813d768-c302de76-116c-49ec-881d-64c21ef23f25', N'039e3de2a728b-95a8888c-5b3c-42e7-a983-379e7f0cefc4', N'sysuser.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e4116b5bdc-19705d4a-67b0-4653-b8d3-01b33ea95dfd', N'039e4116b3565-6ba14a24-ac04-4c42-bd7f-37205a6687d8', N'sysuser.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e4116b5bdd-1744d3c5-d8a9-4ca8-b674-8a29b63f3579', N'039e4116b3565-6ba14a24-ac04-4c42-bd7f-37205a6687d8', N'appsecret.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e4116b5bdd-859268b8-7d41-47f7-b4a0-9c1723fdd8b4', N'039e4116b3565-6ba14a24-ac04-4c42-bd7f-37205a6687d8', N'sysrole.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e4116b5bdd-a1bc3b7c-2e70-4bb6-880e-78ba1ff96ef0', N'039e4116b3565-6ba14a24-ac04-4c42-bd7f-37205a6687d8', N'buildcode.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e4116b5bdd-bd389b48-8646-44cb-aaf6-1ad63e290b31', N'039e4116b3565-6ba14a24-ac04-4c42-bd7f-37205a6687d8', N'databaseLink.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e4116b7175-0a86c2f0-2814-4e7d-8168-5a4a4eb045ab', N'039e4116b4700-0f37d7f4-41b5-4a17-b0fb-67654ff44185', N'sysuser.manage')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e4116b7175-2f158fb9-8231-422a-9aac-58071e7f8634', N'039e4116b4700-0f37d7f4-41b5-4a17-b0fb-67654ff44185', N'sysrole.manage')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e4116b7175-fdbcad8b-fbbb-4414-9df5-b63fdf00f84e', N'039e4116b4700-0f37d7f4-41b5-4a17-b0fb-67654ff44185', N'appsecret.manage')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9bdc364cf-87cb7f78-4ba6-4803-b570-2b490ee92a47', N'039e4630ba5a1-72a1d684-e3e8-4391-b38a-ad393e85b5b8', N'sysuser.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9bdc364cf-418a932a-85c7-4578-85db-b3ec5e140de1', N'039e4630ba5a1-72a1d684-e3e8-4391-b38a-ad393e85b5b8', N'sysuser.manage')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9bdc364cf-26c5e86c-ed54-4218-bbe4-950191d64cb4', N'039e4630ba5a1-72a1d684-e3e8-4391-b38a-ad393e85b5b8', N'sysrole.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9bdc364cf-0428e032-df46-4755-924a-35fe2b4540d1', N'039e4630ba5a1-72a1d684-e3e8-4391-b38a-ad393e85b5b8', N'sysrole.manage')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9bdc364cf-c5fd7128-9a43-4b6b-bf54-f245eade134e', N'039e4630ba5a1-72a1d684-e3e8-4391-b38a-ad393e85b5b8', N'appsecret.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9bdc364cf-77b68db8-1218-4af6-8b39-0224ef11cc1d', N'039e4630ba5a1-72a1d684-e3e8-4391-b38a-ad393e85b5b8', N'appsecret.manage')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9bdc364cf-997ee3f3-226f-49e2-86fb-320c47f2ea81', N'039e4630ba5a1-72a1d684-e3e8-4391-b38a-ad393e85b5b8', N'sysLog.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9bdc364cf-4898daaf-1e54-4dcc-a05d-c884a2d5c50f', N'039e4630ba5a1-72a1d684-e3e8-4391-b38a-ad393e85b5b8', N'sysProject.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9bdc364cf-5fef9edf-26fb-4fe9-9f05-417cf85c7023', N'039e4630ba5a1-72a1d684-e3e8-4391-b38a-ad393e85b5b8', N'sysProject.manage')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9e73d5fd6-1072af57-05a1-4ac5-b535-7cdc40a52fb5', N'039e9b41c04fd-215eac36-aa67-4f11-87f7-2c6957ce0440', N'sysuser.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9e73d5fd8-d5df656c-5b96-46e5-8529-ec3a73a8b0ab', N'039e9b41c04fd-215eac36-aa67-4f11-87f7-2c6957ce0440', N'sysrole.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9e73d5fd8-dfe87dfc-29f0-4d70-87c3-f643ece19934', N'039e9b41c04fd-215eac36-aa67-4f11-87f7-2c6957ce0440', N'appsecret.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9e73d5fd8-10dfa2b2-3370-4c3b-bd35-523a71ea5f25', N'039e9b41c04fd-215eac36-aa67-4f11-87f7-2c6957ce0440', N'sysLog.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9e73d5fd8-a3bf09dc-9bd8-4e37-a553-2176716a86da', N'039e9b41c04fd-215eac36-aa67-4f11-87f7-2c6957ce0440', N'Pxproject.search')
INSERT [dbo].[Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'039e9e73d5fd8-7085c7a2-17c9-42ca-8ea4-89e2111fff4b', N'039e9b41c04fd-215eac36-aa67-4f11-87f7-2c6957ce0440', N'Pxproject.manage')
/****** Object:  Table [dbo].[Base_PermissionAppId]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Base_PermissionAppId](
	[Id] [varchar](50) NOT NULL,
	[AppId] [varchar](50) NULL,
	[PermissionValue] [varchar](50) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE CLUSTERED INDEX [RoleId] ON [dbo].[Base_PermissionAppId] 
(
	[AppId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionAppId', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AppId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionAppId', @level2type=N'COLUMN',@level2name=N'AppId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionAppId', @level2type=N'COLUMN',@level2name=N'PermissionValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AppId权限表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_PermissionAppId'
GO
INSERT [dbo].[Base_PermissionAppId] ([Id], [AppId], [PermissionValue]) VALUES (N'039e4082caa0a-4b9fb0cf-05f3-4287-a5b4-c8004a21627c', N'AppTest', N'sysuser.manage')
INSERT [dbo].[Base_PermissionAppId] ([Id], [AppId], [PermissionValue]) VALUES (N'039e4082caa0a-61c9c1c6-9f27-48a1-b002-ad69eece2fba', N'AppTest', N'sysrole.search')
INSERT [dbo].[Base_PermissionAppId] ([Id], [AppId], [PermissionValue]) VALUES (N'039e4082caa0a-a13df201-5f8b-4178-8823-72709c75d30d', N'AppTest', N'sysuser.search')
INSERT [dbo].[Base_PermissionAppId] ([Id], [AppId], [PermissionValue]) VALUES (N'039e4082caa0a-c10ea62d-290d-4a74-bb0d-74d4891b5fb9', N'AppTest', N'sysrole.manage')
/****** Object:  Table [dbo].[Base_Menu]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Base_Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentMenuId] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[MenuLevel] [varchar](50) NULL,
	[Icon] [varchar](50) NULL,
	[Url] [varchar](500) NULL,
	[Permission] [varchar](50) NULL,
	[IsShow] [bit] NULL,
	[ModuleValue] [varchar](50) NULL,
	[IsShowSearchPermission] [bit] NULL,
	[IsShowManagePermission] [bit] NULL,
 CONSTRAINT [PK_Base_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级菜单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_Menu', @level2type=N'COLUMN',@level2name=N'ParentMenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_Menu', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单级别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_Menu', @level2type=N'COLUMN',@level2name=N'MenuLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_Menu', @level2type=N'COLUMN',@level2name=N'Icon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_Menu', @level2type=N'COLUMN',@level2name=N'Url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_Menu', @level2type=N'COLUMN',@level2name=N'Permission'
GO
/****** Object:  Table [dbo].[Base_DatabaseLink]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Base_DatabaseLink](
	[Id] [varchar](50) NOT NULL,
	[LinkName] [varchar](50) NULL,
	[ConnectionStr] [varchar](1000) NULL,
	[DbType] [varchar](50) NULL,
	[SortNum] [varchar](50) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE CLUSTERED INDEX [LinkName] ON [dbo].[Base_DatabaseLink] 
(
	[LinkName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_DatabaseLink', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'连接名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_DatabaseLink', @level2type=N'COLUMN',@level2name=N'LinkName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'连接字符串' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_DatabaseLink', @level2type=N'COLUMN',@level2name=N'ConnectionStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_DatabaseLink', @level2type=N'COLUMN',@level2name=N'DbType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_DatabaseLink', @level2type=N'COLUMN',@level2name=N'SortNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库连接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_DatabaseLink'
GO
INSERT [dbo].[Base_DatabaseLink] ([Id], [LinkName], [ConnectionStr], [DbType], [SortNum]) VALUES (N'039e9b4015f7a-40657a15-29ca-4ddf-8e34-f90dab8654c3', N'BaseDb', N'Data Source=.;Initial Catalog=CoreFrame.Admin;Integrated Security=True', N'SqlServer', N'1')
/****** Object:  Table [dbo].[Base_AppSecret]    Script Date: 11/21/2018 11:09:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Base_AppSecret](
	[Id] [varchar](50) NOT NULL,
	[AppId] [varchar](50) NULL,
	[AppSecret] [varchar](50) NULL,
	[AppName] [varchar](255) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE CLUSTERED INDEX [AppId] ON [dbo].[Base_AppSecret] 
(
	[AppId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_AppSecret', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_AppSecret', @level2type=N'COLUMN',@level2name=N'AppId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用密钥' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_AppSecret', @level2type=N'COLUMN',@level2name=N'AppSecret'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_AppSecret', @level2type=N'COLUMN',@level2name=N'AppName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用密钥' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_AppSecret'
GO
INSERT [dbo].[Base_AppSecret] ([Id], [AppId], [AppSecret], [AppName]) VALUES (N'039e41170bc72-b89139b1-f3f4-430e-aed7-36b193d256dc', N'AppAdmin', N'7344a9c5-4f8c-4725-bde5-3fb99716f457', N'超级权限')
/****** Object:  Default [DF_Base_Menu_IsShow]    Script Date: 11/21/2018 11:09:16 ******/
ALTER TABLE [dbo].[Base_Menu] ADD  CONSTRAINT [DF_Base_Menu_IsShow]  DEFAULT ((1)) FOR [IsShow]
GO
/****** Object:  Default [DF_Base_Menu_SearchPermission]    Script Date: 11/21/2018 11:09:16 ******/
ALTER TABLE [dbo].[Base_Menu] ADD  CONSTRAINT [DF_Base_Menu_SearchPermission]  DEFAULT ((0)) FOR [IsShowSearchPermission]
GO
/****** Object:  Default [DF_Base_Menu_ManagePermission]    Script Date: 11/21/2018 11:09:16 ******/
ALTER TABLE [dbo].[Base_Menu] ADD  CONSTRAINT [DF_Base_Menu_ManagePermission]  DEFAULT ((0)) FOR [IsShowManagePermission]
GO
