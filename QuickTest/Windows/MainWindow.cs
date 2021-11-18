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
using QuickTest.Windows;

namespace QuickTest
{
    public partial class MainWindow : Form
    {
        private List<Question> Questions { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            // TODO: make checkingg for new version optional
            QuestionTag_tb.Text = Properties.Settings.Default.QuestionTag;
            VariantTag_tb.Text = Properties.Settings.Default.VariantTag;
            QuestionTag_tb.KeyUp += TagTextBoxes_KeyUp;
            VariantTag_tb.KeyUp += TagTextBoxes_KeyUp;
            chooseTestBtn.Enabled = !string.IsNullOrEmpty(QuestionTag_tb.Text) && !string.IsNullOrEmpty(VariantTag_tb.Text);
        }

        private void TagTextBoxes_KeyUp(object sender, KeyEventArgs e)
        {
            chooseTestBtn.Enabled = !string.IsNullOrEmpty(QuestionTag_tb.Text) && !string.IsNullOrEmpty(VariantTag_tb.Text);
            CreateTest_btn.Enabled = !string.IsNullOrEmpty(QuestionTag_tb.Text) && !string.IsNullOrEmpty(VariantTag_tb.Text);
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
                Questions = DocumentParser.ParseTest(ofd.FileName, QuestionTag_tb.Text.Trim(), VariantTag_tb.Text.Trim()); ;
                numberOfQuestions_ud.Minimum = 1;
                numberOfQuestions_ud.Maximum = Questions.Count();
                numberOfQuestions_ud.Value = Questions.Count();
                TestParameters_gb.Enabled = true;
                StartTest_btn.Enabled = Questions.Count() != 0 && !string.IsNullOrEmpty(QuestionTag_tb.Text.Trim()) && !string.IsNullOrEmpty(VariantTag_tb.Text.Trim());
                if (StartTest_btn.Enabled)
                {
                    Properties.Settings.Default.QuestionTag = QuestionTag_tb.Text.Trim();
                    Properties.Settings.Default.VariantTag = VariantTag_tb.Text.Trim();
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void StartTest_btn_Click(object sender, EventArgs e)
        {
            TestWindow tw = new TestWindow(Questions, Convert.ToInt32(numberOfQuestions_ud.Value));
            tw.Show();
        }

        private void CreateTest_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выбрать тест";
            ofd.Filter = "Documents (*.doc, *.docx)|*.doc;*.docx|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var modifiedDocFileName = DocumentParser.CreateTest(ofd.FileName, QuestionTag_tb.Text.Trim(), VariantTag_tb.Text.Trim());
                MessageBox.Show($"Тест создан\n{modifiedDocFileName}", "Тест создан", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CheckForUpdatesAtStartup)
            {
                await Task.Run(async () => await CheckVersion());
            }
        }

        private async Task CheckVersion()
        {
            const string releasesLink = @"https://github.com/cmd-E/QuickTest/releases/latest";
            var githubResponse = await CheckForUpdates.Check();
            var thereIsNewVersion = githubResponse.Item1;
            var versionOnGithub = githubResponse.Item2;
            if (thereIsNewVersion)
            {
                if (MessageBox.Show($"Доступна версия {versionOnGithub}\nНажмите ОК, чтобы перейти на страницу загрузки", "Доступна новая версия", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    System.Diagnostics.Process.Start(releasesLink);
                }
            }
            else if (versionOnGithub == "-1")
            {
                if (MessageBox.Show("Не удалось получить информацию о новых версиях приложения.\nПроверьте ваше соединение с интернетом", "Ошибка сети", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry)
                {
                    await CheckVersion();
                }
            }
        }

        private void aboutProgramMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
    }
}
