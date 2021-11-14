
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
            this.Question_lbl = new System.Windows.Forms.Label();
            this.Variant1_rb = new System.Windows.Forms.RadioButton();
            this.Variant2_rb = new System.Windows.Forms.RadioButton();
            this.Variant3_rb = new System.Windows.Forms.RadioButton();
            this.Variant4_rb = new System.Windows.Forms.RadioButton();
            this.Variant5_rb = new System.Windows.Forms.RadioButton();
            this.NextQ_btn = new System.Windows.Forms.Button();
            this.PrevQ_btn = new System.Windows.Forms.Button();
            this.Done_btn = new System.Windows.Forms.Button();
            this.TestsScroll_panel = new System.Windows.Forms.Panel();
            this.QuestionNumber_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Question_lbl
            // 
            this.Question_lbl.AutoSize = true;
            this.Question_lbl.Location = new System.Drawing.Point(36, 32);
            this.Question_lbl.Name = "Question_lbl";
            this.Question_lbl.Size = new System.Drawing.Size(49, 13);
            this.Question_lbl.TabIndex = 0;
            this.Question_lbl.Text = "Question";
            // 
            // Variant1_rb
            // 
            this.Variant1_rb.AutoSize = true;
            this.Variant1_rb.Location = new System.Drawing.Point(39, 71);
            this.Variant1_rb.Name = "Variant1_rb";
            this.Variant1_rb.Size = new System.Drawing.Size(58, 17);
            this.Variant1_rb.TabIndex = 1;
            this.Variant1_rb.TabStop = true;
            this.Variant1_rb.Tag = "1";
            this.Variant1_rb.Text = "Variant";
            this.Variant1_rb.UseVisualStyleBackColor = true;
            // 
            // Variant2_rb
            // 
            this.Variant2_rb.AutoSize = true;
            this.Variant2_rb.Location = new System.Drawing.Point(39, 107);
            this.Variant2_rb.Name = "Variant2_rb";
            this.Variant2_rb.Size = new System.Drawing.Size(58, 17);
            this.Variant2_rb.TabIndex = 2;
            this.Variant2_rb.TabStop = true;
            this.Variant2_rb.Tag = "2";
            this.Variant2_rb.Text = "Variant";
            this.Variant2_rb.UseVisualStyleBackColor = true;
            // 
            // Variant3_rb
            // 
            this.Variant3_rb.AutoSize = true;
            this.Variant3_rb.Location = new System.Drawing.Point(39, 144);
            this.Variant3_rb.Name = "Variant3_rb";
            this.Variant3_rb.Size = new System.Drawing.Size(58, 17);
            this.Variant3_rb.TabIndex = 3;
            this.Variant3_rb.TabStop = true;
            this.Variant3_rb.Tag = "3";
            this.Variant3_rb.Text = "Variant";
            this.Variant3_rb.UseVisualStyleBackColor = true;
            // 
            // Variant4_rb
            // 
            this.Variant4_rb.AutoSize = true;
            this.Variant4_rb.Location = new System.Drawing.Point(39, 176);
            this.Variant4_rb.Name = "Variant4_rb";
            this.Variant4_rb.Size = new System.Drawing.Size(58, 17);
            this.Variant4_rb.TabIndex = 4;
            this.Variant4_rb.TabStop = true;
            this.Variant4_rb.Tag = "4";
            this.Variant4_rb.Text = "Variant";
            this.Variant4_rb.UseVisualStyleBackColor = true;
            // 
            // Variant5_rb
            // 
            this.Variant5_rb.AutoSize = true;
            this.Variant5_rb.Location = new System.Drawing.Point(39, 209);
            this.Variant5_rb.Name = "Variant5_rb";
            this.Variant5_rb.Size = new System.Drawing.Size(58, 17);
            this.Variant5_rb.TabIndex = 5;
            this.Variant5_rb.TabStop = true;
            this.Variant5_rb.Tag = "5";
            this.Variant5_rb.Text = "Variant";
            this.Variant5_rb.UseVisualStyleBackColor = true;
            // 
            // NextQ_btn
            // 
            this.NextQ_btn.Location = new System.Drawing.Point(589, 327);
            this.NextQ_btn.Name = "NextQ_btn";
            this.NextQ_btn.Size = new System.Drawing.Size(75, 23);
            this.NextQ_btn.TabIndex = 6;
            this.NextQ_btn.Text = "Далее";
            this.NextQ_btn.UseVisualStyleBackColor = true;
            this.NextQ_btn.Click += new System.EventHandler(this.NextQ_btn_Click);
            // 
            // PrevQ_btn
            // 
            this.PrevQ_btn.Location = new System.Drawing.Point(39, 330);
            this.PrevQ_btn.Name = "PrevQ_btn";
            this.PrevQ_btn.Size = new System.Drawing.Size(75, 23);
            this.PrevQ_btn.TabIndex = 8;
            this.PrevQ_btn.Text = "Назад";
            this.PrevQ_btn.UseVisualStyleBackColor = true;
            this.PrevQ_btn.Click += new System.EventHandler(this.PrevQ_btn_Click);
            // 
            // Done_btn
            // 
            this.Done_btn.Location = new System.Drawing.Point(670, 327);
            this.Done_btn.Name = "Done_btn";
            this.Done_btn.Size = new System.Drawing.Size(118, 23);
            this.Done_btn.TabIndex = 9;
            this.Done_btn.Text = "Закончить тест";
            this.Done_btn.UseVisualStyleBackColor = true;
            this.Done_btn.Click += new System.EventHandler(this.Done_btn_Click);
            // 
            // TestsScroll_panel
            // 
            this.TestsScroll_panel.AutoScroll = true;
            this.TestsScroll_panel.BackColor = System.Drawing.SystemColors.Control;
            this.TestsScroll_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TestsScroll_panel.Location = new System.Drawing.Point(120, 300);
            this.TestsScroll_panel.Name = "TestsScroll_panel";
            this.TestsScroll_panel.Size = new System.Drawing.Size(463, 53);
            this.TestsScroll_panel.TabIndex = 10;
            // 
            // QuestionNumber_lbl
            // 
            this.QuestionNumber_lbl.AutoSize = true;
            this.QuestionNumber_lbl.Location = new System.Drawing.Point(326, 261);
            this.QuestionNumber_lbl.Name = "QuestionNumber_lbl";
            this.QuestionNumber_lbl.Size = new System.Drawing.Size(102, 13);
            this.QuestionNumber_lbl.TabIndex = 0;
            this.QuestionNumber_lbl.Text = "QuestionNumber_lbl";
            // 
            // TestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 362);
            this.Controls.Add(this.QuestionNumber_lbl);
            this.Controls.Add(this.TestsScroll_panel);
            this.Controls.Add(this.Done_btn);
            this.Controls.Add(this.PrevQ_btn);
            this.Controls.Add(this.NextQ_btn);
            this.Controls.Add(this.Variant5_rb);
            this.Controls.Add(this.Variant4_rb);
            this.Controls.Add(this.Variant3_rb);
            this.Controls.Add(this.Variant2_rb);
            this.Controls.Add(this.Variant1_rb);
            this.Controls.Add(this.Question_lbl);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "TestWindow";
            this.Text = "TestWindow";
            this.Load += new System.EventHandler(this.TestWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Question_lbl;
        private System.Windows.Forms.RadioButton Variant1_rb;
        private System.Windows.Forms.RadioButton Variant2_rb;
        private System.Windows.Forms.RadioButton Variant3_rb;
        private System.Windows.Forms.RadioButton Variant4_rb;
        private System.Windows.Forms.RadioButton Variant5_rb;
        private System.Windows.Forms.Button NextQ_btn;
        private System.Windows.Forms.Button PrevQ_btn;
        private System.Windows.Forms.Button Done_btn;
        private System.Windows.Forms.Panel TestsScroll_panel;
        private System.Windows.Forms.Label QuestionNumber_lbl;
    }
}