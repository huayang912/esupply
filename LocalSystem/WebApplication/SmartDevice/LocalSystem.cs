using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartDevice.SmartDeviceWS;
using System.Web.Services.Protocols;

namespace SmartDevice
{
    public partial class LocalSystem : Form
    {
        SmartDeviceMgrWS TheSmartDeviceMgr;

        List<BarCode> barCodes;
        AppUser appUser;

        protected DataGridTableStyle ts;

        public LocalSystem()
        {
            InitializeComponent();
            //
            TheSmartDeviceMgr = new SmartDeviceMgrWS();
            this.plLogin.Visible = true;
            this.plOp.Visible = false;
            barCodes = new List<BarCode>();
            //初始化表格
            columnBarCode = new DataGridTextBoxColumn();
            columnBarCode.MappingName = "BarCode";
            columnBarCode.HeaderText = "条码";
            columnBarCode.Width = 115;

            columnItemCode = new DataGridTextBoxColumn();
            columnItemCode.MappingName = "ItemCode";
            columnItemCode.HeaderText = "物料";
            columnItemCode.Width = 80;

            columnQty = new DataGridTextBoxColumn();
            columnQty.MappingName = "Qty";
            columnQty.HeaderText = "数量";
            columnQty.Format = "0.##";
            columnQty.Width = 40;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                TheSmartDeviceMgr.CreateBarCode(this.barCodes.ToArray(), this.appUser.Code);
                this.InitialAll();
                this.lblLoginMessage.Text = "条码保存成功!";
                Logout();
            }
            catch (SoapException ex)
            {
                string messageText = Utility.FormatExMessage(ex.Message);
                MessageBox.Show(messageText);
                this.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                this.InitialAll();
            }
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = textBox.Text.Trim();
            if (e.KeyData.ToString().ToUpper() == "RETURN")
            {
                if (textBox.Text.Trim() == "END")
                {
                    btnConfirm_Click(sender, null);
                }
                else if (textBox.Text.Trim().Length < 5)
                {
                    textBox.Text = string.Empty;
                }
                else
                {
                    try
                    {
                        BarCode barCode = TheSmartDeviceMgr.CheckAndLoadBarCode(textBox.Text.Trim(), this.appUser.Code);
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
                        MessageBox.Show(messageText);
                        textBox.Text = string.Empty;
                        this.DataBind();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        this.InitialAll();
                    }
                }
            }
            else if (e.KeyData.ToString().ToUpper() == "ESCAPE")
            {
                Logout();
            }
        }

        private void Logout()
        {
            this.plLogin.Visible = true;
            this.plOp.Visible = false;
            this.tbUserCode.Focus();
            this.tbUserCode.Text = string.Empty;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            appUser = this.TheSmartDeviceMgr.LoadAppUser(this.tbUserCode.Text.Trim());
            if (appUser != null)
            {
                this.plLogin.Visible = false;
                this.plOp.Visible = true;
                this.InitialAll();
            }
            else
            {
                this.tbUserCode.Text = string.Empty;
                this.lblLoginMessage.Text = "用户登录失败!请重试";
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (this.tbUserCode.Text == "2618")
            {
                this.Dispose();
            }
            else
            {
                this.lblLoginMessage.Text = "请输入正确的密码退出!";
                this.tbUserCode.Text = string.Empty;
                this.tbUserCode.Focus();
            }
        }

        private void tbBarCode_TextChanged(object sender, EventArgs e)
        {
            //this.lblMessage.Text = string.Empty;
        }

        private void DataBind()
        {
            List<BarCode> BindingBarCodeList = this.barCodes
                .Where(b => (b.Status == BusinessConstants.BARCODE_STATUS_VALUE_CREATE
                    || b.Status == BusinessConstants.BARCODE_STATUS_VALUE_WARNING))
                    .OrderByDescending(b => b.CreateDate).ToList();

            ts = new DataGridTableStyle();
            ts.MappingName = this.barCodes.GetType().Name;

            ts.GridColumnStyles.Add(columnBarCode);
            ts.GridColumnStyles.Add(columnItemCode);
            ts.GridColumnStyles.Add(columnQty);

            this.dgList.TableStyles.Clear();
            this.dgList.TableStyles.Add(ts);

            this.ResumeLayout();

            this.tbBarCode.Text = string.Empty;
            this.dgList.DataSource = BindingBarCodeList;
        }

        private void InitialAll()
        {
            this.barCodes = new List<BarCode>();
            this.lblMessage.Text = string.Empty;
            this.tbBarCode.Text = string.Empty;
            this.DataBind();
            this.tbBarCode.Focus();
        }

        private void tbUserCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData.ToString().ToUpper() == "RETURN")
            {
                this.btnLogin_Click(null, null);
            }
        }
    }
}