// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium;

using Core;

/// <summary>
/// MiniFormiumApplication
/// </summary>
public class MiniFormiumApplication
{
    /// <summary>
    /// Current
    /// </summary>
    public static MiniFormiumApplication? Current { get; private set; }

    /// <summary>
    /// Services
    /// </summary>
    public IServiceProvider Services { get; private set; }

    /// <summary>
    /// CreateBuilder
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public static MiniFormiumApplicationBuilder CreateBuilder()
    {
        if (Current != null)
        {
            throw new ApplicationException("已经初始化，只允许运行一个实例。");
        }

        return new MiniFormiumApplicationBuilder();
    }

    internal MiniFormiumApplication(IServiceCollection services)
    {
        if (Current != null)
        {
            throw new ApplicationException("已经初始化，只允许运行一个实例。");
        }

        Current = this;

        Services = services.BuildServiceProvider();
    }

    /// <summary>
    /// 运行
    /// </summary>
    public void Run()
    {
        var Properties = Services.GetRequiredService<PropertyManager>();
        var UseMiniFormium = Properties.GetValue<bool>(nameof(MiniFormiumApplicationExtensions.UseMiniFormium));
        var UseSingleApp = Properties.GetValue<bool>(nameof(MiniFormiumApplicationExtensions.UseSingleApp));
        var UseWebApi = Properties.GetValue<bool>(nameof(MiniFormiumApplicationExtensions.UseWebApi));

        // 使用单例应用
        using var mutex = Services.GetRequiredService<Mutex>();
        if (UseSingleApp)
        {
            if (!mutex.WaitOne(0, false))
            {
                MessageBox.Show("已经有一个正在运行的程序，请勿重复运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        // 使用WebApi
        if (UseWebApi)
        {
            var app = Services.GetRequiredService<WebApplication>();

            app.RunAsync();
        }

        // 使用MiniFormium
        if (UseMiniFormium)
        {
            var createMainWindowAction = Services.GetRequiredService<MiniFormiumCreationAction>();

            var mainWindowOptions = Services.GetRequiredService<MiniFormiumOptions>();

            createMainWindowAction.Invoke(Services);

            createMainWindowAction.Dispose();

            Application.Run(mainWindowOptions.Context);
        }

        if (UseSingleApp)
        {
            mutex?.ReleaseMutex();
        }
    }
}
