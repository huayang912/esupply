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
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using com.LocalSystem.Utility;

public partial class MasterData_Supplier_New : NewModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;
    public event EventHandler NewEvent;

    private Supplier supplier;

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    public void PageCleanup()
    {
        this.tbCode.Text = string.Empty;
        this.tbName.Text = string.Empty;
        this.tbAddress.Text = string.Empty;
        this.tbContact.Text = string.Empty;
        this.tbTele.Text = string.Empty;
        this.tbFax.Text = string.Empty;
        this.tbCarrier.Text = string.Empty;
        this.cbIsActive.Checked = true;
        this.tbLeadTime.Text = string.Empty;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (TheSupplierMgr.LoadSupplier(this.tbCode.Text.Trim()) != null)
        {
            ShowErrorMessage("MasterData.Supplier.Code.Exists");
            return;
        }

        Supplier supplier = new Supplier();
        supplier.Code = this.tbCode.Text.Trim();
        supplier.Name = this.tbName.Text;

        supplier.Address = this.tbAddress.Text.Trim();
        supplier.Contact = this.tbContact.Text.Trim();
        supplier.Phone = this.tbTele.Text.Trim();
        supplier.Fax = this.tbFax.Text.Trim();
        supplier.Carrier = this.tbCarrier.Text.Trim();

        supplier.IsActive = this.cbIsActive.Checked;
        supplier.CreateDate = DateTime.Now;
        supplier.CreateUser = this.CurrentUser.Code;
        supplier.LastmodifyDate = DateTime.Now;
        supplier.LastmodifyUser = this.CurrentUser.Code;

        this.supplier.LeadTime = this.tbLeadTime.Text.Trim() == string.Empty ? 0 : decimal.Parse(this.tbLeadTime.Text.Trim());

        TheSupplierMgr.CreateSupplier(supplier);
        ShowSuccessMessage("MasterData.Supplier.AddSupplier.Successfully", supplier.Code);
        CreateEvent(supplier.Code, null);
    }
}
