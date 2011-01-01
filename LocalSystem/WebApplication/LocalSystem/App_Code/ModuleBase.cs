using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using com.LocalSystem.Service;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Utility;
using System.Text;
using System.IO;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Entity;
using com.LocalSystem.Entity.Exception;
using System.Threading;
using System.Globalization;

namespace com.LocalSystem.Web
{

    /// <summary>
    /// Summary description for ModuleBase
    /// </summary>
    public abstract class ModuleBase : ControlBase, IMessage
    {
        #region 构造方法
        public ModuleBase()
        {
        }
        #endregion

        #region 页面事件
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.Error += new EventHandler(PageErrorHandler);
        }

        //protected virtual void Page_Load(object sender, EventArgs e)
        //{            
        //    if (!IsPostBack)
        //    {
        //        CleanMessage();
        //    }
        //}
        #endregion

        #region 方法
        public void ShowSuccessMessage(string message)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowSuccessMessage(message);
            }
        }

        public void ShowSuccessMessage(string message, params string[] parameters)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowSuccessMessage(message, parameters);
            }
        }

        public void ShowWarningMessage(string message)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowWarningMessage(message);
            }
        }

        public void ShowWarningMessage(string message, params string[] parameters)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowWarningMessage(message, parameters);
            }
        }

        public void ShowErrorMessage(string message)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowErrorMessage(message);
            }
        }

        public void ShowErrorMessage(string message, params string[] parameters)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.ShowErrorMessage(message, parameters);
            }
        }

        public void ShowErrorMessage(BusinessErrorException ex)
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                if (ex.MessageParams != null && ex.MessageParams.Length > 0)
                {
                    ucMessage.ShowErrorMessage(ex.Message, ex.MessageParams);
                }
                else
                {
                    ucMessage.ShowErrorMessage(ex.Message);
                }
            }
        }

        public void CleanMessage()
        {
            IMessage ucMessage = (IMessage)Page.FindControl("ucMessage");
            if (ucMessage != null)
            {
                ucMessage.CleanMessage();
            }
        }

        protected IList<int> GetSelectIdList(GridView gv)
        {
            IList<int> idList = new List<int>();

            foreach (GridViewRow row in gv.Rows)
            {
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
                if (cbSelect.Checked)
                {
                    idList.Add((int)(gv.DataKeys[row.RowIndex].Value));
                }
            }

            return idList;
        }

        private void PageErrorHandler(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is BusinessErrorException)
            {
                //todo 通用业务异常处理页面
                //添加返回按钮，返回原页面history.back(-1);
                Response.Write(ex.Message);
                Server.ClearError();
            }
        }
        #endregion

        public virtual void ExportXLS(com.LocalSystem.Control.GridView GV_List)
        {
            this.ExportXLS(GV_List, "");
        }

        public virtual void ExportXLS(com.LocalSystem.Control.GridView GV_List, string fileName)
        {

            if (GV_List.FindPager().RecordCount > 5000)
            {
                ShowWarningMessage("Common.Export.Warning.GreatThan5000", GV_List.FindPager().RecordCount.ToString());
            }

            if (GV_List.FindPager().RecordCount == 0)
            {
                this.ShowWarningMessage("Common.GridView.NoRecordFound");
            }
            else
            {
                GV_List.ExportXLS(fileName);
            }
        }


        #region Excel导出方法

        protected void ExportDOC(GridView gridview)
        {
            this.ExportDOC(gridview, null);
        }

        protected void ExportXLS(GridView gridview)
        {
            this.ExportXLS(gridview, null);
        }
        protected void ExportXLS(GridView gridview, String FileName)
        {
            if (FileName == null || FileName.Length == 0) FileName = "temp.xls";
            this.Export(gridview, "application/ms-excel", FileName);
        }

        protected void ExportDOC(GridView gridview, String FileName)
        {
            if (FileName == null || FileName.Length == 0) FileName = "temp.doc";
            this.Export(gridview, "application/ms-word", FileName);
        }

        /// <summary>  
        /// 导出数据函数  
        /// </summary>  
        /// <param name="FileType">导出文件MIME类型</param>  
        /// <param name="FileName">导出文件的名称</param>  
        protected void Export(GridView gridview, String FileType, String FileName)
        {
            /*
            gridview.AllowPaging = false;
            gridview.AllowSorting = false;
            */
            if (gridview.Rows.Count > 5000)
            {
                ShowWarningMessage("Common.Export.Warning.GreatThan5000", gridview.Rows.Count.ToString());
            }

            gridview.DataBind();

            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("ZH-CN", true);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(cultureInfo);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();

            gridview.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();

            page.Controls.Add(form);

            form.Controls.Add(gridview);

            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;

            //      Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.AppendHeader("Content-Disposition", "attachment;filename="
                    + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8));

            //设置输出流HttpMiME类型(导出文件格式)  
            Response.ContentType = FileType;
            //Response.Charset = "UTF-8";
            //设定输出字符集  
            Response.Charset = "GB2312";
            //Response.ContentEncoding = Encoding.Default;
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            string content = sw.ToString();
            if (CurrentUser != null && CurrentUser.Language != null && CurrentUser.Language != string.Empty)
            {
                //content = TheLanguageMgr.ProcessLanguage(content, CurrentUser.Language);
            }
            else
            {
                CurrentUser.Language = System.Globalization.CultureInfo.CurrentCulture.Name;
            }
            content = TheLanguageMgr.ProcessLanguage(content, CurrentUser.Language);

            content = FilterInput(content);

            Response.Write(content);
            Response.End();
            Response.Flush();
            /*
                        gridview.AllowPaging = true;
                        gridview.AllowSorting = true;
            */
            gridview.DataBind();
        }

        #region 过滤Input
        private string FilterInput(string content)
        {
            string findStr = "<input ";
            while (content.Contains(findStr))
            {
                int startIndex = content.IndexOf(findStr);
                int lastIndex = content.IndexOf('>', startIndex);
                content = content.Remove(startIndex - findStr.Length, lastIndex - startIndex + findStr.Length + 1);
            }
            return content;
        }
        #endregion

        #endregion

    }

}