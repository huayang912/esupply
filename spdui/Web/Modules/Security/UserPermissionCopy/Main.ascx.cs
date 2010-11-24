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
using Dndp.Web;
using Dndp.Service.Security;
using Dndp.Persistence.Criteria;
using Dndp.Persistence.Criteria.Expression;
using Dndp.Service.Dui;
using System.Collections.Generic;

public partial class Modules_Security_UserPermissionCopy_Main : ModuleBase
{
    protected IUserMgr TheService
    {
        get
        {
            return GetService("UserMgr.service") as IUserMgr;
        }
    }

    protected IDataSourceMgr TheDSService
    {
        get
        {
            return GetService("DataSourceMgr.service") as IDataSourceMgr;
        }
    }

    protected IDWDataSourceMgr TheDWDSService
    {
        get
        {
            return GetService("DWDataSourceMgr.service") as IDWDataSourceMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateFromView();
            UpdateToView();            
        }
    }

    protected void btnSearchFrom_Click(object sender, EventArgs e)
    {
        UpdateFromView();
    }

    protected void btnSearchTo_Click(object sender, EventArgs e)
    {
        UpdateToView();
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;
        this.lblMessage.Visible = false;
        if (hfFromId.Value == string.Empty) 
        {
            this.lblMessage.Text = "Copy From User must select.";
            this.lblMessage.Visible = true;
            return;
        }

        if (hfToId.Value == string.Empty)
        {
            this.lblMessage.Text = "Copy To User must select.";
            this.lblMessage.Visible = true;
            return;
        }

        int fromId = int.Parse(hfFromId.Value);
        IList<int> idList = new List<int>();

        foreach(string id in hfToId.Value.Split(','))
        {
            if (fromId == int.Parse(id))
            {
                this.lblMessage.Text = "Copy from user can not equals to user.";
                this.lblMessage.Visible = true;
                return;
            }
            idList.Add(int.Parse(id));
        }

        try
        {
            if (this.rbDataSource.SelectedValue == "All")
            {
                this.TheDSService.CopyUserPermission(fromId, idList);
                this.TheDWDSService.CopyUserPermission(fromId, idList);
            }
            else if (this.rbDataSource.SelectedValue == "DS")
            {
                this.TheDSService.CopyUserPermission(fromId, idList);
            }
            else
            {
                this.TheDWDSService.CopyUserPermission(fromId, idList);
            }

            this.lblMessage.Text = "User Permission successfully copied.";
            this.lblMessage.Visible = true;
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
            this.lblMessage.Visible = true;
        }

    }

    private void UpdateFromView()
    {
        Criteria SearchCriteria = new Criteria(typeof(Dndp.Persistence.Entity.Security.User));
        if (txtFromUserName.Text.Trim() != string.Empty)
        {
            SearchCriteria.Add(Expression.Like("UserName", txtFromUserName.Text.Trim(), MatchMode.Anywhere));
        }
        IList userList = (GetService("CriteriaMgr.service") as Dndp.Service.Criteria.ICriteriaMgr).List(SearchCriteria);
        this.gvCopyFrom.DataSource = userList;
        this.gvCopyFrom.DataBind();
    }

    private void UpdateToView()
    {
        Criteria SearchCriteria = new Criteria(typeof(Dndp.Persistence.Entity.Security.User));
        if (txtToUserName.Text.Trim() != string.Empty)
        {
            SearchCriteria.Add(Expression.Like("UserName", txtToUserName.Text.Trim(), MatchMode.Anywhere));
        }
        IList userList = (GetService("CriteriaMgr.service") as Dndp.Service.Criteria.ICriteriaMgr).List(SearchCriteria);
        this.gvCopyTo.DataSource = userList;
        this.gvCopyTo.DataBind();
    }
}
