// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Simples.Windows;

/// <summary>
/// 示例窗体
/// </summary>
public class MainForm : MiniForm
{
    protected override string Title => $"示例窗体 - {Version}";

    protected override int DoWorkInterval => GetConfigValue<int>("DoWorkInterval");

    protected override async Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    protected override async Task OnCloseAsync(object sender, FormClosingEventArgs e, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    protected override async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        await AppendBoxAsync("开始执行任务 ...");

        await Parallel.ForEachAsync(ParallelEnumerable.Range(1, 100), async (item, token) =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            await AppendBoxAsync($"任务...{Guid.NewGuid():N}...{item}...ok");

            await Task.Delay(1000, token);
        });

        await AppendBoxAsync("任务执行完成！");
    }

    protected override async Task DoCanceledAsync(OperationCanceledException ex)
    {
        await AppendBoxAsync("任务取消", Color.Red);
        await AppendBoxAsync(ex.Message, Color.Red);
    }

    protected override async Task DoExceptionAsync(Exception ex, CancellationToken cancellationToken)
    {
        await AppendBoxAsync("任务异常", Color.Red);
        await AppendBoxAsync(ex.Message, Color.Red);
    }
}
