using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuickTest.Classes;
namespace QuickTest
{
    public partial class TestWindow : Form
    {
        private readonly TestSession session;
        private readonly CheckBox[] options;
        private readonly Label[] squares;
        private int current;
        public TestWindow(List<Question> bank, int count)
        {
            session = new TestSession(bank, count, new Random());
            InitializeComponent();
            options = Controls.OfType<CheckBox>().OrderBy(c => Convert.ToInt32(c.Tag)).ToArray();
            foreach (var option in options)
                option.Click += AnswerQuestion;
            squares = new Label[count];
            for (int i = 0; i < count; i++)
            {
                int index = i;
                var label =
                    new Label { Text = (i + 1).ToString(), Location = new Point(3 + i * 35, 3), Size = new Size(30, 30),
                                TextAlign = ContentAlignment.MiddleCenter, Cursor = Cursors.Hand };
                label.Click += (s, e) =>
                {
                    current = index;
                    SetQuestion();
                };
                TestsScroll_panel.Controls.Add(label);
                squares[i] = label;
            }
            SetQuestion();
        }
        private void AnswerQuestion(object sender, EventArgs e)
        {
            var option = (CheckBox)sender;
            int displayIndex = Array.IndexOf(options, option);
            session.Questions[current].Selected =
                option.Checked ? (int?)session.Questions[current].Order[displayIndex] : null;
            SetQuestion();
        }
        private void SetQuestion()
        {
            var q = session.Questions[current];
            Question_lbl.Text = q.Question.QuestionText;
            for (int i = 0; i < options.Length; i++)
            {
                options[i].Text = q.Question.Variants[q.Order[i]];
                options[i].Checked = q.Selected == q.Order[i];
            }
            for (int i = 0; i < squares.Length; i++)
                squares[i].BackColor = session.Questions[i].Selected.HasValue ? Color.Lime : Color.LightSeaGreen;
            QuestionNumber_lbl.Text = (current + 1).ToString();
            Done_btn.Enabled = session.Complete;
        }
        private void TestWindow_Load(object sender, EventArgs e)
        {
            SetQuestion();
        }
        private void NextQ_btn_Click(object sender, EventArgs e)
        {
            if (current < session.Questions.Count - 1)
            {
                current++;
                SetQuestion();
            }
        }
        private void PrevQ_btn_Click(object sender, EventArgs e)
        {
            if (current > 0)
            {
                current--;
                SetQuestion();
            }
        }
        private void Done_btn_Click(object sender, EventArgs e)
        {
            if (!session.Complete)
                return;
            new ResultsWindow(session.Results()).Show();
            Close();
        }
    }
}
