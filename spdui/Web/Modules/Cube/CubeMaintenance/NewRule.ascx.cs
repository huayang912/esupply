using System;
using System.Collections;
using System.Collections.Generic;
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
using Dndp.Persistence.Entity.Cube;

public partial class Modules_Cube_CubeMaintenance_NewRule : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("CubeMaintenance");

    //The entity service
    protected ICubeMgr TheService
    {
        get
        {
            return GetService("CubeMgr.service") as ICubeMgr;
        }
    }

    protected ICubeValidationRuleMgr TheRuleService
    {
        get
        {
            return GetService("CubeValidationRuleMgr.service") as ICubeValidationRuleMgr;
        }
    }

    public CubeValidationRule TheCubeValidationRule
    {
        get
        {
            return (CubeValidationRule)ViewState["TheCubeValidationRule"];
        }
        set
        {
            ViewState["TheCubeValidationRule"] = value;
        }
    }

    protected ICubeParameterMgr TheParameterService
    {
        get
        {
            return GetService("CubeParameterMgr.service") as ICubeParameterMgr;
        }
    }

    public void SetCubeId(int cubeId)
    {
        txtCubeId.Value = cubeId.ToString();
    }

    //Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Add code for Page_Load here.
        if (!IsPostBack)
        {
            InitDDL();
            UpdateView();
        }
    }

    private void InitDDL()
    {
        //Get all parameters
        IList<CubeParameter> parameters = TheParameterService.LoadAllActiveCubeParameter();
        ddlParameter.DataSource = parameters;
        ddlParameter.DataTextField = "Name";
        ddlParameter.DataValueField = "Name";
        ddlParameter.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveRule();
        UpdateView();
    }

    protected void btnSubmitContinue_Click(object sender, EventArgs e)
    {
        SaveRule();
        TheCubeValidationRule = null;
        UpdateView();
    }

    protected void SaveRule()
    {
        if (TheCubeValidationRule == null)
        {
            TheCubeValidationRule = new CubeValidationRule();
            TheCubeValidationRule.CreateUser = CurrentUser;
            TheCubeValidationRule.CreateDate = System.DateTime.Now;
            TheCubeValidationRule.TheCube = TheService.LoadCube(int.Parse(txtCubeId.Value));
        }            
        TheCubeValidationRule.Description = txtRuleDescription.Text;
        TheCubeValidationRule.UpdateUser = CurrentUser;
        TheCubeValidationRule.UpdateDate = System.DateTime.Now;
        TheCubeValidationRule.Name = txtRuleName.Text;
        TheCubeValidationRule.Content = txtRuleContent.Text;
        TheCubeValidationRule.UpdateContent = txtUpdateSQLContent.Text;
        TheCubeValidationRule.Type = ddlRuleType.SelectedItem.Value;
        TheCubeValidationRule.ActiveFlag = 1;
        if (rdoValidationTargart_SPDW.Checked)
        {
            TheCubeValidationRule.ValidationTarget = rdoValidationTargart_SPDW.Text;
        }
        else
        {
            TheCubeValidationRule.ValidationTarget = rdoValidationTargart_CubeDB.Text;
        }   

        if (TheCubeValidationRule == null || TheCubeValidationRule.Id == 0)
        {
            TheRuleService.CreateCubeValidationRule(TheCubeValidationRule);
        }
        else
        {
            TheRuleService.UpdateCubeValidationRule(TheCubeValidationRule);
        }         
    }

    public void UpdateView()
    {
        txtCubeRuleId.Value = (TheCubeValidationRule != null && TheCubeValidationRule.Id != 0) ? TheCubeValidationRule.Id.ToString() : "";
        txtRuleName.Text = (TheCubeValidationRule != null && TheCubeValidationRule.Id != 0) ? TheCubeValidationRule.Name : "";
        txtRuleDescription.Text = (TheCubeValidationRule != null && TheCubeValidationRule.Id != 0) ? TheCubeValidationRule.Description : "";
        txtRuleContent.Text = (TheCubeValidationRule != null && TheCubeValidationRule.Id != 0) ? TheCubeValidationRule.Content : "";
        if (TheCubeValidationRule != null && TheCubeValidationRule.Id != 0)
        {
            ddlRuleType.Text = TheCubeValidationRule.Type;
        }
        txtUpdateSQLContent.Text = (TheCubeValidationRule != null && TheCubeValidationRule.Id != 0) ? TheCubeValidationRule.UpdateContent : "";
        lCreateBy.Text = (TheCubeValidationRule != null && TheCubeValidationRule.Id != 0) ? TheCubeValidationRule.CreateUser.UserName : "";
        lCreateDate.Text = (TheCubeValidationRule != null && TheCubeValidationRule.Id != 0) ? TheCubeValidationRule.CreateDate.ToString() : "";
        lLastUpdateBy.Text = (TheCubeValidationRule != null && TheCubeValidationRule.Id != 0) ? TheCubeValidationRule.UpdateUser.UserName : "";
        lLastUpdateDate.Text = (TheCubeValidationRule != null && TheCubeValidationRule.Id != 0) ? TheCubeValidationRule.UpdateDate.ToString() : "";
        if (TheCubeValidationRule != null && TheCubeValidationRule.Id != 0
            && TheCubeValidationRule.ValidationTarget == rdoValidationTargart_CubeDB.Text)
        {
            rdoValidationTargart_CubeDB.Checked = true;
        }
        else
        {
            rdoValidationTargart_SPDW.Checked = true;
        }    
    }

    protected void btnInsertParameter_Click(object sender, EventArgs e)
    {
        txtRuleContent.Text = txtRuleContent.Text + Dndp.Utility.DataParameterHelper.GetParameterPlaceholder(ddlParameter.SelectedValue);
    }
}
