
namespace QuickTest
{
    partial class ResultsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultsWindow));
            this.ResultsTable_dgv = new System.Windows.Forms.DataGridView();
            this.AnsweredQuestionsNumber_lbl = new System.Windows.Forms.Label();
            this.showWrongOnly_cb = new System.Windows.Forms.CheckBox();
            this.QuestionNumberCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuestionTextCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChoosenVariantCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorrectVariantCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsTable_dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // ResultsTable_dgv
            // 
            this.ResultsTable_dgv.AllowUserToAddRows = false;
            this.ResultsTable_dgv.AllowUserToDeleteRows = false;
            this.ResultsTable_dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsTable_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsTable_dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QuestionNumberCol,
            this.QuestionTextCol,
            this.ChoosenVariantCol,
            this.CorrectVariantCol});
            this.ResultsTable_dgv.Location = new System.Drawing.Point(0, 65);
            this.ResultsTable_dgv.Name = "ResultsTable_dgv";
            this.ResultsTable_dgv.ReadOnly = true;
            this.ResultsTable_dgv.RowHeadersVisible = false;
            this.ResultsTable_dgv.Size = new System.Drawing.Size(560, 276);
            this.ResultsTable_dgv.TabIndex = 0;
            // 
            // AnsweredQuestionsNumber_lbl
            // 
            this.AnsweredQuestionsNumber_lbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnsweredQuestionsNumber_lbl.Location = new System.Drawing.Point(0, 0);
            this.AnsweredQuestionsNumber_lbl.Name = "AnsweredQuestionsNumber_lbl";
            this.AnsweredQuestionsNumber_lbl.Size = new System.Drawing.Size(560, 35);
            this.AnsweredQuestionsNumber_lbl.TabIndex = 1;
            this.AnsweredQuestionsNumber_lbl.Text = "Отвечено: X/X";
            this.AnsweredQuestionsNumber_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // showWrongOnly_cb
            // 
            this.showWrongOnly_cb.AutoSize = true;
            this.showWrongOnly_cb.Location = new System.Drawing.Point(12, 38);
            this.showWrongOnly_cb.Name = "showWrongOnly_cb";
            this.showWrongOnly_cb.Size = new System.Drawing.Size(190, 17);
            this.showWrongOnly_cb.TabIndex = 2;
            this.showWrongOnly_cb.Text = "Показать только неправильные";
            this.showWrongOnly_cb.UseVisualStyleBackColor = true;
            this.showWrongOnly_cb.CheckedChanged += new System.EventHandler(this.showWrongOnly_cb_CheckedChanged);
            // 
            // QuestionNumberCol
            // 
            this.QuestionNumberCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.QuestionNumberCol.HeaderText = "#";
            this.QuestionNumberCol.Name = "QuestionNumberCol";
            this.QuestionNumberCol.ReadOnly = true;
            this.QuestionNumberCol.Width = 39;
            // 
            // QuestionTextCol
            // 
            this.QuestionTextCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.QuestionTextCol.HeaderText = "Вопрос";
            this.QuestionTextCol.Name = "QuestionTextCol";
            this.QuestionTextCol.ReadOnly = true;
            this.QuestionTextCol.Width = 69;
            // 
            // ChoosenVariantCol
            // 
            this.ChoosenVariantCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ChoosenVariantCol.HeaderText = "Выбранный вариант";
            this.ChoosenVariantCol.Name = "ChoosenVariantCol";
            this.ChoosenVariantCol.ReadOnly = true;
            this.ChoosenVariantCol.Width = 123;
            // 
            // CorrectVariantCol
            // 
            this.CorrectVariantCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CorrectVariantCol.HeaderText = "Правильный варинат";
            this.CorrectVariantCol.Name = "CorrectVariantCol";
            this.CorrectVariantCol.ReadOnly = true;
            this.CorrectVariantCol.Width = 128;
            // 
            // ResultsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 341);
            this.Controls.Add(this.showWrongOnly_cb);
            this.Controls.Add(this.AnsweredQuestionsNumber_lbl);
            this.Controls.Add(this.ResultsTable_dgv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(576, 380);
            this.Name = "ResultsWindow";
            this.Text = "Результат";
            ((System.ComponentModel.ISupportInitialize)(this.ResultsTable_dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ResultsTable_dgv;
        private System.Windows.Forms.Label AnsweredQuestionsNumber_lbl;
        private System.Windows.Forms.CheckBox showWrongOnly_cb;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuestionNumberCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuestionTextCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChoosenVariantCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorrectVariantCol;
    }
}