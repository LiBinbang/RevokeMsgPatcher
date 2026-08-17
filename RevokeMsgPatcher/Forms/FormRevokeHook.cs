using RevokeMsgPatcher.Model;
using RevokeMsgPatcher.Modifier;
using RevokeMsgPatcher.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RevokeMsgPatcher.Forms
{
    /// <summary>
    /// 集成 EEEEhex/RevokeHook：运行时 Hook，撤回提示贴在对应消息下方。
    /// </summary>
    public partial class FormRevokeHook : Form
    {
        private const string ReleaseZipUrl = "https://github.com/EEEEhex/RevokeHook/releases/download/v5.1.2/RevokeHook.zip";
        private const string ProjectUrl = "https://github.com/EEEEhex/RevokeHook";
        private const string DiscussionsUrl = "https://github.com/EEEEhex/RevokeHook/discussions/12";

        private readonly string _installDir;
        private string _weixinRoot;

        public FormRevokeHook(string weixinPath = null)
        {
            InitializeComponent();
            _installDir = Path.Combine(Application.StartupPath, "RevokeHook");
            SetWeixinPath(weixinPath);
            RefreshStatus();
            InitCboProxyList();
        }

        public void SetWeixinPath(string weixinPath)
        {
            _weixinRoot = NormalizeWeixinRoot(weixinPath);
            if (!string.IsNullOrEmpty(_weixinRoot) && File.Exists(Path.Combine(_weixinRoot, "Weixin.exe")))
            {
                txtWeixinPath.Text = _weixinRoot;
                return;
            }

            try
            {
                string found = new WeixinModifier(new App { Name = "Weixin" }).FindInstallPath();
                _weixinRoot = NormalizeWeixinRoot(found);
                txtWeixinPath.Text = _weixinRoot ?? "";
            }
            catch
            {
                txtWeixinPath.Text = _weixinRoot ?? "";
            }
        }

        /// <summary>
        /// RevokeInject -w 需要微信根目录（含 Weixin.exe），不是版本子目录。
        /// </summary>
        private static string NormalizeWeixinRoot(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            path = path.Trim().TrimEnd('\\', '/');
            if (File.Exists(Path.Combine(path, "Weixin.exe")))
            {
                return path;
            }

            var parent = Directory.GetParent(path)?.FullName;
            if (!string.IsNullOrEmpty(parent) && File.Exists(Path.Combine(parent, "Weixin.exe")))
            {
                return parent;
            }

            return path;
        }

        private void InitCboProxyList()
        {
            cboGithubProxy.Items.Clear();
            foreach (var proxy in ProxySpeedTester.ProxyUrls)
            {
                cboGithubProxy.Items.Add(proxy.Replace("{0}", ""));
            }

            // 默认直连：页面测速快的代理（如 ghproxy.cn）对 release zip 常 405
            cboGithubProxy.SelectedIndex = 0;
            Task.Run(async () =>
            {
                try
                {
                    var fastest = await ProxySpeedTester.GetFastestProxyAsync(ReleaseZipUrl);
                    if (!string.IsNullOrEmpty(fastest.Item1) && !IsDisposed)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            if (cboGithubProxy.Items.Contains(fastest.Item1))
                            {
                                cboGithubProxy.SelectedItem = fastest.Item1;
                            }
                            else
                            {
                                cboGithubProxy.Items.Insert(0, fastest.Item1);
                                cboGithubProxy.SelectedIndex = 0;
                            }
                        }));
                    }
                }
                catch
                {
                    // 代理测速失败时使用直连
                }
            });
        }

        private void RefreshStatus()
        {
            bool hasUi = File.Exists(Path.Combine(_installDir, "RevokeHookUI.exe"));
            bool hasInject = File.Exists(Path.Combine(_installDir, "RevokeInject.exe"));
            bool hasDll = File.Exists(Path.Combine(_installDir, "RevokeHook.dll"));

            if (hasUi && hasInject)
            {
                lblStatus.Text = hasDll
                    ? "已安装 RevokeHook，可先「搜索偏移」再「注入启动」。"
                    : "已安装主程序，目录内缺少 RevokeHook.dll，请重新下载。";
                lblStatus.ForeColor = hasDll ? System.Drawing.Color.Green : System.Drawing.Color.DarkOrange;
            }
            else
            {
                lblStatus.Text = "未安装。点击「下载/更新」获取 EEEEhex/RevokeHook。";
                lblStatus.ForeColor = System.Drawing.Color.DarkRed;
            }

            btnOpenUi.Enabled = hasUi;
            btnInject.Enabled = hasInject;
            btnOpenFolder.Enabled = Directory.Exists(_installDir);
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            btnDownload.Enabled = false;
            lblStatus.Text = "正在下载 RevokeHook...";
            lblStatus.ForeColor = System.Drawing.Color.RoyalBlue;

            try
            {
                await DownloadAndInstallAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "下载失败：" + ex.Message +
                    "\n\n建议：代理选空白（直连），或点「项目主页」手动下载 zip，解压到程序目录下的 RevokeHook 文件夹。",
                    "错误");
            }
            finally
            {
                btnDownload.Enabled = true;
                RefreshStatus();
            }
        }

        private async Task DownloadAndInstallAsync()
        {
            Directory.CreateDirectory(_installDir);
            string zipPath = Path.Combine(Path.GetTempPath(), "RevokeHook_" + Guid.NewGuid().ToString("N") + ".zip");
            string extractTmp = Path.Combine(Path.GetTempPath(), "RevokeHook_extract_" + Guid.NewGuid().ToString("N"));

            var errors = new List<string>();
            byte[] bytes = null;
            foreach (string url in BuildCandidateUrls())
            {
                try
                {
                    lblStatus.Text = "正在下载：\n" + url;
                    using (var client = new HttpClient())
                    {
                        client.Timeout = TimeSpan.FromMinutes(5);
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("RevokeMsgPatcher");
                        bytes = await client.GetByteArrayAsync(url);
                    }

                    if (bytes != null && bytes.Length > 4 && bytes[0] == 0x50 && bytes[1] == 0x4B)
                    {
                        break;
                    }

                    errors.Add(url + " → 返回的不是 zip（可能是代理错误页）");
                    bytes = null;
                }
                catch (Exception ex)
                {
                    errors.Add(url + " → " + ex.Message);
                    bytes = null;
                }
            }

            if (bytes == null)
            {
                throw new Exception("所有下载地址均失败：\n" + string.Join("\n", errors.Take(5)));
            }

            File.WriteAllBytes(zipPath, bytes);

            if (Directory.Exists(extractTmp))
            {
                Directory.Delete(extractTmp, true);
            }

            ZipFile.ExtractToDirectory(zipPath, extractTmp);

            string contentRoot = extractTmp;
            var subs = Directory.GetDirectories(extractTmp);
            if (!File.Exists(Path.Combine(extractTmp, "RevokeInject.exe")) && subs.Length == 1)
            {
                contentRoot = subs[0];
            }

            if (!File.Exists(Path.Combine(contentRoot, "RevokeInject.exe")))
            {
                throw new Exception("解压后未找到 RevokeInject.exe，请检查 zip 内容。");
            }

            foreach (var file in Directory.GetFiles(contentRoot, "*", SearchOption.AllDirectories))
            {
                string rel = file.Substring(contentRoot.Length).TrimStart('\\', '/');
                string dest = Path.Combine(_installDir, rel);
                Directory.CreateDirectory(Path.GetDirectoryName(dest));
                File.Copy(file, dest, true);
            }

            try { File.Delete(zipPath); } catch { }
            try { Directory.Delete(extractTmp, true); } catch { }

            MessageBox.Show(
                "下载完成。\n\n使用步骤：\n1. 打开「搜索偏移」→ 开始搜索 → 保存配置\n2. 再点「注入启动」\n\n若此前打过本工具的 DLL 十六进制补丁，建议先「备份还原」再使用 Hook，避免冲突。",
                "RevokeHook",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private IEnumerable<string> BuildCandidateUrls()
        {
            var urls = new List<string>();
            void Add(string u)
            {
                if (!string.IsNullOrWhiteSpace(u) && !urls.Contains(u))
                {
                    urls.Add(u);
                }
            }

            Add(BuildDownloadUrl(cboGithubProxy.Text));
            Add(ReleaseZipUrl);
            foreach (var proxy in ProxySpeedTester.ProxyUrls)
            {
                Add(string.Format(proxy, ReleaseZipUrl));
            }

            return urls;
        }

        private string BuildDownloadUrl(string proxyText)
        {
            string proxy = (proxyText ?? "").Trim();
            if (string.IsNullOrEmpty(proxy))
            {
                return ReleaseZipUrl;
            }

            if (proxy.Contains("{0}"))
            {
                return string.Format(proxy, ReleaseZipUrl);
            }

            if (!proxy.EndsWith("/"))
            {
                proxy += "/";
            }

            return proxy + ReleaseZipUrl;
        }

        private void btnOpenUi_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(_installDir, "RevokeHookUI.exe");
            if (!File.Exists(path))
            {
                MessageBox.Show("未找到 RevokeHookUI.exe，请先下载。");
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                WorkingDirectory = _installDir,
                UseShellExecute = true
            });
        }

        private void btnInject_Click(object sender, EventArgs e)
        {
            string inject = Path.Combine(_installDir, "RevokeInject.exe");
            if (!File.Exists(inject))
            {
                MessageBox.Show("未找到 RevokeInject.exe，请先下载。");
                return;
            }

            _weixinRoot = NormalizeWeixinRoot(txtWeixinPath.Text);
            if (string.IsNullOrEmpty(_weixinRoot) || !File.Exists(Path.Combine(_weixinRoot, "Weixin.exe")))
            {
                MessageBox.Show(
                    "请填写正确的微信 4.x 根目录（含 Weixin.exe）。\n" +
                    "一般是 C:\\Program Files\\Tencent\\Weixin\n" +
                    "不是旧版 WeChat 目录。");
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = inject,
                WorkingDirectory = _installDir,
                UseShellExecute = true,
                Arguments = "-w \"" + _weixinRoot + "\""
            });
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(_installDir))
            {
                Directory.CreateDirectory(_installDir);
            }

            Process.Start("explorer.exe", _installDir);
        }

        private void btnGithub_Click(object sender, EventArgs e)
        {
            Process.Start(ProjectUrl);
        }

        private void btnDiscuss_Click(object sender, EventArgs e)
        {
            Process.Start(DiscussionsUrl);
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "选择微信根目录（含 Weixin.exe）";
                if (!string.IsNullOrEmpty(txtWeixinPath.Text) && Directory.Exists(txtWeixinPath.Text))
                {
                    dlg.SelectedPath = txtWeixinPath.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtWeixinPath.Text = NormalizeWeixinRoot(dlg.SelectedPath) ?? dlg.SelectedPath;
                }
            }
        }
    }
}
