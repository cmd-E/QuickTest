using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickTest
{
    public partial class TestWindow : Form
    {
        private List<Question> _questions { get; set; }
        private List<Question> _questionsForTests { get; set; } = new List<Question>();
        private Dictionary<int, string> _answers { get; set; } = new Dictionary<int, string>();
        private Dictionary<int, int> _cbPosition { get; set; } = new Dictionary<int, int>();
        private int _currentQuestionIndex = 0;
        public TestWindow(List<Question> questions, int numOfQuestions)
        {
            _questions = questions;
            _questionsForTests = RetrieveRandomQuestions(numOfQuestions);
            //for (int i = 0; i < numOfQuestions; i++)
            //{
            //    _cbPosition.Add(i, 1);
            //}
            InitializeComponent();
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton)
                {
                    var rb = control as RadioButton;
                    rb.CheckedChanged += AnswerQuestion;
                }
            }
            var xPoint = 3;
            for (int i = 1; i <= numOfQuestions; i++)
            {
                Panel p = new Panel
                {
                    Height = 30,
                    Width = 30,
                    BackColor = Color.Aqua,
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
                xPoint += 30;
                xPoint += 5;

            }
        }

        private void L_Click(object sender, EventArgs e)
        {
            _currentQuestionIndex = Convert.ToInt32((sender as Label).Text) - 1;
            SetQuestion(_currentQuestionIndex);
        }

        private void L_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            var lbl = sender as Label;
            Panel p = lbl.Parent as Panel;
            p.BackColor = Color.Aqua;
        }

        private void L_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            var lbl = sender as Label;
            Panel p = lbl.Parent as Panel;
            p.BackColor = Color.Black;
        }

        private void AnswerQuestion(object sender, EventArgs e)
        {
            if (!(sender as RadioButton).Checked) return;
            if (_answers.ContainsKey(_currentQuestionIndex))
            {
                _answers[_currentQuestionIndex] = (sender as RadioButton).Text;
            }
            else 
            {
                _answers.Add(_currentQuestionIndex, (sender as RadioButton).Text);
            }

            if (_cbPosition.ContainsKey(_currentQuestionIndex))
            {
                _cbPosition[_currentQuestionIndex] = Convert.ToInt32((sender as RadioButton).Tag);
            }
            else 
            {
                _cbPosition.Add(_currentQuestionIndex, Convert.ToInt32((sender as RadioButton).Tag));
            }
            if (_answers.Count() == _questionsForTests.Count()) 
            {
                Done_btn.Enabled = true;
            }
        }

        private List<Question> RetrieveRandomQuestions(int numberOfQuestions)
        {
            Random rand = new Random();
            List<Question> randomQuestions = new List<Question>();
            for (int i = 0; i < numberOfQuestions; i++)
            {
                var rq = _questions[rand.Next(_questions.Count())];
                while (_questionsForTests.Contains(rq))
                {
                    rq = _questions[rand.Next(_questions.Count())];
                }
                rq = ShuffleVariants(rq);
                randomQuestions.Add(rq);
            }
            return randomQuestions;
        }

        private Question ShuffleVariants(Question q)
        {
            var rnd = new Random();
            var randomized = q.Variants.OrderBy(item => rnd.Next()).ToList();
            q.Variants = randomized;
            return q;
        }

        private void TestWindow_Load(object sender, EventArgs e)
        {
            SetQuestion(_currentQuestionIndex);
        }

        private void SetQuestion(int qIndex)
        {
            Question q = _questionsForTests[qIndex];
            int variantIndex = 0;
            Question_lbl.Text = _questionsForTests[qIndex].QuestionText;
            // If question hasn't been seen add it to position and answers
            if (!_cbPosition.ContainsKey(qIndex)) _cbPosition.Add(qIndex, 1);
            if (!_answers.ContainsKey(qIndex)) _answers.Add(qIndex, _questionsForTests[qIndex].Variants[0]);
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton)
                {
                    var rb = control as RadioButton;
                    rb.Text = q.Variants[variantIndex++];
                    rb.Checked = _cbPosition[qIndex] == Convert.ToInt32(rb.Tag); // TODO: check algorithm, wrong checkbox keeps checking
                }
            }
            QuestionNumber_lbl.Text = (qIndex+1).ToString();
        }

        private void NextQ_btn_Click(object sender, EventArgs e)
        {
            if (_currentQuestionIndex == _questionsForTests.Count() - 1) return;
            SetQuestion(++_currentQuestionIndex);
        }

        // TODO: check radiobutton position on click
        private void PrevQ_btn_Click(object sender, EventArgs e)
        {
            if (_currentQuestionIndex == 0) return;
            SetQuestion(--_currentQuestionIndex);
        }

        private void Done_btn_Click(object sender, EventArgs e)
        {
            ResultsWindow rw = new ResultsWindow();
            rw.Show();
        }
    }
}
