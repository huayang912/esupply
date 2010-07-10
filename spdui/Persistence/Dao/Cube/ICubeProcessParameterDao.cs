using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeProcessParameterDao
    {
        #region Method Created By CodeSmith

        void CreateCubeProcessParameter(CubeProcessParameter entity);

        CubeProcessParameter LoadCubeProcessParameter(int id);

        void UpdateCubeProcessParameter(CubeProcessParameter entity);
        
        void DeleteCubeProcessParameter(int id);

        void DeleteCubeProcessParameter(CubeProcessParameter entity);

        void DeleteCubeProcessParameter(IList<int> idList);

        void DeleteCubeProcessParameter(IList<CubeProcessParameter> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeProcessParameter> FindCubeProcessParameterByProcessId(int processId);

        void DeleteCubeProcessParameterByProcessId(int processId);

        #endregion Customized Methods
    }
}
