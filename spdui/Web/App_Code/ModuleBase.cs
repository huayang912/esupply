using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Dndp.Service;
using System.Collections.Generic;

namespace Dndp.Web
{

    /// <summary>
    /// Summary description for ModuleBase
    /// </summary>
    public class ModuleBase : System.Web.UI.UserControl
    {
        public ModuleBase()
        {
        }

        public ISession GetService(string serviceName)
        {
            return ServiceLocator.GetService(serviceName) as ISession;
        }

        public string CurrentModuleName
        {
            get
            {
                return (new SessionHelper(Page)).CurrentModuleName;
            }
            set
            {
                (new SessionHelper(Page)).CurrentModuleName = value;
            }
        }

        protected Dndp.Persistence.Entity.Security.User CurrentUser
        {
            get
            {
                return (new SessionHelper(Page)).CurrentUser;
            }
        }

        protected virtual void InitByPermission()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InitByPermission();
        }

        protected virtual bool PermissionView
        {
            get
            {
                return CurrentUser.HasPermissionView(CurrentModuleName)
                    || CurrentUser.HasPermissionUpdate(CurrentModuleName)
                    || CurrentUser.HasPermissionAdd(CurrentModuleName)
                    || CurrentUser.HasPermissionDelete(CurrentModuleName)
                    || CurrentUser.HasPermissionFull(CurrentModuleName);
            }
        }

        protected virtual bool PermissionUpdate
        {
            get
            {
                return CurrentUser.HasPermissionUpdate(CurrentModuleName)
                    || CurrentUser.HasPermissionAdd(CurrentModuleName)
                    || CurrentUser.HasPermissionDelete(CurrentModuleName)
                    || CurrentUser.HasPermissionFull(CurrentModuleName);
            }
        }

        protected virtual bool PermissionAdd
        {
            get
            {
                return CurrentUser.HasPermissionAdd(CurrentModuleName)
                    || CurrentUser.HasPermissionDelete(CurrentModuleName)
                    || CurrentUser.HasPermissionFull(CurrentModuleName);
            }
        }

        protected virtual bool PermissionDelete
        {
            get
            {
                return CurrentUser.HasPermissionDelete(CurrentModuleName)
                    || CurrentUser.HasPermissionFull(CurrentModuleName);
            }
        }

        protected virtual bool PermissionFull
        {
            get
            {
                return CurrentUser.HasPermissionFull(CurrentModuleName);
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
    }

}