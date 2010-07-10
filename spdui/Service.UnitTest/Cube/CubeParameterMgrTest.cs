using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using Dndp.Service.UnitTest;
using Dndp.Service.Cube;
using Dndp.Persistence.Entity.Cube;

namespace Dndp.Service.UnitTest.Cube
{
    [TestClass]
    public class CubeParameterMgrTest : MgrTestBase
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CubeParameterMgrTest));
        private static ICubeParameterMgr entityMgr;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            log4net.Config.XmlConfigurator.Configure();
            entityMgr = GetService("CubeParameterMgr.service") as ICubeParameterMgr;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            entityMgr = null;
        }

        //TODO: Add other test methods.
		
        //[TestMethod]
        //public void YourTestMothod()
        //{
        //   
        //}
    }
}
