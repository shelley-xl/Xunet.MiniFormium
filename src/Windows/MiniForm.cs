// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) ���� ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// �������
/// </summary>
public partial class MiniForm : Form, IMiniFormium
{
    #region ˽������

    /// <summary>
    /// ȡ������
    /// </summary>
    private static CancellationTokenSource TokenSource { get; set; } = new();

    /// <summary>
    /// �ź���
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
    /// ����
    /// </summary>
    private static IConfigurationRoot Configuration
    {
        get
        {
            return DependencyResolver.Current?.GetRequiredService<IConfigurationRoot>() ?? throw new InvalidOperationException("No service for type 'IConfigurationRoot' has been registered.");
        }
    }

    /// <summary>
    /// ��Ļ���ű���
    /// </summary>
    private static float ScalingFactor
    {
        get
        {
            return DpiHelper.GetDpiScalingFactor();
        }
    }

    #endregion

    #region �����ԣ��������д

    /// <summary>
    /// �������
    /// </summary>
    protected virtual string? Title { get; } = "��������";

    /// <summary>
    /// ����Ƶ�ʣ���λ���룩������ 0 ʱ������һ��
    /// </summary>
    protected virtual int DoWorkInterval { get; } = 0;

    /// <summary>
    /// ���ù��ڴ���
    /// </summary>
    protected virtual bool DisabledAboutForm { get; } = false;

    /// <summary>
    /// ��С����ϵͳ����
    /// </summary>
    protected virtual bool IsTray { get; } = false;

    #endregion

    #region �鷽�����������д

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// ����ر�
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task OnCloseAsync(object sender, FormClosingEventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// ִ������
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task DoWorkAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// ����ȡ��
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected virtual Task DoCanceledAsync(OperationCanceledException ex) => Task.CompletedTask;

    /// <summary>
    /// �����쳣
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task DoExceptionAsync(Exception ex, CancellationToken cancellationToken) => Task.CompletedTask;

    #endregion

    #region ���캯��

    /// <summary>
    /// ���캯��
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

    #region ͨ�����ԣ�����ɼ̳�

    #region �汾��

    /// <summary>
    /// �汾��
    /// </summary>
    protected static string Version
    {
        get
        {
            return $"v{Assembly.GetEntryAssembly()?.GetName().Version}";
        }
    }

    #endregion

    #region Ĭ������ͻ���

    /// <summary>
    /// Ĭ������ͻ���
    /// </summary>
    protected static HttpClient DefaultClient
    {
        get
        {
            return DependencyResolver.Current?.GetRequiredService<IHttpClientFactory>()?.CreateClient("default") ?? throw new InvalidOperationException("No headers for type 'StartupOptions.RequestHeaders' has been initialized.");
        }
    }

    #endregion

    #region ���ݿ����

    /// <summary>
    /// ���ݿ����
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

    #region ͨ�÷���������ɼ̳�

    #region ��UI�߳����첽ִ��Action

    /// <summary>
    /// ��UI�߳����첽ִ��Action
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

    #region ��ȡ����

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    protected static T? GetConfigValue<T>(string key)
    {
        return Configuration.GetSection(key).Get<T>();
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected static string? GetConfigValue(string key)
    {
        return Configuration[key];
    }

    #endregion

    #region ��־���

    /// <summary>
    /// ��־���
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
            rtbMessage.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}��{text}");
            rtbMessage.SelectionColor = rtbMessage.ForeColor;
            rtbMessage.ScrollToCaret();
        });

        return Task.CompletedTask;
    }

    #endregion

    #endregion

    #region ˽�з���

    #region ����ʱ�����ڴ�ռ�������ʾ

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
                        Text = $"�����˿ڣ�{string.Join(",", WebApp.Urls.Select(x => new Uri(x).Port))}",
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
                var nextRun = "δ������ʱ��ҵ";

                if (JobManager.GetSchedule(nameof(DoWork)) is Schedule doWork)
                {
                    nextRun = $"�´�ִ�л�ʣ��{(int)(doWork.NextRun - DateTime.Now).TotalSeconds:00} ��";
                }

                InvokeOnUIThread(new Action(() =>
                {
                    tsslStatus.Text = $"����ʱ����{hours:00} Сʱ {minutes:00} �� {remainingSeconds:00} �룬�ڴ棺{usedMemory:0.0} MB��{nextRun}";
                }));

                seconds++;

                await Task.Delay(1000);
            }
        });

        return Task.CompletedTask;
    }

    #endregion

    #region ���������ҵ

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

    #region ��������¼�

    private void MiniForm_Load(object sender, EventArgs e)
    {
        Task.WhenAll(OnlineTimerAsync(), AddJobAsync(), OnLoadAsync(sender, e, TokenSource.Token));
    }

    #endregion

    #region ����ر��¼�

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

    #region �˵��¼�

    #region ������ҵ

    private void TsmiStart_Click(object sender, EventArgs e)
    {
        tsmiStart.Enabled = false;
        tsmiStop.Enabled = true;

        Task.Run(AddJobAsync);
    }

    #endregion

    #region ֹͣ��ҵ

    private void TsmiStop_Click(object sender, EventArgs e)
    {
        tsmiStart.Enabled = true;
        tsmiStop.Enabled = false;

        JobManager.RemoveJob("DoWork");
    }

    #endregion

    #region ִ������

    private void TsmiExecute_Click(object sender, EventArgs e)
    {
        Task.Run(DoWork);
    }

    #endregion

    #region ȡ������

    private void TsmiCancel_Click(object sender, EventArgs e)
    {
        TokenSource.Cancel();
        TokenSource = new CancellationTokenSource();
    }

    #endregion

    #region ������־

    private void TsmiExportLog_Click(object sender, EventArgs e)
    {
        var sfd = new SaveFileDialog
        {
            // ���ñ����ļ��Ի���ı���
            Title = "��ѡ��Ҫ������ļ�·��",
            // ��ʼ������Ŀ¼��Ĭ��exe�ļ�Ŀ¼
            InitialDirectory = Application.StartupPath,
            // ���ñ����ļ�������
            Filter = "��־�ļ�|*.log",
            // ����Ĭ���ļ���
            FileName = $"{DateTime.Now:yyyyMMddHHmmssffff}.log"
        };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            // ��ñ����ļ���·��
            string filePath = sfd.FileName;
            // ����
            using var writer = new StreamWriter(filePath);
            writer.Write(rtbMessage.Text);
            writer.Flush();
        }
    }

    #endregion

    #region �����־

    private void TsmiClearLog_Click(object sender, EventArgs e)
    {
        rtbMessage.Clear();
    }

    #endregion

    #region �������

    private void TsmiAboutMe_Click(object sender, EventArgs e)
    {
        DependencyResolver.Current?.GetService<AboutForm>()?.ShowDialog();
    }

    #endregion

    #endregion

    #region ϵͳ�����¼�

    #region ϵͳ����

    private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            Show();
            Activate();
        }
    }

    #endregion

    #region ������

    private void TsmiMainForm_Click(object sender, EventArgs e)
    {
        Show();
        Activate();
    }

    #endregion

    #region ����

    private void TsmiAboutMe2_Click(object sender, EventArgs e)
    {
        DependencyResolver.Current?.GetService<AboutForm>()?.ShowDialog();
    }

    #endregion

    #region �˳�

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
