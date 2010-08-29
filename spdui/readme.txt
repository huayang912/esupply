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

