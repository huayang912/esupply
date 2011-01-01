using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using NHibernate.Expression;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Web;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Entity.Exception;

public partial class MasterData_Supplier_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    private List<Supplier> suppliers
    {
        get { return (List<Supplier>)ViewState["Suppliers"]; }
        set { ViewState["Suppliers"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text.Trim();
        string desc = this.tbDesc.Text.Trim();
        //bool isActive = this.cbIsActive.Checked;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Supplier));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Supplier)).SetProjection(Projections.Count("Code"));
            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Code", code, MatchMode.Anywhere));
                selectCountCriteria.Add(Expression.Like("Code", code, MatchMode.Anywhere));
            }

            if (desc != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Description", desc, MatchMode.Anywhere));
                selectCountCriteria.Add(Expression.Like("Description", desc, MatchMode.Anywhere));
            }
            //selectCriteria.Add(Expression.Eq("IsActive", isActive));
            //selectCountCriteria.Add(Expression.Eq("IsActive", isActive));

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            suppliers = TheBusinessMgr.ReadSupplierFromCSV(fileUpload.PostedFile.InputStream, this.CurrentUser.Code);
            this.GV_List.DataSource = suppliers;
            this.GV_List.DataBind();
            this.GridView.Visible = true;
            ShowSuccessMessage("Import.Result.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            TheSupplierMgr.UpdateOrCreateSupplier(suppliers, this.CurrentUser.Code);
            ShowSuccessMessage("Import.Result.Successfully");
            this.GridView.Visible = false;
            this.DoSearch();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            this.GridView.Visible = false;
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

}
