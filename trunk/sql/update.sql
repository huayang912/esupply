--add external do no
alter table do add ext_do_no varchar(20);
update do set ext_do_no = do_no;
alter table do alter column ext_do_no varchar(20) not null;
CREATE UNIQUE NONCLUSTERED INDEX [DO_INDEX] ON [dbo].[do] 
(
	[ext_do_no] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];

--add orderedqty and receivedqty
alter table do_detail add order_Qty numeric(18, 4);
alter table do_detail add receive_Qty numeric(18, 4);