// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// WebView2窗体
/// </summary>
public partial class WebView2Form : BaseForm
{
    #region 重写属性

    /// <summary>
    /// 链接地址
    /// </summary>
    protected virtual string Url { get; } = "https://www.51xulai.net/";

    /// <summary>
    /// 显示菜单
    /// </summary>
    protected override bool ShowMenu => true;

    /// <summary>
    /// 显示状态栏
    /// </summary>
    protected override bool ShowStatus => true;

    /// <summary>
    /// 日志控件
    /// </summary>
    protected override RichTextBox OutputTextBox => rtbMessage;

    #endregion

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public WebView2Form()
    {
        InitializeComponent();
    }

    #endregion

    #region 重写方法

    /// <summary>
    /// 初始化完成事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task WebView2InitializationCompletedAsync(object? sender, CoreWebView2InitializationCompletedEventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// 导航完成事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task WebView2NavigationCompletedAsync(object? sender, CoreWebView2NavigationCompletedEventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// 消息接收事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task WebView2WebMessageReceivedAsync(object? sender, CoreWebView2WebMessageReceivedEventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    #endregion

    #region 窗体加载

    /// <summary>
    /// 窗体加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken)
    {
        var UserDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Xunet.MiniFormium", "cache", "WebView2Cache", Assembly.GetEntryAssembly()?.GetName().Name ?? Guid.NewGuid().ToString());

        if (!Directory.Exists(UserDataDir)) Directory.CreateDirectory(UserDataDir);

        // 环境选项
        var options = new CoreWebView2EnvironmentOptions
        {

        };

        // 使用指定的路径创建 WebView2 环境
        // 第一个参数为 browserExecutableFolder，传入 null 表示使用系统安装的或应用自带的 WebView2 Runtime
        var ENV = await CoreWebView2Environment.CreateAsync(null, UserDataDir, options);

        // 必须在设置 Source 属性前初始化环境
        await webView2.EnsureCoreWebView2Async(ENV);

        webView2.Source = new Uri(Url);
    }

    #endregion

    #region 初始化完成事件

    private void WebView2_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
    {
        WebView2InitializationCompletedAsync(sender, e, TokenSource.Token);
    }

    #endregion

    #region 导航完成事件

    private void WebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        WebView2NavigationCompletedAsync(sender, e, TokenSource.Token);
    }

    #endregion

    #region 消息接收事件

    private void WebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        WebView2WebMessageReceivedAsync(sender, e, TokenSource.Token);
    }

    #endregion
}
