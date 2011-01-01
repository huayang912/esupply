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
using com.LocalSystem.Web;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Control;

public partial class MasterData_Supplier_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected Supplier supplier
    {
        get { return (Supplier)ViewState["supplier"]; }
        set { ViewState["supplier"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        this.supplier = TheSupplierMgr.LoadSupplier(code);
        this.lbCode.Text = supplier.Code;
        this.tbName.Text = supplier.Name;
        this.tbAddress.Text = supplier.Address;
        this.tbContact.Text = supplier.Contact;
        this.tbCarrier.Text = supplier.Carrier;
        this.tbTele.Text = supplier.Phone;
        this.tbFax.Text = supplier.Fax;
        this.tbLeadTime.Text = supplier.LeadTime.HasValue ? supplier.LeadTime.Value.ToString("0.########") : "0";

        this.lbLastModifyDate.Text = supplier.LastmodifyDate.HasValue ? supplier.LastmodifyDate.Value.ToString() : string.Empty;
        this.lbLastModifyUser.Text = supplier.LastmodifyUser;
        this.cbIsActive.Checked = supplier.IsActive;
        this.lbCreateDate.Text = supplier.CreateDate.HasValue ? supplier.CreateDate.Value.ToString() : string.Empty;
        this.lbCreateUser.Text = supplier.CreateUser;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.supplier.Name = this.tbName.Text.Trim();
        this.supplier.IsActive = this.cbIsActive.Checked;
        this.supplier.LastmodifyDate = DateTime.Now;
        this.supplier.LastmodifyUser = this.CurrentUser.Code;

        this.supplier.Address = this.tbAddress.Text.Trim();
        this.supplier.Contact = this.tbContact.Text.Trim();
        this.supplier.Carrier = this.tbCarrier.Text.Trim();
        this.supplier.Phone = this.tbTele.Text.Trim();
        this.supplier.Fax = this.tbFax.Text.Trim();
        this.supplier.LeadTime = this.tbLeadTime.Text.Trim() == string.Empty ? 0 : decimal.Parse(this.tbLeadTime.Text.Trim());

        TheSupplierMgr.UpdateSupplier(this.supplier);
        ShowSuccessMessage("MasterData.Supplier.UpdateSupplier.Successfully", supplier.Code);
        InitPageParameter(this.supplier.Code);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            TheSupplierMgr.DeleteSupplier(this.supplier.Code);
            ShowSuccessMessage("MasterData.Supplier.DeleteSupplier.Successfully", this.supplier.Code);
            BackEvent(this, e);
        }
        catch (Exception)
        {
            ShowErrorMessage("MasterData.Supplier.DeleteSupplier.Fail", this.supplier.Code);
        }
    }



}
