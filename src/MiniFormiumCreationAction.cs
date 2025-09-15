// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium;

/// <summary>
/// MiniFormiumCreationAction
/// </summary>
/// <param name="createAction"></param>
public sealed class MiniFormiumCreationAction(Action<IServiceProvider> createAction) : IDisposable
{
    internal Action<IServiceProvider> CreateAction { get; } = createAction;

    internal void Invoke(IServiceProvider services)
    {
        CreateAction.Invoke(services);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {

    }
}
