﻿namespace ToyChromium
{
    partial class MainFrm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.chPl = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.debug = new System.Windows.Forms.Button();
            this.chPl.SuspendLayout();
            this.SuspendLayout();
            // 
            // chPl
            // 
            this.chPl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chPl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.chPl.Controls.Add(this.lblStatus);
            this.chPl.Location = new System.Drawing.Point(0, 0);
            this.chPl.Name = "chPl";
            this.chPl.Size = new System.Drawing.Size(200, 100);
            this.chPl.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Black;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 12);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "已启动";
            // 
            // debug
            // 
            this.debug.Location = new System.Drawing.Point(691, 12);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(97, 89);
            this.debug.TabIndex = 1;
            this.debug.Text = "debug";
            this.debug.UseVisualStyleBackColor = true;
            this.debug.Visible = false;
            this.debug.Click += new System.EventHandler(this.Debug_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.chPl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "MainFrm";
            this.Text = "ToyChromium";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.chPl.ResumeLayout(false);
            this.chPl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel chPl;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button debug;
    }
}

