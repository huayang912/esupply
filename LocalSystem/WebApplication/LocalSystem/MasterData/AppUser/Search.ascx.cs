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
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Service.Ext.Criteria;
using com.LocalSystem.Web;
using com.LocalSystem.Entity;
using NHibernate.Expression;
using System.Collections.Generic;


public partial class MasterData_User_Search : SearchModuleBase
{

    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text != string.Empty ? this.tbCode.Text.Trim() : string.Empty;
        string firstName = this.tbFirstName.Text != string.Empty ? this.tbFirstName.Text.Trim() : string.Empty;
        string lastName = this.tbLastName.Text != string.Empty ? this.tbLastName.Text.Trim() : string.Empty;

        #region DetachedCriteria
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(AppUser));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(AppUser))
            .SetProjection(Projections.Count("Code"));

        if (SearchEvent != null)
        {
            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Code", code, MatchMode.Anywhere));
                selectCountCriteria.Add(Expression.Like("Code", code, MatchMode.Anywhere));
            }

            if (firstName != string.Empty)
            {
                selectCriteria.Add(Expression.Like("FirstName", firstName, MatchMode.Anywhere));
                selectCountCriteria.Add(Expression.Like("FirstName", firstName, MatchMode.Anywhere));
            }
            if (lastName != string.Empty)
            {
                selectCriteria.Add(Expression.Like("LastName", lastName, MatchMode.Anywhere));
                selectCountCriteria.Add(Expression.Like("LastName", lastName, MatchMode.Anywhere));
            }

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
        #endregion
    }


    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }
}
