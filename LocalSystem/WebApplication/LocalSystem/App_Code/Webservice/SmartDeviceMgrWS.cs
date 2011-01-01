using System;
using System.Web.Services;
using System.Web.Services.Protocols;
using com.LocalSystem.Entity.Exception;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Entity.Operation;
using System.Collections.Generic;

/// <summary>
/// Summary description for ClientMgrWS
/// </summary>
[WebService(Namespace = "http://com.LocalSystem.Webservice")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SmartDeviceMgrWS : BaseWS
{
    public SmartDeviceMgrWS()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region


    [WebMethod]
    public AppUser LoadAppUser(string userCode)
    {
        try
        {
            AppUser user = TheAppUserMgr.LoadAppUser(userCode);
            return user;
        }
        catch (BusinessErrorException ex)
        {
            //string exMessage = RenderingLanguage(ex.Message, userCode, ex.MessageParams);
            //throw new SoapException(exMessage, SoapException.ServerFaultCode, string.Empty);
            return null;
        }
    }

    [WebMethod]
    public BarCode CheckAndLoadBarCode(string barCode, string userCode)
    {
        try
        {
            return TheBarCodeMgr.CheckAndLoadBarCode(barCode, userCode);

        }
        catch (BusinessErrorException ex)
        {
            string exMessage = RenderingLanguage(ex.Message, userCode, ex.MessageParams);
            throw new SoapException(exMessage, SoapException.ServerFaultCode, string.Empty);
        }
        catch (Exception ex)
        {
            throw new SoapException(ex.Message, SoapException.ServerFaultCode, string.Empty);
        }
    }

    [WebMethod]
    public void CreateBarCode(List<BarCode> barCodes, string createUser)
    {
        try
        {
            TheBarCodeMgr.CreateBarCode(barCodes);
        }
        catch (BusinessErrorException ex)
        {
            string exMessage = RenderingLanguage(ex.Message, createUser, ex.MessageParams);
            throw new SoapException(exMessage, SoapException.ServerFaultCode, string.Empty);
        }
        catch (Exception ex)
        {
            throw new SoapException(ex.Message, SoapException.ServerFaultCode, string.Empty);
        }
    }
    #endregion

}

