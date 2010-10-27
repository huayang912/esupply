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
alter table DATA_SOURCE add DATA_STRUCTURE_SQL nvarchar(max);

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


----2010.10.21
alter table dbo.DATA_SOURCE_UPLOAD add [Archive_BY] [int] NULL,[Archive_DATE] [datetime] NULL,[Archive_Flag] [int] NOT NULL DEFAULT ((0))

----增加的archive表

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[CustomerLinkageTable_ARCHIVE]    脚本日期: 10/23/2010 13:48:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerLinkageTable_ARCHIVE](
	[Rec_Id] [numeric](18, 0)  NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawCustomerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerSubGroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerSubGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerType] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesPersonCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Year] [int] NULL,
	[Month] [int] NULL,
	[PromotionPattern] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[IMSApproved] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_CustomerLinkageTable_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[LinkageTable_ARCHIVE]    脚本日期: 10/23/2010 14:00:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkageTable_ARCHIVE](
	[Rec_Id] [numeric](18, 0)  NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawSalesPersonCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerSubGroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerSubGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Year] [int] NULL,
	[Month] [int] NULL,
	[PromotionPattern] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[IMSApproved] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_LinkageTable_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_City_ARCHIVE]    脚本日期: 10/23/2010 14:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_City_ARCHIVE](
	[Rec_Id] [numeric](18, 0)  NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[CityId] [int] NULL,
	[RawCityCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCityName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Map_City_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[CityMarketSegment_ARCHIVE]    脚本日期: 10/23/2010 14:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityMarketSegment_ARCHIVE](
	[Rec_Id] [numeric](18, 0)  NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[MarketSegmentName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CityId] [int] NULL,
	[CityName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_CityMarketSegment_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Customer_SubKA_ARCHIVE]    脚本日期: 10/23/2010 14:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer_SubKA_ARCHIVE](
	[Rec_Id] [numeric](18, 0)  NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawCustomerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSubKACode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSubKAName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerType] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Year] [int] NULL,
	[Month] [int] NULL,
 CONSTRAINT [PK_Customer_SubKA_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_Customer_ARCHIVE]    脚本日期: 10/23/2010 14:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_Customer_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[CustomerId] [int] NULL,
	[RawCustomerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Map_Customer_Id_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Customer_CustomerSubgroup_ARCHIVE]    脚本日期: 10/23/2010 14:22:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer_CustomerSubgroup_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawCustomerSubGroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerSubGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerSubGroupType] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Disabled] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Customer_CustomerSubgroup_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_CustomerGroup_ARCHIVE]    脚本日期: 10/23/2010 14:19:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_CustomerGroup_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawCustomerGroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerGroupId] [int] NULL,
 CONSTRAINT [PK_Map_CustomerGroup_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_CustomerSubGroup_ARCHIVE]    脚本日期: 10/23/2010 14:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_CustomerSubGroup_ARCHIVE](
	[Rec_Id] [numeric](18, 0)  NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawCustomerSubGroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerSubGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerSubGroupId] [int] NULL,
 CONSTRAINT [PK_Map_CustomerSubGroup_Id_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_Distributor_ARCHIVE]    脚本日期: 10/23/2010 14:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_Distributor_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[DistributorId] [int] NULL,
	[RawDistributorName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawDistributorCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Map_Distributor_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[KeyAccount_ARCHIVE]    脚本日期: 10/23/2010 14:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeyAccount_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawKeyAccountCode] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[RawKeyAccountName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[KeyAccountName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_KeyAccount_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_ProductCommonName_ARCHIVE]    脚本日期: 10/23/2010 14:29:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_ProductCommonName_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProductCommonNameId] [int] NOT NULL,
	[RawProductCommonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawProductCommonNameCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Map_ProductCommonName_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_ProductSegment_ARCHIVE]    脚本日期: 10/23/2010 14:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_ProductSegment_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawProductSegmentCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawProductSegmentName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProductSegmentId] [int] NOT NULL,
 CONSTRAINT [PK_Map_ProductSegment_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_Product_ARCHIVE]    脚本日期: 10/23/2010 14:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_Product_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProductId] [int] NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Map_Product_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_SalesOrg_ARCHIVE]    脚本日期: 10/23/2010 14:31:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_SalesOrg_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SalesOrgId] [int] NULL,
	[RawSalesOrgCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesOrgName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Map_SalesOrg_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_SalesPerson_ARCHIVE]    脚本日期: 10/23/2010 14:32:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_SalesPerson_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SalesPersonId] [int] NULL,
	[RawSalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesPersonCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Map_SalesPerson_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SalesPerson_SalesOrg_ARCHIVE]    脚本日期: 10/23/2010 14:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesPerson_SalesOrg_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawSalesOrgCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesOrgName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesPersonCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RecordYear] [int] NULL,
	[RecordMonth] [int] NULL,
	[Position] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_SalesPerson_SalesOrg_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_SubKA_ARCHIVE]    脚本日期: 10/23/2010 14:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_SubKA_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SubKAId] [int] NULL,
	[RawSubKACode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSubKAName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Map_SubKA_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_Vendor_ARCHIVE]    脚本日期: 10/23/2010 14:35:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_Vendor_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[VendorId] [int] NULL,
	[RawVendorCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawVendorName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Map_Vendor_Id_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Map_WholeSaler_ARCHIVE]    脚本日期: 10/23/2010 14:35:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map_WholeSaler_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WholeSalerID] [int] NULL,
	[RawWholeSalerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawWholeSalerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Map_WholeSaler_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[City_ARCHIVE]    脚本日期: 10/23/2010 15:54:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Province] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CityName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Country] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCityCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCityName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CityId] [int] NULL,
	[IMSCity] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Region] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_City_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Customer_ARCHIVE]    脚本日期: 10/23/2010 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[CustomerType] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[City] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerId] [int] NULL,
	[CustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[MOH] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerGroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Customer_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[CustomerSubGroupCommonName_ARCHIVE]    脚本日期: 10/23/2010 15:56:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerSubGroupCommonName_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
 CONSTRAINT [PK_CustomerSubGroupCommonName_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[CustomerGroup_ARCHIVE]    脚本日期: 10/23/2010 15:57:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerGroup_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawCustomerGroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerGroupId] [int] NULL,
	[CustomerGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerBanner] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[KeyAccount] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_CustomerGroup_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[CustomerSubGroup_ARCHIVE]    脚本日期: 10/23/2010 15:58:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerSubGroup_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawCustomerSubGroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerSubGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerSubGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerSubGroupId] [int] NULL,
	[CustomerGroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerSubGroupType] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[City] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[MOH] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_CustomerSubGroup_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[CustomerQuintile_ARCHIVE]    脚本日期: 10/23/2010 15:58:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerQuintile_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[QuintileLevel] [int] NULL,
	[RawCustomerSubGroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerSubGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_CustomerQuintile_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Distributor_ARCHIVE]    脚本日期: 10/23/2010 15:59:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Distributor_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[DistributorId] [int] NULL,
	[RawDistributorName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawDistributorCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Region] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[DistributorName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Distributor_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[ExchangeRate_ARCHIVE]    脚本日期: 10/23/2010 16:00:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeRate_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ActualRate] [numeric](18, 2) NULL,
	[NeutralRate] [numeric](18, 2) NULL,
	[Year] [int] NULL,
	[Month] [int] NULL,
 CONSTRAINT [PK_ExchangeRate_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Product_ARCHIVE]    脚本日期: 10/23/2010 16:00:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductBrand] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Vendor] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CommonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductSegment] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Package] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductId] [int] NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[InMarketSalesProductPrice] [numeric](18, 4) NULL,
	[BrandPackage] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Franchise] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CHNFranchise] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CHNProductBrand] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CHNBrandPackage] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CHNProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Product_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[ProductSegment_ARCHIVE]    脚本日期: 10/23/2010 16:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSegment_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProductSegmentId] [int] NULL,
	[ProductSegmentName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductSegmentCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductSegmentName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductSegmentLevel1Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductSegmentLevel2Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductSegmentLevel3Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductSegmentLevel4Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductSegmentLevel5Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductSegmentLevel6Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductSegmentLevel7Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductSegmentLevel8Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductSegmentLevel9Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawParentProductSegmentCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_ProductSegment_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[ProductCommonName_ARCHIVE]    脚本日期: 10/23/2010 16:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCommonName_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProductCommonNameId] [int] NULL,
	[ProductCommonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductCommonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductCommonNameCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CHNProductCommonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_ProuductCommonName_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SalesOrg_ARCHIVE]    脚本日期: 10/23/2010 16:02:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrg_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SalesOrgId] [int] NULL,
	[RawSalesOrgCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesOrgLevel1Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesOrgLevel2Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesOrgLevel3Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesOrgLevel4Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesOrgLevel5Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesOrgLevel6Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesOrgLevel7Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesOrgLevel8Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesOrgLevel9Name] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ParentRawSalesOrgCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesOrgName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesOrgName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_SalesOrg_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SalesPerson_ARCHIVE]    脚本日期: 10/23/2010 16:03:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesPerson_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SalesPersonId] [int] NULL,
	[RawSalesPersonCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Gender] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Tel] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Address] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Email_Addr] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[JoinDate] [datetime] NULL,
	[LeaveDate] [datetime] NULL,
	[Title] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[EmployeeHRNo] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_SalesPerson_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SubKeyAccount_ARCHIVE]    脚本日期: 10/23/2010 16:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubKeyAccount_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawSubKACode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SubKAName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SubKA_Abbr] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[KAName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[KAType] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[KAGroup] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SubKAId] [int] NULL,
	[RawSubKAName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SubKALevel] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_SubKeyAccount_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[Vendor_ARCHIVE]    脚本日期: 10/23/2010 16:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[VendorName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[VendorGroup] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawVendorCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawVendorName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[VendorId] [int] NULL,
	[VendorSubGroup] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Vendor_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[WholeSaler_ARCHIVE]    脚本日期: 10/23/2010 16:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WholeSaler_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WholeSalerID] [int] NULL,
	[WholeSalerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawWholeSalerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawWholeSalerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_WholeSaler_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SDA_ARCHIVE]    脚本日期: 10/23/2010 16:06:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SDA_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[CustomerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerCity] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductVendor] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RecordYear] [int] NULL,
	[ProductPackage] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SoldQty] [numeric](18, 2) NULL,
	[ProductPrice] [numeric](18, 2) NULL,
	[ProductAmount] [numeric](18, 2) NULL,
	[RecordMonth] [int] NULL,
 CONSTRAINT [PK_SDA_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[CustomerBiding_ARCHIVE]    脚本日期: 10/23/2010 16:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerBiding_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[CustomerId] [int] NULL,
	[RawCustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[BidCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[BidDescription] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[Status] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_CustomerBiding_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[CustomerListing_ARCHIVE]    脚本日期: 10/23/2010 16:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerListing_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Status] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ListYear] [int] NULL,
	[ListMonth] [int] NULL,
	[RawCustomerSubGroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerSubGroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_CustomerListing_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[CustomerReimbursement_ARCHIVE]    脚本日期: 10/23/2010 16:08:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerReimbursement_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Country] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Province] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[City] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Status] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[FromDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_CustomerReimbursement_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[DistributorInventory_ARCHIVE]    脚本日期: 10/23/2010 21:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistributorInventory_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[DistributorCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[DistributorName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[DistributorCity] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[InventoryYear] [int] NULL,
	[InventoryMonth] [int] NULL,
	[BatchNo] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SampleQty] [int] NULL,
	[InventoryQty] [int] NULL,
 CONSTRAINT [PK_DistributorInventory_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[IMS_ARCHIVE]    脚本日期: 10/23/2010 21:57:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMS_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawCityCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCityName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Year] [int] NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Value] [numeric](18, 2) NULL,
	[Volume] [int] NULL,
	[DataType] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Period] [int] NULL,
 CONSTRAINT [PK_IMS_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[InMarketSales_ARCHIVE]    脚本日期: 10/23/2010 21:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InMarketSales_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerCity] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SoldQty] [numeric](18, 2) NULL,
	[SampleQty] [numeric](18, 2) NULL,
	[SalesDate] [datetime] NULL,
	[ProductPrice] [numeric](18, 4) NULL,
	[DistributorName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[DistributorCity] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[BatchNo] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerType] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[DistributorCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SoldOutQty] [numeric](18, 2) NULL,
	[UploadFlag] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSubKACode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSubKAName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ZUELLEG_ID] [int] NULL,
	[DistributorCustomerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_InMarketSales_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[InMarketSalesBudget_ARCHIVE]    脚本日期: 10/23/2010 21:59:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InMarketSalesBudget_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawCustomerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesPersonCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Year] [int] NULL,
	[Month] [int] NULL,
	[BudgetQty] [numeric](18, 2) NULL,
	[BudgetType] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[BudgetAmount] [numeric](18, 6) NULL,
	[City] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_InMarketSalesBudget_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[KAASFE_ARCHIVE]    脚本日期: 10/23/2010 22:00:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KAASFE_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawSubKACode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSubKAName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RecYear] [int] NULL,
	[RecMonth] [int] NULL,
	[Frequency] [numeric](18, 2) NULL,
	[Coverage] [numeric](18, 2) NULL,
 CONSTRAINT [PK_KAASFE_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[PrimarySales_ARCHIVE]    脚本日期: 10/23/2010 22:01:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrimarySales_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[WholeSalerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[WholeSalerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SoldQty] [numeric](18, 2) NULL,
	[SalesDate] [datetime] NULL,
	[SampleQty] [numeric](18, 2) NULL,
	[ProductPrice] [numeric](18, 2) NULL,
	[BatchNo] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_PrimarySales_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[PrimarySalesBudget_ARCHIVE]    脚本日期: 10/23/2010 22:18:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrimarySalesBudget_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[BudgetQty] [int] NULL,
	[BudgetYear] [int] NULL,
	[BudgetMonth] [int] NULL,
	[BudgetType] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_PrimarySalesBudget_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SalesCallActual_ARCHIVE]    脚本日期: 10/23/2010 22:19:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesCallActual_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ContactPriority] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ContactCount] [int] NULL,
	[CallActual] [numeric](18, 2) NULL,
	[CallPan] [numeric](18, 2) NULL,
	[CallYear] [int] NULL,
	[CallMonth] [int] NULL,
	[PromotionPattern] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[IMS_City_Type] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Approved] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Potential] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesPersonCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_SalesCallActual_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SalesCallKPIActual_ARCHIVE]    脚本日期: 10/23/2010 22:20:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesCallKPIActual_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SalesPersonCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CallYear] [int] NULL,
	[CallMonth] [int] NULL,
	[CallPerDay] [numeric](18, 2) NULL,
	[FQY] [numeric](18, 2) NULL,
	[CVG] [numeric](18, 2) NULL,
	[TIF] [numeric](18, 2) NULL,
	[FQY_A] [numeric](18, 2) NULL,
	[FQY_B] [numeric](18, 2) NULL,
	[FQY_C] [numeric](18, 2) NULL,
	[FQY_D] [numeric](18, 2) NULL,
	[CVG_A] [numeric](18, 2) NULL,
	[CVG_B] [numeric](18, 2) NULL,
	[CVG_C] [numeric](18, 2) NULL,
	[CVG_D] [numeric](18, 2) NULL,
	[DetailsPerCall] [numeric](18, 2) NULL,
 CONSTRAINT [PK_SalesCallKPIActual_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SalesCallPromotionPatternTarget_ARCHIVE]    脚本日期: 10/23/2010 22:21:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesCallPromotionPatternTarget_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[CustomerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[PromotionPattern] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CallYear] [int] NULL,
	[CallMonth] [int] NULL,
	[CallPlan] [numeric](18, 2) NULL,
	[IMS_City_Type] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Approved] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Potential] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_SalesCallPromotionPatternTarget_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SalesCoachCall_ARCHIVE]    脚本日期: 10/23/2010 22:22:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesCoachCall_ARCHIVE](
	[Rec_Id] [numeric](18, 0)  NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesManagerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CallYear] [int] NULL,
	[CallMonth] [int] NULL,
	[CoachTime] [numeric](18, 2) NULL,
	[SalesPersonCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesManagerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_SalesCoachCall_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SalesCoachPCT_ARCHIVE]    脚本日期: 10/23/2010 22:23:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesCoachPCT_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SalesManagerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesManagerName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CallYear] [int] NULL,
	[CallMonth] [int] NULL,
	[CoachTime] [numeric](18, 2) NULL,
	[WorkTime] [numeric](18, 2) NULL,
	[CoachingPCT] [numeric](18, 2) NULL,
 CONSTRAINT [PK_SalesCoachPCT_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SalesCallKPIActual_Product_ARCHIVE]    脚本日期: 10/23/2010 22:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesCallKPIActual_Product_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SalesPersonCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CallYear] [int] NULL,
	[CallMonth] [int] NULL,
	[ProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[FQY] [numeric](18, 2) NULL,
	[CVG] [numeric](18, 2) NULL,
 CONSTRAINT [PK_SalesCallKPIActual_Product_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SalesCallStat_ARCHIVE]    脚本日期: 10/23/2010 22:25:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesCallStat_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SalesPersonCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CallYear] [int] NULL,
	[ContactPirority] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CallPerTarget] [int] NULL,
	[StatCount] [int] NULL,
	[SalesPersonName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CallMonth] [int] NULL,
 CONSTRAINT [PK_SalesCallStat_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SecondarySales_ARCHIVE]    脚本日期: 10/23/2010 22:26:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecondarySales_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[DistributorCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[DistributorName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[DistributorCity] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[SoldQty] [numeric](18, 2) NULL,
	[SampleQty] [numeric](18, 2) NULL,
	[SalesDate] [datetime] NULL,
	[ProductPrice] [numeric](18, 2) NULL,
	[WholeSalerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[BatchNo] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_SecondarySales_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[SubKABudget_ARCHIVE]    脚本日期: 10/23/2010 22:27:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubKABudget_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawSubKACode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawSubKAName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerSoldQty] [int] NULL,
	[Qty] [int] NULL,
	[ProductPrice] [numeric](18, 2) NULL,
	[Amount] [int] NULL,
	[City] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Year] [int] NULL,
	[Month] [int] NULL,
 CONSTRAINT [PK_SubKABudget_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[TargetDrugstoreSFE_ARCHIVE]    脚本日期: 10/23/2010 22:27:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TargetDrugstoreSFE_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawCustomerSubgroupCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCustomerSubgroupName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RecYear] [int] NULL,
	[RecMonth] [int] NULL,
	[POP] [int] NULL,
	[Display] [int] NULL,
	[Facing] [int] NULL,
 CONSTRAINT [PK_TargetDrugstoreSFE_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[URC_ARCHIVE]    脚本日期: 10/23/2010 22:28:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[URC_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RawCityCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawCityName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Year] [int] NULL,
	[Month] [int] NULL,
	[RawProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[RawProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Value] [numeric](18, 2) NULL,
	[Volume] [int] NULL,
	[NumDist] [numeric](18, 2) NULL,
	[WtdDist] [numeric](18, 2) NULL,
	[AveragePriceByPack] [numeric](18, 2) NULL,
	[CoverDays] [numeric](18, 2) NULL,
 CONSTRAINT [PK_URC_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[WholeSalerInventory_ARCHIVE]    脚本日期: 10/23/2010 22:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WholeSalerInventory_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WholeSalerCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductCode] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[ProductName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[InventoryYear] [int] NULL,
	[InventoryMonth] [int] NULL,
	[InventoryQty] [int] NULL,
	[SampleQty] [int] NULL,
	[BatchNo] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_WholeSalerInventory_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

USE [SPStaging]
GO
/****** 对象:  Table [dbo].[DWValidation_ARCHIVE]    脚本日期: 10/23/2010 22:31:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DWValidation_ARCHIVE](
	[Rec_Id] [numeric](18, 0) NOT NULL,
	[BATCH_NO] [int] NOT NULL,
	[ROW_NO] [int] NOT NULL,
	[CATEGORY_id] [int] NOT NULL,
	[CATEGORY] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[RecYear] [int] NULL,
	[RecMonth] [int] NULL,
 CONSTRAINT [PK_DWValidation_ARCHIVE_1] PRIMARY KEY CLUSTERED 
(
	[Rec_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

alter table DW_Data_Source add Query_Merge_RULE_CONTENT varchar(max);



-----------2010/10/27-----------------------
alter table DW_Data_Source add Merge_RESULT_CONTENT varchar(max);