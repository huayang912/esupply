using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeMeasureDao
    {
        #region Method Created By CodeSmith

        void CreateCubeMeasure(CubeMeasure entity);

        CubeMeasure LoadCubeMeasure(int id);

        void UpdateCubeMeasure(CubeMeasure entity);
        
        void DeleteCubeMeasure(int id);

        void DeleteCubeMeasure(CubeMeasure entity);

        void DeleteCubeMeasure(IList<int> idList);

        void DeleteCubeMeasure(IList<CubeMeasure> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeMeasure> FindMeasureByCubeId(int cubeId);

        void DeleteCubeMeasureByCubeId(int cubeId);

        #endregion Customized Methods
    }
}
