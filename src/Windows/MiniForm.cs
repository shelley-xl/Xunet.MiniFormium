// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium;

/// <summary>
/// 窗体基类
/// </summary>
public partial class MiniForm : Form, IMiniFormium
{
    #region 私有属性

    /// <summary>
    /// 取消令牌
    /// </summary>
    private static CancellationTokenSource TokenSource { get; set; } = new();

    #endregion

    #region 虚属性，子类可重写

    /// <summary>
    /// 窗体标题
    /// </summary>
    protected virtual string? Title { get; } = "基础窗体";

    #endregion

    #region 虚方法，子类可重写

    /// <summary>
    /// 窗体加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// 窗体关闭
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task OnCloseAsync(object sender, FormClosingEventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    #endregion

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public MiniForm()
    {
        InitializeComponent();

        Text = Title;
    }

    #endregion

    #region 在UI线程上异步执行Action

    /// <summary>
    /// 在UI线程上异步执行Action
    /// </summary>
    /// <param name="action"></param>
    protected void InvokeOnUIThread(Action action)
    {
        if (InvokeRequired)
        {
            Invoke(new MethodInvoker(action));
        }
        else
        {
            action.Invoke();
        }
    }

    #endregion

    #region 通用方法，子类可继承

    #region 日志输出

    /// <summary>
    /// 日志输出
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    protected Task AppendBoxAsync(string text, Color? color = null)
    {
        InvokeOnUIThread(() =>
        {
            if (rtbMessage.Lines.Length > 0)
            {
                rtbMessage.AppendText(Environment.NewLine);
            };
            rtbMessage.SelectionStart = rtbMessage.TextLength;
            rtbMessage.SelectionLength = 0;
            rtbMessage.SelectionColor = color ?? Color.Black;
            rtbMessage.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}：{text}");
            rtbMessage.SelectionColor = rtbMessage.ForeColor;
            rtbMessage.ScrollToCaret();
        });

        return Task.CompletedTask;
    }

    #endregion

    #endregion

    #region 在线时长、内存占用情况显示

    private Task OnlineTimer()
    {
        PerformanceCounter? counter = null;

        Task.Run(() =>
        {
            counter = new PerformanceCounter("Process", "Working Set - Private", Process.GetCurrentProcess().ProcessName);
        });

        Task.Run(async () =>
        {
            var seconds = 0;

            while (!IsDisposed && !TokenSource.IsCancellationRequested)
            {
                var usedMemory = 0d;
                if (counter != null)
                {
                    usedMemory = Math.Round(counter.RawValue / 1024.0 / 1024.0, 1);
                }
                int hours = seconds / 3600;
                int minutes = seconds % 3600 / 60;
                int remainingSeconds = seconds % 3600 % 60;
                var nextRun = string.Empty;

                //if (JobManager.GetSchedule("DoWork") is Schedule doWork)
                //{
                //    nextRun = $"下次执行还剩：{(int)(doWork.NextRun - DateTime.Now).TotalSeconds:00} 秒";
                //}
                //else
                //{
                //    nextRun = "未开启定时作业";
                //}

                InvokeOnUIThread(new Action(() =>
                {
                    tsslStatus.Text = $"在线时长：{hours:00} 小时 {minutes:00} 分 {remainingSeconds:00} 秒，内存：{usedMemory:0.0} MB，{nextRun}";
                }));

                seconds++;

                await Task.Delay(1000);
            }
        });

        return Task.CompletedTask;
    }

    #endregion

    #region 窗体加载事件

    private void MiniForm_Load(object sender, EventArgs e)
    {
        Task.WhenAll(OnlineTimer(), OnLoadAsync(sender, e, TokenSource.Token));
    }

    #endregion

    #region 窗体关闭事件

    private void MiniForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Task.Run(() => OnCloseAsync(sender, e, TokenSource.Token));
    }

    #endregion
}
