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
using Dndp.Persistence.Dao.Dui;
using Dndp.Persistence.Entity.Dui;
using Dndp.Service.Dui;

public partial class Modules_Dui_DSMaintenance_NewField : ModuleBase
{
    public event EventHandler Back;

    public int DataSourceId
    {
        get
        {
            return (int)ViewState["DataSourceId"];
        }
        set
        {
            ViewState["DataSourceId"] = value;
        }
    }

    protected IDataSourceMgr TheService
    {
        get
        {
            return GetService("DataSourceMgr.service") as IDataSourceMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateView();
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AddField();
        if (lblMessage.Text != string.Empty)
        {
            Back(this, e);
        }
    }

    protected void btnSubmitContinue_Click(object sender, EventArgs e)
    {
        AddField();
        txtFieldName.Text = string.Empty;
        ddlFieldType.SelectedIndex = 0;
        txtFieldLength.Text = string.Empty;
        txtDescription.Text = string.Empty;
        cbAllowNull.Checked = false;
        cbDataKey.Checked = false;
    }

    private void AddField()
    {
        lblMessage.Visible = true;

        if (CheckEnteredField())
        {
            DataSourceField dsf = new DataSourceField();
            dsf.Name = txtFieldName.Text;
            dsf.FieldType = ddlFieldType.SelectedValue;
            if (dsf.FieldType.Equals("Text") || dsf.FieldType.Equals("Numeric"))
            {
                dsf.FieldLength = txtFieldLength.Text.Trim();
            }
            dsf.Description = txtDescription.Text.Trim();
            dsf.IsDataKey = cbDataKey.Checked;    //this field we will never use.
            dsf.IsNullable = cbAllowNull.Checked;

            try
            {
                TheService.AddDataSourceField(DataSourceId, dsf);
                lblMessage.Text = "Add field sucessfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }           
        }
    }

    private bool CheckEnteredField()
    {
        if (!CheckEnteredFieldName())
        {
            return false;
        }

        /*
        if (!CheckEnteredFieldLength())
        {
            return false;
        }
         * */

        return true;
    }

    private bool CheckEnteredFieldName()
    {
        string name = txtFieldName.Text.Trim();
        //check field name
        if (name.Length == 0)
        {
            lblMessage.Text = "The field name cannot be empty.";
            return false;
        }
        if (TheService.IsDuplicateField(DataSourceId, name))
        {
            lblMessage.Text = "The field name you entered has already been defined.";
            return false;
        }

        return true;
    }

    private bool CheckEnteredFieldLength()
    {
        //check field type
        if (ddlFieldType.SelectedValue.Equals("Text")) {
            if (txtFieldLength.Text.Trim().Length == 0)
            {
                lblMessage.Text = "The field length cannot be empty.";
                return false;
            }
            else
            {
                try
                {
                    Convert.ToInt32(txtFieldLength.Text);
                }
                catch (FormatException)
                {
                    lblMessage.Text = "The field length you entered is not a valid integer value";
                    return false;
                }
            }
        }
        else if (ddlFieldType.SelectedValue.Equals("Integer"))
        {

        }
        else if (ddlFieldType.SelectedValue.Equals("Numeric"))
        {
            if (txtFieldLength.Text.Trim().Length == 0)
            {
                lblMessage.Text = "The field length cannot be empty.";
                return false;
            }
            else
            {
                if (txtFieldLength.Text.IndexOf(",") == -1)
                {
                    lblMessage.Text = "In field length you must use symbol \",\" to sperate the total length and fraction length";
                    return false;
                }
                else
                {
                    string[] part = txtFieldLength.Text.Split(',');
                    if (part.Length > 2)
                    {
                        lblMessage.Text = "The field length you entered is not valid";
                        return false;
                    }
                    else
                    {
                        try
                        {
                            int total = Convert.ToInt32(part[0]);
                            int fraction = Convert.ToInt32(part[1]);
                            if (fraction >= total)
                            {
                                lblMessage.Text = "In field length the fraction cannot be large than total length";
                                return false;
                            }
                        }
                        catch (FormatException)
                        {
                            lblMessage.Text = "The field length you entered is not valid";
                            return false;
                        }
                    }
                }

            }
        }
        else if (ddlFieldType.SelectedValue.Equals("DateTime"))
        {

        }

        return true;
    }

    public void UpdateView()
    {
        lblMessage.Text = string.Empty;
        txtFieldName.Text = string.Empty;
        ddlFieldType.SelectedIndex = 0;
        txtFieldLength.Text = string.Empty;
        txtDescription.Text = string.Empty;
        cbAllowNull.Checked = false;
        cbDataKey.Checked = false;
    }
    
}
