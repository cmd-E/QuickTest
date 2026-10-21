using System;
using System.Reflection;
using System.Windows.Forms;
using QuickTest.Classes;
namespace QuickTest.Windows
{
    public partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            InitializeComponent();
            CreatorLink_lbl.Links.Add(new LinkLabel.Link { LinkData = "https://github.com/cmd-e/" });
            CreatorLink_lbl.Text = "By cmd-E";
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            AppInfo_lbl.Text = string.Format("QuickTest v{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            CheckUpdatesAtStartup_cb.Checked = Properties.Settings.Default.CheckForUpdatesAtStartup;
        }
        private void CreatorLink_lbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/cmd-e/");
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckUpdatesAtStartup_cb_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CheckForUpdatesAtStartup = CheckUpdatesAtStartup_cb.Checked;
            Properties.Settings.Default.Save();
        }
        private async void CheckUpdates_btn_Click(object sender, EventArgs e)
        {
            var button = sender as Control;
            if (button != null)
                button.Enabled = false;
            try
            {
                await UpdatePresenter.Show(this, true);
            }
            finally
            {
                if (button != null && !button.IsDisposed)
                    button.Enabled = true;
            }
        }
    }
}
