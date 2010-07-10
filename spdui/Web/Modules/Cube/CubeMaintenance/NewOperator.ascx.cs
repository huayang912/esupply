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
using Dndp.Service.Cube;
using Dndp.Service.Dui;
using Dndp.Persistence.Entity.Security;
using System.Collections.Generic;
using Dndp.Persistence.Entity.Cube;

public partial class Modules_Cube_CubeMaintenance_NewOperator : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("CubeMaintenance");

    //The entity service
    protected ICubeOperatorMgr TheService
    {
        get
        {
            return GetService("CubeOperatorMgr.service") as ICubeOperatorMgr;
        }
    }

    protected IDataSourceMgr TheDSService
    {
        get
        {
            return GetService("DataSourceMgr.service") as IDataSourceMgr;
        }
    }

    public IList<CubeOperator> TheCubeOperators
    {
        get
        {
            return (IList<CubeOperator>)ViewState["TheCubeOperators"];
        }
        set
        {
            ViewState["TheCubeOperators"] = value;
        }
    }

    protected bool hasPermission(int userId, string type)
    {
        if (TheCubeOperators != null)
        {
            for (int i = 0; i < TheCubeOperators.Count; i++)
            {
                if (TheCubeOperators[i].AllowType.Equals(type)
                    && TheCubeOperators[i].TheUser.Id == userId)
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
        if (type.Equals("Process"))
        {
            foreach (GridViewRow row in gvOWNER.Rows)
            {
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
                if (cbSelect.Checked)
                {                    
                    userIdList.Add((int)(gvOWNER.DataKeys[row.RowIndex].Value));
                }
            }
            TheService.UpdateCubeOperator(userIdList, int.Parse(txtCubeId.Value), "Process");
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
            TheService.UpdateCubeOperator(userIdList, int.Parse(txtCubeId.Value), "Release");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
    }

    public void UpdateView()
    {
      gvOWNER.DataSource = TheDSService.FindUserByRole(7000);
      gvOWNER.DataBind();
      gvOWNER.Visible = true;
      gvETL.Visible = false;
      rblType.SelectedIndex = 0;
    }

    public void SetCubeId(int cubeId)
    {
        txtCubeId.Value = cubeId.ToString();
    }

    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cubeId = int.Parse(txtCubeId.Value);
        string type = rblType.SelectedValue;
        if (type.Equals("Process"))
        {
            TheCubeOperators = TheService.FindOperatorByCubeIdAndAllowType(cubeId, "Process");
            gvOWNER.DataSource = TheDSService.FindUserByRole(7000);
            gvOWNER.DataBind();
            gvOWNER.Visible = true;
            gvETL.Visible = false;
        }
        else
        {
            TheCubeOperators = TheService.FindOperatorByCubeIdAndAllowType(cubeId, "Release");
            gvETL.DataSource = TheDSService.FindUserByRole(8000);
            gvETL.DataBind();
            gvETL.Visible = true;
            gvOWNER.Visible = false;
        }
    }
}
