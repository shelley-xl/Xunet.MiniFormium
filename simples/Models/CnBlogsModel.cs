// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Simples.Models;

/// <summary>
/// CnBlogsModel
/// </summary>
[SugarTable("cnblogs")]
public class CnBlogsModel
{
    /// <summary>
    /// Id
    /// </summary>
    [Description("编号")]
    [SugarColumn(IsPrimaryKey = true)]
    public string? Id { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    [Description("标题")]
    public string? Title { get; set; }

    /// <summary>
    /// Url
    /// </summary>
    [Description("地址")]
    public string? Url { get; set; }

    /// <summary>
    /// Summary
    /// </summary>
    [Description("摘要")]
    public string? Summary { get; set; }

    /// <summary>
    /// CreateTime
    /// </summary>
    [Description("创建时间")]
    public DateTime? CreateTime { get; set; }
}
