
namespace QuickTest.Windows
{
    partial class AboutWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.AppInfo_lbl = new System.Windows.Forms.Label();
            this.CreatorLink_lbl = new System.Windows.Forms.LinkLabel();
            this.CheckUpdatesAtStartup_cb = new System.Windows.Forms.CheckBox();
            this.CheckUpdates_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AppInfo_lbl
            // 
            this.AppInfo_lbl.Location = new System.Drawing.Point(12, 9);
            this.AppInfo_lbl.Name = "AppInfo_lbl";
            this.AppInfo_lbl.Size = new System.Drawing.Size(320, 23);
            this.AppInfo_lbl.TabIndex = 0;
            this.AppInfo_lbl.Text = "App name, version";
            this.AppInfo_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CreatorLink_lbl
            // 
            this.CreatorLink_lbl.Location = new System.Drawing.Point(15, 32);
            this.CreatorLink_lbl.Name = "CreatorLink_lbl";
            this.CreatorLink_lbl.Size = new System.Drawing.Size(317, 23);
            this.CreatorLink_lbl.TabIndex = 1;
            this.CreatorLink_lbl.TabStop = true;
            this.CreatorLink_lbl.Text = "By creator";
            this.CreatorLink_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CreatorLink_lbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CreatorLink_lbl_LinkClicked);
            // 
            // CheckUpdatesAtStartup_cb
            // 
            this.CheckUpdatesAtStartup_cb.Checked = true;
            this.CheckUpdatesAtStartup_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckUpdatesAtStartup_cb.Location = new System.Drawing.Point(18, 66);
            this.CheckUpdatesAtStartup_cb.Name = "CheckUpdatesAtStartup_cb";
            this.CheckUpdatesAtStartup_cb.Size = new System.Drawing.Size(168, 39);
            this.CheckUpdatesAtStartup_cb.TabIndex = 2;
            this.CheckUpdatesAtStartup_cb.Text = "Проверять обновления при запуске";
            this.CheckUpdatesAtStartup_cb.UseVisualStyleBackColor = true;
            this.CheckUpdatesAtStartup_cb.CheckedChanged += new System.EventHandler(this.CheckUpdatesAtStartup_cb_CheckedChanged);
            // 
            // CheckUpdates_btn
            // 
            this.CheckUpdates_btn.Location = new System.Drawing.Point(186, 66);
            this.CheckUpdates_btn.Name = "CheckUpdates_btn";
            this.CheckUpdates_btn.Size = new System.Drawing.Size(146, 39);
            this.CheckUpdates_btn.TabIndex = 3;
            this.CheckUpdates_btn.Text = "Проверить обновления";
            this.CheckUpdates_btn.UseVisualStyleBackColor = true;
            this.CheckUpdates_btn.Click += new System.EventHandler(this.CheckUpdates_btn_Click);
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 117);
            this.Controls.Add(this.CheckUpdates_btn);
            this.Controls.Add(this.CheckUpdatesAtStartup_cb);
            this.Controls.Add(this.CreatorLink_lbl);
            this.Controls.Add(this.AppInfo_lbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(360, 156);
            this.MinimumSize = new System.Drawing.Size(360, 156);
            this.Name = "AboutWindow";
            this.ShowInTaskbar = false;
            this.Text = "О программе";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label AppInfo_lbl;
        private System.Windows.Forms.LinkLabel CreatorLink_lbl;
        private System.Windows.Forms.CheckBox CheckUpdatesAtStartup_cb;
        private System.Windows.Forms.Button CheckUpdates_btn;
    }
}