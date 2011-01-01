namespace SmartDevice
{
    partial class LocalSystem
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalSystem));
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblLoginMessage = new System.Windows.Forms.Label();
            this.lblUserCode = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tbBarCode = new System.Windows.Forms.TextBox();
            this.lblItem = new System.Windows.Forms.Label();
            this.tbUserCode = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.plLogin = new System.Windows.Forms.Panel();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.plOp = new System.Windows.Forms.Panel();
            this.dgList = new System.Windows.Forms.DataGrid();
            this.plLogin.SuspendLayout();
            this.plOp.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(6, 30);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(229, 20);
            // 
            // lblLoginMessage
            // 
            this.lblLoginMessage.ForeColor = System.Drawing.Color.Red;
            this.lblLoginMessage.Location = new System.Drawing.Point(8, 169);
            this.lblLoginMessage.Name = "lblLoginMessage";
            this.lblLoginMessage.Size = new System.Drawing.Size(195, 21);
            // 
            // lblUserCode
            // 
            this.lblUserCode.Location = new System.Drawing.Point(18, 135);
            this.lblUserCode.Name = "lblUserCode";
            this.lblUserCode.Size = new System.Drawing.Size(42, 21);
            this.lblUserCode.Text = "用户:";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(186, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(49, 21);
            this.btnConfirm.TabIndex = 10;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // tbBarCode
            // 
            this.tbBarCode.Location = new System.Drawing.Point(46, 4);
            this.tbBarCode.MaxLength = 50;
            this.tbBarCode.Name = "tbBarCode";
            this.tbBarCode.Size = new System.Drawing.Size(134, 23);
            this.tbBarCode.TabIndex = 8;
            this.tbBarCode.TextChanged += new System.EventHandler(this.tbBarCode_TextChanged);
            this.tbBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // lblItem
            // 
            this.lblItem.Location = new System.Drawing.Point(6, 7);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(40, 18);
            this.lblItem.Text = "条码:";
            // 
            // tbUserCode
            // 
            this.tbUserCode.Location = new System.Drawing.Point(66, 132);
            this.tbUserCode.Name = "tbUserCode";
            this.tbUserCode.Size = new System.Drawing.Size(111, 23);
            this.tbUserCode.TabIndex = 14;
            this.tbUserCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbUserCode_KeyUp);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(128, 193);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(49, 21);
            this.btnLogin.TabIndex = 17;
            this.btnLogin.Text = "登录";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // plLogin
            // 
            this.plLogin.Controls.Add(this.lblLoginMessage);
            this.plLogin.Controls.Add(this.Logo);
            this.plLogin.Controls.Add(this.lblUserCode);
            this.plLogin.Controls.Add(this.tbUserCode);
            this.plLogin.Controls.Add(this.btnLogout);
            this.plLogin.Controls.Add(this.btnLogin);
            this.plLogin.Location = new System.Drawing.Point(16, 28);
            this.plLogin.Name = "plLogin";
            this.plLogin.Size = new System.Drawing.Size(206, 252);
            // 
            // Logo
            // 
            this.Logo.Image = ((System.Drawing.Image)(resources.GetObject("Logo.Image")));
            this.Logo.Location = new System.Drawing.Point(18, 49);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(159, 50);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(66, 193);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(49, 21);
            this.btnLogout.TabIndex = 17;
            this.btnLogout.Text = "退出";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // plOp
            // 
            this.plOp.Controls.Add(this.dgList);
            this.plOp.Controls.Add(this.btnConfirm);
            this.plOp.Controls.Add(this.lblItem);
            this.plOp.Controls.Add(this.tbBarCode);
            this.plOp.Controls.Add(this.lblMessage);
            this.plOp.Location = new System.Drawing.Point(0, 0);
            this.plOp.Name = "plOp";
            this.plOp.Size = new System.Drawing.Size(240, 294);
            // 
            // dgList
            // 
            this.dgList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgList.Location = new System.Drawing.Point(0, 46);
            this.dgList.Name = "dgList";
            this.dgList.RowHeadersVisible = false;
            this.dgList.Size = new System.Drawing.Size(240, 245);
            this.dgList.TabIndex = 16;
            // 
            // LocalSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(298, 325);
            this.Controls.Add(this.plLogin);
            this.Controls.Add(this.plOp);
            this.Location = new System.Drawing.Point(0, -23);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LocalSystem";
            this.Text = "LocalSystem";
            this.TopMost = true;
            this.plLogin.ResumeLayout(false);
            this.plOp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblLoginMessage;
        private System.Windows.Forms.Label lblUserCode;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox tbBarCode;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.TextBox tbUserCode;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel plLogin;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel plOp;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.DataGrid dgList;
        private System.Windows.Forms.DataGridTextBoxColumn columnBarCode;
        private System.Windows.Forms.DataGridTextBoxColumn columnItemCode;
        private System.Windows.Forms.DataGridTextBoxColumn columnQty;
    }
}

