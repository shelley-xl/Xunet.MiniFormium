// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Exceptions;

/// <summary>
/// 接口未找到异常
/// </summary>
public partial class MiniFormiumNotFoundException : Exception
{
    /// <summary>
    /// 接口未找到异常
    /// </summary>
    public MiniFormiumNotFoundException() { }

    /// <summary>
    /// 接口未找到异常
    /// </summary>
    /// <param name="message">消息</param>
    public MiniFormiumNotFoundException(string? message) : base(message) { }

    /// <summary>
    /// 消息
    /// </summary>
    public override string Message => "404 NotFound";
}
