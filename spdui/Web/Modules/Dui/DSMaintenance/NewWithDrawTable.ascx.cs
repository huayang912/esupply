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

public partial class Modules_Dui_DSMaintenance_NewWithDrawTable : ModuleBase
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
        SaveWithDrawTable();
        this.Back(this, e);
    }

    protected void btnSubmitContinue_Click(object sender, EventArgs e)
    {
        SaveWithDrawTable();
        UpdateView();
    }

    protected void SaveWithDrawTable()
    {
        DataSourceWithDrawTable dsc = new DataSourceWithDrawTable();
        dsc.WithDrawTableName = txtWithDrawTableName.Text;
        DataSource ds = TheService.LoadDataSource(int.Parse(txtDsId.Value));
        dsc.TheDataSource = ds;

        TheService.CreateDataSourceWithDrawTable(dsc);
        TheService.UpdateDataSource(ds);
    }

    public void UpdateView()
    {
        txtWithDrawTableName.Text = string.Empty;
    }
}
