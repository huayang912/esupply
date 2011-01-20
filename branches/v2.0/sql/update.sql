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
alter table do_detail add order_lot numeric(9, 2);
alter table do_detail add box_count numeric(9, 2);

--add some fields for print label
alter table do add murn varchar(20);
alter table do add order_group varchar(20);
alter table do add delivery_order_group varchar(20);
alter table do add dock varchar(20);
alter table do add route varchar(20);
alter table do add main_route varchar(20);
alter table do add total_weight numeric(18, 4);
alter table do add unit_weight numeric(18, 4);
alter table do add total_volume numeric(18, 4);
alter table do add unit_volume numeric(18, 4);
alter table do add total_nb_pallets numeric(18, 4);
alter table do add title varchar(50);

alter table do_detail add label int;

--alter table plant add version int;
--update plant set version = 1;
--alter table plant alter column version int not null;

alter table do_detail add indice int;
alter table do_detail alter column reference_order_no varchar(20);
alter table do_detail alter column reference_sequence varchar(10);


alter table do_detail add package_type varchar(20);
alter table do_detail add storage_code varchar(20);
alter table do_detail add sebango varchar(20);

alter table plant add box_template_name varchar(20);