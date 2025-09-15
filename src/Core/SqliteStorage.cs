// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Core;

/// <summary>
/// 数据存储
/// </summary>
public class SqliteStorage
{
    /// <summary>
    /// 本地数据版本
    /// </summary>
    public string? DataVersion { get; set; }

    /// <summary>
    /// 数据存储名称
    /// </summary>
    public string? DbName { get; set; } = "Default";

    /// <summary>
    /// 实体表
    /// </summary>
    public Type[]? EntityTypes { get; set; } = [];
}
