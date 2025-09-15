// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// 关于窗体
/// </summary>
public partial class AboutForm : Form, IMiniFormium
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public AboutForm()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 窗体加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AboutForm_Load(object sender, EventArgs e)
    {
        var version = Assembly.GetEntryAssembly()?.GetName().Version;

        var sdkVersion = Assembly.GetExecutingAssembly().GetName().Version;

        lblAbout.Text = $"软件版本：{version}\r\n\r\nSDK版本：{sdkVersion}\r\n\r\n开发作者：徐来（QQ386710057）\r\n\r\n个人博客：https://www.51xulai.net";
    }

    private void NavigateUrl_Click(object sender, EventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://www.51xulai.net")
        {
            UseShellExecute = true,
        });
    }
}
