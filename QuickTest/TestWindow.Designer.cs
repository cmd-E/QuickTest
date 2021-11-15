
namespace QuickTest
{
    partial class TestWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestWindow));
            this.Question_lbl = new System.Windows.Forms.Label();
            this.NextQ_btn = new System.Windows.Forms.Button();
            this.PrevQ_btn = new System.Windows.Forms.Button();
            this.Done_btn = new System.Windows.Forms.Button();
            this.TestsScroll_panel = new System.Windows.Forms.Panel();
            this.QuestionNumber_lbl = new System.Windows.Forms.Label();
            this.Variant1_cb = new System.Windows.Forms.CheckBox();
            this.Variant2_cb = new System.Windows.Forms.CheckBox();
            this.Variant3_cb = new System.Windows.Forms.CheckBox();
            this.Variant4_cb = new System.Windows.Forms.CheckBox();
            this.Variant5_cb = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Question_lbl
            // 
            this.Question_lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Question_lbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.Question_lbl.Location = new System.Drawing.Point(0, 0);
            this.Question_lbl.MinimumSize = new System.Drawing.Size(800, 72);
            this.Question_lbl.Name = "Question_lbl";
            this.Question_lbl.Size = new System.Drawing.Size(800, 72);
            this.Question_lbl.TabIndex = 0;
            this.Question_lbl.Text = "Question";
            // 
            // NextQ_btn
            // 
            this.NextQ_btn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NextQ_btn.Location = new System.Drawing.Point(589, 68);
            this.NextQ_btn.Name = "NextQ_btn";
            this.NextQ_btn.Size = new System.Drawing.Size(75, 23);
            this.NextQ_btn.TabIndex = 6;
            this.NextQ_btn.Text = "Далее";
            this.NextQ_btn.UseVisualStyleBackColor = true;
            this.NextQ_btn.Click += new System.EventHandler(this.NextQ_btn_Click);
            // 
            // PrevQ_btn
            // 
            this.PrevQ_btn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PrevQ_btn.Location = new System.Drawing.Point(39, 71);
            this.PrevQ_btn.Name = "PrevQ_btn";
            this.PrevQ_btn.Size = new System.Drawing.Size(75, 23);
            this.PrevQ_btn.TabIndex = 8;
            this.PrevQ_btn.Text = "Назад";
            this.PrevQ_btn.UseVisualStyleBackColor = true;
            this.PrevQ_btn.Click += new System.EventHandler(this.PrevQ_btn_Click);
            // 
            // Done_btn
            // 
            this.Done_btn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Done_btn.Enabled = false;
            this.Done_btn.Location = new System.Drawing.Point(670, 68);
            this.Done_btn.Name = "Done_btn";
            this.Done_btn.Size = new System.Drawing.Size(118, 23);
            this.Done_btn.TabIndex = 9;
            this.Done_btn.Text = "Закончить тест";
            this.Done_btn.UseVisualStyleBackColor = true;
            this.Done_btn.Click += new System.EventHandler(this.Done_btn_Click);
            // 
            // TestsScroll_panel
            // 
            this.TestsScroll_panel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TestsScroll_panel.AutoScroll = true;
            this.TestsScroll_panel.BackColor = System.Drawing.SystemColors.Control;
            this.TestsScroll_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TestsScroll_panel.Location = new System.Drawing.Point(120, 41);
            this.TestsScroll_panel.Name = "TestsScroll_panel";
            this.TestsScroll_panel.Size = new System.Drawing.Size(463, 53);
            this.TestsScroll_panel.TabIndex = 10;
            // 
            // QuestionNumber_lbl
            // 
            this.QuestionNumber_lbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.QuestionNumber_lbl.AutoSize = true;
            this.QuestionNumber_lbl.Location = new System.Drawing.Point(349, 8);
            this.QuestionNumber_lbl.Name = "QuestionNumber_lbl";
            this.QuestionNumber_lbl.Size = new System.Drawing.Size(102, 13);
            this.QuestionNumber_lbl.TabIndex = 0;
            this.QuestionNumber_lbl.Text = "QuestionNumber_lbl";
            this.QuestionNumber_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Variant1_cb
            // 
            this.Variant1_cb.Location = new System.Drawing.Point(12, 75);
            this.Variant1_cb.Name = "Variant1_cb";
            this.Variant1_cb.Size = new System.Drawing.Size(788, 39);
            this.Variant1_cb.TabIndex = 11;
            this.Variant1_cb.Tag = "1";
            this.Variant1_cb.Text = "Variant1";
            this.Variant1_cb.UseVisualStyleBackColor = true;
            // 
            // Variant2_cb
            // 
            this.Variant2_cb.Location = new System.Drawing.Point(12, 120);
            this.Variant2_cb.Name = "Variant2_cb";
            this.Variant2_cb.Size = new System.Drawing.Size(788, 39);
            this.Variant2_cb.TabIndex = 12;
            this.Variant2_cb.Tag = "2";
            this.Variant2_cb.Text = "Variant2";
            this.Variant2_cb.UseVisualStyleBackColor = true;
            // 
            // Variant3_cb
            // 
            this.Variant3_cb.Location = new System.Drawing.Point(12, 165);
            this.Variant3_cb.Name = "Variant3_cb";
            this.Variant3_cb.Size = new System.Drawing.Size(788, 39);
            this.Variant3_cb.TabIndex = 13;
            this.Variant3_cb.Tag = "3";
            this.Variant3_cb.Text = "Variant3";
            this.Variant3_cb.UseVisualStyleBackColor = true;
            // 
            // Variant4_cb
            // 
            this.Variant4_cb.Location = new System.Drawing.Point(12, 210);
            this.Variant4_cb.Name = "Variant4_cb";
            this.Variant4_cb.Size = new System.Drawing.Size(788, 39);
            this.Variant4_cb.TabIndex = 14;
            this.Variant4_cb.Tag = "4";
            this.Variant4_cb.Text = "Variant4";
            this.Variant4_cb.UseVisualStyleBackColor = true;
            // 
            // Variant5_cb
            // 
            this.Variant5_cb.Location = new System.Drawing.Point(12, 255);
            this.Variant5_cb.Name = "Variant5_cb";
            this.Variant5_cb.Size = new System.Drawing.Size(788, 39);
            this.Variant5_cb.TabIndex = 15;
            this.Variant5_cb.Tag = "5";
            this.Variant5_cb.Text = "Variant5";
            this.Variant5_cb.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.QuestionNumber_lbl);
            this.groupBox1.Controls.Add(this.TestsScroll_panel);
            this.groupBox1.Controls.Add(this.NextQ_btn);
            this.groupBox1.Controls.Add(this.PrevQ_btn);
            this.groupBox1.Controls.Add(this.Done_btn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 300);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 96);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Управление";
            // 
            // TestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 396);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Variant5_cb);
            this.Controls.Add(this.Variant4_cb);
            this.Controls.Add(this.Variant3_cb);
            this.Controls.Add(this.Variant2_cb);
            this.Controls.Add(this.Variant1_cb);
            this.Controls.Add(this.Question_lbl);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(816, 435);
            this.Name = "TestWindow";
            this.Text = "Тестирование";
            this.Load += new System.EventHandler(this.TestWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Question_lbl;
        private System.Windows.Forms.Button NextQ_btn;
        private System.Windows.Forms.Button PrevQ_btn;
        private System.Windows.Forms.Button Done_btn;
        private System.Windows.Forms.Panel TestsScroll_panel;
        private System.Windows.Forms.Label QuestionNumber_lbl;
        private System.Windows.Forms.CheckBox Variant1_cb;
        private System.Windows.Forms.CheckBox Variant2_cb;
        private System.Windows.Forms.CheckBox Variant3_cb;
        private System.Windows.Forms.CheckBox Variant4_cb;
        private System.Windows.Forms.CheckBox Variant5_cb;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}