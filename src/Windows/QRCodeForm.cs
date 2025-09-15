// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// 扫码窗体
/// </summary>
public partial class QRCodeForm : Form, IMiniFormium
{
    /// <summary>
    /// 取消令牌
    /// </summary>
    private static CancellationTokenSource TokenSource { get; set; } = new();

    /// <summary>
    /// 窗体标题
    /// </summary>
    protected virtual string? Title { get; } = "扫码登录";

    /// <summary>
    /// 二维码地址
    /// </summary>
    protected virtual string? QRCodeUrl { get; }

    /// <summary>
    /// 二维码标题
    /// </summary>
    protected virtual string? QRCodeTitle { get; } = "用 [ 微信 ] 扫一扫";

    /// <summary>
    /// 二维码居中显示
    /// </summary>
    protected virtual bool IsCenterImage { get; } = false;

    /// <summary>
    /// 窗体加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// 检查登录
    /// </summary>
    /// <returns></returns>
    protected virtual Task<bool> CheckLoginAsync() => Task.FromResult(false);

    /// <summary>
    /// 登录成功
    /// </summary>
    /// <returns></returns>
    protected virtual Task LoginSuccessAsync() => Task.CompletedTask;

    /// <summary>
    /// 构造函数
    /// </summary>
    public QRCodeForm()
    {
        InitializeComponent();

        Text = Title;
        lblMessage.Text = QRCodeTitle;

        if (!string.IsNullOrEmpty(QRCodeUrl))
        {
            pbQRCode.ImageLocation = QRCodeUrl;
        }

        if (IsCenterImage)
        {
            pbQRCode.SizeMode = PictureBoxSizeMode.CenterImage;
        }
    }

    private Task CheckLogin()
    {
        Task.Run(async () =>
        {
            while (!IsDisposed && !TokenSource.IsCancellationRequested)
            {
                if (await CheckLoginAsync())
                {
                    await LoginSuccessAsync();
                    break;
                }
                await Task.Delay(2000);
            }
        });

        return Task.CompletedTask;
    }

    /// <summary>
    /// 窗体加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void QRCodeForm_Load(object sender, EventArgs e)
    {
        Task.WhenAll(CheckLogin(), OnLoadAsync(sender, e, TokenSource.Token));
    }

    /// <summary>
    /// 加载二维码
    /// </summary>
    /// <param name="url"></param>
    /// <param name="text"></param>
    protected Task AppendQRCodeAsync(string url, string text = "用 [ 微信 ] 扫一扫")
    {
        InvokeOnUIThread(() =>
        {
            pbQRCode.ImageLocation = url;
            lblMessage.Text = text;
        });

        return Task.CompletedTask;
    }

    /// <summary>
    /// 加载二维码
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="text"></param>
    protected void AppendQRCode(byte[] bytes, string text = "用 [ 微信 ] 扫一扫")
    {
        InvokeOnUIThread(() =>
        {
            pbQRCode.Image = Image.FromStream(new MemoryStream(bytes));
            lblMessage.Text = text;
        });
    }

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

    /// <summary>
    /// 显示主窗体
    /// </summary>
    /// <param name="main"></param>
    protected Task ShowMainFormAsync(Form main)
    {
        InvokeOnUIThread(() =>
        {
            Hide();
            main.Show();
            main.Activate();
        });

        return Task.CompletedTask;
    }
}
