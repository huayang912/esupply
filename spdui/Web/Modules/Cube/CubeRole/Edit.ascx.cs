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

using Dndp.Web;
using Dndp.Persistence.Dao.Cube;
using Dndp.Persistence.Entity.Cube;
using Dndp.Service.Cube;
using System.Collections.Generic;

//TODO: Add other using statements here.

public partial class Modules_Cube_CubeRole_Edit : ModuleBase
{
    private const int SHOW_ASSIGNED_MEMEBER_LENTH = 100;
    public event EventHandler Back;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("CubeRole");
	
	//The entity service
	protected ICubeAuthorizationMgr TheService
    {
        get
        {
            return GetService("CubeAuthorizationMgr.service") as ICubeAuthorizationMgr;
        }
    }

    protected ICubeReleaseMgr TheCubeReleaseMgr
    {
        get
        {
            return GetService("CubeReleaseMgr.service") as ICubeReleaseMgr;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        NewCubeUser1.Back += new System.EventHandler(this.NewCubeUser1_Back);
        NewRoleDimensionMember1.Back += new System.EventHandler(this.NewRoleDimensionMember1_Back);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
    }

    protected override void InitByPermission()
    {
        base.InitByPermission();

        btnUpdateCube.Visible = PermissionUpdate;
        btnSave.Visible = PermissionUpdate;   
		
		//TODO: Add other permission init codes here.
    }

    public CubeRole TheCubeRole
    {
        get
        {
            return (CubeRole)ViewState["TheCubeRole"];
        }
        set
        {
            ViewState["TheCubeRole"] = value;
        }
    }

	//Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        txtCubeName.Text = TheCubeRole.TheCube.Description;
        txtName.Text = TheCubeRole.Name;
        txtDescription.Text = TheCubeRole.Description;
        //cbVisualtotal.Checked = (TheCubeRole.IsVisualtotal == 1);

        gvUserList.DataSource = TheCubeRole.CubeUserList;
        gvUserList.DataBind();

        //if there is duplicate dimension name and attribute name in cubedimension list, only show one in screen.
        IList<CubeDimension> cubeDimensionList = TheCubeRole.CubeDimensionList;
        gvDimensionList.DataSource = removeSameDimensionNameAndAttributeName(cubeDimensionList);
        gvDimensionList.DataBind();

        //Modified by vincent at 2007-11-09 begin

        gvSetDimensionVisualTotal.DataSource = removeSameSetDimensionName(cubeDimensionList);
        gvSetDimensionVisualTotal.DataBind();
        //Modified by vincent at 2007-11-09 end        
    }

    //Modified by vincent at 2007-11-09 begin
    IList<CubeDimension> removeSameSetDimensionName(IList<CubeDimension> cubeDimensionList)
    {
        IList<CubeDimension> resultList = new List<CubeDimension>();
        Hashtable cubeHt = new Hashtable();
        if (cubeDimensionList != null && cubeDimensionList.Count > 0)
        {
            foreach (CubeDimension dim in cubeDimensionList)
            {
                string key = dim.SetDimensionName;
                if (!cubeHt.Contains(key))
                {                    
                    cubeHt.Add(key, key);
                    resultList.Add(dim);
                }
            }
        }

        return resultList;
    }
    //Modified by vincent at 2007-11-09 end
	//Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //Event handler when user click button "Save"
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string name = txtName.Text.Trim();
        if (name.Length == 0)
        {
            lblMessage.Text = "User name cannot be empty.";
            lblMessage.Visible = true;
            return;
        }

        TheCubeRole.Name = name;
        TheCubeRole.Description = txtDescription.Text.Trim();
        TheCubeRole.IsVisualtotal = 1;
        TheCubeRole.IsDrillthroughAndLocalCube = 1;

        try
        {
            TheService.UpdateCubeRole(TheCubeRole);
            // Modified by vincent at 2007-11-09 begin
            InsertSetDimensionVisualTotal(TheCubeRole.Id, gvSetDimensionVisualTotal);
            // Modified by vincent at 2007-11-09 end
            lblMessage.Text = "Save Cube Role successfully.";
            lblMessage.Visible = true;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Save Cube Role fail. " + ex.Message;
            lblMessage.Visible = true;
        }
    }

    protected void btnUpdateCube_Click(object sender, EventArgs e)
    {
        try
        {
            TheCubeReleaseMgr.UploadRoleToCube(new int[] { TheCubeRole.Id });
            lblMessage.Text = "Update Cube Role successfully.";
            lblMessage.Visible = true;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Update Cube Role fail. " + ex.Message;
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
        }
    }

    //The event handler when user click button "Delete".
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TheService.DeleteCubeRole(TheCubeRole);
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //The event handler when user click button "Back" button on NewCubeUser page.
    void NewCubeUser1_Back(object sender, EventArgs e)
    {
        NewCubeUser1.Visible = false;
        TheCubeRole.CubeUserList = TheService.FindCubeUserByRoleId(TheCubeRole.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        NewCubeUser1.TheCubeRole = this.TheCubeRole;
        NewCubeUser1.UpdateView();
        NewCubeUser1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void btnDeleteUser_Click(object sender, EventArgs e)
    {
        TheService.DeleteCubeUserRole(TheCubeRole.Id, GetSelectIdList(gvUserList));

        //re-load the data source
        TheCubeRole.CubeUserList = TheService.FindCubeUserByRoleId(TheCubeRole.Id);

        UpdateView();
    }

    protected void btnAssignMember_Click(object sender, EventArgs e)
    {
        NewRoleDimensionMember1.TheCubeRole = this.TheCubeRole;
        NewRoleDimensionMember1.InitFilterSelect();
        NewRoleDimensionMember1.UpdateView();
        NewRoleDimensionMember1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void gvDimensionList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && TheCubeRole.CubeRoleDimensionMemberList != null && TheCubeRole.CubeRoleDimensionMemberList.Count > 0)
        {
            string memberNames = "";
            for (int i = 0; i < TheCubeRole.CubeRoleDimensionMemberList.Count; i++)
            {
                if (TheCubeRole.CubeRoleDimensionMemberList[i].TheDimension.Id == int.Parse(e.Row.Cells[0].Text))
                {
                    memberNames += TheCubeRole.CubeRoleDimensionMemberList[i].MemberName + "; ";
                }
            }

            if (memberNames.Length > SHOW_ASSIGNED_MEMEBER_LENTH)
            {
                e.Row.Cells[3].Text = memberNames.Substring(0, SHOW_ASSIGNED_MEMEBER_LENTH) + "...";
                e.Row.Cells[3].ToolTip = memberNames;
            }
            else
            {
                e.Row.Cells[3].Text = memberNames;
            }
        }
    }

    //The event handler when user click button "Back" button on NewRoleDimensionMember page.
    void NewRoleDimensionMember1_Back(object sender, EventArgs e)
    {
        NewRoleDimensionMember1.Visible = false;
        TheCubeRole.CubeRoleDimensionMemberList = TheService.FindCubeRoleDimensionMemberByRoleId(TheCubeRole.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    IList<CubeDimension> removeSameDimensionNameAndAttributeName(IList<CubeDimension> cubeDimensionList)
    {
        IList<CubeDimension> resultList = new List<CubeDimension>();
        Hashtable cubeHt = new Hashtable();
        if (cubeDimensionList != null && cubeDimensionList.Count > 0)
        {
            foreach(CubeDimension dim in cubeDimensionList)
            {
                string key = dim.DimensionName + "|" + dim.AttributeName;
                if (!cubeHt.Contains(key))
                {
                    cubeHt.Add(key, key);
                    resultList.Add(dim);
                }
            }
        }

        return resultList;
    }
    // Modified by vincent at 2007-11-09 begin    
    protected void gvSetDimensionVisualTotal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CheckBox cb = (CheckBox)e.Row.FindControl("chkVisualTotal");
        if (cb != null)
        {
            int roleid = TheCubeRole.Id;
            string setDimensionName = e.Row.Cells[1].Text;
            
            cb.Checked = TheService.GetSetDimensionVisualTotal(roleid, setDimensionName);
        }
    }

    private void InsertSetDimensionVisualTotal(int roleid, GridView gv)
    {
        TheService.DeleteSetDimensionVisualTotal(roleid);        
        foreach (GridViewRow gvr in gv.Rows)
        {
            CheckBox cb = (CheckBox)gvr.FindControl("chkVisualTotal");
            if (cb != null)
            {
                string visualtotal = cb.Checked ? "1" : "0";
                string setDimensionName = gvr.Cells[1].Text;
                TheService.InsertSetDimensionVisualTotal(roleid, setDimensionName, visualtotal);
            }
        }
    }
     

    // Modified by vincent at 2007-11-09 end
}