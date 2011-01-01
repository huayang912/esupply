using System;
using System.Collections.Generic;
using com.LocalSystem.Entity.Operation;
using com.LocalSystem.Web;
using System.Web.UI.WebControls;
using System.Drawing;
using com.LocalSystem.Entity;
using System.Linq;

//todo:按ESC删除

public partial class Operation_BarCode_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd HH:mm");
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            this.fldList.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch(false);
    }

    protected void btnOutBound_Click(object sender, EventArgs e)
    {
        this.DoSearch(true);
        this.ExportXLS(GV_List, DateTime.Now.ToString("ddHHmm") + "BarCode.xls");
    }

    private void DoSearch(bool isExport)
    {
        string lotNo = this.tbLotNo.Text.Trim();
        string itemCode = this.tbItemCode.Text.Trim();
        string supplier = this.tbSupplier.Text.Trim();
        string createUser = this.tbCreateUser.Text.Trim();

        DateTime? startDate = null;
        DateTime? endDate = null;

        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            startDate = DateTime.Parse(this.tbStartDate.Text.Trim());
        }

        if (this.tbEndDate.Text.Trim() != string.Empty)
        {
            endDate = DateTime.Parse(this.tbEndDate.Text.Trim()).AddDays(1);
        }
        List<string> status = new List<string>();

        foreach (ListItem li in this.cbStatus.Items)
        {
            if (li.Selected)
            {
                status.Add(li.Value);
            }
        }

        IList<BarCode> barCodeList = TheBarCodeMgr.GetBarCode(lotNo, itemCode, supplier, createUser, startDate, endDate, status);
        this.GV_List.DataSource = barCodeList;
        if (barCodeList != null && barCodeList.Count > 500)
        {
            ShowWarningMessage("Common.ListCount.Warning.GreatThan500");
        }
        if (isExport)
        {
            this.GV_List.Columns[0].Visible = false;
            this.GV_List.Columns[10].Visible = false;
            this.GV_List.DataBind();
            this.ExportXLS(GV_List, DateTime.Now.ToString("ddHHmm") + "BarCode.xls");
        }
        else
        {
            this.fldList.Visible = true;
            this.GV_List.Columns[0].Visible = true;
            this.GV_List.Columns[10].Visible = true;
            this.GV_List.DataBind();
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            bool mark = false;
            List<BarCode> barCodes = new List<BarCode>();
            foreach (GridViewRow row in this.GV_List.Rows)
            {
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfId = row.FindControl("hfId") as HiddenField;
                    BarCode barCode = TheBarCodeMgr.LoadBarCode(int.Parse(hfId.Value));
                    if (barCode.Status == BusinessConstants.BARCODE_STATUS_VALUE_CREATE
                        || barCode.Status == BusinessConstants.BARCODE_STATUS_VALUE_WARNING)
                    {
                        barCodes.Add(barCode);
                    }
                    else
                    {
                        mark = true;
                    }
                }
            }

            if (mark)
            {
                ShowWarningMessage("Operation.BarCode.Warning.Contain.InvalidBarCode");
            }
            if (barCodes.Count > 0)
            {
                this.ThePoMgr.CreatePo(barCodes, this.CurrentUser.Code);
                if (!mark)
                {
                    this.ShowSuccessMessage("MasterData.Po.Create.Successfully");
                }
                this.DoSearch(false);
            }
            else
            {
                ShowErrorMessage("Operation.BarCode.Error.Contain.NoBarCode");
            }
        }
        else
        {
            ShowErrorMessage("Operation.BarCode.Error.Contain.NoBarCode");
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((BarCodeBase)(e.Row.DataItem)).Status == BusinessConstants.BARCODE_STATUS_VALUE_CREATE)
            {
                e.Row.BackColor = Color.GreenYellow;
            }
            else if (((BarCodeBase)(e.Row.DataItem)).Status == BusinessConstants.BARCODE_STATUS_VALUE_CLOSE)
            {
                e.Row.BackColor = Color.Silver;
            }
            else if (((BarCodeBase)(e.Row.DataItem)).Status == BusinessConstants.BARCODE_STATUS_VALUE_ERROR)
            {
                e.Row.BackColor = Color.Red;
            }
            else if (((BarCodeBase)(e.Row.DataItem)).Status == BusinessConstants.BARCODE_STATUS_VALUE_WARNING)
            {
                e.Row.BackColor = Color.Yellow;
            }
            e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[2].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        }
    }

    protected void GV_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)this.GV_List.DataKeys[e.RowIndex].Values[0];
        try
        {
            TheBarCodeMgr.DeleteBarCode(id);
            ShowSuccessMessage("Operation.BarCode.Successfully");
            btnSearch_Click(null, null);
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("Operation.BarCode.Fail");
        }
    }

    protected void GV_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GV_List.EditIndex = e.NewEditIndex;
        this.GV_List.DataBind();
    }

    protected void GV_List_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.GV_List.EditIndex = -1;
        this.GV_List.DataBind();
    }

    protected void GV_List_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int id = int.Parse(this.GV_List.DataKeys[e.RowIndex].Value.ToString()); //gridview's DataKeyNames
        //string Qty = (((TextBox)(this.GV_List.Rows[e.RowIndex].Cells[4].Controls[1]))).Text.Trim();  //获取选中行单元格数据
    }


}
