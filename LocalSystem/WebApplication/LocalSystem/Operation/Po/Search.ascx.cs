using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using NHibernate.Expression;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Web;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Entity.Operation;

public partial class MasterData_Operation_Po_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text.Trim();
        string status = this.ddlStatus.SelectedValue;
        string startDate = this.tbStartDate.Text.Trim();
        string endDate = this.tbEndDate.Text.Trim();

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Po));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Po)).SetProjection(Projections.Count("Code"));
        if (code != string.Empty)
        {
            selectCriteria.Add(Expression.Like("Code", code, MatchMode.Anywhere));
            selectCountCriteria.Add(Expression.Like("Code", code, MatchMode.Anywhere));
        }

        if (status != string.Empty)
        {
            selectCriteria.Add(Expression.Like("Status", status, MatchMode.Anywhere));
            selectCountCriteria.Add(Expression.Like("Status", status, MatchMode.Anywhere));
        }

        if (startDate != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
            selectCountCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
        }

        if (endDate != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1)));
        }

        SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
    }

}
