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
    public partial class MainWindow : Form
    {
        private List<Question> Questions { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseTestBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выбрать тест";
            ofd.Filter = "Documents (*.doc, *.docx)|*.doc;*.docx|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                testPathLbl.Visible = true;
                numberOfQuestions_lbl.Visible = true;
                numberOfQuestions_ud.Visible = true;
                testPathLbl.Text = ofd.FileName;
                Questions = DocumentParser.ParseTest(ofd.FileName);
                numberOfQuestions_ud.Minimum = 1;
                numberOfQuestions_ud.Maximum = Questions.Count();
                numberOfQuestions_ud.Value = Questions.Count();
                StartTest_btn.Enabled = Questions.Count() != 0;
            }
        }

        private void StartTest_btn_Click(object sender, EventArgs e)
        {
            TestWindow tw = new TestWindow(Questions, Convert.ToInt32(numberOfQuestions_ud.Value));
            tw.Show();
        }
    }
}
