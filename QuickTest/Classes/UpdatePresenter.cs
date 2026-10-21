using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace QuickTest.Classes
{
    internal static class UpdatePresenter
    {
        public static async Task Show(IWin32Window owner, bool reportCurrent)
        {
            var result = await CheckForUpdates.Check();
            var control = owner as Control;
            if (control != null && control.IsDisposed)
                return;
            if (result.Status == UpdateStatus.Available)
            {
                if (MessageBox.Show(owner, "Доступна версия " + result.Tag + "\nОткрыть страницу загрузки?",
                                    "Обновление", MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Information) == DialogResult.OK)
                {
                    try
                    {
                        Process.Start("https://github.com/cmd-E/QuickTest/releases/latest");
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(owner, error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (result.Status == UpdateStatus.Failed)
                MessageBox.Show(owner, "Не удалось проверить обновления. Повторите попытку позже.", "Ошибка сети",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (reportCurrent)
                MessageBox.Show(owner, "Используется последняя версия", "Обновление", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
        }
    }
}
