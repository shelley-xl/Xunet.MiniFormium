# Xunet.MiniFormium

基于.NET Core的轻量级现代化窗体应用，专为爬虫设计，支持标准的http请求，网页解析，网页自动化，执行js脚本等，内置.NET WebApi支持和数据持久化存储。

Support .NET 8.0/9.0

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
                DataVersion = "24.8.9.1822",
                DbName = "Xunet.MiniFormium.Simples",
                EntityTypes = [typeof(CnBlogsModel)],
            };
            options.Snowflake = new()
            {
                WorkerId = 1,
                DataCenterId = 1,
            };
        });

        builder.Services.AddWebApi(Assembly.GetExecutingAssembly(), (provider, services) =>
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

```

## 更新日志

[CHANGELOG](CHANGELOG.md)
