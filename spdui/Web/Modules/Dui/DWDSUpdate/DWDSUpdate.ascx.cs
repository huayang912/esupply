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
using System.Text.RegularExpressions;

//TODO: Add other using statements here.

public partial class Modules_Dui_DWDSUpdate_DWDSUpdate : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("DWDSUpdate");

    //The entity service
    protected IDWDataSourceMgr TheService
    {
        get
        {
            return GetService("DWDataSourceMgr.service") as IDWDataSourceMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //txtCondition.Text = string.Empty;
    }

    public DWDataSource TheDWDataSource
    {
        get
        {
            return (DWDataSource)ViewState["TheDWDataSource"];
        }
        set
        {
            ViewState["TheDWDataSource"] = value;
        }
    }

    public void ClearSearchCondition()
    {
        txtCondition.Text = string.Empty;
    }

    //Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        lblMessage.Visible = false;
        try
        {
            DataSet ds = TheService.FindViewUpdateResult(TheDWDataSource);
            if (ds.Tables.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.RowFilter = txtCondition.Text.Trim();

                //gvDWDSUpdate.Columns = dv.Table.Columns;
                gvDWDSUpdate.DataSource = dv;
                //gvDWDSUpdate.DataSource = TheService.FindViewUpdateResult(TheDWDataSource);
                gvDWDSUpdate.DataBind();
            }
            else
            {
                gvDWDSUpdate.DataSource = null;
                gvDWDSUpdate.DataBind();
            }
        }
        catch
        {
            lblMessage.Text = "There are errors with the inputed condition or system configuration!";
            lblMessage.Visible = true;
        }
    }

    //The event handler when user button "Search".
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        UpdateView();
        //TODO: Add other event handler code here.
    }

    //Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void gvDWDSUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDWDSUpdate.PageIndex = e.NewPageIndex;
        UpdateView();
    }

    protected void gvDWDSUpdate_DataBinding(object sender, EventArgs e)
    {

    }
    
    protected void gvDWDSUpdate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow gvr = gvDWDSUpdate.Rows[e.RowIndex];

            #region find key value pair of the pk of this dw data source
            IList<KeyValuePair<string, string>> pkKeyValuePairList = new List<KeyValuePair<string, string>>();

            Regex regex = new Regex(@"<[\\$].+?[\\$]>", RegexOptions.Multiline);
            MatchCollection matchCollection = regex.Matches(TheDWDataSource.DeleteSQL);

            if (matchCollection != null && matchCollection.Count > 0)
            {
                IList<string> primaryKeys = new List<string>();
                foreach (Match pk in matchCollection)
                {
                    string strPk = pk.Value.TrimStart(new char[] { '<', '$' }).TrimEnd(new char[] { '$', '>' });
                    if (!primaryKeys.Contains(strPk))
                    {
                        primaryKeys.Add(strPk);
                    }
                }

                for (int i = 1; i < gvDWDSUpdate.HeaderRow.Cells.Count; i++)
                {
                    TableCell cell = gvDWDSUpdate.HeaderRow.Cells[i];
                    foreach (string pk in primaryKeys)
                    {
                        if (cell.Text.ToUpper() == pk.ToUpper())
                        {
                            KeyValuePair<string, string> pkKeyValuePair = new KeyValuePair<string, string>(pk, gvr.Cells[i].Text);

                            pkKeyValuePairList.Add(pkKeyValuePair);
                        }
                    }
                }
            }

            #region if not find, use the first column as key value pair
            //if (pkKeyValuePairList.Count == 0)
            //{
            //    KeyValuePair<string, string> pkKeyValuePair = new KeyValuePair<string, string>(gvDWDSUpdate.HeaderRow.Cells[1].Text, gvr.Cells[1].Text);

            //    pkKeyValuePairList.Add(pkKeyValuePair);
            //}
            #endregion
            #endregion

            //TheService.DeleteSelectedResult(TheDWDataSource, (gvDWDSUpdate.PageIndex) * 20 + e.RowIndex, TheDWDataSource.Name, (new SessionHelper(Page)).CurrentUser.UserName.ToString(), txtCondition.Text.Trim());

            TheService.DeleteSelectedResult(TheDWDataSource, pkKeyValuePairList, this.CurrentUser);
            gvDWDSUpdate.EditIndex = -1;
            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
        }
    }
    protected void gvDWDSUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            LinkButton ltnDelete = (LinkButton)e.Row.Cells[0].Controls[0];
            if (ltnDelete.Text.Equals("Delete"))
            {
                ltnDelete.Attributes.Add("onclick", "javascript:return ButtonWarning('Delete')");
            }
        }
    }
}