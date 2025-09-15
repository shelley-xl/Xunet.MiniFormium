// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Core;

/// <summary>
/// 请求头
/// </summary>
public class RequestHeaders : Dictionary<string, string>
{
    /// <summary>
    /// UserAgent
    /// </summary>
    public string UserAgent
    {
        get => GetHeader(HeaderNames.UserAgent);
        set => this[HeaderNames.UserAgent] = value;
    }

    /// <summary>
    /// Referrer
    /// </summary>
    public string Referrer
    {
        get => GetHeader(HeaderNames.Referer);
        set => this[HeaderNames.Referer] = value;
    }

    /// <summary>
    /// Authorization
    /// </summary>
    public string Authorization
    {
        get => GetHeader(HeaderNames.Authorization);
        set => this[HeaderNames.Authorization] = value;
    }

    /// <summary>
    /// Cookie
    /// </summary>
    public string Cookie
    {
        get => GetHeader(HeaderNames.Cookie);
        set => this[HeaderNames.Cookie] = value;
    }

    string GetHeader(string name) => ContainsKey(name) ? this[name] : string.Empty;
}