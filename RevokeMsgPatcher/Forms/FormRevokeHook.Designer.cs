namespace RevokeMsgPatcher.Forms
{
    partial class FormRevokeHook
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWeixinPath = new System.Windows.Forms.TextBox();
            this.btnChoose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboGithubProxy = new System.Windows.Forms.ComboBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnOpenUi = new System.Windows.Forms.Button();
            this.btnInject = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnGithub = new System.Windows.Forms.Button();
            this.btnDiscuss = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 22);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "RevokeHook（带提示防撤回）";
            // 
            // lblDesc
            // 
            this.lblDesc.Location = new System.Drawing.Point(14, 42);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(500, 48);
            this.lblDesc.TabIndex = 1;
            this.lblDesc.Text = "基于 EEEEhex/RevokeHook 运行时 Hook：撤回提示会显示在对应消息下方，比纯 DLL 特征补丁更易辨认。本窗口仅负责下载与启动第三方工具。";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(14, 94);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(500, 36);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "状态";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "微信根目录：";
            // 
            // txtWeixinPath
            // 
            this.txtWeixinPath.Location = new System.Drawing.Point(109, 136);
            this.txtWeixinPath.Name = "txtWeixinPath";
            this.txtWeixinPath.Size = new System.Drawing.Size(350, 21);
            this.txtWeixinPath.TabIndex = 4;
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(465, 134);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(40, 23);
            this.btnChoose.TabIndex = 5;
            this.btnChoose.Text = "...";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "GitHub 代理：";
            // 
            // cboGithubProxy
            // 
            this.cboGithubProxy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboGithubProxy.FormattingEnabled = true;
            this.cboGithubProxy.Location = new System.Drawing.Point(109, 168);
            this.cboGithubProxy.Name = "cboGithubProxy";
            this.cboGithubProxy.Size = new System.Drawing.Size(396, 20);
            this.cboGithubProxy.TabIndex = 7;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(16, 208);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(110, 32);
            this.btnDownload.TabIndex = 8;
            this.btnDownload.Text = "下载/更新";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnOpenUi
            // 
            this.btnOpenUi.Location = new System.Drawing.Point(138, 208);
            this.btnOpenUi.Name = "btnOpenUi";
            this.btnOpenUi.Size = new System.Drawing.Size(110, 32);
            this.btnOpenUi.TabIndex = 9;
            this.btnOpenUi.Text = "搜索偏移";
            this.btnOpenUi.UseVisualStyleBackColor = true;
            this.btnOpenUi.Click += new System.EventHandler(this.btnOpenUi_Click);
            // 
            // btnInject
            // 
            this.btnInject.Location = new System.Drawing.Point(260, 208);
            this.btnInject.Name = "btnInject";
            this.btnInject.Size = new System.Drawing.Size(110, 32);
            this.btnInject.TabIndex = 10;
            this.btnInject.Text = "注入启动";
            this.btnInject.UseVisualStyleBackColor = true;
            this.btnInject.Click += new System.EventHandler(this.btnInject_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(382, 208);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(110, 32);
            this.btnOpenFolder.TabIndex = 11;
            this.btnOpenFolder.Text = "打开目录";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // btnGithub
            // 
            this.btnGithub.Location = new System.Drawing.Point(16, 254);
            this.btnGithub.Name = "btnGithub";
            this.btnGithub.Size = new System.Drawing.Size(110, 28);
            this.btnGithub.TabIndex = 12;
            this.btnGithub.Text = "项目主页";
            this.btnGithub.UseVisualStyleBackColor = true;
            this.btnGithub.Click += new System.EventHandler(this.btnGithub_Click);
            // 
            // btnDiscuss
            // 
            this.btnDiscuss.Location = new System.Drawing.Point(138, 254);
            this.btnDiscuss.Name = "btnDiscuss";
            this.btnDiscuss.Size = new System.Drawing.Size(110, 28);
            this.btnDiscuss.TabIndex = 13;
            this.btnDiscuss.Text = "问题反馈";
            this.btnDiscuss.UseVisualStyleBackColor = true;
            this.btnDiscuss.Click += new System.EventHandler(this.btnDiscuss_Click);
            // 
            // lblHint
            // 
            this.lblHint.ForeColor = System.Drawing.Color.DimGray;
            this.lblHint.Location = new System.Drawing.Point(14, 292);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(500, 48);
            this.lblHint.TabIndex = 14;
            this.lblHint.Text = "建议：微信 4.0 选此 Hook 方案获得「消息下方提示」。先搜索偏移并保存，再注入。远程撤回提示可能需重新进入聊天窗口才刷新。";
            // 
            // FormRevokeHook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 351);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnDiscuss);
            this.Controls.Add(this.btnGithub);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnInject);
            this.Controls.Add(this.btnOpenUi);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.cboGithubProxy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.txtWeixinPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormRevokeHook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RevokeHook 带提示防撤回";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWeixinPath;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboGithubProxy;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnOpenUi;
        private System.Windows.Forms.Button btnInject;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnGithub;
        private System.Windows.Forms.Button btnDiscuss;
        private System.Windows.Forms.Label lblHint;
    }
}
