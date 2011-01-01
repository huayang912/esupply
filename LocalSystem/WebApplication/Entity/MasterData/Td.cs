using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace com.LocalSystem.Entity.MasterData
{
    [Serializable]
    public class Td : EntityBase
    {
        public int Id { get; set; }
        public int OrderLocTransId { get; set; }
        public string OrderNo { get; set; }
        public int Sequence { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ReferenceItemCode { get; set; }
        public string UomCode { get; set; }
        public decimal UnitCount { get; set; }
        public string LocationFromCode { get; set; }
        public string LocationToCode { get; set; }
        public string StorageBinCode { get; set; }
        public string LotNo { get; set; }
        public decimal OrderedQty { get; set; }
        public decimal ShippedQty { get; set; }
        public decimal ReceivedQty { get; set; }
        public int LocationLotDetId { get; set; }
        public string OddShipOption { get; set; }
        //用于上下架,投料
        public string LocationCode { get; set; }
        /// <summary>
        /// 待收、待发数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 本次输入数量
        /// </summary>
        public decimal CurrentQty { get; set; }
        public decimal CurrentRejectQty { get; set; }
        public decimal ScrapQty { get; set; }
        public decimal RejectedQty { get; set; }
        public int Cartons { get; set; }
        public int Operation { get; set; }

        /// <summary>
        /// 调整数，收货调整使用 //Picklist待收数
        /// </summary>
        public decimal AdjustQty { get; set; }

        //辅助字段

        /// <summary>
        /// 用于生产:疵点数 用于收货:参考订单号
        /// </summary>
        public string s1 { get; set; } 
        public string s2 { get; set; }
        public string s3 { get; set; }
        public string s4 { get; set; }
        public string s5 { get; set; }
        public decimal? d1 { get; set; }
        public decimal? d2 { get; set; }
        public decimal? d3 { get; set; }
        public decimal? d4 { get; set; }
        public int? i { get; set; }

    }

}
