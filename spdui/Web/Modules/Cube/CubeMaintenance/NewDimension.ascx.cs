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

public partial class Modules_Cube_CubeMaintenance_NewDimension : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("CubeMaintenance");

    //The entity service
    protected ICubeDimensionMgr TheService
    {
        get
        {
            return GetService("CubeDimensionMgr.service") as ICubeDimensionMgr;
        }
    }

    protected ICubeMgr TheCubeService
    {
        get
        {
            return GetService("CubeMgr.service") as ICubeMgr;
        }
    }

    public CubeDimension TheCubeDimension
    {
        get
        {
            return (CubeDimension)ViewState["TheCubeDimension"];
        }
        set
        {
            ViewState["TheCubeDimension"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
        {
            InitDDL();
            //UpdateView();
        }
    }

    private void InitDDL()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Text");
        dt.Columns.Add("Value");

        DataRow row1 = dt.NewRow();
        dt.Rows.Add(row1);
        dt.Rows[0]["Text"] = CubeDimension.Dimension_Data_Place_Holder;
        dt.Rows[0]["Value"] = CubeDimension.Dimension_Data_Place_Holder;

        DataRow row2 = dt.NewRow();
        dt.Rows.Add(row2);
        dt.Rows[1]["Text"] = CubeDimension.Related_Dimension_Data_Place_Holder;
        dt.Rows[1]["Value"] = CubeDimension.Related_Dimension_Data_Place_Holder;

        ddlParameter.DataSource = dt;
        ddlParameter.DataBind();

        ddlRelatedParameter.DataSource = dt;
        ddlRelatedParameter.DataBind();
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveDimension();
        //this.Back(this, e);
    }

    protected void btnSubmitContinue_Click(object sender, EventArgs e)
    {
        SaveDimension();
        TheCubeDimension = null;
        UpdateView();
    }

    protected void SaveDimension()
    {
        // Modified by vincent at 2007-11-08 begin

        if (TheCubeDimension == null)
        {
            TheCubeDimension = new CubeDimension();

            TheCubeDimension.DimensionName = txtDimensionName.Text;
            TheCubeDimension.AttributeName = txtAttributeName.Text;
            TheCubeDimension.SetDimensionName = txtSetDimensionName.Text;
            TheCubeDimension.SetAttributeName = txtSetAttributeName.Text;
            TheCubeDimension.RelatedDimensionName = txtRelatedDimensionName.Text;
            TheCubeDimension.RelatedAttributeName = txtRelatedAttributeName.Text;

            CubeDefinition cube = TheCubeService.LoadCube(int.Parse(txtCubeId.Value));
            TheCubeDimension.TheCube = cube;
        }
        else
        {
            TheCubeDimension.DimensionName = txtDimensionName.Text;
            TheCubeDimension.AttributeName = txtAttributeName.Text;
            TheCubeDimension.SetDimensionName = txtSetDimensionName.Text;
            TheCubeDimension.SetAttributeName = txtSetAttributeName.Text;
            TheCubeDimension.RelatedDimensionName = txtRelatedDimensionName.Text;
            TheCubeDimension.RelatedAttributeName = txtRelatedAttributeName.Text;
        }

        // Modified by vincent at 2007-11-08 End
        
        TheCubeDimension.MDXFormula = txtMDXFormula.Text;
        TheCubeDimension.RelatedMDXFormula = txtRelatedMDXFormula.Text;

        if (TheCubeDimension.Id == 0)
        {
            TheService.CreateCubeDimension(TheCubeDimension);
        }
        else
        {
            TheService.UpdateCubeDimension(TheCubeDimension);
        }
        
    }

    public void UpdateView()
    {
        if (TheCubeDimension == null)
        {
            txtDimensionName.Text = String.Empty;
            txtAttributeName.Text = String.Empty;
            txtSetDimensionName.Text = String.Empty;
            txtSetAttributeName.Text = String.Empty;
            txtRelatedDimensionName.Text = String.Empty;
            txtRelatedAttributeName.Text = String.Empty;
            txtMDXFormula.Text = String.Empty;
            txtRelatedMDXFormula.Text = String.Empty;
            
            txtDimensionName.ReadOnly = false;
            txtAttributeName.ReadOnly = false;
            txtSetDimensionName.ReadOnly = false;
            txtSetAttributeName.ReadOnly = false;
            txtRelatedDimensionName.ReadOnly = false;
            txtRelatedAttributeName.ReadOnly = false;
            
        }
        else
        {
            txtDimensionName.Text = TheCubeDimension.DimensionName;
            
            txtAttributeName.Text = TheCubeDimension.AttributeName;
            
            txtSetDimensionName.Text = TheCubeDimension.SetDimensionName;
            
            txtSetAttributeName.Text = TheCubeDimension.SetAttributeName;
            
            txtRelatedDimensionName.Text = TheCubeDimension.RelatedDimensionName;
            
            txtRelatedAttributeName.Text = TheCubeDimension.RelatedAttributeName;
            
            // Modified by vincent at 2007-11-08 begin
            txtDimensionName.ReadOnly = false;
            txtAttributeName.ReadOnly = false;
            txtSetDimensionName.ReadOnly = false;
            txtSetAttributeName.ReadOnly = false;
            txtRelatedDimensionName.ReadOnly = false;
            txtRelatedAttributeName.ReadOnly = false;
            // Modified by vincent at 2007-11-08 begin
            txtMDXFormula.Text = TheCubeDimension.MDXFormula;
            txtRelatedMDXFormula.Text = TheCubeDimension.RelatedMDXFormula;
        }
    }

    protected void btnInsertParameter_Click(object sender, EventArgs e)
    {
        txtMDXFormula.Text = txtMDXFormula.Text + Dndp.Utility.DataParameterHelper.GetParameterPlaceholder(ddlParameter.SelectedValue);
    }

    protected void btnInsertRelatedParameter_Click(object sender, EventArgs e)
    {
        txtRelatedMDXFormula.Text = txtRelatedMDXFormula.Text + Dndp.Utility.DataParameterHelper.GetParameterPlaceholder(ddlRelatedParameter.SelectedValue);
    }
}
