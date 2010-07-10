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
using Dndp.Persistence.Entity.Dui;
using Dndp.Service.Cube;
using Dndp.Persistence.Entity.Cube;

public partial class Modules_Cube_CubeMaintenance_NewMDX : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("CubeMaintenance");

    //The entity service
    protected ICubeWarmMDXMgr TheService
    {
        get
        {
            return GetService("CubeWarmMDXMgr.service") as ICubeWarmMDXMgr;
        }
    }

    protected ICubeMgr TheCubeService
    {
        get
        {
            return GetService("CubeMgr.service") as ICubeMgr;
        }
    }

    public CubeWarmMDX TheCubeWarmMDX
    {
        get
        {
            return (CubeWarmMDX)ViewState["TheCubeWarmMDX"];
        }
        set
        {
            ViewState["TheCubeWarmMDX"] = value;
        }
    }

    //Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    public void SetCubeId(int cubeId)
    {
        txtCubeId.Value = cubeId.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Add code for Page_Load here.
        if (!IsPostBack)
        {
            UpdateView();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveMDX();
    }

    protected void btnSubmitContinue_Click(object sender, EventArgs e)
    {
        SaveMDX();
        TheCubeWarmMDX = null;
        UpdateView();
    }

    protected void SaveMDX()
    {
        if (txtSequenceNo.Text.Trim().Length == 0)
        {
            lblMessage.Text = "Sequence No must fill";
            lblMessage.Visible = true;
            return;
        }

        if (txtDescription.Text.Trim().Length == 0)
        {
            lblMessage.Text = "Description must fill";
            lblMessage.Visible = true;
            return;
        }

        try
        {
            Int32.Parse(txtSequenceNo.Text);
        }
        catch (FormatException ex)
        {
            lblMessage.Text = "Sequence No must be an integer";
            lblMessage.Visible = true;
            return;
        }

        if (TheCubeWarmMDX == null)
        {
            TheCubeWarmMDX = new CubeWarmMDX();
            TheCubeWarmMDX.TheCube = TheCubeService.LoadCube(int.Parse(txtCubeId.Value));
            TheCubeWarmMDX.ActiveFlag = 1;
        }
        TheCubeWarmMDX.SequenceNo = Int32.Parse(txtSequenceNo.Text);
        TheCubeWarmMDX.Description = txtDescription.Text;
        TheCubeWarmMDX.MDXStatement = txtMDXStatement.Text;

        if (TheCubeWarmMDX == null || TheCubeWarmMDX.Id == 0)
        {
            TheService.CreateCubeWarmMDX(TheCubeWarmMDX);
        }
        else
        {
            TheService.UpdateCubeWarmMDX(TheCubeWarmMDX);
        }    
    }

    public void UpdateView()
    {
        if (TheCubeWarmMDX == null || TheCubeWarmMDX.Id == 0)
        {
            txtSequenceNo.Text = String.Empty;
            txtDescription.Text = String.Empty;
            txtMDXStatement.Text = String.Empty;
            txtCubeMDXId.Value = String.Empty;            
        }
        else
        {
            txtCubeMDXId.Value = TheCubeWarmMDX.Id.ToString();
            txtDescription.Text = TheCubeWarmMDX.Description;
            txtSequenceNo.Text = TheCubeWarmMDX.SequenceNo.ToString();
            txtMDXStatement.Text = TheCubeWarmMDX.MDXStatement;
        }
        lblMessage.Text = String.Empty;
        lblMessage.Visible = false;
    }
}
