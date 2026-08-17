using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace RevokeMsgPatcher
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // ClickOnce 不支持清单 requireAdministrator，改为启动时提权
                if (!IsRunAsAdmin())
                {
                    try
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = Application.ExecutablePath,
                            UseShellExecute = true,
                            Verb = "runas"
                        };
                        Process.Start(psi);
                    }
                    catch
                    {
                        MessageBox.Show(
                            "需要管理员权限才能修改微信/QQ 安装目录下的文件。\n请右键本程序，选择“以管理员身份运行”。",
                            "权限不足",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace.Trim(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static bool IsRunAsAdmin()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message + "\n" + e.Exception.StackTrace.Trim(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).Message + "\n" + (e.ExceptionObject as Exception).StackTrace.Trim(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
