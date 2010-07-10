using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Castle.Windsor;
using Dndp.Service.Security;

namespace Dndp.Web
{

    /// <summary>
    /// Summary description for ServiceLocator
    /// </summary>
    public class ServiceLocator
    {
        public ServiceLocator()
        {
        }

        public static Object GetService(string serviceNm)
        {
            return ObtainContainer().Resolve(serviceNm);
        }

        /// <summary>
        /// get an instance of windsor container from HTTP application
        /// </summary>
        /// <returns>return IWindsorContainer</returns>
        private static IWindsorContainer ObtainContainer()
        {

            IContainerAccessor containerAccessor =
                HttpContext.Current.ApplicationInstance as IContainerAccessor;

            if (containerAccessor == null)
            {
                throw new ApplicationException("HttpApplication doesn't implement the interface IContainerAccessor...");
            }

            IWindsorContainer container = containerAccessor.Container;
            if (container == null)
            {
                throw new ApplicationException("Can not find windsor container from HttpApplication...");
            }

            return container;
        }
    }

}
