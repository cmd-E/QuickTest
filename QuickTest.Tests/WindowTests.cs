using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;
using QuickTest.Classes;
using Xunit;

namespace QuickTest.Tests
{
    [Trait("Category", "Integration")]
    public class WindowTests
    {
        private static void OnSta(Action action)
        {
            ExceptionDispatchInfo error = null;
            var thread = new Thread(() => { try { action(); } catch (Exception e) { error = ExceptionDispatchInfo.Capture(e); } });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (error != null) error.Throw();
        }

        [Fact]
        public void ResultsRenderScoreAndFilterWrongAnswersWithoutDuplicatingRows()
        {
            OnSta(() =>
            {
                var results = new List<ResultRow> {
                    new ResultRow { Number = 1, Question = "Right", Selected = "A", Correct = "A", IsCorrect = true },
                    new ResultRow { Number = 2, Question = "Wrong", Selected = "B", Correct = "A", IsCorrect = false } };
                using (var window = new ResultsWindow(results))
                {
                    var grid = (DataGridView)window.Controls.Find("ResultsTable_dgv", true).Single();
                    var label = (Label)window.Controls.Find("AnsweredQuestionsNumber_lbl", true).Single();
                    var filter = (CheckBox)window.Controls.Find("showWrongOnly_cb", true).Single();
                    Assert.Contains("1/2 (50%)", label.Text);
                    filter.Checked = false;
                    Assert.Equal(2, grid.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow));
                    filter.Checked = true;
                    var row = Assert.Single(grid.Rows.Cast<DataGridViewRow>(), r => !r.IsNewRow);
                    Assert.Equal("Wrong", row.Cells["QuestionTextCol"].Value);
                    Assert.Equal(Color.Firebrick, row.DefaultCellStyle.BackColor);
                    filter.Checked = false;
                    Assert.Equal(2, grid.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow));
                }
            });
        }

        [Fact]
        public void EmptyResultsDoNotDivideByZero()
        {
            OnSta(() =>
            {
                using (var window = new ResultsWindow(new List<ResultRow>()))
                {
                    var label = (Label)window.Controls.Find("AnsweredQuestionsNumber_lbl", true).Single();
                    Assert.Contains("0/0 (0%)", label.Text);
                }
            });
        }

        [Fact]
        public void NewTestWindowHasFiveUnselectedOptionsAndCannotFinish()
        {
            OnSta(() =>
            {
                var question = QuestionParserTests.Question();
                using (var window = new TestWindow(new List<Question> { question }, 1))
                {
                    var options = window.Controls.OfType<CheckBox>().ToArray();
                    Assert.Equal(5, options.Length);
                    Assert.All(options, option => Assert.False(option.Checked));
                    Assert.Equal(question.Variants.OrderBy(v => v), options.Select(o => o.Text).OrderBy(v => v));
                    Assert.False(((Button)window.Controls.Find("Done_btn", true).Single()).Enabled);
                }
            });
        }
    }
}
