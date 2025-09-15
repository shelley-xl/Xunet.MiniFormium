// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium;

using Core;

/// <summary>
/// MiniFormiumApplication扩展
/// </summary>
public static class MiniFormiumApplicationExtensions
{
    static PropertyManager? Properties { get; set; }

    /// <summary>
    /// 使用MiniFormium
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static MiniFormiumApplication UseMiniFormium(this MiniFormiumApplication app)
    {
        Properties = app.Services.GetRequiredService<PropertyManager>();

        Properties?.SetValue(nameof(UseMiniFormium), true);

        DependencyResolver.Initialize(app.Services);

        return app;
    }

    /// <summary>
    /// 使用单例应用
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static MiniFormiumApplication UseSingleApp(this MiniFormiumApplication app)
    {
        Properties?.SetValue(nameof(UseSingleApp), true);

        return app;
    }

    /// <summary>
    /// 使用WebApi
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static MiniFormiumApplication UseWebApi(this MiniFormiumApplication app)
    {
        Properties?.SetValue(nameof(UseWebApi), true);

        return app;
    }
}
