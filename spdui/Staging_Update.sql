-----------2010/8/21-----------------------
alter table DATA_SOURCE_CATEGORY add ACTIVE_FLAG bit null;
update DATA_SOURCE_CATEGORY set active_flag = 1;


CREATE TABLE [dbo].[DATA_SOURCE_CATEGORY_USER](
	[CATEGORY_ID] [int] NOT NULL,
	[USER_ID] [int] NOT NULL,
 CONSTRAINT [PK_DATA_SOURCE_CATEGORY_USER] PRIMARY KEY CLUSTERED 
(
	[CATEGORY_ID] ASC,
	[USER_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


-----------2010/8/28-----------------------
alter table DATA_SOURCE_UPLOAD add OWNER_CONFIRM_BY int;
alter table DATA_SOURCE_UPLOAD add OWNER_CONFIRM_DATE datetime;
alter table DATA_SOURCE_UPLOAD add ETL_CONFIRM_BY int;
alter table DATA_SOURCE_UPLOAD add ETL_CONFIRM_DATE datetime;


-----------2010/9/3-----------------------
USE [SPStaging]
GO
/****** 对象:  Table [dbo].[OffLine_Report_Validation_Rule]    脚本日期: 08/29/2010 13:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OffLine_Report_Validation_Rule](
	[Rule_Id] [int] IDENTITY(1,1) NOT NULL,
	[ReportBatch_ID] [int] NOT NULL,
	[Rule_Name] [nvarchar](200) NOT NULL,
	[Rule_Description] [nvarchar](200) NULL,
	[Seq_No] [int] NOT NULL,
	[Rule_Type] [nvarchar](20) NOT NULL,
	[Rule_Content] [nvarchar](4000) NOT NULL,
	[Update_Content] [nvarchar](4000) NULL,
	[Create_Date] [datetime] NOT NULL,
	[Create_User] [int] NOT NULL,
	[Last_Update_Date] [datetime] NULL,
	[Last_Update_User] [int] NULL,
	[Active_Flag] [int] NOT NULL CONSTRAINT [DF__OffLine_R__Activ__35FCF52C]  DEFAULT ((1)),
 CONSTRAINT [PK_OffLine_Report_Validation_Rule] PRIMARY KEY CLUSTERED 
(
	[Rule_Id] ASC
)
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OffLine_Report_Validation_Rule]  WITH CHECK ADD  CONSTRAINT [FK_OffLine_Report_Validation_Rule_OffLine_Report_Batch] FOREIGN KEY([ReportBatch_ID])
REFERENCES [dbo].[OffLine_Report_Batch] ([ReportBatch_ID])
GO
ALTER TABLE [dbo].[OffLine_Report_Validation_Rule] CHECK CONSTRAINT [FK_OffLine_Report_Validation_Rule_OffLine_Report_Batch]
GO
ALTER TABLE [dbo].[OffLine_Report_Validation_Rule]  WITH CHECK ADD  CONSTRAINT [FK_OffLine_Report_Validation_Rule_USERS_Create_User] FOREIGN KEY([Create_User])
REFERENCES [dbo].[USERS] ([USER_ID])
GO
ALTER TABLE [dbo].[OffLine_Report_Validation_Rule] CHECK CONSTRAINT [FK_OffLine_Report_Validation_Rule_USERS_Create_User]
GO
ALTER TABLE [dbo].[OffLine_Report_Validation_Rule]  WITH CHECK ADD  CONSTRAINT [FK_OffLine_Report_Validation_Rule_USERS_Last_Update_User] FOREIGN KEY([Last_Update_User])
REFERENCES [dbo].[USERS] ([USER_ID])
GO
ALTER TABLE [dbo].[OffLine_Report_Validation_Rule] CHECK CONSTRAINT [FK_OffLine_Report_Validation_Rule_USERS_Last_Update_User]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[OffLine_Report_Validation_Result]    脚本日期: 08/29/2010 13:37:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OffLine_Report_Validation_Result](
	[RecId] [int] IDENTITY(1,1) NOT NULL,
	[Job_Id] [numeric](18, 0) NOT NULL,
	[Rule_Id] [int] NOT NULL,
	[Status] [nvarchar](20) NULL,
	[Failed_Row_Count] [int] NULL,
	[Row_No_List] [nvarchar](4000) NULL,
 CONSTRAINT [PK_OffLine_Report_Validation_Result] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
)
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OffLine_Report_Validation_Result]  WITH CHECK ADD  CONSTRAINT [FK_OffLine_Report_Validation_Result_OffLine_Report_Job] FOREIGN KEY([Job_Id])
REFERENCES [dbo].[OffLine_Report_Job] ([Job_Id])
GO
ALTER TABLE [dbo].[OffLine_Report_Validation_Result] CHECK CONSTRAINT [FK_OffLine_Report_Validation_Result_OffLine_Report_Job]
GO
ALTER TABLE [dbo].[OffLine_Report_Validation_Result]  WITH CHECK ADD  CONSTRAINT [FK_OffLine_Report_Validation_Result_OffLine_Report_Validation_Rule] FOREIGN KEY([Rule_Id])
REFERENCES [dbo].[OffLine_Report_Validation_Rule] ([Rule_Id])
GO
ALTER TABLE [dbo].[OffLine_Report_Validation_Result] CHECK CONSTRAINT [FK_OffLine_Report_Validation_Result_OffLine_Report_Validation_Rule]
GO

alter table OffLine_Report_JobUser add Report_Create_Status varchar(100);
alter table OffLine_Report_JobUser add Report_Create_Date DateTime;
alter table OffLine_Report_JobUser add Report_Email_Status varchar(100);
alter table OffLine_Report_JobUser add Report_Email_Date DateTime;
alter table OffLine_Report_JobUser add Report_Portal_Status varchar(100);
alter table OffLine_Report_JobUser add Report_Portal_Date DateTime;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offline_Report_BatchUser](
	[BatchUser_ID] [int] IDENTITY(1,1) NOT NULL,
	[Batch_ID] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
 CONSTRAINT [PK_Offline_Report_BatchUser] PRIMARY KEY CLUSTERED 
(
	[BatchUser_ID] ASC
)
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Offline_Report_BatchUser]  WITH CHECK ADD  CONSTRAINT [FK_Offline_Report_BatchUser_OffLine_Report_Batch] FOREIGN KEY([Batch_ID])
REFERENCES [dbo].[OffLine_Report_Batch] ([ReportBatch_ID])
GO
ALTER TABLE [dbo].[Offline_Report_BatchUser] CHECK CONSTRAINT [FK_Offline_Report_BatchUser_OffLine_Report_Batch]
GO
ALTER TABLE [dbo].[Offline_Report_BatchUser]  WITH CHECK ADD  CONSTRAINT [FK_Offline_Report_BatchUser_USERS] FOREIGN KEY([User_Id])
REFERENCES [dbo].[USERS] ([USER_ID])
GO
ALTER TABLE [dbo].[Offline_Report_BatchUser] CHECK CONSTRAINT [FK_Offline_Report_BatchUser_USERS]
GO