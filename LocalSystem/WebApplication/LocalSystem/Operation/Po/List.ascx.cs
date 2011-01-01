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
using System.IO;
using com.LocalSystem.Entity;
using com.LocalSystem.Entity.Operation;
using System.Drawing;

public partial class MasterData_Operation_Po_List : ListModuleBase
{
    public EventHandler EditEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string code = ((LinkButton)sender).CommandArgument;
            EditEvent(code, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).CommandArgument;
        try
        {
            ThePoMgr.DeletePo(code);
            ShowSuccessMessage("MasterData.Po.DeletePo.Successfully", code);
            UpdateView();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.Po.DeletePo.Fail", code);
        }

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((Po)(e.Row.DataItem)).Status == BusinessConstants.PO_STATUS_VALUE_CREATE)
                {
                    e.Row.BackColor = Color.GreenYellow;
                }
                else if (((Po)(e.Row.DataItem)).Status == BusinessConstants.PO_STATUS_VALUE_CLOSE)
                {
                    e.Row.BackColor = Color.Silver;
                }
                else if (((Po)(e.Row.DataItem)).Status == BusinessConstants.PO_STATUS_VALUE_SUBMIT)
                {
                    e.Row.BackColor = Color.Yellow;
                }
                else if (((Po)(e.Row.DataItem)).Status == BusinessConstants.PO_STATUS_VALUE_CANCEL)
                {
                    e.Row.BackColor = Color.White;
                }
                e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                e.Row.Cells[2].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            }
        }
    }
}
