-----------2010/7/30-----------------------
1. restart the report job no matter success, cancel or fail
2. selected cube role update, not all of them
3. bug: 在页面上删除过原始数据后， rec#就不对了
4. 规则检查时,窗口要自适应大小,以隐藏scrollbar/39。检查完规则要自动关闭窗口
5. 上传数据的历史清单已太长，应分页
6. 下载的文件名很奇怪, 如含+号(如模板下载和有中文的dw query下载)
7. 进入系统时很慢，或一段时间不用后再次操作时很慢(session issue)


-----------2010/8/12-----------------------
1. DWQuery不能下载数据量很多的记录。


-----------2010/8/21-----------------------
1. security by category。
2. disable category 是指不再用的category可以disable掉, disable的category不可用于上传新数据,但还是可以查看历史数据
3. dw update定位出错问题(现在只能单独定位,然后操作)


-----------2010/8/29-----------------------
1. 上传中断后，再上传会导入双倍数据(需error capture 和 rollback)
2. 在data_source_upload中加confirmed by 

-----------2010/9/4-----------------------
1. report job 完成后可在report job页面根据report用户查看是否成功
2. report job 授权


-----------2010/9/12-----------------------
1. validation rule for report job (same as cube job)，在Batch中加Validation Rule
		Rule Content中可以传<$ReportDate$>作为参数
2. 没有Data Preparation权限登录后都会抱Sorry, you have no permission to access this module.
3. 没有任何菜单权限，登录后报错。
4. 在data_source_upload中加confirmed by 
5. Category授权中最后一个人无法通过界面拿掉
6. DW Query中Data Source Name查询不需要区分大小写，所有查询都应该不区分大小写
7. Validation Rule SQL改为Validation Rule Result SQL,在增加Validation Rule SQL，Validation的时候取Validation Rule SQL执行。DataSource Upload和Cube Maint.和Report Batch Maint.都要改。
8. DW query在页面上的查询结果可下载，在页面上显示结果集有几条数据



-----------2010/9/16-----------------------
1. 在Rule Maint.的时候增加选择Dependence Rule，默认为空。如果选择了Dependence Rule，Validation的时候是不Run的，状态和Dependence Rule的状态一样。
2. 在数据准备中,每一笔已经process的rawdata上都应有可以下载相应dw数据的link,在ETL Confirmation History中增加Download DWData,WithDraw之后就不显示该按钮。


-----------2010/10/2-----------------------
1. DataSource Preparation中增加Show Data Structure的页面。
2. 所有替换Rule的地方都要增加替换操作用户Id的参数。
3. 有根据”Name”的查询， 应又查Name, 又查Description, 且不分大小写
4. Data Validation的时候，光标移到Rule上，显示Description。Rule Description维护的时候使用TextArea输入，可以带折行。
5. data process平台权限控制角色(low, 把权限控制单拿出来)。用DataSource Category的User权限来控制可以授权的DataSourceh和DataSource Category。DW DataSource授权也相同

-----------2010/10/24-----------------------
1. history archieve
2. configurable modules to merge duplicate items in master
	Data to Merge Query SQL中可以传入参数<$RecID$>来定位需要Merge的数据。
	Merge ValidationRule SQL和Merge SQL中可以传入参数<$MergeFromRecId$>和<$MergeToRecId$>来定位Merge前后的记录。
	
-----------2010/11/3-----------------------
1. 解决2个用户同时在一个数据源下上传同一个Category的数据。
2. Cube Process的时候，取最后一次Cube Process的时候设定的日期作为缺省值。