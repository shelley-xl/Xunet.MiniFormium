// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Simples.Models;

/// <summary>
/// 博客园
/// </summary>
[SugarTable("cnblogs", "博客园")]
public class CnBlogsModel
{
    /// <summary>
    /// 编号
    /// </summary>
    [SugarColumn(ColumnDescription = "编号", IsPrimaryKey = true)]
    public string? Id { get; set; } = SnowFlakeSingle.Instance.NextId().ToString();

    /// <summary>
    /// 标题
    /// </summary>
    [SugarColumn(ColumnDescription = "标题")]
    public string? Title { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [SugarColumn(ColumnDescription = "地址")]
    public string? Url { get; set; }

    /// <summary>
    /// 摘要
    /// </summary>
    [SugarColumn(ColumnDescription = "摘要")]
    public string? Summary { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnDescription = "创建时间")]
    public DateTime? CreateTime { get; set; } = DateTime.Now;
}
