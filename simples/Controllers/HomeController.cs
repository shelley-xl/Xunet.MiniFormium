// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Simples.Controllers;

/// <summary>
/// 首页
/// </summary>
/// <param name="Db"></param>
[Route("api/home")]
public class HomeController(ISqlSugarClient Db) : BaseController
{
    /// <summary>
    /// 获取csdn博客列表
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    [HttpGet("csdn/page")]
    public async Task<IResult> CsdnListPage(int page = 1, int size = 20)
    {
        RefAsync<int> totalNumber = new(0);

        var list = await Db.Queryable<CnBlogsModel>().ToPageListAsync(page, size, totalNumber);

        return XunetResult(list, totalNumber);
    }
}
