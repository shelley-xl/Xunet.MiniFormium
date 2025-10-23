# Xunet.MiniFormium

.NET 轻量级现代化桌面应用，专为爬虫设计，支持标准的Http请求，网页解析，网页自动化，执行js脚本等，内置.NET WebApi支持和数据持久化存储。

Support .NET 8.0+

[![Nuget](https://img.shields.io/nuget/v/Xunet.MiniFormium.svg?style=flat-square)](https://www.nuget.org/packages/Xunet.MiniFormium)
[![Downloads](https://img.shields.io/nuget/dt/Xunet.MiniFormium.svg?style=flat-square)](https://www.nuget.org/stats/packages/Xunet.MiniFormium?groupby=Version)
[![License](https://img.shields.io/github/license/shelley-xl/Xunet.MiniFormium.svg)](https://github.com/shelley-xl/Xunet.MiniFormium/blob/master/LICENSE)

功能特性：

- 轻量级现代化桌面应用框架，支持.NET 8.0及更高版本
- 重写DI容器，支持依赖注入
- 自定义全局异常处理
- 集成WebApi服务，可提供RESTful API
- 集成WebView2，支持网页渲染，可实现自动化
- 内置数据持久化存储
- 内置任务调度器，支持定时任务
- 内置日志系统，支持日志记录
- 支持作业暂停、开启，任务执行、取消
- 支持在线时长、内存占用情况显示
- 支持网页解析、执行JavaScript脚本
- 面向异步编程，支持异步方法
- 面向对象编程，实现封装、继承、多态

## 安装

Xunet.MiniFormium 以 NuGet 包的形式提供。您可以使用 NuGet 包控制台窗口安装它：

```
PM> Install-Package Xunet.MiniFormium
```

## 使用

Program.cs

```c#
internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        var builder = MiniFormiumApplication.CreateBuilder();

        builder.Services.AddMiniFormium<MainForm>(options =>
        {
            options.Headers = new()
            {
                {
                    HeaderNames.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36"
                },
            };
            options.Storage = new()
            {
                DataVersion = "25.9.15.1036",
                DbName = "Xunet.MiniFormium.Simples",
                EntityTypes = [typeof(CnBlogsModel)],
            };
            options.Snowflake = new()
            {
                WorkerId = 1,
                DataCenterId = 1,
            };
        });

        builder.Services.AddWebApi((provider, services) =>
        {
            var db = provider.GetService<ISqlSugarClient>();
            if (db != null)
            {
                services.AddSingleton(db);
            }
        });

        var app = builder.Build();

        app.UseMiniFormium();

        app.UseSingleApp();

        app.UseWebApi();

        app.Run();
    }
}
```

MainForm.cs

```c#
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
```

## 示例应用

[simples](simples)

## 官方博客

[清风徐来](https://www.51xulai.net/)

## 更新日志

[CHANGELOG](CHANGELOG.md)
