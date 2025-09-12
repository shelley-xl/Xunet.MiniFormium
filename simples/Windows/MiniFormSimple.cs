// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Simples.Windows;

/// <summary>
/// 示例窗体
/// </summary>
public class MiniFormSimple : MiniForm
{
    protected override string Title => "示例窗体";

    protected override async Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken)
    {
        await Parallel.ForEachAsync(ParallelEnumerable.Range(1, 100), async (item, token) =>
        {
            await AppendBoxAsync($"测试一下{item}");

            await Task.Delay(1000, token);
        });
    }

    protected override async Task OnCloseAsync(object sender, FormClosingEventArgs e, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
