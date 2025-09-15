// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Core;

/// <summary>
/// 分布式雪花Id
/// </summary>
public class Snowflake
{
    /// <summary>
    /// 唯一工作机器Id
    /// </summary>
    public ushort WorkerId { get; set; } = 1;

    /// <summary>
    /// 唯一数据中心Id
    /// </summary>
    public ushort DataCenterId { get; set; } = 1;
}
