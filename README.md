# Xunet.MiniFormium

基于.NET Core的轻量级现代化桌面应用，专为爬虫设计，支持标准的http请求，网页解析，网页自动化，执行js脚本等，内置.NET WebApi支持和数据持久化存储。

Support .NET 8.0+

功能特性：

- 轻量级现代化窗体应用框架，支持.NET 8.0及更高版本
- 重写DI容器，支持依赖注入
- 自定义全局异常处理
- 集成WebApi服务，可提供RESTful API
- 内置数据持久化存储
- 内置任务调度器，支持定时任务
- 内置日志系统，支持日志记录
- 支持作业暂停、开启，任务执行、取消
- 支持在线时长、内存占用情况显示
- 面向异步编程，支持异步方法
- 面向对象编程，实现封装、继承、多态

[![Nuget](https://img.shields.io/nuget/v/Xunet.MiniFormium.svg?style=flat-square)](https://www.nuget.org/packages/Xunet.MiniFormium)
[![Downloads](https://img.shields.io/nuget/dt/Xunet.MiniFormium.svg?style=flat-square)](https://www.nuget.org/stats/packages/Xunet.MiniFormium?groupby=Version)
[![License](https://img.shields.io/github/license/shelley-xl/Xunet.MiniFormium.svg)](https://github.com/shelley-xl/Xunet.MiniFormium/blob/master/LICENSE)
![Vistors](https://visitor-badge.laobi.icu/badge?page_id=https://github.com/shelley-xl/Xunet.MiniFormium)

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
                    HeaderNames.UserAgent, "Mozilla/5.0 (iPhone; CPU iPhone OS 6_1_3 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Mobile/10B329 MicroMessenger/5.0.1"
                }
            };
            options.Storage = new()
            {
                DataVersion = "25.9.15.1036",
                DbName = "Xunet.MiniFormium.Simples",
                EntityTypes = [],
            };
            options.Snowflake = new()
            {
                WorkerId = 1,
                DataCenterId = 1,
            };
        });

        builder.Services.AddWebApi((provider, services) =>
        {
            var db = provider.GetRequiredService<ISqlSugarClient>();

            services.AddSingleton(db);
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
    protected override string Title => $"示例窗体 - {Version}";

    protected override int DoWorkInterval => GetConfigValue<int>("DoWorkInterval");

    protected override async Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    protected override async Task OnCloseAsync(object sender, FormClosingEventArgs e, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    protected override async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        await AppendBoxAsync("开始执行任务 ...");

        await Parallel.ForEachAsync(ParallelEnumerable.Range(1, 100), async (item, token) =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            await AppendBoxAsync($"任务...{Guid.NewGuid():N}...{item}...ok");

            await Task.Delay(1000, token);
        });

        await AppendBoxAsync("任务执行完成！");
    }

    protected override async Task DoCanceledAsync(OperationCanceledException ex)
    {
        await AppendBoxAsync("任务取消", Color.Red);
        await AppendBoxAsync(ex.Message, Color.Red);
    }

    protected override async Task DoExceptionAsync(Exception ex, CancellationToken cancellationToken)
    {
        await AppendBoxAsync("任务异常", Color.Red);
        await AppendBoxAsync(ex.Message, Color.Red);
    }
}
```

## 更新日志

[CHANGELOG](CHANGELOG.md)
