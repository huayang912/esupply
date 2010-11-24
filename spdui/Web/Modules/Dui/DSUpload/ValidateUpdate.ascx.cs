using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using log4net;

using Dndp.Web;
using Dndp.Persistence.Dao.Dui;
using Dndp.Persistence.Entity.Dui;
using Dndp.Service.Dui;
using System.Text;
using System.IO;

//TODO: Add other using statements here.

public partial class Modules_Dui_DSUpload_ValidateUpdate : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("DSUpload");

    //The entity service
    protected IDataSourceUploadMgr TheService
    {
        get
        {
            return GetService("DataSourceUploadMgr.service") as IDataSourceUploadMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //TODO: Add code for Page_Load here.
    }

    public ValidationResult TheValidationResult
    {
        get
        {
            return (ValidationResult)ViewState["TheValidationResult"];
        }
        set
        {
            ViewState["TheValidationResult"] = value;
        }
    }

    //Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        gvValidationUpdate.DataSource = TheService.FindValidateUpdateResult(TheValidationResult);
        gvValidationUpdate.DataBind();
    }

    //Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void gvValidationUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvValidationUpdate.PageIndex = e.NewPageIndex;
        UpdateView();
    }
    protected void gvValidationUpdate_DataBinding(object sender, EventArgs e)
    {

    }
    protected void gvValidationUpdate_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvValidationUpdate.EditIndex = e.NewEditIndex;
        UpdateView();
    }
    protected void gvValidationUpdate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvValidationUpdate.EditIndex = -1;
        UpdateView();
    }
    protected void gvValidationUpdate_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvValidationUpdate.Rows[e.RowIndex];
        Hashtable updFieldTable = new Hashtable();
        for (int i = 1; i < row.Cells.Count; i++)
        {
            if (!((DataControlFieldCell)(row.Cells[i])).ContainingField.HeaderText.ToUpper().Equals("REC_ID"))
            {
                TextBox tBox = row.Cells[i].Controls[0] as TextBox;
                updFieldTable.Add(((DataControlFieldCell)(row.Cells[i])).ContainingField.HeaderText.ToUpper(), tBox.Text);
            }
        }
        TheService.SaveUpdateRecord(TheValidationResult.TheDataSourceUpload, gvValidationUpdate.DataKeys[e.RowIndex].Value.ToString(), updFieldTable);
        e.Cancel = true;
        gvValidationUpdate.EditIndex = -1;
        UpdateView();
    }
    protected void gvValidationUpdate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow gvr = gvValidationUpdate.Rows[e.RowIndex];
            TheService.DeleteUpdateRecord(TheValidationResult.TheDataSourceUpload, gvValidationUpdate.DataKeys[e.RowIndex].Value.ToString());
            gvValidationUpdate.EditIndex = -1;
            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
        }
    }
    protected void gvValidationUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
    protected void gvValidationUpdate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvValidationUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Controls.Count > 2)
            {
                LinkButton ltnDelete = (LinkButton)e.Row.Cells[0].Controls[2];
                if (ltnDelete.Text.Equals("Delete")) {
                    ltnDelete.Attributes.Add("onclick", "javascript:return ButtonWarning('Delete')");
                }
            }
        }
    }
}