using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickTest.Classes;
using QuickTest.Windows;
namespace QuickTest
{
    public partial class MainWindow : Form
    {
        private List<Question> Questions;
        private bool busy;
        public MainWindow()
        {
            InitializeComponent();
            QuestionTag_tb.Text = Properties.Settings.Default.QuestionTag;
            VariantTag_tb.Text = Properties.Settings.Default.VariantTag;
            QuestionTag_tb.KeyUp += TagTextBoxes_KeyUp;
            VariantTag_tb.KeyUp += TagTextBoxes_KeyUp;
            RefreshControls();
        }
        private bool ValidTags()
        {
            try
            {
                TaggedQuestionParser.ValidateTags(QuestionTag_tb.Text, VariantTag_tb.Text);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        private void RefreshControls()
        {
            chooseTestBtn.Enabled = CreateTest_btn.Enabled = !busy && ValidTags();
            QuestionTag_tb.Enabled = VariantTag_tb.Enabled = !busy;
            TestParameters_gb.Enabled = !busy && Questions != null && Questions.Count > 0;
            StartTest_btn.Enabled = !busy && Questions != null && Questions.Count > 0 && ValidTags();
        }
        private void TagTextBoxes_KeyUp(object sender, KeyEventArgs e)
        {
            RefreshControls();
        }
        private async void ChooseTestBtn_Click(object sender, EventArgs e)
        {
            if (busy)
                return;
            using (var dialog =
                       new OpenFileDialog { Title = "Выбрать тест", Filter = "Documents (*.doc, *.docx)|*.doc;*.docx" })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                string path = dialog.FileName, qTag = QuestionTag_tb.Text, vTag = VariantTag_tb.Text;
                busy = true;
                Questions = null;
                RefreshControls();
                try
                {
                    var parsed = await Task.Run(() => DocumentParser.ParseTest(path, qTag, vTag));
                    if (IsDisposed)
                        return;
                    Questions = parsed;
                    testPathLbl.Text = path;
                    testPathLbl.Visible = numberOfQuestions_lbl.Visible = numberOfQuestions_ud.Visible = true;
                    numberOfQuestions_ud.Minimum = 1;
                    numberOfQuestions_ud.Maximum = parsed.Count;
                    numberOfQuestions_ud.Value = parsed.Count;
                    Properties.Settings.Default.QuestionTag = qTag.Trim();
                    Properties.Settings.Default.VariantTag = vTag.Trim();
                    Properties.Settings.Default.Save();
                }
                catch (Exception error)
                {
                    if (!IsDisposed)
                        MessageBox.Show(this, error.Message, "Ошибка документа", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                }
                finally
                {
                    busy = false;
                    if (!IsDisposed)
                        RefreshControls();
                }
            }
        }
        private void StartTest_btn_Click(object sender, EventArgs e)
        {
            if (Questions != null && Questions.Count > 0)
                new TestWindow(Questions, Convert.ToInt32(numberOfQuestions_ud.Value)).Show();
        }
        private async void CreateTest_btn_Click(object sender, EventArgs e)
        {
            if (busy)
                return;
            using (var dialog =
                       new OpenFileDialog { Title = "Выбрать тест", Filter = "Documents (*.doc, *.docx)|*.doc;*.docx" })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                string path = dialog.FileName, qTag = QuestionTag_tb.Text, vTag = VariantTag_tb.Text;
                busy = true;
                RefreshControls();
                try
                {
                    string output = await Task.Run(() => DocumentParser.CreateTest(path, qTag, vTag));
                    if (!IsDisposed)
                        MessageBox.Show(this, "Тест создан\n" + output, "Тест создан", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                }
                catch (Exception error)
                {
                    if (!IsDisposed)
                        MessageBox.Show(this, error.Message, "Ошибка документа", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                }
                finally
                {
                    busy = false;
                    if (!IsDisposed)
                        RefreshControls();
                }
            }
        }
        private async void MainWindow_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CheckForUpdatesAtStartup)
                await UpdatePresenter.Show(this, false);
        }
        private void aboutProgramMenuItem_Click(object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog(this);
        }
    }
}
