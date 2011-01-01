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

public partial class MasterData_Item_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected Item item
    {
        get { return (Item)ViewState["item"]; }
        set { ViewState["item"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        this.item = TheItemMgr.LoadItem(code);
        this.lbCode.Text = item.Code;
        this.tbDescription.Text = item.Description;
        this.tbUc.Text = item.UC.HasValue ? item.UC.Value.ToString("0.########") : "0";
        this.tbUom.Text = item.Uom;
        this.lbCreateUser.Text = item.CreateUser;
        this.lbLastModifyDate.Text = item.LastmodifyDate.HasValue ? item.LastmodifyDate.Value.ToString() : string.Empty;
        this.lbLastModifyUser.Text = item.LastmodifyUser;
        this.cbIsActive.Checked = item.IsActive;
        this.lbCreateDate.Text = item.CreateDate.HasValue ? item.CreateDate.Value.ToString() : string.Empty;
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
        this.item.Description = this.tbDescription.Text.Trim();
        this.item.IsActive = this.cbIsActive.Checked;
        this.item.LastmodifyDate = DateTime.Now;
        this.item.LastmodifyUser = this.CurrentUser.Code;
        this.item.UC = decimal.Parse(this.tbUc.Text.Trim());
        this.item.Uom = this.tbUom.Text.Trim();

        TheItemMgr.UpdateItem(this.item);
        ShowSuccessMessage("MasterData.Item.UpdateItem.Successfully", item.Code);
        InitPageParameter(this.item.Code);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            TheItemMgr.DeleteItem(this.item.Code);
            ShowSuccessMessage("MasterData.Item.DeleteItem.Successfully", this.item.Code);
            BackEvent(this, e);
        }
        catch (Exception)
        {
            ShowErrorMessage("MasterData.Item.DeleteItem.Fail", this.item.Code);
        }
    }
}
