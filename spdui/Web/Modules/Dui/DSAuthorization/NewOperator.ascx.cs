using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Mobile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Dndp.Web;
using log4net;
using Dndp.Service.Dui;
using Dndp.Persistence.Entity.Security;
using System.Collections.Generic;
using Dndp.Persistence.Entity.Dui;

public partial class Modules_Dui_DSAuthorization_NewOperator : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("DSMaintenance");

    //The entity service
    protected IDataSourceMgr TheService
    {
        get
        {
            return GetService("DataSourceMgr.service") as IDataSourceMgr;
        }
    }

    public IList<DataSourceOperator> TheDataSourceOperators
    {
        get
        {
            return (IList<DataSourceOperator>)ViewState["TheDataSourceOperators"];
        }
        set
        {
            ViewState["TheDataSourceOperators"] = value;
        }
    }

    protected bool hasPermission(int userId, string type)
    {
        if (TheDataSourceOperators != null)
        {
            for (int i = 0; i < TheDataSourceOperators.Count; i++)
            {
                if (TheDataSourceOperators[i].AllowType.Equals(type)
                    && TheDataSourceOperators[i].TheUser.Id == userId)
                {
                    return true;
                }
            }
        }

        return false;
    }

    //Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string type = rblType.SelectedValue;
        IList<int> userIdList = new List<int>();
        if (type.Equals("OWNER"))
        {
            foreach (GridViewRow row in gvOWNER.Rows)
            {
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
                if (cbSelect.Checked)
                {
                    userIdList.Add((int)(gvOWNER.DataKeys[row.RowIndex].Value));
                }
            }
            TheService.UpdateDataSourceOperator(userIdList, int.Parse(txtDsId.Value), "OWNER");
        }
        else
        {
            foreach (GridViewRow row in gvETL.Rows)
            {
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
                if (cbSelect.Checked)
                {
                    userIdList.Add((int)(gvETL.DataKeys[row.RowIndex].Value));
                }
            }
            TheService.UpdateDataSourceOperator(userIdList, int.Parse(txtDsId.Value), "ETL");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void UpdateView()
    {
        gvOWNER.DataSource = TheService.FindUserByRole(2000);
        gvOWNER.DataBind();
        gvOWNER.Visible = true;
        gvETL.Visible = false;
        rblType.SelectedIndex = 0;
    }

    public void SetDataSourceId(int dsId)
    {
        txtDsId.Value = dsId.ToString();
    }

    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int dsId = int.Parse(txtDsId.Value);
        string type = rblType.SelectedValue;
        if (type.Equals("OWNER"))
        {
            TheDataSourceOperators = TheService.FindOperatorByDSIdAndAllowType(dsId, "OWNER");
            gvOWNER.DataSource = TheService.FindUserByRole(2000);
            gvOWNER.DataBind();
            gvOWNER.Visible = true;
            gvETL.Visible = false;
        }
        else
        {
            TheDataSourceOperators = TheService.FindOperatorByDSIdAndAllowType(dsId, "ETL");
            gvETL.DataSource = TheService.FindUserByRole(3000);
            gvETL.DataBind();
            gvETL.Visible = true;
            gvOWNER.Visible = false;
        }
    }
}
