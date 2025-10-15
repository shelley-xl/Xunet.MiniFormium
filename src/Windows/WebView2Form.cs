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

    #region 继承方法

    #region 前进

    /// <summary>
    /// 前进
    /// </summary>
    /// <returns></returns>
    protected Task WebView2ForwardAsync()
    {
        InvokeOnUIThread(webView2.GoForward);

        return Task.CompletedTask;
    }

    #endregion

    #region 后退

    /// <summary>
    /// 后退
    /// </summary>
    /// <returns></returns>
    protected Task WebView2BackAsync()
    {
        InvokeOnUIThread(webView2.GoBack);

        return Task.CompletedTask;
    }

    #endregion

    #region 重新加载

    /// <summary>
    /// 重新加载
    /// </summary>
    /// <returns></returns>
    protected Task WebView2ReloadAsync()
    {
        InvokeOnUIThread(webView2.Reload);

        return Task.CompletedTask;
    }

    #endregion

    #region 设置标签

    /// <summary>
    /// 设置标签
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    protected Task WebView2TagSetAsync(object? tag)
    {
        InvokeOnUIThread(() =>
        {
            webView2.Tag = tag;
        });

        return Task.CompletedTask;
    }

    #endregion

    #region 跳转到指定地址

    /// <summary>
    /// 跳转到指定地址
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    protected Task WebView2NavigateAsync(string url)
    {
        InvokeOnUIThread(() =>
        {
            webView2.Source = new Uri(url);
        });

        return Task.CompletedTask;
    }

    #endregion

    #region 获取Cookies

    /// <summary>
    /// 获取Cookies
    /// </summary>
    /// <param name="url">指定url</param>
    /// <returns></returns>
    protected Task<string> WebView2GetCookiesAsync(string? url = null)
    {
        return InvokeOnUIThread(async () =>
        {
            var cookies = await webView2.CoreWebView2.CookieManager.GetCookiesAsync(url);

            if (cookies == null || cookies.Count == 0) return string.Empty;

            return string.Join("; ", cookies.Select(x => $"{x.Name}={x.Value}"));
        });
    }

    #endregion

    #region 执行JavaScript脚本

    /// <summary>
    /// 执行JavaScript脚本
    /// </summary>
    /// <param name="script"></param>
    /// <returns></returns>
    protected Task<string> WebView2ExecuteScriptAsync(string script)
    {
        return InvokeOnUIThread(async () =>
        {
            return await webView2.ExecuteScriptAsync(script);
        });
    }

    #endregion

    #endregion

    #region 初始化完成事件

    private void WebView2_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
    {
        try
        {
            WebView2InitializationCompletedAsync(sender, e, TokenSource.Token);
        }
        catch (OperationCanceledException ex)
        {
            DoCanceledAsync(ex);
        }
        catch (Exception ex)
        {
            DoExceptionAsync(ex, TokenSource.Token);
        }
    }

    #endregion

    #region 导航完成事件

    private void WebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        try
        {
            WebView2NavigationCompletedAsync(sender, e, TokenSource.Token);
        }
        catch (OperationCanceledException ex)
        {
            DoCanceledAsync(ex);
        }
        catch (Exception ex)
        {
            DoExceptionAsync(ex, TokenSource.Token);
        }
    }

    #endregion

    #region 消息接收事件

    private void WebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        try
        {
            WebView2WebMessageReceivedAsync(sender, e, TokenSource.Token);
        }
        catch (OperationCanceledException ex)
        {
            DoCanceledAsync(ex);
        }
        catch (Exception ex)
        {
            DoExceptionAsync(ex, TokenSource.Token);
        }
    }

    #endregion
}
