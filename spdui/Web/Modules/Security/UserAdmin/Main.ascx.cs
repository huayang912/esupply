using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using log4net;

using Dndp.Service.Security;
using Dndp.Web;
using System.Collections.Generic;
using Dndp.Persistence.Criteria;
using Dndp.Persistence.Criteria.Expression;

public partial class Modules_Security_UserAdmin_Main : ModuleBase
{
    private static ILog log = LogManager.GetLogger("User Admin");

    protected IUserMgr TheService
    {
        get
        {
            return GetService("UserMgr.service") as IUserMgr;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void InitByPermission()
    {
        base.InitByPermission();

        btnNew.Visible = PermissionAdd;
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Edit1.Back += new System.EventHandler(this.Edit1_Back);
        New1.Back += new EventHandler(New1_Back);
    }

    void New1_Back(object sender, EventArgs e)
    {
        pnlMain.Visible = true;
        UpdateView();
        New1.Visible = false;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchCriteria = new Criteria(typeof(Dndp.Persistence.Entity.Security.User));
        SearchCriteria.Add(Expression.Like("UserName", txtUserName.Text.Trim(), MatchMode.Anywhere));
        
        UpdateView();
    }

    private void UpdateView()
    {
        IList result = SearchProvider();
        if (result != null)
        {
            gvUser.DataSource = result;
            gvUser.DataBind();
            btnDelete.Visible = PermissionDelete;
            lblRecordCount.Text = string.Format(GetGlobalResourceObject("GlobalWebResource", "RecordCount").ToString(), result.Count);
            lblText.Visible = false;
        }
    }

    protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUser.PageIndex = e.NewPageIndex;
        UpdateView();
    }

    protected void gvUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        int userId = (int)(gvUser.SelectedDataKey.Value);
        Edit1.TheUser = TheService.LoadUserWithRoles(userId);
        Edit1.UpdateView();
        Edit1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void Edit1_Back(object sender, EventArgs e)
    {
        Edit1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMain.Visible = false;
        New1.Visible = true;
        New1.UpdateView();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IList<int> userIdList = new List<int>();

            foreach (GridViewRow row in gvUser.Rows)
            {
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
                if (cbSelect.Checked)
                {
                    userIdList.Add((int)(gvUser.DataKeys[row.RowIndex].Value));
                }
            }


            TheService.DeleteUser(userIdList);

            UpdateView();
        }
        catch (Exception ex)
        {
            this.lblText.Visible = true;
            this.lblText.Text = "Record have been cited, can not be deleted";
        }

    }

    protected void gvUser_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (SearchCriteria.IsOrderExist(e.SortExpression, true))
        {
            SearchCriteria.RemoveAllOrders();
            SearchCriteria.AddOrder(new Order(e.SortExpression, false));
        }
        else
        {
            SearchCriteria.RemoveAllOrders();
            SearchCriteria.AddOrder(new Order(e.SortExpression, true));
        }
        

        UpdateView();
    }

    private Criteria SearchCriteria
    {
        get
        {
            if (ViewState["SearchCriteria"] == null)
            {
                return null;
            }
            else
            {
                return (Criteria)ViewState["SearchCriteria"];
            }
        }
        set
        {
            ViewState["SearchCriteria"] = value;
        }
    }
    
    private IList SearchProvider()
    {
        if (SearchCriteria == null)
        {
            return null;
        }
        return (GetService("CriteriaMgr.service") as Dndp.Service.Criteria.ICriteriaMgr).List(SearchCriteria);
    }
}
