USE [esupply]
GO
/****** 对象:  Table [dbo].[resource]    脚本日期: 12/25/2010 20:30:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[resource](
	[id] [numeric](19, 0) IDENTITY(1,1) NOT NULL,
	[code] [varchar](50) NOT NULL,
	[type] [varchar](20) NOT NULL,
 CONSTRAINT [PK_resource] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [esupply]
GO
/****** 对象:  Table [dbo].[role_resource]    脚本日期: 12/25/2010 20:29:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_resource](
	[role_id] [numeric](19, 0) NOT NULL,
	[resource_id] [numeric](19, 0) NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[role_resource]  WITH CHECK ADD  CONSTRAINT [FK_role_resource_resource] FOREIGN KEY([resource_id])
REFERENCES [dbo].[resource] ([id])
GO
ALTER TABLE [dbo].[role_resource] CHECK CONSTRAINT [FK_role_resource_resource]
GO
ALTER TABLE [dbo].[role_resource]  WITH CHECK ADD  CONSTRAINT [FK_role_resource_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([id])
GO
ALTER TABLE [dbo].[role_resource] CHECK CONSTRAINT [FK_role_resource_role]

USE [esupply]
GO
/****** 对象:  Table [dbo].[user_resource]    脚本日期: 12/25/2010 20:32:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_resource](
	[user_id] [numeric](19, 0) NOT NULL,
	[resource_id] [numeric](19, 0) NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[user_resource]  WITH CHECK ADD  CONSTRAINT [FK_user_resource_app_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[app_user] ([id])
GO
ALTER TABLE [dbo].[user_resource] CHECK CONSTRAINT [FK_user_resource_app_user]
GO
ALTER TABLE [dbo].[user_resource]  WITH CHECK ADD  CONSTRAINT [FK_user_resource_resource] FOREIGN KEY([resource_id])
REFERENCES [dbo].[resource] ([id])
GO
ALTER TABLE [dbo].[user_resource] CHECK CONSTRAINT [FK_user_resource_resource]


alter table resource add description varchar(50);

set identity_insert resource on;
insert into resource(id, code, type, description) values(1001001, '/mainMenu.html', 'url', 'Main Menu');
insert into resource(id, code, type, description) values(1001002, '/editProfile.html', 'url', 'Edit Profile');
insert into resource(id, code, type, description) values(2001001, '/users.html', 'url', 'List Users');
insert into resource(id, code, type, description) values(2001002, '/editUser.html', 'url', 'Edit Users');
insert into resource(id, code, type, description) values(2001003, '/saveUser.html', 'url', 'Save Users');
insert into resource(id, code, type, description) values(2001004, '/roles.html', 'url', 'List Roles');
insert into resource(id, code, type, description) values(2001005, '/editRole.html', 'url', 'Edit Roles');
insert into resource(id, code, type, description) values(2001006, '/saveRole.html', 'url', 'Save Roles');
insert into resource(id, code, type, description) values(2001007, '/userResources.html', 'url', 'List Users To Authorization');
insert into resource(id, code, type, description) values(2001008, '/editUserResource.html', 'url', 'Authorization To User');
insert into resource(id, code, type, description) values(2001009, '/roleResources.html', 'url', 'List Roles To Authorization');
insert into resource(id, code, type, description) values(2001010, '/editRoleResource.html', 'url', 'Authorization To Role');
insert into resource(id, code, type, description) values(2001011, '/userRoles.html', 'url', 'List Roles To User');
insert into resource(id, code, type, description) values(2001012, '/editUserRole.html', 'url', 'Edit Role Users');
insert into resource(id, code, type, description) values(2002001, '/plants.html', 'url', 'List Plants');
insert into resource(id, code, type, description) values(2002002, '/editPlant.html', 'url', 'Edit Plants');
insert into resource(id, code, type, description) values(2002003, '/savePlant.html', 'url', 'Save Plants');
insert into resource(id, code, type, description) values(2003001, '/suppliers.html', 'url', 'List Suppliers');
insert into resource(id, code, type, description) values(2003002, '/editSupplier.html', 'url', 'Edit Suppliers');
insert into resource(id, code, type, description) values(2003003, '/saveSupplier.html', 'url', 'Save Suppliers');
insert into resource(id, code, type, description) values(2003004, '/exportSupplier.html', 'url', 'Export Suppliers');
insert into resource(id, code, type, description) values(2004001, '/items.html', 'url', 'List Parts');
insert into resource(id, code, type, description) values(2004002, '/editItem.html', 'url', 'Edit Parts');
insert into resource(id, code, type, description) values(2004003, '/saveItem.html', 'url', 'Save Parts');
insert into resource(id, code, type, description) values(2004004, '/exportItem.html', 'url', 'Export Parts');
insert into resource(id, code, type, description) values(2005001, '/plantScheduleGroups.html', 'url', 'List Schedule Groups');
insert into resource(id, code, type, description) values(2005002, '/editPlantScheduleGroup.html', 'url', 'Edit Schedule Group');
insert into resource(id, code, type, description) values(2005003, '/savePlantScheduleGroup.html', 'url', 'Save Schedule Group');
insert into resource(id, code, type, description) values(2006001, '/listScheduleControl.html', 'url', 'List Schedule Control');
insert into resource(id, code, type, description) values(2006002, '/saveScheduleControl.html', 'url', 'Save Schedule Control');
insert into resource(id, code, type, description) values(2007001, '/activeUsers.html', 'url', 'List Active Users');
insert into resource(id, code, type, description) values(2008001, '/supplierItems.html', 'url', 'List Supplier Parts');
insert into resource(id, code, type, description) values(2008002, '/editSupplierItem.html', 'url', 'Edit Supplier Parts');
insert into resource(id, code, type, description) values(2008003, '/saveSupplierItem.html', 'url', 'Save Supplier Parts');
insert into resource(id, code, type, description) values(2008004, '/importSupplierItem.html', 'url', 'Import Supplier Parts');
insert into resource(id, code, type, description) values(2008005, '/uploadSupplierItem.html', 'url', 'Upload Supplier Parts');
insert into resource(id, code, type, description) values(3001001, '/purchaseOrders.html', 'url', 'List Purchase Orders');
insert into resource(id, code, type, description) values(3001002, '/editPurchaseOrder.html', 'url', 'Edit Purchase Orders');
insert into resource(id, code, type, description) values(3001003, '/savePurchaseOrder.html', 'url', 'Save Purchase Orders');
insert into resource(id, code, type, description) values(3001004, '/cancelPurchaseOrder.html', 'url', 'Cancel Purchase Orders');
insert into resource(id, code, type, description) values(3002001, '/listScheduleAudit.html', 'url', 'Schedule Audit');
insert into resource(id, code, type, description) values(3003001, '/schedules.html', 'url', 'List Schedules');
insert into resource(id, code, type, description) values(3003002, '/scheduleHistory.html', 'url', 'List Schedule History');
insert into resource(id, code, type, description) values(3003003, '/editSchedule.html', 'url', 'Edit Schedules');
insert into resource(id, code, type, description) values(3003004, '/saveSchedule.html', 'url', 'Save Schedules');
insert into resource(id, code, type, description) values(3004001, '/deliveryOrders.html', 'url', 'List Manifests');
insert into resource(id, code, type, description) values(3004002, '/editDeliveryOrder.html', 'url', 'Edit Manifests');
insert into resource(id, code, type, description) values(3004003, '/saveDeliveryOrder.html', 'url', 'Save Manifests');
insert into resource(id, code, type, description) values(3004004, '/confirmDeliveryOrder.html', 'url', 'Confirm Manifests');
insert into resource(id, code, type, description) values(3004005, '/cancelDeliveryOrder.html', 'url', 'Cancel Manifests');
insert into resource(id, code, type, description) values(3004006, '/deleteDeliveryOrder.html', 'url', 'Delete Manifests');
insert into resource(id, code, type, description) values(3004007, '/printDeliveryOrder.html', 'url', 'Print Manifests');
insert into resource(id, code, type, description) values(3004008, '/printPalletLabel.html', 'url', 'Print Pallet Label');
insert into resource(id, code, type, description) values(3004009, '/printBoxLabel.html', 'url', 'Print Box Label');
insert into resource(id, code, type, description) values(3004010, '/deliveryOrders2.html', 'url', 'List Manifest Files');
insert into resource(id, code, type, description) values(3004011, '/deliveryOrders3.html', 'url', 'Edit Manifest From Files');
insert into resource(id, code, type, description) values(3004012, '/printLogisticDeliveryOrder.html', 'url', 'Print Logistic Partner''s Manifests');
insert into resource(id, code, type, description) values(3005001, '/receipts.html', 'url', 'List Receipts');
insert into resource(id, code, type, description) values(3005002, '/editReceipt.html', 'url', 'Edit Receipts');
insert into resource(id, code, type, description) values(3005003, '/saveReceipt.html', 'url', 'Save Receipts');
insert into resource(id, code, type, description) values(3005004, '/receiptDetails.html', 'url', 'List Receipt Report');
insert into resource(id, code, type, description) values(3006001, '/searchReceiptDetail.html', 'url', 'Search Receipt Report');
insert into resource(id, code, type, description) values(3006002, '/exportReceiptDetail.html', 'url', 'Export Receipt Report');    
insert into resource(id, code, type, description) values(4001001, '/notices.html', 'url', 'List Notices (Publish)');
insert into resource(id, code, type, description) values(4001002, '/editNotice.html', 'url', 'Edit Notice');
insert into resource(id, code, type, description) values(4001003, '/saveNotice.html', 'url', 'Save Notice');
insert into resource(id, code, type, description) values(4002001, '/noticeReaders.html', 'url', 'List Notices');
insert into resource(id, code, type, description) values(4002002, '/editNoticeReader.html', 'url', 'View Notice Detail');
insert into resource(id, code, type, description) values(4002003, '/downloadAttachement.html', 'url', 'Download Notice Attachement');
insert into resource(id, code, type, description) values(4003001, '/inboundLogs.html', 'url', 'Inbound Logs');
insert into resource(id, code, type, description) values(4004001, '/outboundLogs.html', 'url', 'Outbound Logs');                                                            
insert into resource(id, code, type, description) values(4004002, '/exportOutboundLog.html', 'url', 'Export Outbound Log');
insert into resource(id, code, type, description) values(4003002, '/importInboundLog.html', 'url', 'Import Inbound Log');
insert into resource(id, code, type, description) values(4999999, '/logout.jsp', 'url', 'Logout');
set identity_insert resource off;

insert into resource(code, type, description) select code, 'plant', name from plant;
insert into resource(code, type, description) select code, 'supplier', name from supplier;

