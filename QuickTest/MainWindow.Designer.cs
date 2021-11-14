
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
            this.chooseTestBtn = new System.Windows.Forms.Button();
            this.testPathLbl = new System.Windows.Forms.Label();
            this.numberOfQuestions_ud = new System.Windows.Forms.NumericUpDown();
            this.numberOfQuestions_lbl = new System.Windows.Forms.Label();
            this.StartTest_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfQuestions_ud)).BeginInit();
            this.SuspendLayout();
            // 
            // chooseTestBtn
            // 
            this.chooseTestBtn.Location = new System.Drawing.Point(159, 12);
            this.chooseTestBtn.Name = "chooseTestBtn";
            this.chooseTestBtn.Size = new System.Drawing.Size(99, 25);
            this.chooseTestBtn.TabIndex = 1;
            this.chooseTestBtn.Text = "Выбрать тест";
            this.chooseTestBtn.UseVisualStyleBackColor = true;
            this.chooseTestBtn.Click += new System.EventHandler(this.ChooseTestBtn_Click);
            // 
            // testPathLbl
            // 
            this.testPathLbl.Location = new System.Drawing.Point(-5, 40);
            this.testPathLbl.Name = "testPathLbl";
            this.testPathLbl.Size = new System.Drawing.Size(538, 29);
            this.testPathLbl.TabIndex = 2;
            this.testPathLbl.Text = "Путь";
            this.testPathLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.testPathLbl.Visible = false;
            // 
            // numberOfQuestions_ud
            // 
            this.numberOfQuestions_ud.Location = new System.Drawing.Point(281, 67);
            this.numberOfQuestions_ud.Name = "numberOfQuestions_ud";
            this.numberOfQuestions_ud.Size = new System.Drawing.Size(59, 20);
            this.numberOfQuestions_ud.TabIndex = 4;
            this.numberOfQuestions_ud.Visible = false;
            // 
            // numberOfQuestions_lbl
            // 
            this.numberOfQuestions_lbl.AutoSize = true;
            this.numberOfQuestions_lbl.Location = new System.Drawing.Point(179, 69);
            this.numberOfQuestions_lbl.Name = "numberOfQuestions_lbl";
            this.numberOfQuestions_lbl.Size = new System.Drawing.Size(96, 13);
            this.numberOfQuestions_lbl.TabIndex = 5;
            this.numberOfQuestions_lbl.Text = "Вопросов в тесте";
            this.numberOfQuestions_lbl.Visible = false;
            // 
            // StartTest_btn
            // 
            this.StartTest_btn.Enabled = false;
            this.StartTest_btn.Location = new System.Drawing.Point(264, 12);
            this.StartTest_btn.Name = "StartTest_btn";
            this.StartTest_btn.Size = new System.Drawing.Size(99, 25);
            this.StartTest_btn.TabIndex = 6;
            this.StartTest_btn.Text = "Начать тест";
            this.StartTest_btn.UseVisualStyleBackColor = true;
            this.StartTest_btn.Click += new System.EventHandler(this.StartTest_btn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 97);
            this.Controls.Add(this.StartTest_btn);
            this.Controls.Add(this.numberOfQuestions_lbl);
            this.Controls.Add(this.numberOfQuestions_ud);
            this.Controls.Add(this.testPathLbl);
            this.Controls.Add(this.chooseTestBtn);
            this.Name = "MainWindow";
            this.Text = "QuickTest";
            ((System.ComponentModel.ISupportInitialize)(this.numberOfQuestions_ud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button chooseTestBtn;
        private System.Windows.Forms.Label testPathLbl;
        private System.Windows.Forms.NumericUpDown numberOfQuestions_ud;
        private System.Windows.Forms.Label numberOfQuestions_lbl;
        private System.Windows.Forms.Button StartTest_btn;
    }
}

