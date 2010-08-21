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
using Dndp.Persistence.Entity.Security;
using System.Collections.Generic;

public partial class Modules_Dui_DSMaintenance_NewCategory : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("DSMaintenance");

    //The entity service
    protected IDataSourceMgr TheService
    {
        get
        {
            return GetService("DataSourceMgr.service") as IDataSourceMgr;
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

    public void SetDataSourceId(int dsId)
    {
        txtDsId.Value = dsId.ToString();
    }

    public void SetDataSourceCategoryId(int dsId)
    {
        txtDscId.Value = dsId.ToString();
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
        SaveCategory();
        //this.Back(this, e);
    }

    protected void btnSubmitContinue_Click(object sender, EventArgs e)
    {
        SaveCategory();
        UpdateView();
    }

    protected void SaveCategory()
    {
        if (txtDscId.Value == string.Empty)
        {
            DataSourceCategory dsc = new DataSourceCategory();
            dsc.Name = txtCategoryName.Text;
            dsc.Description = txtCategoryDescription.Text;
            dsc.ActiveFlag = bool.Parse(rblIsActive.SelectedValue);
            DataSource ds = TheService.LoadDataSource(int.Parse(txtDsId.Value));
            dsc.TheDataSource = ds;

            TheService.CreateDataSourceCategory(dsc);
        }
        else
        {
            DataSourceCategory dsc = TheService.LoadDataSourceCategory(int.Parse(txtDscId.Value));
            dsc.Description = txtCategoryDescription.Text;
            dsc.ActiveFlag = bool.Parse(rblIsActive.SelectedValue);

            IList<int> userIdList = new List<int>();
            if (gvUser.Rows != null && gvUser.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvUser.Rows)
                {
                    CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
                    if (cbSelect.Checked)
                    {
                        int userId = int.Parse(row.Cells[1].Text);
                        userIdList.Add(userId);
                    }
                }
            }

            TheService.UpdateDataSourceCategory(dsc, userIdList);
        }
    }

    public void UpdateView()
    {
        gvUser.DataSource = TheService.GetAllUser();
        gvUser.DataBind();

        if (txtDscId.Value == string.Empty)
        {
            txtCategoryName.Text = string.Empty;
            txtCategoryDescription.Text = string.Empty;
            rblIsActive.SelectedIndex = 0;
        }
        else
        {
            DataSourceCategory dataSourceCategory = TheService.LoadDataSourceCategory(int.Parse(txtDscId.Value), true);
            txtCategoryName.Text = dataSourceCategory.Name;
            txtCategoryName.ReadOnly = true;
            txtCategoryDescription.Text = dataSourceCategory.Description;
            if (dataSourceCategory.ActiveFlag)
            {
                rblIsActive.SelectedIndex = 0;
            }
            else
            {
                rblIsActive.SelectedIndex = 1;
            }

            if (gvUser.Rows != null && gvUser.Rows.Count > 0 && dataSourceCategory.Users != null && dataSourceCategory.Users.Count > 0)
            {
                foreach(GridViewRow row in gvUser.Rows)
                {
                    int userId = int.Parse(row.Cells[1].Text);

                    foreach (User user in dataSourceCategory.Users)
                    {
                        if (userId == user.Id)
                        {
                            CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
                            cbSelect.Checked = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}
