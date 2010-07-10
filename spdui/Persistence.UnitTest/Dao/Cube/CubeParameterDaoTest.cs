using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.Windsor;
using System.Collections;
using Castle.Facilities.NHibernateIntegration;

using Dndp.Persistence.Dao;
using Dndp.Persistence.UnitTest.Dao;
using Dndp.Persistence.Dao.Cube;
using Dndp.Persistence.Entity.Cube;

//TODO: Add other using statements here.

namespace Persistence.UnitTest.Dao.Cube
{
    [TestClass]
    public class CubeParameterDaoTest : DaoTestBase
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CubeParameterDaoTest));
        private static ICubeParameterDao entityDao;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            log4net.Config.XmlConfigurator.Configure();
            entityDao = GetService("cubeparameter.data.access") as ICubeParameterDao;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            entityDao = null;
        }

        #region Method Created By CodeSmith
        [TestMethod]
        public void TestCreateAndDelete()
        {
            //Create Entity
            CubeParameter entity = new CubeParameter();
			
            //Add code here to init the entity property.
			
            entityDao.CreateCubeParameter(entity);
            Assert.IsTrue(true);

            //Read Entity
            entity = entityDao.LoadCubeParameter(entity.Id);
            Assert.IsNotNull(entity);

            //Delete Entity
            entityDao.DeleteCubeParameter(entity.Id);
            Assert.IsTrue(true);            
        }
        #endregion Method Created By CodeSmith

		//TODO: Add other test methods.
		
        //[TestMethod]
        //public void YourTestMothod()
        //{
        //   
        //}
    }
}
