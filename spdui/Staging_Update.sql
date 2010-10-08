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
GO

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

-----------2010/9/12-----------------------
alter table DATA_SOURCE_UPLOAD add WithDraw_BY int;
alter table DATA_SOURCE_UPLOAD add WithDraw_DATE datetime;
alter table DATA_SOURCE_UPLOAD add RowDel_BY int;
alter table DATA_SOURCE_UPLOAD add RowDel_DATE datetime;
alter table OffLine_Report_Job add Errors int;
alter table OffLine_Report_Job add Problems int;
alter table OffLine_Report_Job add Warnings int;
alter table OffLine_Report_Job add Validate_Status varchar(20);

alter table DATA_SOURCE_RULE add RULE_RESULT_CONTENT nvarchar(max);
GO
update DATA_SOURCE_RULE set RULE_RESULT_CONTENT = RULE_CONTENT
GO
alter table DATA_SOURCE_RULE alter column RULE_RESULT_CONTENT nvarchar(max) not null
GO
alter table Cube_Validation_Rule add RULE_RESULT_CONTENT nvarchar(max)
GO
update Cube_Validation_Rule set RULE_RESULT_CONTENT = RULE_CONTENT
GO
alter table Cube_Validation_Rule alter column RULE_RESULT_CONTENT nvarchar(max) not null
GO
alter table OffLine_Report_Validation_Rule add RULE_RESULT_CONTENT nvarchar(max)
GO
update OffLine_Report_Validation_Rule set RULE_RESULT_CONTENT = RULE_CONTENT
GO
alter table OffLine_Report_Validation_Rule alter column RULE_RESULT_CONTENT nvarchar(max) not null
GO



-----------2010/9/16-----------------------
alter table DATA_SOURCE add DW_QUERY_SQL nvarchar(max);
alter table DATA_SOURCE_RULE add DEPENDENCE_RULE int;
GO
ALTER TABLE [dbo].[DATA_SOURCE_RULE]  WITH CHECK ADD  CONSTRAINT [FK_DATA_SOURCE_RULE_DATA_SOURCE_RULE] FOREIGN KEY([DEPENDENCE_RULE])
REFERENCES [dbo].[DATA_SOURCE_RULE] ([RULE_ID])
GO
alter table Cube_Validation_Rule add DEPENDENCE_RULE int;
GO
ALTER TABLE [dbo].[Cube_Validation_Rule]  WITH CHECK ADD  CONSTRAINT [FK_Cube_Validation_Rule_Cube_Validation_Rule] FOREIGN KEY([DEPENDENCE_RULE])
REFERENCES [dbo].[Cube_Validation_Rule] ([Rule_Id])
GO
alter table OffLine_Report_Validation_Rule add DEPENDENCE_RULE int;
GO
ALTER TABLE [dbo].[OffLine_Report_Validation_Rule]  WITH CHECK ADD  CONSTRAINT [FK_OffLine_Report_Validation_Rule_OffLine_Report_Validation_Rule] FOREIGN KEY([DEPENDENCE_RULE])
REFERENCES [dbo].[OffLine_Report_Validation_Rule] ([Rule_Id])
GO



-----------2010/10/2-----------------------
alter table DATA_SOURCE add DATA_STRUCTURE_DESC nvarchar(255);

set identity_insert MODULES on;
insert into MODULES(MODULE_ID, MODULE_NAME, SOURCE_FILE, DESCRIPTION) values(23, 'Data Source Authorization', 'Modules/Dui/DSAuthorization/Main.ascx', 'The data source authorization.')
insert into MODULES(MODULE_ID, MODULE_NAME, SOURCE_FILE, DESCRIPTION) values(24, 'DW Data Source Authorization', 'Modules/Dui/DWDSAuthorization/Main.ascx', 'The DW data source authorization.')
set identity_insert MODULES off;

set identity_insert MENUS on;
insert into MENUS(MENU_ID, PARENT_MENU_ID, TITLE, PATH_CODE, DESCRIPTION, MODULE_ID) values (25, 5, 'Data Source Auth.', '08.05', 'The data source authorization.', 23)
insert into MENUS(MENU_ID, PARENT_MENU_ID, TITLE, PATH_CODE, DESCRIPTION, MODULE_ID) values (26, 5, 'DW Data Source Auth.', '08.06', 'The DW data source authorization.', 24)
set identity_insert MENUS off;

insert into Roles values(3500, 'Data Administrator', 'Data Source, DW Data Source Authorization');

insert into AUTHORIZATIONS(ROLE_ID, MODULE_ID, PERMISSION_VIEW, PERMISSION_UPDATE, PERMISSION_ADD, PERMISSION_DELETE, PERMISSION_FULL) values (3500, 23, 1, 1, 1, 1, 1);
insert into AUTHORIZATIONS(ROLE_ID, MODULE_ID, PERMISSION_VIEW, PERMISSION_UPDATE, PERMISSION_ADD, PERMISSION_DELETE, PERMISSION_FULL) values (3500, 24, 1, 1, 1, 1, 1);

alter table DW_Data_Source add Merge_RULE_CONTENT varchar(max);

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[DW_Data_Source_Merge_Rule]    脚本日期: 10/04/2010 15:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DW_Data_Source_Merge_Rule](
	[RULE_ID] [int] IDENTITY(1,1) NOT NULL,
	[DW_DATA_SOURCE_ID] [int] NOT NULL,
	[RULE_NAME] [varchar](200) NOT NULL,
	[DESCRIPTION] [varchar](200) NULL,
	[RULE_TYPE] [varchar](20) NOT NULL,
	[SEQ_NO] [int] NOT NULL,
	[RULE_CONTENT] [varchar](max) NOT NULL,
	[RULE_RESULT_CONTENT] [varchar](max) NULL,
	[UPDATE_CONTENT] [varchar](max) NULL,
	[DEPENDENCE_RULE] [int] NULL,
	[CREATE_BY_USER_ID] [int] NOT NULL,
	[CREATE_DATE] [datetime] NOT NULL,
	[LAST_UPDATE_USER_ID] [int] NULL,
	[LAST_UPDATE_DATE] [datetime] NULL,
 CONSTRAINT [PK_DW_Data_Source_Merge_Rule] PRIMARY KEY CLUSTERED 
(
	[RULE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[DW_Data_Source_Merge_Rule]  WITH CHECK ADD  CONSTRAINT [FK_DW_Data_Source_Merge_Rule_DW_Data_Source_Merge_Rule] FOREIGN KEY([DEPENDENCE_RULE])
REFERENCES [dbo].[DW_Data_Source_Merge_Rule] ([RULE_ID])
GO
ALTER TABLE [dbo].[DW_Data_Source_Merge_Rule] CHECK CONSTRAINT [FK_DW_Data_Source_Merge_Rule_DW_Data_Source_Merge_Rule]
GO
ALTER TABLE [dbo].[DW_Data_Source_Merge_Rule]  WITH CHECK ADD  CONSTRAINT [FK_DW_Data_Source_Merge_Rule_USERS_Create_By] FOREIGN KEY([CREATE_BY_USER_ID])
REFERENCES [dbo].[USERS] ([USER_ID])
GO
ALTER TABLE [dbo].[DW_Data_Source_Merge_Rule] CHECK CONSTRAINT [FK_DW_Data_Source_Merge_Rule_USERS_Create_By]
GO
ALTER TABLE [dbo].[DW_Data_Source_Merge_Rule]  WITH CHECK ADD  CONSTRAINT [FK_DW_Data_Source_Merge_Rule_USERS_Last_Update_By] FOREIGN KEY([LAST_UPDATE_USER_ID])
REFERENCES [dbo].[USERS] ([USER_ID])
GO
ALTER TABLE [dbo].[DW_Data_Source_Merge_Rule] CHECK CONSTRAINT [FK_DW_Data_Source_Merge_Rule_USERS_Last_Update_By]