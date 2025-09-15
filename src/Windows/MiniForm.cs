// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

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

    /// <summary>
    /// 信号量
    /// </summary>
    private readonly static AsyncSemaphore AsyncSemaphore = new();

    /// <summary>
    /// WebApp
    /// </summary>
    private static WebApplication? WebApp
    {
        get
        {
            return DependencyResolver.Current?.GetService<WebApplication>();
        }
    }

    /// <summary>
    /// 配置
    /// </summary>
    private static IConfigurationRoot Configuration
    {
        get
        {
            return DependencyResolver.Current?.GetRequiredService<IConfigurationRoot>() ?? throw new InvalidOperationException("No service for type 'IConfigurationRoot' has been registered.");
        }
    }

    /// <summary>
    /// 屏幕缩放比例
    /// </summary>
    private static float ScalingFactor
    {
        get
        {
            return DpiHelper.GetDpiScalingFactor();
        }
    }

    #endregion

    #region 虚属性，子类可重写

    /// <summary>
    /// 窗体标题
    /// </summary>
    protected virtual string? Title { get; } = "基础窗体";

    /// <summary>
    /// 工作频率（单位：秒），设置 0 时仅工作一次
    /// </summary>
    protected virtual int DoWorkInterval { get; } = 0;

    /// <summary>
    /// 禁用关于窗体
    /// </summary>
    protected virtual bool DisabledAboutForm { get; } = false;

    /// <summary>
    /// 最小化到系统托盘
    /// </summary>
    protected virtual bool IsTray { get; } = false;

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

    /// <summary>
    /// 执行任务
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task DoWorkAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// 任务取消
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected virtual Task DoCanceledAsync(OperationCanceledException ex) => Task.CompletedTask;

    /// <summary>
    /// 任务异常
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task DoExceptionAsync(Exception ex, CancellationToken cancellationToken) => Task.CompletedTask;

    #endregion

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public MiniForm()
    {
        InitializeComponent();

        Text = Title;
        notifyIcon.Text = Title;

        if (DisabledAboutForm)
        {
            msMenu.Items.Remove(tsmiAbout);
            cmsMenu.Items.Remove(tsmiAboutMe2);
        }
    }

    #endregion

    #region 通用属性，子类可继承

    #region 版本号

    /// <summary>
    /// 版本号
    /// </summary>
    protected static string Version
    {
        get
        {
            return $"v{Assembly.GetEntryAssembly()?.GetName().Version}";
        }
    }

    #endregion

    #region 默认请求客户端

    /// <summary>
    /// 默认请求客户端
    /// </summary>
    protected static HttpClient DefaultClient
    {
        get
        {
            return DependencyResolver.Current?.GetRequiredService<IHttpClientFactory>()?.CreateClient("default") ?? throw new InvalidOperationException("No headers for type 'StartupOptions.RequestHeaders' has been initialized.");
        }
    }

    #endregion

    #region 数据库访问

    /// <summary>
    /// 数据库访问
    /// </summary>
    protected static ISqlSugarClient Db
    {
        get
        {
            return DependencyResolver.Current?.GetRequiredService<ISqlSugarClient>() ?? throw new InvalidOperationException("No storage for type 'StartupOptions.SqliteStorage' has been initialized.");
        }
    }

    #endregion

    #endregion

    #region 通用方法，子类可继承

    #region 在UI线程上异步执行Action

    /// <summary>
    /// 在UI线程上异步执行Action
    /// </summary>
    /// <param name="action"></param>
    protected void InvokeOnUIThread(Action action)
    {
        if (IsDisposed || TokenSource.IsCancellationRequested) return;

        if (InvokeRequired)
        {
            Invoke(new System.Windows.Forms.MethodInvoker(action));
        }
        else
        {
            action.Invoke();
        }
    }

    #endregion

    #region 获取配置

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    protected static T? GetConfigValue<T>(string key)
    {
        return Configuration.GetSection(key).Get<T>();
    }

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected static string? GetConfigValue(string key)
    {
        return Configuration[key];
    }

    #endregion

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

    #region 私有方法

    #region 在线时长、内存占用情况显示

    private Task OnlineTimerAsync()
    {
        PerformanceCounter? counter = null;

        Task.Run(() =>
        {
            counter = new PerformanceCounter("Process", "Working Set - Private", Process.GetCurrentProcess().ProcessName);
        });

        Task.Run(async () =>
        {
            if (WebApp != null && WebApp.Urls.Count > 0)
            {
                InvokeOnUIThread(new Action(() =>
                {
                    var tsslStatus = tsStatus.Items[0];
                    var portLabel = new ToolStripStatusLabel
                    {
                        Text = $"监听端口：{string.Join(",", WebApp.Urls.Select(x => new Uri(x).Port))}",
                    };
                    tsStatus.Items.Clear();
                    tsStatus.Items.Add(portLabel);
                    tsStatus.Items.Add(new ToolStripSeparator());
                    tsStatus.Items.Add(tsslStatus);
                }));
            }

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
                var nextRun = "未开启定时作业";

                if (JobManager.GetSchedule(nameof(DoWork)) is Schedule doWork)
                {
                    nextRun = $"下次执行还剩：{(int)(doWork.NextRun - DateTime.Now).TotalSeconds:00} 秒";
                }

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

    #region 添加周期作业

    private async Task AddJobAsync()
    {
        if (JobManager.GetSchedule(nameof(DoWork)) == null)
        {
            JobManager.AddJob(DoWork, schedule =>
            {
                schedule.WithName(nameof(DoWork));
                if (DoWorkInterval > 0)
                {
                    schedule.ToRunNow().AndEvery(DoWorkInterval).Seconds();
                    InvokeOnUIThread(() =>
                    {
                        tsmiStop.Enabled = true;
                    });
                }
                else
                {
                    schedule.ToRunNow();
                }
            });
        }
        await Task.CompletedTask;
    }

    private async void DoWork()
    {
        using (await AsyncSemaphore.WaitAsync())
        {
            try
            {
                InvokeOnUIThread(() =>
                {
                    tsmiExecute.Enabled = false;
                    tsmiCancel.Enabled = true;
                });
                await DoWorkAsync(TokenSource.Token);
            }
            catch (OperationCanceledException ex)
            {
                await DoCanceledAsync(ex);
            }
            catch (Exception ex)
            {
                await DoExceptionAsync(ex, TokenSource.Token);
            }
            finally
            {
                InvokeOnUIThread(() =>
                {
                    tsmiExecute.Enabled = true;
                    tsmiCancel.Enabled = false;
                });
            }
        }
    }

    #endregion

    #endregion

    #region 窗体加载事件

    private void MiniForm_Load(object sender, EventArgs e)
    {
        Task.WhenAll(OnlineTimerAsync(), AddJobAsync(), OnLoadAsync(sender, e, TokenSource.Token));
    }

    #endregion

    #region 窗体关闭事件

    private void MiniForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (IsTray)
        {
            e.Cancel = true;
            Hide();
            notifyIcon.Visible = true;
        }

        Task.Run(() => OnCloseAsync(sender, e, TokenSource.Token));
    }

    #endregion

    #region 菜单事件

    #region 开启作业

    private void TsmiStart_Click(object sender, EventArgs e)
    {
        tsmiStart.Enabled = false;
        tsmiStop.Enabled = true;

        Task.Run(AddJobAsync);
    }

    #endregion

    #region 停止作业

    private void TsmiStop_Click(object sender, EventArgs e)
    {
        tsmiStart.Enabled = true;
        tsmiStop.Enabled = false;

        JobManager.RemoveJob("DoWork");
    }

    #endregion

    #region 执行任务

    private void TsmiExecute_Click(object sender, EventArgs e)
    {
        Task.Run(DoWork);
    }

    #endregion

    #region 取消任务

    private void TsmiCancel_Click(object sender, EventArgs e)
    {
        TokenSource.Cancel();
        TokenSource = new CancellationTokenSource();
    }

    #endregion

    #region 导出日志

    private void TsmiExportLog_Click(object sender, EventArgs e)
    {
        var sfd = new SaveFileDialog
        {
            // 设置保存文件对话框的标题
            Title = "请选择要保存的文件路径",
            // 初始化保存目录，默认exe文件目录
            InitialDirectory = Application.StartupPath,
            // 设置保存文件的类型
            Filter = "日志文件|*.log",
            // 设置默认文件名
            FileName = $"{DateTime.Now:yyyyMMddHHmmssffff}.log"
        };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            // 获得保存文件的路径
            string filePath = sfd.FileName;
            // 保存
            using var writer = new StreamWriter(filePath);
            writer.Write(rtbMessage.Text);
            writer.Flush();
        }
    }

    #endregion

    #region 清空日志

    private void TsmiClearLog_Click(object sender, EventArgs e)
    {
        rtbMessage.Clear();
    }

    #endregion

    #region 关于软件

    private void TsmiAboutMe_Click(object sender, EventArgs e)
    {
        DependencyResolver.Current?.GetService<AboutForm>()?.ShowDialog();
    }

    #endregion

    #endregion

    #region 系统托盘事件

    #region 系统托盘

    private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            Show();
            Activate();
        }
    }

    #endregion

    #region 主窗体

    private void TsmiMainForm_Click(object sender, EventArgs e)
    {
        Show();
        Activate();
    }

    #endregion

    #region 关于

    private void TsmiAboutMe2_Click(object sender, EventArgs e)
    {
        DependencyResolver.Current?.GetService<AboutForm>()?.ShowDialog();
    }

    #endregion

    #region 退出

    private void TsmiExit_Click(object sender, EventArgs e)
    {
        TokenSource.Cancel();
        TokenSource = new CancellationTokenSource();
        notifyIcon.Dispose();
        Environment.Exit(0);
    }

    #endregion

    #endregion
}
