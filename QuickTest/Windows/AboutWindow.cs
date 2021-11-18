using QuickTest.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickTest.Windows
{
    public partial class AboutWindow : Form
    {
        private const string _creatorLink = @"https://github.com/cmd-e/";
        private const string _creatorName = "cmd-E";
        public AboutWindow()
        {
            InitializeComponent();
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = _creatorLink;
            CreatorLink_lbl.Links.Add(link);
            CreatorLink_lbl.Text = $"By {_creatorName}";
            var major = Assembly.GetExecutingAssembly().GetName().Version.Major;
            var minor = Assembly.GetExecutingAssembly().GetName().Version.Minor;
            var patch = Assembly.GetExecutingAssembly().GetName().Version.Build;
            AppInfo_lbl.Text = $"QuickTest v{major}.{minor}.{patch}";
            CheckUpdatesAtStartup_cb.Checked = Properties.Settings.Default.CheckForUpdatesAtStartup;
        }

        private void CreatorLink_lbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(_creatorLink);
        }

        private void CheckUpdatesAtStartup_cb_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CheckForUpdatesAtStartup = (sender as CheckBox).Checked;
            Properties.Settings.Default.Save();
        }

        private async void CheckUpdates_btn_Click(object sender, EventArgs e)
        {
            await Task.Run(async () => await CheckVersion());
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
            else
            {
                MessageBox.Show("Используется самая последняя версия программы", "Самая последняя версия", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
