using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections;

using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Dndp.Web{

    /// <summary>
    /// Summary description for GridViewExcelExporter
    /// </summary>
    public class GridViewExcelExporter
    {
        public static void Export(HttpResponse response,IList data)
        {
            response.Clear();
            response.AddHeader("content-disposition", "attachment;filename=exportfile.xls");
            response.Charset = "";

            // If you want the option to open the Excel file without saving then
            // comment out the line below
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView gridview = new GridView();
            gridview.DataSource = data;
            gridview.DataBind();
            gridview.RenderControl(htmlWrite);
            response.Write(stringWrite.ToString());
            response.End();
        }
    }
}
