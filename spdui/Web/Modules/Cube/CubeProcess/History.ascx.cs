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

public partial class Modules_Cube_CubeProcess_History : ModuleBase
{
	public event EventHandler Back;
	
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

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Edit1.Back += new EventHandler(Edit1_Back);
    }

    void Edit1_Back(object sender, EventArgs e)
    {
        Edit1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

	protected void Page_Load(object sender, EventArgs e)
	{
		//Add code for Page_Load here.
	}
	
	//The event handler when user click button "Submit"
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		//Check input

		//Perform "New" action.
		
		//Show feedback message to user.
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
        IList<CubeProcess> result = TheService.FindAllCubeProcessByCubeId(TheCube.Id, CurrentUser.Id) as IList<CubeProcess>;
        txtCubeName.Text = TheCube.Description;
        gvHistory.DataSource = result;
        gvHistory.DataBind();
    }

    protected void lbtnEditProcess_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        CubeProcess process = TheService.FindCubeProcessWithAllInfoById(Id);        
        Edit1.TheCubeProcess = process;
        Edit1.Editable = (process.Id == TheCube.TheLastestCubeProcess.Id);
        Edit1.Visible = true;
        Edit1.UpdateView();
        pnlMain.Visible = false;
    }

    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistory.PageIndex = e.NewPageIndex;
        UpdateView();
    }
}