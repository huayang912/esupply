using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube
{
    public interface ICubeDimensionMgr
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

        void DeleteCubeDimensionByCubeId(int cubeId);

        #endregion Customized Methods

    }
}
