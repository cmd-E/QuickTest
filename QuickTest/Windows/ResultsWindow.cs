using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuickTest.Classes;
namespace QuickTest
{
    public partial class ResultsWindow : Form
    {
        private readonly IList<ResultRow> results;
        public ResultsWindow(IList<ResultRow> results)
        {
            this.results = results;
            InitializeComponent();
            int correct = results.Count(r => r.IsCorrect);
            double fraction = results.Count == 0 ? 0 : (double)correct / results.Count;
            AnsweredQuestionsNumber_lbl.Text =
                string.Format("Отвечено: {0}/{1} ({2:0.##}%)", correct, results.Count, 100 * fraction);
            AnsweredQuestionsNumber_lbl.BackColor = new HSV((float)(120 * fraction), 1, 0.5f).ColorFromHSL();
            Render();
        }
        private void Render()
        {
            ResultsTable_dgv.Rows.Clear();
            foreach (var result in results.Where(r => !showWrongOnly_cb.Checked || !r.IsCorrect))
            {
                int index = ResultsTable_dgv.Rows.Add();
                var row = ResultsTable_dgv.Rows[index];
                row.Cells["QuestionNumberCol"].Value = result.Number;
                row.Cells["QuestionTextCol"].Value = result.Question;
                row.Cells["ChoosenVariantCol"].Value = result.Selected;
                row.Cells["CorrectVariantCol"].Value = result.Correct;
                row.DefaultCellStyle.BackColor = result.IsCorrect ? Color.Lime : Color.Firebrick;
            }
        }
        private void showWrongOnly_cb_CheckedChanged(object sender, EventArgs e)
        {
            Render();
        }
    }
}
