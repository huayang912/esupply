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
using Castle.Windsor.Configuration.Interpreters;
using Castle.Model.Resource;


namespace Dndp.Web
{

    /// <summary>
    /// Summary description for GlobalApplication
    /// </summary>

    public class Global : HttpApplication, IContainerAccessor
    {
        private static WindsorContainer container = null;
        private static log4net.ILog log = log4net.LogManager.GetLogger("Application");

        public Global()
        {

        }

        public void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            container = new WindsorContainer(new XmlInterpreter(new ConfigResource()));
        }

        public void Application_End()
        {
            container.Dispose();
        }

        #region IContainerAccessor implementation

        public IWindsorContainer Container
        {
            get { return container; }
        }

        #endregion IContainerAccessor implementation
    }
}

