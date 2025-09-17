// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Simples.Windows;

/// <summary>
/// 示例窗体
/// </summary>
public class BrowserForm : WebView2Form
{
    /// <summary>
    /// 窗体标题
    /// </summary>
    protected override string Title => $"示例窗体 - {Version}";

    /// <summary>
    /// 窗体大小
    /// </summary>
    protected override Size WindowSize => new(1200, 700);

    /// <summary>
    /// 显示系统托盘
    /// </summary>
    protected override bool ShowTray => true;

    /// <summary>
    /// 工作频率（单位：秒），设置 0 时仅工作一次
    /// </summary>
    protected override int DoWorkInterval => GetConfigValue<int>("DoWorkInterval");

    /// <summary>
    /// 链接地址
    /// </summary>
    protected override string Url => "https://www.baidu.com";

    /// <summary>
    /// 任务执行
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        await AppendBoxAsync("任务执行", ColorTranslator.FromHtml("#1296db"));
    }

    /// <summary>
    /// 任务取消
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected override async Task DoCanceledAsync(OperationCanceledException ex)
    {
        await AppendBoxAsync("任务取消", Color.Red);
        await AppendBoxAsync(ex.Message, Color.Red);
    }

    /// <summary>
    /// 任务异常
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task DoExceptionAsync(Exception ex, CancellationToken cancellationToken)
    {
        await AppendBoxAsync("任务异常", Color.Red);
        await AppendBoxAsync(ex.Message, Color.Red);
    }

    /// <summary>
    /// 初始化完成事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task WebView2InitializationCompletedAsync(object? sender, CoreWebView2InitializationCompletedEventArgs e, CancellationToken cancellationToken)
    {
        await AppendBoxAsync("初始化完成", ColorTranslator.FromHtml("#1296db"));
    }

    /// <summary>
    /// 导航完成事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task WebView2NavigationCompletedAsync(object? sender, CoreWebView2NavigationCompletedEventArgs e, CancellationToken cancellationToken)
    {
        if (sender is WebView2 webView2 && e.IsSuccess)
        {
            await AppendBoxAsync(webView2.Source.AbsoluteUri);

            switch (webView2.Source.AbsolutePath)
            {
                case string x when x.Equals("/"):
                    webView2.Source = new Uri("https://www.51xulai.net");
                    break;
            }
        }
    }
}
