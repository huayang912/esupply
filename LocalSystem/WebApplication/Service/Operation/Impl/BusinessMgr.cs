using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.LocalSystem.Persistence.Operation;
using NHibernate.Expression;
using com.LocalSystem.Entity.Operation;
using com.LocalSystem.Service.Ext.Criteria;
using com.LocalSystem.Entity.Exception;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using com.LocalSystem.Entity.MasterData;
using System.IO;
using com.LocalSystem.Utility;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation.Impl
{
    [Transactional]
    public class BusinessMgr : IBusinessMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.
        public ICriteriaMgrE criteriaMgrE { get; set; }

        [Transaction(TransactionMode.Unspecified)]
        public List<Item> ReadItemFromXls(Stream inputStream, string userCode)
        {
            List<Item> items = new List<Item>();

            if (inputStream.Length == 0)
                throw new BusinessErrorException("Import.Stream.Empty");

            HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

            Sheet sheet = workbook.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();

            //ImportHelper.JumpRows(rows, 1);

            #region �ж���
            int colItemCode = 0;//���ϴ���
            int colItemDescription = 1;//��������
            int colUom = 3;//��λ
            int colUC = 2;//����װ
            #endregion

            while (rows.MoveNext())
            {
                Item item = new Item();
                Row row = (HSSFRow)rows.Current;
                if (!this.CheckValidDataRow(row, 0, 3))
                {
                    break;//�߽�
                }

                #region ��ȡitemCode
                item.Code = GetCellStringValue(row.GetCell(colItemCode));
                if (item.Code == null)
                {
                    throw new BusinessErrorException("MasterData.Item.NotExist", (row.RowNum + 1).ToString());
                }
                #endregion

                item.Description = GetCellStringValue(row.GetCell(colItemDescription));
                try
                {
                    item.UC = GetCellStringValue(row.GetCell(colUC)) == null ? 1 : decimal.Parse(GetCellStringValue(row.GetCell(colUC)));
                }
                catch (Exception)
                {
                    item.UC = 1;
                }
                item.Uom = GetCellStringValue(row.GetCell(colUom));

                items.Add(item);
            }

            if (items.Count == 0)
                throw new BusinessErrorException("Import.Result.Error.ImportNothing");

            return items;
        }

        public List<Item> ReadItemFromCSV(Stream inputStream, string userCode)
        {
            List<Item> items = new List<Item>();

            if (inputStream.Length == 0)
                throw new BusinessErrorException("Import.Stream.Empty");

            CSVReader reader = new CSVReader(inputStream, Encoding.GetEncoding("GBK"));

            //ImportHelper.JumpRows(rows, 1);

            #region �ж���
            int colItemCode = 0;//���ϴ���
            int colItemDescription = 1;//��������
            int colUom = 3;//��λ
            int colUC = 2;//����װ
            #endregion

            int i = 0;
            while (true)
            {
                i++;
                string[] data = reader.GetCSVLine();
                if (data == null)
                {
                    break;
                }

                Item item = new Item();;
                #region ��ȡitemCode
                item.Code = data[colItemCode];
                if (item.Code == null)
                {
                    throw new BusinessErrorException("MasterData.Item.NotExist", i.ToString());
                }
                #endregion

                item.Description = data[colItemDescription];
                try
                {
                    item.UC = data[colUC] == null ? 1 : decimal.Parse(data[colUC]);
                }
                catch (Exception)
                {
                    item.UC = 1;
                }
                item.Uom = data[colUom];

                items.Add(item);
            }

            if (items.Count == 0)
                throw new BusinessErrorException("Import.Result.Error.ImportNothing");

            return items;
        }

        [Transaction(TransactionMode.Unspecified)]
        public List<Supplier> ReadSupplierFromXls(Stream inputStream, string userCode)
        {
            List<Supplier> suppliers = new List<Supplier>();

            if (inputStream.Length == 0)
                throw new BusinessErrorException("Import.Stream.Empty");

            HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

            Sheet sheet = workbook.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();

            //sImportHelper.JumpRows(rows, 1);

            #region �ж���
            int colCode = 0;//����
            int colName = 1;//����
            int colAddress1 = 2;//��ַ1
            int colAddress2 = 3;//��ַ2
            int colContanct = 4;//��ϵ��
            int colPhone = 5;//�绰
            int colFax = 6;//����
            int colCarrier = 7;//����
            #endregion

            while (rows.MoveNext())
            {
                Supplier supplier = new Supplier();
                Row row = (HSSFRow)rows.Current;
                if (!this.CheckValidDataRow(row, 0, 6))
                {
                    break;//�߽�
                }

                #region ��ȡSupplierCode
                supplier.Code = GetCellStringValue(row.GetCell(colCode));
                if (supplier.Code == null)
                {
                    throw new BusinessErrorException("MasterData.Item.NotExist", (row.RowNum + 1).ToString());
                }
                #endregion

                supplier.Name = GetCellStringValue(row.GetCell(colName));
                supplier.Address = GetCellStringValue(row.GetCell(colAddress1));
                if (supplier.Address == null)
                {
                    supplier.Address = GetCellStringValue(row.GetCell(colAddress2));
                }
                else if (GetCellStringValue(row.GetCell(colAddress2)) != null)
                {
                    supplier.Address += GetCellStringValue(row.GetCell(colAddress2));
                }
                supplier.Contact = GetCellStringValue(row.GetCell(colContanct));
                supplier.Phone = GetCellStringValue(row.GetCell(colPhone));
                supplier.Fax = GetCellStringValue(row.GetCell(colFax));
                supplier.Carrier = GetCellStringValue(row.GetCell(colCarrier));

                suppliers.Add(supplier);
            }

            if (suppliers.Count == 0)
                throw new BusinessErrorException("Import.Result.Error.ImportNothing");

            return suppliers;
        }

        [Transaction(TransactionMode.Unspecified)]
        public List<Supplier> ReadSupplierFromCSV(Stream inputStream, string userCode)
        {
            List<Supplier> suppliers = new List<Supplier>();

            if (inputStream.Length == 0)
                throw new BusinessErrorException("Import.Stream.Empty");

            CSVReader reader = new CSVReader(inputStream, Encoding.GetEncoding("GBK"));

            #region �ж���
            int colCode = 0;//����
            int colName = 1;//����
            int colAddress1 = 2;//��ַ1
            int colAddress2 = 3;//��ַ2
            int colContanct = 4;//��ϵ��
            int colPhone = 5;//�绰
            int colFax = 6;//����
            //int colCarrier = 7;//����
            #endregion

            int i = 0;
            while (true)
            {
                i++;
                string[] data = reader.GetCSVLine();
                if (data == null)
                {
                    break;
                }

                Supplier supplier = new Supplier();
  
                #region ��ȡSupplierCode
                supplier.Code = data[colCode];
                if (supplier.Code == null)
                {
                    throw new BusinessErrorException("MasterData.Item.NotExist", i.ToString());
                }
                #endregion

                supplier.Name = data[colName];
                supplier.Address = data[colAddress1];
                if (supplier.Address == null)
                {
                    supplier.Address = data[colAddress2];
                }
                else if (data[colAddress2] != null)
                {
                    supplier.Address += data[colAddress2];
                }
                supplier.Contact =data[colContanct];
                supplier.Phone = data[colPhone];
                supplier.Fax = data[colFax];
                //supplier.Carrier = data[colCarrier];

                suppliers.Add(supplier);
            }

            if (suppliers.Count == 0)
                throw new BusinessErrorException("Import.Result.Error.ImportNothing");

            return suppliers;
        }

        private bool CheckValidDataRow(Row row, int startColIndex, int endColIndex)
        {
            for (int i = startColIndex; i < endColIndex; i++)
            {
                Cell cell = row.GetCell(i);
                if (cell != null && cell.CellType != NPOI.SS.UserModel.CellType.BLANK)
                {
                    return true;
                }
            }

            return false;
        }

        private string GetCellStringValue(Cell cell)
        {
            string strValue = null;
            if (cell != null)
            {
                if (cell.CellType == CellType.STRING)
                {
                    strValue = cell.StringCellValue;
                }
                else if (cell.CellType == CellType.NUMERIC)
                {
                    strValue = cell.NumericCellValue.ToString("0.########");
                }
                else if (cell.CellType == CellType.BOOLEAN)
                {
                    strValue = cell.NumericCellValue.ToString();
                }
                else if (cell.CellType == CellType.FORMULA)
                {
                    if (cell.CachedFormulaResultType == CellType.STRING)
                    {
                        strValue = cell.StringCellValue;
                    }
                    else if (cell.CachedFormulaResultType == CellType.NUMERIC)
                    {
                        strValue = cell.NumericCellValue.ToString("0.########");
                    }
                    else if (cell.CachedFormulaResultType == CellType.BOOLEAN)
                    {
                        strValue = cell.NumericCellValue.ToString();
                    }
                }
            }
            if (strValue != null)
            {
                strValue = strValue.Trim();
            }
            strValue = (strValue == string.Empty || strValue == "0") ? null : strValue;
            return strValue;
        }
        #endregion Customized Methods
    }
}


#region Extend Class

namespace com.LocalSystem.Service.Ext.Operation.Impl
{
    [Transactional]
    public partial class BusinessMgrE : com.LocalSystem.Service.Operation.Impl.BusinessMgr, IBusinessMgrE
    {
    }
}

#endregion Extend Class