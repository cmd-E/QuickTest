using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickTest.Classes;

namespace QuickTest
{
    public partial class ResultsWindow : Form
    {
        private readonly List<Question> _testQuestions;
        private readonly Dictionary<int, string> _userAnswers;

        #region Constants which are representing columns in datagridview
        private const string _questionNumberCol = "QuestionNumberCol";
        private const string _questionTextCol = "QuestionTextCol";
        private const string _choosenVariantCol = "ChoosenVariantCol";
        private const string _correctVariantCol = "CorrectVariantCol";
        #endregion
        public ResultsWindow(List<Question> testQuestions, Dictionary<int, string> userAnswers)
        {
            _testQuestions = testQuestions;
            _userAnswers = userAnswers;
            InitializeComponent();
            CheckResults();
        }

        private void CheckResults()
        {
            var correctAnswers = 0;
            for (int i = 0; i < _testQuestions.Count(); i++)
            {
                if (_testQuestions[i].CorrectVariant == _userAnswers[i])
                {
                    correctAnswers++;
                }
                var rowId = ResultsTable_dgv.Rows.Add();
                var row = ResultsTable_dgv.Rows[rowId];
                row.Cells[_questionNumberCol].Value = i + 1;
                row.Cells[_questionTextCol].Value = _testQuestions[i].QuestionText;
                row.Cells[_choosenVariantCol].Value = _userAnswers[i];
                row.Cells[_correctVariantCol].Value = _testQuestions[i].CorrectVariant;
                row.DefaultCellStyle.BackColor = _testQuestions[i].CorrectVariant == _userAnswers[i] ? Color.Lime : Color.Firebrick;
            }
            AnsweredQuestionsNumber_lbl.Text = $"Отвечено: {correctAnswers}/{_testQuestions.Count()} ({((float)correctAnswers / (float)_testQuestions.Count()) * 100f}%)";
            const int maxHue = 120; // 0 - red, 120 - green
            const float maxSaturation = 1f;
            const float halfValue = 0.5f;
            /*
            all_questions = maxHue
            answered_questions = x
            x = (answered questions * 120) / all_questions
            */
            float hueValueOfAnsweredQuestions = correctAnswers * maxHue / _testQuestions.Count();
            AnsweredQuestionsNumber_lbl.BackColor = new HSV(hueValueOfAnsweredQuestions, maxSaturation, halfValue).ColorFromHSL(); // hsv(x, 100%, 50%) -> hsv(x, 1, 0.5f);
        }

        private void showWrongOnly_cb_CheckedChanged(object sender, EventArgs e)
        {
            ResultsTable_dgv.Rows.Clear();
            if ((sender as CheckBox).Checked)
            {
                for (int i = 0; i < _testQuestions.Count(); i++)
                {
                    if (_testQuestions[i].CorrectVariant != _userAnswers[i])
                    {
                        var rowId = ResultsTable_dgv.Rows.Add();
                        var row = ResultsTable_dgv.Rows[rowId];
                        row.Cells[_questionNumberCol].Value = i + 1;
                        row.Cells[_questionTextCol].Value = _testQuestions[i].QuestionText;
                        row.Cells[_choosenVariantCol].Value = _userAnswers[i];
                        row.Cells[_correctVariantCol].Value = _testQuestions[i].CorrectVariant;
                        row.DefaultCellStyle.BackColor = _testQuestions[i].CorrectVariant == _userAnswers[i] ? Color.Lime : Color.Firebrick;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _testQuestions.Count(); i++)
                {
                    var rowId = ResultsTable_dgv.Rows.Add();
                    var row = ResultsTable_dgv.Rows[rowId];
                    row.Cells[_questionNumberCol].Value = i + 1;
                    row.Cells[_questionTextCol].Value = _testQuestions[i].QuestionText;
                    row.Cells[_choosenVariantCol].Value = _userAnswers[i];
                    row.Cells[_correctVariantCol].Value = _testQuestions[i].CorrectVariant;
                    row.DefaultCellStyle.BackColor = _testQuestions[i].CorrectVariant == _userAnswers[i] ? Color.Lime : Color.Firebrick;
                }
            }
        }
    }
}
