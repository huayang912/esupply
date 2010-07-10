using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Service.Property
{
    public interface IPropertyMgr
    {
        int CommitRecordCount
        {
            get;
        }

        string CSVEncoding
        {
            get;
        }

        string DWDBString
        {
            get;
        }

        int CSVRecordPerParse
        {
            get;
        }

        string ProcessCubeBackupFolder
        {
            get;
        }

        string ProductCubeUserName
        {
            get;
        }

        string ProductCubePassword
        {
            get;
        } 
    }
}
