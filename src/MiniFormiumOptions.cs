// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium;

/// <summary>
/// MiniFormiumOptions
/// </summary>
public sealed class MiniFormiumOptions
{
    internal ApplicationContext Context { get; set; } = new ApplicationContext();

    private IServiceCollection Services { get; }

    internal MiniFormiumOptions(IServiceCollection services)
    {
        Services = services;
    }

    /// <summary>
    /// UseMiniFormium
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public MiniFormiumCreationAction UseMiniFormium<T>() where T : Form
    {
        Services.AddSingleton<T>();

        return new MiniFormiumCreationAction(provider =>
        {
            var form = provider.GetRequiredService<T>();

            Context.MainForm = form;
        });
    }
}
