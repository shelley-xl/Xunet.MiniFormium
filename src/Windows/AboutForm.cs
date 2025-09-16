// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// 关于窗体
/// </summary>
public partial class AboutForm : BaseForm
{
    #region 窗体标题

    /// <summary>
    /// 窗体标题
    /// </summary>
    protected override string Title => "关于软件";

    #endregion

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public AboutForm()
    {
        InitializeComponent();
    }

    #endregion

    #region 窗体加载

    /// <summary>
    /// 窗体加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken)
    {
        lblAbout.Text = $"软件版本：{Version}\r\n\r\nSDK版本：{SDKVersion}\r\n\r\n开发作者：徐来（QQ386710057）\r\n\r\n个人博客：https://www.51xulai.net";

        return Task.CompletedTask;
    }

    #endregion

    #region 打开链接

    /// <summary>
    /// 打开链接
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NavigateUrl_Click(object sender, EventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://www.51xulai.net")
        {
            UseShellExecute = true,
        });
    }

    #endregion
}
