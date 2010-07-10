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
using System.Collections.Generic;

using log4net;

using Dndp.Web;
using Dndp.Persistence.Dao.Cube;
using Dndp.Persistence.Entity.Cube;
using Dndp.Service.Cube;

//TODO:Add other using statements here.

public partial class Modules_Cube_CubeProcess_New : ModuleBase
{
	public event EventHandler Back;
    public event EventHandler Submit;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("CubeProcess");
	
	//The entity service
	protected ICubeProcessMgr TheService
    {
        get
        {
            return GetService("CubeProcessMgr.service") as ICubeProcessMgr;
        }
    }

    protected ICubeDefinedParameterMgr TheParameterService
    {
        get
        {
            return GetService("CubeDefinedParameterMgr.service") as ICubeDefinedParameterMgr;
        }
    }

    public CubeDefinition TheCube
    {
        get
        {
            return (CubeDefinition)ViewState["TheCube"];
        }
        set
        {
            ViewState["TheCube"] = value;
        }
    }

    private CubeProcess _newCubeProcess;
    public CubeProcess NewCubeProcess
    {
        get
        {
            return _newCubeProcess;
        }
        set
        {
            _newCubeProcess = value;
        }
    }

	protected void Page_Load(object sender, EventArgs e)
	{
		//Add code for Page_Load here.
	}
	
	//The event handler when user click button "Submit"
    protected void btnSave_Click(object sender, EventArgs e)
	{
        Hashtable ht = new Hashtable();
        if (gvParameterList.Rows.Count > 0)
        {            
            for (int i = 0; i < gvParameterList.Rows.Count; i++)
            {
                GridViewRow gvr = gvParameterList.Rows[i];
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    TextBox tb = (TextBox)gvr.FindControl("txtParameterValue");
                    if (tb.Text.Trim().Length == 0)
                    {
                        lblMessage.Text = "Parameter Value must enter";
                        lblMessage.Visible = true;
                        return;
                    }
                    else
                    {
                        Label l = (Label)gvr.FindControl("lblParameterId");
                        ht.Add(l.Text, tb.Text);                        
                    }
                }
            }
        }

        this.NewCubeProcess = TheService.CreateNewCubeProcess(TheCube, CurrentUser, ht, txtProcessDescription.Text.Trim());

        if (Submit != null)
        {
            Submit(this, null);
        }
	}

	//The event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //The public method to clear the view
	public void UpdateView()
    {
        lblCubeDescription.Text = TheCube.Description;
        lblCubeName.Text = TheCube.ProcessCubeName;
        lblProcessServerAddr.Text = TheCube.ProcessServerAddr;
        lblProcessDatabaseName.Text = TheCube.ProcessDatabaseName;
        lblMessage.Text = string.Empty;
        lblMessage.Visible = false;
        
        //txtProcessDescription.Text = string.Empty;
        txtProcessDescription.Text = "Cube has been updated on " + DateTime.Now.ToString("yyyy-MM-dd") + "";

        gvParameterList.DataSource = TheCube.CubeDefinedParameterList;
        gvParameterList.DataBind();
    }

    //Modified By vincent at 2007-11-8 begin    
    protected void gvParameterList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        DateTime dt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-1"));
        TextBox tb = (TextBox)e.Row.FindControl("txtParameterValue");
        if (tb != null)
        {
            tb.Text = dt.AddMonths(-1).ToString("yyyy-MM-dd");
        }
    }
    //Modified By vincent at 2007-11-8 end
}