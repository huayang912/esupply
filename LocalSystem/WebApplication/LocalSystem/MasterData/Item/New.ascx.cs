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

public partial class MasterData_Item_New : NewModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;
    public event EventHandler NewEvent;

    private Item item;

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    public void PageCleanup()
    {
        this.tbCode.Text = string.Empty;
        this.tbDescription.Text = string.Empty;
        this.tbUc.Text = "1";
        this.tbUom.Text = string.Empty;
        this.cbIsActive.Checked = true;
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
        if (TheItemMgr.LoadItem(this.tbCode.Text.Trim()) != null)
        {
            ShowErrorMessage("MasterData.Item.CodeExist", this.tbCode.Text.Trim());
            return;
        }

        Item item = new Item();
        item.Code = this.tbCode.Text.Trim();
        item.Description = this.tbDescription.Text;
        item.IsActive = this.cbIsActive.Checked;
        item.UC = this.tbUc.Text.Trim() == string.Empty ? 1 : Convert.ToDecimal(this.tbUc.Text.Trim());
        item.Uom = this.tbUom.Text.Trim();
        item.CreateDate = DateTime.Now;
        item.CreateUser = this.CurrentUser.Code;
        item.LastmodifyDate = DateTime.Now;
        item.LastmodifyUser = this.CurrentUser.Code;
        TheItemMgr.CreateItem(item);
        ShowSuccessMessage("MasterData.Item.AddItem.Successfully", item.Code);
        CreateEvent(item.Code, null);
    }
}
