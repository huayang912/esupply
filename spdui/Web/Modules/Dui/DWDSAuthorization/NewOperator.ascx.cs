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

public partial class Modules_Dui_DWDSAuthorization_NewOperator : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("DWDSMaintenance");

    //The entity service
    protected IDWDataSourceMgr TheService
    {
        get
        {
            return GetService("DWDataSourceMgr.service") as IDWDataSourceMgr;
        }
    }

    public IList<DWDataSourceOperator> TheDWDataSourceOperators
    {
        get
        {
            return (IList<DWDataSourceOperator>)ViewState["TheDWDataSourceOperators"];
        }
        set
        {
            ViewState["TheDWDataSourceOperators"] = value;
        }
    }

    protected bool hasPermission(int userId, string type)
    {
        if (TheDWDataSourceOperators != null)
        {
            for (int i = 0; i < TheDWDataSourceOperators.Count; i++)
            {
                if (TheDWDataSourceOperators[i].AllowType.Equals(type)
                    && TheDWDataSourceOperators[i].TheUser.Id == userId)
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
        if (type.Equals(DWDataSourceOperator.OPERATOR_VIEWER_Value))
        {
            foreach (GridViewRow row in gvOWNER.Rows)
            {
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
                if (cbSelect.Checked)
                {
                    userIdList.Add((int)(gvOWNER.DataKeys[row.RowIndex].Value));
                }
            }
            TheService.UpdateDWDataSourceOperator(userIdList, int.Parse(txtDsId.Value), DWDataSourceOperator.OPERATOR_VIEWER_Value);
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
            TheService.UpdateDWDataSourceOperator(userIdList, int.Parse(txtDsId.Value), DWDataSourceOperator.OPERATOR_ADMIN_Value);
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
        if (type.Equals("VIEWER"))
        {
            TheDWDataSourceOperators = TheService.FindDWOperatorByDSIdAndAllowType(dsId, "VIEWER");
            gvOWNER.DataSource = TheService.FindUserByRole(2000);
            gvOWNER.DataBind();
            gvOWNER.Visible = true;
            gvETL.Visible = false;
        }
        else
        {
            TheDWDataSourceOperators = TheService.FindDWOperatorByDSIdAndAllowType(dsId, "ADMIN");
            gvETL.DataSource = TheService.FindUserByRole(3000);
            gvETL.DataBind();
            gvETL.Visible = true;
            gvOWNER.Visible = false;
        }
    }
}

