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

public partial class Modules_Cube_CubeRoleDimensionMember_New : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("CubeRoleDimensionMember");

    //The entity service
    protected ICubeAuthorizationMgr TheService
    {
        get
        {
            return GetService("CubeAuthorizationMgr.service") as ICubeAuthorizationMgr;
        }
    }

    protected ICubeDimensionMgr TheDimensionService
    {
        get
        {
            return GetService("CubeDimensionMgr.service") as ICubeDimensionMgr;
        }
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

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //The event handler when user click button "Update"
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        IList<CubeRoleDimensionMember> list = GetAllSelectMember();

        TheService.UpdateCubeRoleDimensionMember(TheCubeRole, list, ddlDimensionName.SelectedValue, ddlAttributeName.SelectedValue);

        //TheService.UploadRoleToCube(TheCubeRole);

        TheCubeRole.CubeRoleDimensionMemberList = TheService.FindCubeRoleDimensionMemberByRoleId(TheCubeRole.Id);
    }

    //The event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void ddlDimensionName_Change(object sender, EventArgs e)
    {
        //SynchronizeSelectMember();
        InitAttributeSelect();
        UpdateView();
        ChangeRepeatColumns(int.Parse(ddlColumns.SelectedValue));
    }

    protected void ddlAttributeName_Change(object sender, EventArgs e)
    {
        //SynchronizeSelectMember();
        UpdateView();
        ChangeRepeatColumns(int.Parse(ddlColumns.SelectedValue));
    }

    protected void ddlColumns_Change(object sender, EventArgs e)
    {
        ChangeRepeatColumns(int.Parse(ddlColumns.SelectedValue));
    }

    //The public method to clear the view
    public void UpdateView()
    {
        IList<CubeDimension> result = new List<CubeDimension>();
        bool selectVisualtotal = false;
        string selectedDim = ddlDimensionName.SelectedValue;
        string selectedAtt = ddlAttributeName.SelectedValue;
        if (TheCubeRole.CubeDimensionList != null)
        {
            foreach (CubeDimension dim in TheCubeRole.CubeDimensionList)
            {
                if (dim.DimensionName == selectedDim
                    && dim.AttributeName == selectedAtt)
                {
                    result.Add(dim);
                    if (TheCubeRole.CubeRoleDimensionMemberList != null)
                    {
                        foreach (CubeRoleDimensionMember member in TheCubeRole.CubeRoleDimensionMemberList)
                        {
                            if (member.TheDimension.DimensionName == selectedDim
                                && member.TheDimension.AttributeName == selectedAtt)
                            {
                                if (member.IsVisualtotal == 1)
                                {
                                    selectVisualtotal = true;
                                }
                                break;
                            }
                        }
                    }
                }
                else
                {
                    selectVisualtotal = true;
                }
            }
        }
        else
        {
            selectVisualtotal = true;
        }

        gvDimensionList.DataSource = result;
        gvDimensionList.DataBind();

        hfSelectedDimensionName.Value = ddlDimensionName.SelectedValue;
        hfSelectedAttributeName.Value = ddlAttributeName.SelectedValue;

        cbVisualtotal.Checked = selectVisualtotal;
    }

    protected void gvDimensionList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex > 0)
            {
                e.Row.Visible = false;
            }
            else
            {
                CheckBoxList cbl = (CheckBoxList)e.Row.FindControl("cbMemberSelect");
                string dimensionName = e.Row.Cells[1].Text;
                string attributeName = e.Row.Cells[2].Text;
                cbl.DataSource = TheService.GetDimensionMembers(TheCubeRole.TheCube, dimensionName, attributeName);
                cbl.DataBind();

                for (int i = 0; i < cbl.Items.Count; i++)
                {
                    ListItem li = cbl.Items[i];
                    if (li.Text == "Deselect All")
                    {
                        li.Selected = false;
                    }
                    else if (isDimensionMemberSeleted(dimensionName, attributeName, li.Value))
                    {
                        li.Selected = true;
                    }
                }
            }
        }
    }

    private bool isDimensionMemberSeleted(string dimensionName, string attributeName, string memberValue)
    {
        if (TheCubeRole == null || TheCubeRole.CubeRoleDimensionMemberList == null || TheCubeRole.CubeRoleDimensionMemberList.Count == 0)
        {
            return false;
        }

        foreach (CubeRoleDimensionMember cubeRoleDimensionMember in TheCubeRole.CubeRoleDimensionMemberList)
        {
            if (cubeRoleDimensionMember.TheDimension.DimensionName == dimensionName
                && cubeRoleDimensionMember.TheDimension.AttributeName == attributeName)
            {
                if (cubeRoleDimensionMember.MemberName == "Select All")
                {
                    return true;
                }

                if (cubeRoleDimensionMember.MemberValue == memberValue)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void InitFilterSelect()
    {
        InitDimensionSelect();

        InitAttributeSelect();

        ddlColumns.SelectedIndex = 2;

        ChangeRepeatColumns(5);
    }

    private void InitDimensionSelect()
    {
        IList<CubeDimension> selectDimList = new List<CubeDimension>();
        Hashtable dimHT = new Hashtable();
        foreach (CubeDimension dim in TheCubeRole.CubeDimensionList)
        {
            if (!dimHT.ContainsKey(dim.DimensionName))
            {
                dimHT.Add(dim.DimensionName, dim.DimensionName);
                selectDimList.Add(dim);
            }
        }

        ddlDimensionName.DataSource = selectDimList;
        ddlDimensionName.DataBind();
    }

    private void InitAttributeSelect()
    {
        IList<CubeDimension> selectAttList = new List<CubeDimension>();
        Hashtable attHT = new Hashtable();
        foreach (CubeDimension dim in TheCubeRole.CubeDimensionList)
        {
            if (dim.DimensionName == ddlDimensionName.SelectedValue
                && !attHT.ContainsKey(dim.AttributeName))
            {
                attHT.Add(dim.AttributeName, dim.AttributeName);
                selectAttList.Add(dim);
            }
        }

        ddlAttributeName.DataSource = selectAttList;
        ddlAttributeName.DataBind();
    }

    private void ChangeRepeatColumns(int RepeatColumns)
    {
        for (int i = 0; i < gvDimensionList.Rows.Count; i++)
        {
            ((CheckBoxList)gvDimensionList.Rows[i].FindControl("cbMemberSelect")).RepeatColumns = RepeatColumns;
        }
    }

    private void SynchronizeSelectMember()
    {
        IList<CubeRoleDimensionMember> selectedMember = GetAllSelectMember();

        string selectedAttributeName = hfSelectedAttributeName.Value;
        string selectedDimensionName = hfSelectedDimensionName.Value;

        //Select dimension member in CubeRoleDimensionMemberList
        IList<CubeRoleDimensionMember> selectMemberList = new List<CubeRoleDimensionMember>();
        if (TheCubeRole.CubeRoleDimensionMemberList != null
            && TheCubeRole.CubeRoleDimensionMemberList.Count > 0)
        {
            foreach (CubeRoleDimensionMember member in TheCubeRole.CubeRoleDimensionMemberList)
            {
                if (!(member.TheDimension.DimensionName == selectedDimensionName
                    && member.TheDimension.AttributeName == selectedAttributeName))
                {
                    selectMemberList.Add(member);
                }
            }
        }

        if (selectedMember != null && selectedMember.Count > 0)
        {
            foreach (CubeRoleDimensionMember member in selectedMember)
            {
                selectMemberList.Add(member);
            }
        }

        TheCubeRole.CubeRoleDimensionMemberList = selectMemberList;
    }

    private IList<CubeRoleDimensionMember> GetAllSelectMember()
    {
        IList<CubeRoleDimensionMember> list = new List<CubeRoleDimensionMember>();
        for (int i = 0; i < gvDimensionList.Rows.Count; i++)
        {
            GridViewRow gvr = gvDimensionList.Rows[i];
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                CheckBoxList cbl = (CheckBoxList)gvr.FindControl("cbMemberSelect");

                int dimensionId = int.Parse(gvr.Cells[0].Text);
                string dimensionName = gvr.Cells[1].Text;
                string attributeName = gvr.Cells[2].Text;
                CubeDimension dimension = TheDimensionService.LoadCubeDimension(dimensionId);

                bool isSelectAll = false;
                bool deSelectAll = false;

                for (int j = 0; j < cbl.Items.Count; j++)
                {
                    ListItem li = cbl.Items[j];
                    if (li.Text == "Select All" && li.Selected)
                    {
                        isSelectAll = true;
                    }

                    if (li.Text == "Deselect All" && li.Selected && !isSelectAll)
                    {
                        deSelectAll = true;
                    }

                    if ((li.Selected || isSelectAll) && !deSelectAll && li.Text != "Deselect All")
                    {

                        if (li.Text == "Select All" || !isSelectAll)
                        {
                            CubeRoleDimensionMember member = new CubeRoleDimensionMember();
                            member.TheCubeRole = this.TheCubeRole;
                            member.TheDimension = dimension;
                            //member.MemberId = li.Value.Split(SEPERATE_SIGN.ToCharArray())[0];
                            member.MemberName = li.Text;
                            member.MemberValue = li.Value;
                            member.IsVisualtotal = cbVisualtotal.Checked ? 1 : 0;

                            list.Add(member);
                        }

                        li.Selected = true;
                    }
                    else
                    {
                        li.Selected = false;
                    }
                }
            }
        }

        return list;
    }
}