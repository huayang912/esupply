using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using com.LocalSystem.Entity.Exception;

namespace com.LocalSystem.Entity.MasterData
{
    [Serializable]
    public class Resolver : EntityBase
    {
        public string ModuleType { get; set; }
        public string UserCode { get; set; }
        public string CodePrefix { get; set; }
        public string BarcodeHead { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string PickBy { get; set; }
        /// <summary>
        /// 传递输入值(HuId,ItemCode)
        /// </summary>
        public string Input { get; set; }
        public string BinCode { get; set; }
        //是否强制打箱
        public bool IsOddCreateHu { get; set; }
        public bool IsScanHu { get; set; }
        public bool NeedPrintAsn { get; set; }
        public bool NeedPrintReceipt { get; set; }
        public bool NeedInspection { get; set; }
        public bool AutoPrintHu { get; set; }
        public bool AllowExceed { get; set; }
        public bool AllowCreateDetail { get; set; }
        public bool IsPickFromBin { get; set; }
        public bool IsShipByOrder { get; set; }
        public string Type { get; set; }
        public string Command { get; set; }
        public string Result { get; set; }
        public string FlowType { get; set; }
        public string AntiResolveHu { get; set; }
        public string PrintUrl { get; set; }
        public string ExternalOrderNo { get; set; }
        //用于移库和退库
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }

        public List<Td> Td { get; set; }
        public string FlowCode { get; set; }
        public string OrderNo { get; set; }

        /// <summary>
        /// 辅助字段
        /// </summary>
        public List<string> s { get; set; }

        public void AddTd(Td td)
        {
            if (td != null)
            {
                if (this.Td == null)
                {
                    this.Td = new List<Td>();
                }
                this.Td.Add(td);
            }
        }

    }
}
