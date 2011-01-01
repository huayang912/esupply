using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.LocalSystem.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.LocalSystem.Persistence.MasterData
{
    public interface IAppUserBaseDao
    {
        #region Method Created By CodeSmith

        void CreateAppUser(AppUser entity);

        AppUser LoadAppUser(String code);
  
        IList<AppUser> GetAllAppUser();
  
        IList<AppUser> GetAllAppUser(bool includeInactive);
  
        void UpdateAppUser(AppUser entity);
        
        void DeleteAppUser(String code);
    
        void DeleteAppUser(AppUser entity);
    
        void DeleteAppUser(IList<String> pkList);
    
        void DeleteAppUser(IList<AppUser> entityList);    
        #endregion Method Created By CodeSmith
    }
}
