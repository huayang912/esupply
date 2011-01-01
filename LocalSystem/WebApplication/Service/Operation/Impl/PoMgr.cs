using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.LocalSystem.Entity;
using com.LocalSystem.Entity.Exception;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Entity.Operation;
using com.LocalSystem.Persistence.Operation;
using com.LocalSystem.Service.Ext.Criteria;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Service.Ext.Operation;
using NHibernate.Expression;
using System.Xml.Serialization;
using System.IO;
using com.LocalSystem.Utility;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation.Impl
{
    [Transactional]
    public class PoMgr : PoBaseMgr, IPoMgr
    {
        #region Customized Methods

        public IPoDetailMgrE poDetailMgrE { get; set; }
        public ICriteriaMgrE criteriaMgrE { get; set; }
        public IEntityPreferenceMgrE entityPreferenceMgrE { get; set; }
        public ISupplierMgrE supplierMgrE { get; set; }
        public IBarCodeMgrE barCodeMgrE { get; set; }
        public IOutboundLogMgrE outboundLogMgrE { get; set; }
        public string ftpServer { get; set; }
        public int ftpPort { get; set; }
        public string ftpFolder { get; set; }
        public string ftpUser { get; set; }
        public string ftpPassword { get; set; }

        [Transaction(TransactionMode.Unspecified)]
        public override void CreatePo(Po po)
        {
            base.CreatePo(po);
            foreach (PoDetail poDetail in po.PoDetails)
            {
                poDetailMgrE.CreatePoDetail(poDetail);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public override void DeletePo(Po po)
        {
            if (po != null)
            {
                if (po.PoDetails != null)
                {
                    List<int> poDetailsId = po.PoDetails.Select(p => p.Id).ToList();
                    barCodeMgrE.UnCloseBarCode(poDetailsId);
                }
                poDetailMgrE.DeletePoDetail(po.PoDetails);
                base.DeletePo(po);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void CancelPo(Po po)
        {
            if (po != null)
            {
                if (po.PoDetails != null)
                {
                    List<int> poDetailsId = po.PoDetails.Select(p => p.Id).ToList();
                    barCodeMgrE.UnCloseBarCode(poDetailsId);
                }
                po.Status = BusinessConstants.PO_STATUS_VALUE_CANCEL;
                this.UpdatePo(po);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public Po LoadPo(string poCode, bool includeDetail)
        {
            Po po = this.LoadPo(poCode);
            if (includeDetail && po.PoDetails != null)
            {
                foreach (var item in po.PoDetails)
                {
                    //laze load
                }
            }
            return po;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void CreatePo(List<PoDetail> poDetails, Supplier supplier, string createUser)
        {
            if (poDetails == null || poDetails.Count == 0)
            {
                throw new BusinessErrorException("Common.Business.Error.OprationFailed");
            }

            Po po = new Po();
            po.Code = LoadNextPoCode();
            po.CreateDate = DateTime.Now;
            po.CreateUser = createUser;
            po.IsOutbound = false;
            po.LastmodifyDate = DateTime.Now;
            po.LastmodifyUser = createUser;
            po.Status = BusinessConstants.PO_STATUS_VALUE_CREATE;
            po.Supplier = supplier.Code;

            //Plant
            IList<EntityPreference> enList = entityPreferenceMgrE.GetAllEntityPreference();
            po.PlantCode = enList.Where(e => e.Code == BusinessConstants.CODE_MASTER_PLANTCODE).FirstOrDefault().Value;
            po.PlantName = enList.Where(e => e.Code == BusinessConstants.CODE_MASTER_PLANTNAME).FirstOrDefault().Value;
            po.PlantContact = enList.Where(e => e.Code == BusinessConstants.CODE_MASTER_PLANTCONTACT).FirstOrDefault().Value;
            po.PlantAddress = enList.Where(e => e.Code == BusinessConstants.CODE_MASTER_PLANTADDRESS).FirstOrDefault().Value;
            po.PlantFax = enList.Where(e => e.Code == BusinessConstants.CODE_MASTER_PLANTFAX).FirstOrDefault().Value;
            po.PlantPhone = enList.Where(e => e.Code == BusinessConstants.CODE_MASTER_PLANTPHONE).FirstOrDefault().Value;
            //po.PlantDock = enList.Where(e => e.Code == BusinessConstants.CODE_MASTER_PLANTCODE).FirstOrDefault().Value;

            //Supplier
            po.SupplierName = supplier.Name;
            po.SupplierContact = supplier.Contact;
            po.SupplierFax = supplier.Fax;
            po.SupplierPhone = supplier.Phone;
            po.SupplierAddress = supplier.Address;
            po.LotNo = DateTime.Now.ToString("yyMMddHHmm");
            po.WinTime = supplier.LeadTime.HasValue ? DateTime.Now.AddHours((double)supplier.LeadTime.Value) : DateTime.Now;

            foreach (PoDetail poDetail in poDetails)
            {
                poDetail.PoCode = po.Code;
            }

            po.PoDetails = poDetails;
            this.CreatePo(po);
        }

        [Transaction(TransactionMode.Unspecified)]
        public void CreatePo(List<BarCode> barCodes, string createUser)
        {
            if (barCodes == null || barCodes.Count() == 0)
            {
                return;
            }

            barCodes = barCodes.Where(b => (b.Status == BusinessConstants.BARCODE_STATUS_VALUE_CREATE || b.Status == BusinessConstants.BARCODE_STATUS_VALUE_WARNING)).ToList();

            if (barCodes == null || barCodes.Count() == 0)
            {
                return;
            }

            List<string> SupplierCodes = barCodes.Select(b => b.SupplierCode).ToList<string>().Distinct().ToList();

            foreach (string supplierCode in SupplierCodes)
            {
                List<PoDetail> poDetails = new List<PoDetail>();

                Supplier supplier = supplierMgrE.LoadSupplier(supplierCode);

                foreach (BarCode barCode in barCodes)
                {
                    if (barCode.SupplierCode == supplierCode)
                    {
                        if (poDetails.Count > 0 && poDetails.Select(n => n.ItemCode).Contains(barCode.ItemCode))
                        {
                            poDetails.Where(n => n.ItemCode == barCode.ItemCode).First().Qty += barCode.Qty;
                            poDetails.Where(n => n.ItemCode == barCode.ItemCode).First().BarCodeIds.Add(barCode.Id);
                        }
                        else
                        {
                            PoDetail poDetail = new PoDetail();
                            poDetail.ItemCode = barCode.ItemCode;
                            poDetail.ItemDescription = barCode.ItemDescription;
                            poDetail.Qty = barCode.Qty;
                            poDetail.Seq = poDetails.Count() + 1;
                            poDetail.UC = barCode.UC;
                            poDetail.Uom = barCode.Uom;
                            poDetail.BarCodeIds = new List<int>() { barCode.Id };
                            poDetails.Add(poDetail);
                        }
                    }
                }
                this.CreatePo(poDetails, supplier, createUser);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void ReleasePo(Po po, string user)
        {
            po = this.LoadPo(po.Code);
            if (po.Status != BusinessConstants.PO_STATUS_VALUE_CREATE)
            {
                throw new BusinessErrorException();
            }
            po.Status = BusinessConstants.PO_STATUS_VALUE_SUBMIT;
            po.LastmodifyDate = DateTime.Now;
            po.LastmodifyUser = user;
            po.IsExport = true;
            this.UpdatePo(po);

            string tempPath = Path.GetTempPath();
            string randomFileSuffix = DateTime.Now.ToString("yyyyMMddHHmmssSSS");
            string tempFileName = "PO" + randomFileSuffix + ".tmp";
            string fileName = "PO" + randomFileSuffix + ".xml";
            string tempFileFullPath = tempPath + tempFileName;

            #region po转换为ManifestFile
            EntityPreference ep = entityPreferenceMgrE.LoadEntityPreference("PlantCode");
            ManifestFile mf = new ManifestFile();
            mf.Items = new object[3];

            ManifestFileFileHeader fileHeader = new ManifestFileFileHeader();
            fileHeader.PCODE = ep.Value;
            fileHeader.START = "START";
            mf.Items[0] = fileHeader;

            OutboundLog log = new OutboundLog();
            log.CreateDate = DateTime.Now;
            log.CreateUser = user;
            log.LastmodifyDate = DateTime.Now;
            log.LastmodifyUser = user;
            log.PoCode = po.Code;
            log.FileName = fileName;
            log.OutboundResult = "Successful";
            outboundLogMgrE.CreateOutboundLog(log);

            ManifestFileDelivery delivery = new ManifestFileDelivery();
            mf.Items[1] = delivery;

            ManifestFileDeliveryRecheader recheader = new ManifestFileDeliveryRecheader();
            delivery.recheader = new ManifestFileDeliveryRecheader[] { recheader };
            recheader.SUCODE = po.Supplier;
            recheader.SUNAME = po.SupplierName;
            recheader.SUPADDR1 = po.SupplierAddress;
            recheader.SUCONTACT = po.SupplierContact;
            recheader.SUPTEL = po.SupplierPhone;
            recheader.SUFAX = po.SupplierFax;
            recheader.MANCODE = po.Code;
            recheader.FAUADDR1 = po.PlantAddress;
            recheader.FAUCONTACT = po.PlantContact;
            recheader.FAUTEL = po.PlantPhone;
            recheader.FAUFAX = po.PlantFax;
            recheader.MURN = po.murn;
            recheader.ORDERG = po.orderg;
            recheader.DELORDGR = po.delordgr;
            recheader.FDCODE = po.fdcode;
            recheader.ROUTE = po.route;
            recheader.MROUTE = po.mroute;
            recheader.MANTITLE = po.mantitle;

            delivery.recpos = new ManifestFileDeliveryRecpos[po.PoDetails.Count];
            for (int j = 0; j < po.PoDetails.Count; j++)
            {
                delivery.recpos[j] = new ManifestFileDeliveryRecpos();
                delivery.recpos[j].PNUMBER = po.PoDetails[j].ItemCode;
                delivery.recpos[j].DESC = po.PoDetails[j].ItemDescription;
                delivery.recpos[j].PCS_PU = Convert.ToString(po.PoDetails[j].UC);
                delivery.recpos[j].NB_PU = Convert.ToString(po.PoDetails[j].Qty / po.PoDetails[j].UC);
            }

            ManifestFileFileEnd fileEnd = new ManifestFileFileEnd();
            fileEnd.c_end = "ENDFIL";
            mf.Items[2] = fileEnd;
            #endregion

            #region 存为临时文件
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ManifestFile));
            FileStream fs = new FileStream(tempFileFullPath, FileMode.CreateNew);
            StreamWriter streamWriter = new StreamWriter(fs);
            xmlSerializer.Serialize(streamWriter, mf);
            streamWriter.Flush();
            streamWriter.Close();
            #endregion

            #region 上传至FTP
            FtpHelper ftp = new FtpHelper(ftpServer, ftpPort, ftpFolder, ftpUser, ftpPassword);
            ftp.Upload(tempFileFullPath);
            ftp.Rename(tempFileName, fileName);
            #endregion

            #region 删除临时文件
            File.Delete(tempFileFullPath);
            #endregion

            this.FlushSession();
            this.CleanSession();
        }

        //        [Transaction(TransactionMode.Unspecified)]
        //        public void CreatePo(List<PoDetail> poDetails, string createUser)
        //        {
        //            List<string> suppliers = poDetails.Select(p => p.Supplier).ToList<string>().Distinct().ToList();

        //            foreach (string supplierCode in suppliers)
        //            {
        //                List<PoDetail> newPoDetail = new List<PoDetail>();
        //                Supplier supplier = supplierMgrE.LoadSupplier(supplierCode);
        //                foreach (PoDetail poDetail in poDetails)
        //                {
        //                    if (poDetail.Supplier == supplierCode)
        //                    {
        //                        if (newPoDetail.Count > 0 && newPoDetail.Select(n => n.ItemCode).Contains(poDetail.ItemCode))
        //                        {
        //                            newPoDetail.Where(n => n.ItemCode == poDetail.ItemCode).First().Qty += poDetail.Qty;
        //                        }
        //                        else
        //                        {
        //                            newPoDetail.Add(poDetail);
        //                        }
        //                    }
        //                }
        //                this.CreatePo(newPoDetail, supplier, createUser);
        //            }
        //#if false
        //            #region 记录原始记录 条码
        //            var barCodes = from p in poDetails
        //                           select new BarCode
        //                           {
        //                               BarCode = p.BarCode,
        //                               CreateDate = DateTime.Now,
        //                               CreateUser = createUser,
        //                               ItemCode = p.ItemCode,
        //                               ItemDescription = p.ItemDescription,
        //                               LotNo = DateTime.Now.ToString("yyMMddHHmm"),
        //                               Seq = p.Seq,
        //                               SupplierCode = p.Supplier,
        //                               UC = p.UC,
        //                               Uom = p.Uom
        //                           };
        //            foreach (var barCode in barCodes)
        //            {
        //                barCodeMgrE.CreateBarCode(barCode);
        //            }
        //            #endregion
        //#endif
        //        }

        #region private Methods
        private string LoadNextPoCode()
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Po));
            criteria.AddOrder(Order.Desc("Code"));
            IList<Po> poCodes = criteriaMgrE.FindAll<Po>(criteria, 0, 1);

            string nextPoCode = "0000000001";
            if (poCodes.Count > 0)
            {
                nextPoCode = (Int64.Parse(poCodes[0].Code) + 1).ToString();
            }
            return nextPoCode;
        }

        #endregion
        #endregion Customized Methods
    }
}


#region Extend Class

namespace com.LocalSystem.Service.Ext.Operation.Impl
{
    [Transactional]
    public partial class PoMgrE : com.LocalSystem.Service.Operation.Impl.PoMgr, IPoMgrE
    {
    }
}

#endregion Extend Class