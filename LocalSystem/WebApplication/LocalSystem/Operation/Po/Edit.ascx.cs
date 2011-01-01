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
using com.LocalSystem.Entity.Operation;
using com.LocalSystem.Entity;
using System.Collections.Generic;
using System.Drawing;

public partial class MasterData_Operation_Po_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected Po po
    {
        get { return (Po)ViewState["po"]; }
        set { ViewState["po"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(string code)
    {
        this.po = ThePoMgr.LoadPo(code, true);
        if (this.po != null)
        {
            this.GV_List_DataBind();

            this.tbPlantAddress.ReadOnly = true;
            this.tbPlantFax.ReadOnly = true;
            this.tbPlantPhone.ReadOnly = true;
            this.tbPlantContanct.ReadOnly = true;
            this.tbPlantDock.ReadOnly = true;

            this.tbSupplierAddress.ReadOnly = true;
            this.tbSupplierContant.ReadOnly = true;
            this.tbSupplierFax.ReadOnly = true;
            this.tbSupplierPhone.ReadOnly = true;
            this.tbSupplierLotNo.ReadOnly = true;
            this.tbWinTime.ReadOnly = true;

            if (this.po.Status == BusinessConstants.PO_STATUS_VALUE_CREATE)
            {
                this.btnCancel.Visible = true;
                this.btnSubmit.Visible = true;
                this.btnSave.Visible = true;
                this.btnCancel.Text = "${Common.Button.Delete}";
                this.GV_List.Columns[5].Visible = true;

                this.tbPlantAddress.ReadOnly = false;
                this.tbPlantFax.ReadOnly = false;
                this.tbPlantPhone.ReadOnly = false;
                this.tbPlantContanct.ReadOnly = false;
                this.tbPlantDock.ReadOnly = false;

                this.tbSupplierAddress.ReadOnly = false;
                this.tbSupplierContant.ReadOnly = false;
                this.tbSupplierFax.ReadOnly = false;
                this.tbSupplierPhone.ReadOnly = false;
                this.tbSupplierLotNo.ReadOnly = false;

                this.tbWinTime.ReadOnly = false;
            }
            else if (this.po.Status == BusinessConstants.PO_STATUS_VALUE_SUBMIT)
            {
                this.btnCancel.Visible = false;
                this.btnSubmit.Visible = false;
                this.btnSave.Visible = false;
                this.btnCancel.Text = "${Common.Button.Cancel}";
                this.GV_List.Columns[5].Visible = false;
            }
            else
            {
                this.btnCancel.Visible = false;
                this.btnSubmit.Visible = false;
                this.GV_List.Columns[5].Visible = false;
                this.btnSave.Visible = false;
            }

            this.lblCode.Text = this.po.Code;
            this.lblSupplier.Text = this.po.Supplier + "[" + this.po.SupplierName + "]";
            this.lblStatus.Text = this.po.Status;
            this.lblRefPo.Text = this.po.RefPo;
            this.lblOutBoundDate.Text = this.po.OutboundDate.HasValue ? this.po.OutboundDate.Value.ToString("yyyy-MM-dd HH:mm") : string.Empty;

            this.lbLastModifyDate.Text = this.po.LastmodifyDate.HasValue ? this.po.LastmodifyDate.Value.ToString() : string.Empty;
            this.lbLastModifyUser.Text = this.po.LastmodifyUser;
            this.lbCreateDate.Text = this.po.CreateDate.ToString("yyyy-MM-dd HH:mm");
            this.lbCreateUser.Text = this.po.CreateUser;

            this.lblPlantName.Text = this.po.PlantName;
            this.lblPlantCode.Text = this.po.PlantCode;

            this.tbPlantAddress.Text = this.po.PlantAddress;
            this.tbPlantFax.Text = this.po.PlantFax;
            this.tbPlantPhone.Text = this.po.PlantPhone;
            this.tbPlantContanct.Text = this.po.PlantContact;
            this.tbPlantDock.Text = this.po.PlantDock;

            this.tbSupplierAddress.Text = this.po.SupplierAddress;
            this.tbSupplierContant.Text = this.po.SupplierContact;
            this.tbSupplierFax.Text = this.po.SupplierFax;
            this.tbSupplierPhone.Text = this.po.SupplierPhone;
            this.tbSupplierLotNo.Text = this.po.LotNo;

            this.tbWinTime.Text = this.po.WinTime.HasValue ? this.po.WinTime.Value.ToString("yyyy-MM-dd HH:mm") : this.po.CreateDate.ToString("yyyy-MM-dd HH:mm");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.po.Status == BusinessConstants.PO_STATUS_VALUE_CREATE)
            {
                ThePoMgr.DeletePo(po);
                BackEvent(this, e);
                ShowSuccessMessage("MasterData.Po.Delete.Successfully", po.Code);
            }
            else
            {
                //this.po.Status = BusinessConstants.PO_STATUS_VALUE_CANCEL;
                ThePoMgr.CancelPo(this.po);
                ShowSuccessMessage("MasterData.Po.Cancel.Successfully", po.Code);
                InitPageParameter(this.po.Code);
            }
        }
        catch (Exception)
        {
            ShowErrorMessage("MasterData.Po.Cancel.Fail", this.po.Code);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ThePoMgr.ReleasePo(this.po, this.CurrentUser.Code);
            ShowSuccessMessage("MasterData.Po.Submit.Successfully", po.Code);
            InitPageParameter(this.po.Code);
        }
        catch (Exception ex)
        {
            ShowErrorMessage("MasterData.Po.Submit.Fail", this.po.Code);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            this.po.PlantAddress = this.tbPlantAddress.Text.Trim();
            this.po.PlantFax = this.tbPlantFax.Text.Trim();
            this.po.PlantPhone = this.tbPlantPhone.Text.Trim();
            this.po.PlantContact = this.tbPlantContanct.Text.Trim();
            this.po.PlantDock = this.tbPlantDock.Text.Trim();

            this.po.SupplierAddress = this.tbSupplierAddress.Text.Trim();
            this.po.SupplierContact = this.tbSupplierContant.Text.Trim();
            this.po.SupplierFax = this.tbSupplierFax.Text.Trim();
            this.po.SupplierPhone = this.tbSupplierPhone.Text.Trim();
            this.po.LotNo = this.tbSupplierLotNo.Text.Trim();

            this.po.WinTime = this.tbWinTime.Text.Trim() == string.Empty ? this.po.CreateDate : DateTime.Parse(this.tbWinTime.Text.Trim());

            ThePoMgr.UpdatePo(this.po);
            ShowSuccessMessage("MasterData.Po.Save.Successfully", po.Code);
            InitPageParameter(this.po.Code);
        }
        catch (Exception)
        {
            ShowErrorMessage("MasterData.Po.Save.Fail", this.po.Code);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void GV_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(this.GV_List.DataKeys[e.RowIndex].Value.ToString());
        try
        {
            ThePoDetailMgr.DeletePoDetail(id);
            ShowSuccessMessage("MasterData.Po.DeletePo.Successfully");
            this.po = ThePoMgr.LoadPo(this.po.Code, true);
            this.GV_List_DataBind();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.Po.DeletePo.Fail");
        }
    }

    protected void GV_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GV_List.EditIndex = e.NewEditIndex;
        this.GV_List_DataBind();
    }

    protected void GV_List_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.GV_List.EditIndex = -1;
        this.GV_List_DataBind();
    }

    protected void GV_List_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = int.Parse(this.GV_List.DataKeys[e.RowIndex].Value.ToString()); //gridview's DataKeyNames
        string Qty = (((TextBox)(this.GV_List.Rows[e.RowIndex].Cells[4].Controls[1]))).Text.Trim();  //获取选中行单元格数据

        try
        {
            PoDetail poDetail = ThePoDetailMgr.LoadPoDetail(id);
            poDetail.Qty = decimal.Parse(Qty);
            ThePoDetailMgr.UpdatePoDetail(poDetail);
            ShowSuccessMessage("MasterData.PoDetail.Update.Successfully");
        }
        catch (FormatException)
        {
            ShowErrorMessage("MasterData.PoDetail.Update.Fail.FormatException");
        }
        catch (Exception ex)
        {
            ShowErrorMessage("MasterData.PoDetail.Update.Fail");
        }
        finally
        {
            this.GV_List.EditIndex = -1;
            this.po = ThePoMgr.LoadPo(this.po.Code, true);
            this.GV_List_DataBind();
        }
    }

    private void GV_List_DataBind()
    {
        this.GV_List.DataSource = this.po.PoDetails;
        this.GV_List.DataBind();
    }

}
