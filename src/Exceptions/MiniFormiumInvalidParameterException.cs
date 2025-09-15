// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Exceptions;

/// <summary>
/// 无效的参数异常
/// </summary>
public partial class MiniFormiumInvalidParameterException : Exception
{
    /// <summary>
    /// 无效的参数异常
    /// </summary>
    public MiniFormiumInvalidParameterException() { }

    /// <summary>
    /// 无效的参数异常
    /// </summary>
    /// <param name="message">消息</param>
    public MiniFormiumInvalidParameterException(string? message) : base(message) { }

    /// <summary>
    /// 消息
    /// </summary>
    public override string Message => "无效的参数";
}
