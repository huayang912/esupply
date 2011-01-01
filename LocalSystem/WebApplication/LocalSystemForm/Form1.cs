using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using LocalSystemForm.LocalSystemWS;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Web.Services.Protocols;

namespace LocalSystemForm
{
    public partial class Form1 : Form
    {
        SmartDeviceMgrWS SmartDeviceMgr;

        List<BarCode> barCodes;
        AppUser appUser;
        public Form1()
        {
            InitializeComponent();
            this.dgList.AutoGenerateColumns = false;
            SmartDeviceMgr = new SmartDeviceMgrWS();
            this.gbLogin.Visible = true;
            this.gbOp.Visible = false;
            barCodes = new List<BarCode>();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                SmartDeviceMgr.CreateBarCode(this.barCodes.ToArray(), this.appUser.Code);
                this.InitialAll();
                this.lblMessage.Text = "要货令创建成功!";
            }
            catch (SoapException ex)
            {
                string messageText = Utility.FormatExMessage(ex.Message);
                MessageBox.Show(this, messageText);
                this.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InitialAll();
            }
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.ToString().ToUpper() == "RETURN")
            {
                this.tb_Leave(sender, null);
            }
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            try
            {
                BarCode barCode = SmartDeviceMgr.CheckAndLoadBarCode(textBox.Text.Trim(), this.appUser.Code);
                barCode.CreateDate = DateTime.Now;
                if (this.barCodes.Where(b => b.BarCode == barCode.BarCode).Count() > 0)
                {
                    barCode.Status = BusinessConstants.BARCODE_STATUS_VALUE_ERROR;
                    barCode.Memo = "重复条码";
                }
                this.barCodes.Add(barCode);
                textBox.Text = string.Empty;

                this.DataBind();

                #region Message显示
                //一共扫描数
                this.lblMessage.Text = "总共:" + this.barCodes.Count.ToString();
                //正常数
                this.lblMessage.Text += " 正常:" + this.barCodes.Where(b => b.Status == BusinessConstants.BARCODE_STATUS_VALUE_CREATE
                        || b.Status == BusinessConstants.BARCODE_STATUS_VALUE_WARNING).Count().ToString();
                //错误数
                this.lblMessage.Text += " 错误:" + this.barCodes.Where(b => (b.Status == BusinessConstants.BARCODE_STATUS_VALUE_ERROR
                    && b.Memo != "重复条码")).Count().ToString();
                //重复数
                this.lblMessage.Text += " 重复:" + this.barCodes.Where(b => b.Memo == "重复条码").Count().ToString();
                if (barCode.Status == BusinessConstants.BARCODE_STATUS_VALUE_CREATE)
                {
                    this.lblMessage.ForeColor = Color.Green;
                }
                else if (barCode.Status == BusinessConstants.BARCODE_STATUS_VALUE_ERROR)
                {
                    this.lblMessage.ForeColor = Color.Red;
                }
                else if (barCode.Status == BusinessConstants.BARCODE_STATUS_VALUE_WARNING)
                {
                    this.lblMessage.ForeColor = Color.Orange;
                }
                #endregion
            }
            catch (SoapException ex)
            {
                string messageText = Utility.FormatExMessage(ex.Message);
                MessageBox.Show(this, messageText);
                textBox.Text = string.Empty;
                this.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InitialAll();
            }
        }

        private void DataBind()
        {
            List<BarCode> BindingBarCodeList = this.barCodes
                .Where(b => (b.Status == BusinessConstants.BARCODE_STATUS_VALUE_CREATE
                    || b.Status == BusinessConstants.BARCODE_STATUS_VALUE_WARNING))
                .OrderByDescending(b => b.CreateDate).ToList();
            this.tbBarCode.Text = string.Empty;
            this.dgList.DataSource = new BindingList<BarCode>(BindingBarCodeList);
            this.dgList.ClearSelection();


        }

        private void InitialAll()
        {
            this.barCodes = new List<BarCode>();
            this.lblMessage.Text = string.Empty;
            this.tbBarCode.Text = string.Empty;
            this.DataBind();
        }

        private int? NextSeq(string supplierCode)
        {
            //if (this.barCodes != null && this.barCodes.Select(p => p.SupplierCode).Contains(supplierCode))
            //{
            //    return barCodes.Where(p => p.SupplierCode == supplierCode).Select(p => p.Seq).Max() + 1;
            //}
            //else
            //{
            return 1;
            //}
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            appUser = this.SmartDeviceMgr.LoadAppUser(this.tbUserCode.Text.Trim());
            if (appUser != null && appUser.Password == Utility.Md5(this.tbPassword.Text.Trim()))
            {
                this.gbLogin.Visible = false;
                this.gbOp.Visible = true;
                this.tbBarCode.Focus();
            }
            else
            {
                this.tbPassword.Text = string.Empty;
                this.lblLoginMessage.Text = "用户登录失败!请重试";
            }
        }

        private void tbQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.TextBoxDecimalFilter(sender, e);
        }

        private void tbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData.ToString().ToUpper() == "RETURN")
            {
                this.btnLogin_Click(null, null);
            }
        }

        private void tbSupplier_TextChanged(object sender, EventArgs e)
        {
            this.lblMessage.Text = string.Empty;
        }

    }
}
