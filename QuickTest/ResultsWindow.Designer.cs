
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
            this.ResultsTable_dgv = new System.Windows.Forms.DataGridView();
            this.AnsweredQuestionsNumber_lbl = new System.Windows.Forms.Label();
            this.QuestionNumberCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuestionTextCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChoosenVariantCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorrectVariantCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsTable_dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // ResultsTable_dgv
            // 
            this.ResultsTable_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsTable_dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QuestionNumberCol,
            this.QuestionTextCol,
            this.ChoosenVariantCol,
            this.CorrectVariantCol});
            this.ResultsTable_dgv.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ResultsTable_dgv.Location = new System.Drawing.Point(0, 38);
            this.ResultsTable_dgv.Name = "ResultsTable_dgv";
            this.ResultsTable_dgv.RowHeadersVisible = false;
            this.ResultsTable_dgv.Size = new System.Drawing.Size(560, 303);
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
            // QuestionNumberCol
            // 
            this.QuestionNumberCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.QuestionNumberCol.HeaderText = "#";
            this.QuestionNumberCol.Name = "QuestionNumberCol";
            this.QuestionNumberCol.ReadOnly = true;
            this.QuestionNumberCol.Width = 21;
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
            this.Controls.Add(this.AnsweredQuestionsNumber_lbl);
            this.Controls.Add(this.ResultsTable_dgv);
            this.Name = "ResultsWindow";
            this.Text = "ResultsWindow";
            ((System.ComponentModel.ISupportInitialize)(this.ResultsTable_dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ResultsTable_dgv;
        private System.Windows.Forms.Label AnsweredQuestionsNumber_lbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuestionNumberCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuestionTextCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChoosenVariantCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorrectVariantCol;
    }
}