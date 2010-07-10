using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeDimensionDao
    {
        #region Method Created By CodeSmith

        void CreateCubeDimension(CubeDimension entity);

        CubeDimension LoadCubeDimension(int id);

        void UpdateCubeDimension(CubeDimension entity);
        
        void DeleteCubeDimension(int id);

        void DeleteCubeDimension(CubeDimension entity);

        void DeleteCubeDimension(IList<int> idList);

        void DeleteCubeDimension(IList<CubeDimension> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeDimension> FindDimensionByCubeId(int cubeId);

        IList<CubeDimension> FindDimensionByDimensionNameAndAttributeName(string dimensionName, string attributeName);

        void DeleteCubeDimensionByCubeId(int cubeId);

        #endregion Customized Methods
    }
}
