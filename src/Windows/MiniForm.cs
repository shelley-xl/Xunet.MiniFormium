// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) ���� ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium;

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

    #endregion

    #region �����ԣ��������д

    /// <summary>
    /// �������
    /// </summary>
    protected virtual string? Title { get; } = "��������";

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

    #endregion

    #region ���캯��

    /// <summary>
    /// ���캯��
    /// </summary>
    public MiniForm()
    {
        InitializeComponent();

        Text = Title;
    }

    #endregion

    #region ��UI�߳����첽ִ��Action

    /// <summary>
    /// ��UI�߳����첽ִ��Action
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

    #region ͨ�÷���������ɼ̳�

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

    #region ����ʱ�����ڴ�ռ�������ʾ

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
                //    nextRun = $"�´�ִ�л�ʣ��{(int)(doWork.NextRun - DateTime.Now).TotalSeconds:00} ��";
                //}
                //else
                //{
                //    nextRun = "δ������ʱ��ҵ";
                //}

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

    #region ��������¼�

    private void MiniForm_Load(object sender, EventArgs e)
    {
        Task.WhenAll(OnlineTimer(), OnLoadAsync(sender, e, TokenSource.Token));
    }

    #endregion

    #region ����ر��¼�

    private void MiniForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Task.Run(() => OnCloseAsync(sender, e, TokenSource.Token));
    }

    #endregion
}
