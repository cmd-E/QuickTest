using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuickTest.Classes;

namespace QuickTest
{
    public partial class TestWindow : Form
    {
        /// <summary>
        /// All questions pased from document
        /// </summary>
        private List<Question> _questions { get; set; }
        /// <summary>
        /// Questions randomly selected for test
        /// </summary>
        private List<Question> _questionsForTests { get; set; }
        /// <summary>
        /// Holds text of selected variant. Used to compare selected variants with correct ones
        /// </summary>
        private Dictionary<int, string> _answers { get; set; }
        /// <summary>
        /// Holds selected radiobutton of each question
        /// </summary>
        private Dictionary<int, int> _cbPosition { get; set; }
        /// <summary>
        /// Index of question user is currently on
        /// </summary>
        private int _currentQuestionIndex = 0;
        /// <summary>
        /// Contains all question squares of scrollable menu
        /// </summary>
        private readonly Panel[] _questionsSquares;
        private readonly Random _random;
        public TestWindow(List<Question> questions, int numOfQuestions)
        {
            _random = new Random();
            _questions = questions;
            _cbPosition = new Dictionary<int, int>();
            _answers = new Dictionary<int, string>();
            _questionsForTests = RetrieveRandomQuestions(numOfQuestions);
            InitializeComponent();
            foreach (Control control in Controls)
            {
                if (control is CheckBox cb)
                {
                    cb.Click += AnswerQuestion;
                }
            }
            // Generate scrollable menu which holds all question squares
            _questionsSquares = new Panel[numOfQuestions];
            var _questionSquaresIndex = 0;
            var xPoint = 3;
            for (int i = 1; i <= numOfQuestions; i++)
            {
                Panel p = new Panel
                {
                    Height = 30,
                    Width = 30,
                    BackColor = Color.LightSeaGreen,
                };
                Label l = new Label
                {
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = i.ToString(),
                    Cursor = Cursors.Hand
                };
                l.MouseHover += L_MouseHover;
                l.MouseLeave += L_MouseLeave;
                l.Click += L_Click;
                p.Controls.Add(l);
                p.Location = new Point
                {
                    X = xPoint,
                    Y = 3
                };
                TestsScroll_panel.Controls.Add(p);
                _questionsSquares[_questionSquaresIndex++] = p;
                xPoint += 35;
            }
        }

        private void L_Click(object sender, EventArgs e)
        {
            _currentQuestionIndex = Convert.ToInt32((sender as Label).Text) - 1;
            SetQuestion(_currentQuestionIndex);
        }
        /// <summary>
        /// Event for color change of label in scrollable menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void L_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            Panel p = (sender as Label).Parent as Panel;
            p.BackColor = _answers.ContainsKey(Convert.ToInt32((p.Controls[0] as Label).Text) - 1) ? Color.Lime : Color.LightSeaGreen;
        }

        /// <summary>
        /// Event for color change of label in scrollable menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void L_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            Panel p = (sender as Label).Parent as Panel;
            p.BackColor = Color.MediumSeaGreen;
            //p.BackColor = ControlPaint.Dark(p.BackColor);
        }
        /// <summary>
        /// Unchecks rest of the checkboxes, stores selected variant's text and selected checkbox number in correspoding dictionaries
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnswerQuestion(object sender, EventArgs e)
        {
            if (!(sender as CheckBox).Checked)
            {
                _answers.Remove(_currentQuestionIndex);
                _cbPosition.Remove(_currentQuestionIndex);
                _questionsSquares[_currentQuestionIndex].BackColor = Color.LightSeaGreen;
            }
            else
            {
                foreach (var control in Controls)
                {
                    if (control is CheckBox cb && cb != (sender as CheckBox))
                    {
                        cb.Checked = false;
                    }
                }
                var selectedCheckbox = (sender as CheckBox);
                if (_answers.ContainsKey(_currentQuestionIndex))
                {
                    _answers[_currentQuestionIndex] = selectedCheckbox.Text;
                }
                else
                {
                    _answers.Add(_currentQuestionIndex, selectedCheckbox.Text);
                }

                if (_cbPosition.ContainsKey(_currentQuestionIndex))
                {
                    _cbPosition[_currentQuestionIndex] = Convert.ToInt32(selectedCheckbox.Tag);
                }
                else
                {
                    _cbPosition.Add(_currentQuestionIndex, Convert.ToInt32(selectedCheckbox.Tag));
                }
                _questionsSquares[_currentQuestionIndex].BackColor = Color.Lime;

            }
            Done_btn.Enabled = _answers.Count() == _questionsForTests.Count();
        }

        /// <summary>
        /// Gathers random questions from all document parsed tests
        /// </summary>
        /// <param name="numberOfQuestions">Number of questions to retrieve</param>
        /// <returns></returns>
        private List<Question> RetrieveRandomQuestions(int numberOfQuestions)
        {
            Random rand = new Random();
            List<Question> randomQuestions = new List<Question>();
            for (int i = 0; i < numberOfQuestions; i++)
            {
                var randomQuestion = _questions[rand.Next(_questions.Count())];
                while (randomQuestions.Contains(randomQuestion))
                {
                    randomQuestion = _questions[rand.Next(_questions.Count())];
                }
                randomQuestion = ShuffleVariants(randomQuestion);
                randomQuestions.Add(randomQuestion);
            }
            return randomQuestions;
        }
        /// <summary>
        /// Changes order of variants in question's variants list
        /// </summary>
        /// <param name="q">Question which variants must be shuffled</param>
        /// <returns></returns>
        private Question ShuffleVariants(Question q)
        {
            var randomized = q.Variants.OrderBy(item => _random.Next()).ToList();
            q.Variants = randomized;
            return q;
        }

        private void TestWindow_Load(object sender, EventArgs e)
        {
            SetQuestion(_currentQuestionIndex);
        }
        /// <summary>
        /// Sets question text and variants to corresponding form controls
        /// </summary>
        /// <param name="qIndex">Index of question in _questionsForTests</param>
        private void SetQuestion(int qIndex)
        {
            Question q = _questionsForTests[qIndex];
            int variantIndex = 0;
            Question_lbl.Text = _questionsForTests[qIndex].QuestionText;
            foreach (Control control in Controls)
            {
                if (control is CheckBox cb)
                {
                    cb.Text = q.Variants[variantIndex++];
                    if (_cbPosition.ContainsKey(qIndex))
                    {
                        cb.Checked = _cbPosition[qIndex] == Convert.ToInt32(cb.Tag);
                    }
                    else
                    {
                        cb.Checked = false;
                    }
                }
            }
            QuestionNumber_lbl.Text = (qIndex + 1).ToString();
        }

        /// <summary>
        /// Set next question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextQ_btn_Click(object sender, EventArgs e)
        {
            if (_currentQuestionIndex == _questionsForTests.Count() - 1) return;
            SetQuestion(++_currentQuestionIndex);
        }

        /// <summary>
        /// Set previous question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevQ_btn_Click(object sender, EventArgs e)
        {
            if (_currentQuestionIndex == 0) return;
            SetQuestion(--_currentQuestionIndex);
        }

        /// <summary>
        /// Show test result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Done_btn_Click(object sender, EventArgs e)
        {
            ResultsWindow rw = new ResultsWindow(_questionsForTests, _answers);
            rw.Show();
            Close();
        }
    }
}
