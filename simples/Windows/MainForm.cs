// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Simples.Windows;

/// <summary>
/// 示例窗体
/// </summary>
public class MainForm : MiniForm
{
    /// <summary>
    /// 窗体标题
    /// </summary>
    protected override string Title => $"示例窗体 - {Version}";

    /// <summary>
    /// 显示系统托盘
    /// </summary>
    protected override bool ShowTray => true;

    /// <summary>
    /// 最小化到系统托盘
    /// </summary>
    protected override bool IsTray => false;

    /// <summary>
    /// 禁用关于窗体
    /// </summary>
    protected override bool DisabledAboutForm => false;

    /// <summary>
    /// 工作频率（单位：秒），设置 0 时仅工作一次
    /// </summary>
    protected override int DoWorkInterval => GetConfigValue<int>("DoWorkInterval");

    /// <summary>
    /// 窗体关闭
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override Task OnCloseAsync(object sender, FormClosingEventArgs e, CancellationToken cancellationToken)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            Application.Exit();
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 任务执行
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        await AppendBoxAsync("开始执行任务，请稍后 ...", ColorTranslator.FromHtml("#1296db"));

        var html = await DefaultClient.GetStringAsync("https://www.cnblogs.com/", cancellationToken);

        CreateHtmlDocument(html);

        var list = FindElementsByXPath("//*[@id=\"post_list\"]/article");

        await Parallel.ForEachAsync(list, async (item, token) =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            var model = new CnBlogsModel
            {
                Id = CreateNextIdString(),
                Title = FindText(FindElementByXPath(item, "section/div/a")),
                Url = FindAttributeValue(FindElementByXPath(item, "section/div/a"), "href"),
                Summary = Trim(FindText(FindElementByXPath(item, "section/div/p"))),
                CreateTime = DateTime.Now
            };

            await AppendBoxAsync($"{model.Title} ...");

            await Db.Insertable(model).ExecuteCommandAsync(cancellationToken);

            await Task.Delay(1000, token);
        });

        await AppendBoxAsync("任务执行完成！", ColorTranslator.FromHtml("#1296db"));
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
}
