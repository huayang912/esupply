-----------2010/7/30-----------------------
1. restart the report job no matter success, cancel or fail
2. selected cube role update, not all of them
3. bug: ��ҳ����ɾ����ԭʼ���ݺ� rec#�Ͳ�����
4. ������ʱ,����Ҫ����Ӧ��С,������scrollbar/39����������Ҫ�Զ��رմ���
5. �ϴ����ݵ���ʷ�嵥��̫����Ӧ��ҳ
6. ���ص��ļ��������, �纬+��(��ģ�����غ������ĵ�dw query����)
7. ����ϵͳʱ��������һ��ʱ�䲻�ú��ٴβ���ʱ����(session issue)


-----------2010/8/12-----------------------
1. DWQuery���������������ܶ�ļ�¼��


-----------2010/8/21-----------------------
1. security by category��
2. disable category ��ָ�����õ�category����disable��, disable��category���������ϴ�������,�����ǿ��Բ鿴��ʷ����
3. dw update��λ��������(����ֻ�ܵ�����λ,Ȼ�����)


-----------2010/8/29-----------------------
1. �ϴ��жϺ����ϴ��ᵼ��˫������(��error capture �� rollback)
2. ��data_source_upload�м�confirmed by 

-----------2010/9/4-----------------------
1. report job ��ɺ����report jobҳ�����report�û��鿴�Ƿ�ɹ�
2. report job ��Ȩ


-----------2010/9/12-----------------------
1. validation rule for report job (same as cube job)����Batch�м�Validation Rule
		Rule Content�п��Դ�<$ReportDate$>��Ϊ����
2. û��Data PreparationȨ�޵�¼�󶼻ᱧSorry, you have no permission to access this module.
3. û���κβ˵�Ȩ�ޣ���¼�󱨴�
4. ��data_source_upload�м�confirmed by 
5. Category��Ȩ�����һ�����޷�ͨ�������õ�
6. DW Query��Data Source Name��ѯ����Ҫ���ִ�Сд�����в�ѯ��Ӧ�ò����ִ�Сд
7. Validation Rule SQL��ΪValidation Rule Result SQL,������Validation Rule SQL��Validation��ʱ��ȡValidation Rule SQLִ�С�DataSource Upload��Cube Maint.��Report Batch Maint.��Ҫ�ġ�
8. DW query��ҳ���ϵĲ�ѯ��������أ���ҳ������ʾ������м�������



-----------2010/9/16-----------------------
1. ��Rule Maint.��ʱ������ѡ��Dependence Rule��Ĭ��Ϊ�ա����ѡ����Dependence Rule��Validation��ʱ���ǲ�Run�ģ�״̬��Dependence Rule��״̬һ����
2. ������׼����,ÿһ���Ѿ�process��rawdata�϶�Ӧ�п���������Ӧdw���ݵ�link,��ETL Confirmation History������Download DWData,WithDraw֮��Ͳ���ʾ�ð�ť��