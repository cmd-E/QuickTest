
namespace QuickTest
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.chooseTestBtn = new System.Windows.Forms.Button();
            this.testPathLbl = new System.Windows.Forms.Label();
            this.numberOfQuestions_ud = new System.Windows.Forms.NumericUpDown();
            this.numberOfQuestions_lbl = new System.Windows.Forms.Label();
            this.StartTest_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.QuestionTag_tb = new System.Windows.Forms.TextBox();
            this.VariantTag_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfQuestions_ud)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chooseTestBtn
            // 
            this.chooseTestBtn.Enabled = false;
            this.chooseTestBtn.Location = new System.Drawing.Point(214, 90);
            this.chooseTestBtn.Name = "chooseTestBtn";
            this.chooseTestBtn.Size = new System.Drawing.Size(99, 25);
            this.chooseTestBtn.TabIndex = 1;
            this.chooseTestBtn.Text = "Выбрать тест";
            this.chooseTestBtn.UseVisualStyleBackColor = true;
            this.chooseTestBtn.Click += new System.EventHandler(this.ChooseTestBtn_Click);
            // 
            // testPathLbl
            // 
            this.testPathLbl.Location = new System.Drawing.Point(6, 16);
            this.testPathLbl.Name = "testPathLbl";
            this.testPathLbl.Size = new System.Drawing.Size(515, 29);
            this.testPathLbl.TabIndex = 2;
            this.testPathLbl.Text = "Путь";
            this.testPathLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.testPathLbl.Visible = false;
            // 
            // numberOfQuestions_ud
            // 
            this.numberOfQuestions_ud.Location = new System.Drawing.Point(108, 53);
            this.numberOfQuestions_ud.Name = "numberOfQuestions_ud";
            this.numberOfQuestions_ud.Size = new System.Drawing.Size(59, 20);
            this.numberOfQuestions_ud.TabIndex = 4;
            this.numberOfQuestions_ud.Visible = false;
            // 
            // numberOfQuestions_lbl
            // 
            this.numberOfQuestions_lbl.AutoSize = true;
            this.numberOfQuestions_lbl.Location = new System.Drawing.Point(6, 55);
            this.numberOfQuestions_lbl.Name = "numberOfQuestions_lbl";
            this.numberOfQuestions_lbl.Size = new System.Drawing.Size(96, 13);
            this.numberOfQuestions_lbl.TabIndex = 5;
            this.numberOfQuestions_lbl.Text = "Вопросов в тесте";
            this.numberOfQuestions_lbl.Visible = false;
            // 
            // StartTest_btn
            // 
            this.StartTest_btn.Enabled = false;
            this.StartTest_btn.Location = new System.Drawing.Point(416, 72);
            this.StartTest_btn.Name = "StartTest_btn";
            this.StartTest_btn.Size = new System.Drawing.Size(99, 25);
            this.StartTest_btn.TabIndex = 6;
            this.StartTest_btn.Text = "Начать тест";
            this.StartTest_btn.UseVisualStyleBackColor = true;
            this.StartTest_btn.Click += new System.EventHandler(this.StartTest_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.StartTest_btn);
            this.groupBox1.Controls.Add(this.numberOfQuestions_lbl);
            this.groupBox1.Controls.Add(this.numberOfQuestions_ud);
            this.groupBox1.Controls.Add(this.testPathLbl);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 109);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры теста";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Тэг вопроса";
            // 
            // QuestionTag_tb
            // 
            this.QuestionTag_tb.Location = new System.Drawing.Point(264, 12);
            this.QuestionTag_tb.Name = "QuestionTag_tb";
            this.QuestionTag_tb.Size = new System.Drawing.Size(100, 20);
            this.QuestionTag_tb.TabIndex = 7;
            this.QuestionTag_tb.Text = "<question>";
            // 
            // VariantTag_tb
            // 
            this.VariantTag_tb.Location = new System.Drawing.Point(264, 38);
            this.VariantTag_tb.Name = "VariantTag_tb";
            this.VariantTag_tb.Size = new System.Drawing.Size(100, 20);
            this.VariantTag_tb.TabIndex = 8;
            this.VariantTag_tb.Text = "<variant>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Тэг варианта";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.chooseTestBtn);
            this.groupBox2.Controls.Add(this.VariantTag_tb);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.QuestionTag_tb);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(527, 121);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры чтения файла";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 236);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(543, 136);
            this.Name = "MainWindow";
            this.Text = "QuickTest";
            ((System.ComponentModel.ISupportInitialize)(this.numberOfQuestions_ud)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button chooseTestBtn;
        private System.Windows.Forms.Label testPathLbl;
        private System.Windows.Forms.NumericUpDown numberOfQuestions_ud;
        private System.Windows.Forms.Label numberOfQuestions_lbl;
        private System.Windows.Forms.Button StartTest_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox VariantTag_tb;
        private System.Windows.Forms.TextBox QuestionTag_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

