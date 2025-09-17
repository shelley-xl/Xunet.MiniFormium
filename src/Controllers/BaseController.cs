// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Controllers;

/// <summary>
/// 控制器基类
/// </summary>
public class BaseController : ControllerBase
{
    /// <summary>
    /// 公共查询返回
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="total"></param>
    /// <returns></returns>
    [NonAction]
    public virtual IResult XunetResult<TValue>(TValue value, int? total = null) where TValue : notnull
    {
        if (total.HasValue)
        {
            return Results.Ok(new PageResultDto<TValue>
            {
                Data = value,
                Total = total.Value,
                Code = ResultCode.Success,
                Message = "成功",
            });
        }
        else
        {
            return Results.Ok(new OperateResultDto<TValue>
            {
                Data = value,
                Code = ResultCode.Success,
                Message = "成功",
            });
        }
    }

    /// <summary>
    /// 公共操作返回
    /// </summary>
    /// <returns></returns>
    [NonAction]
    public virtual IResult XunetResult()
    {
        return Results.Ok(new OperateResultDto
        {
            Code = ResultCode.Success,
            Message = "成功",
        });
    }

    /// <summary>
    /// 公共操作返回
    /// </summary>
    /// <returns></returns>
    [NonAction]
    public virtual IResult XunetResult(string? error)
    {
        return Results.Ok(new OperateResultDto
        {
            Code = ResultCode.Failure,
            Message = error ?? "失败",
        });
    }
}
