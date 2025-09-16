// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// 扫码窗体
/// </summary>
public partial class QRCodeForm : BaseForm
{
    #region 重写属性

    /// <summary>
    /// 二维码居中显示
    /// </summary>
    protected virtual bool IsCenterImage { get; } = false;

    #endregion

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public QRCodeForm()
    {
        InitializeComponent();
    }

    #endregion

    #region 重写方法

    /// <summary>
    /// 加载二维码
    /// </summary>
    /// <returns></returns>
    protected virtual Task LoadQRCodeAsync() => Task.CompletedTask;

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

    #endregion

    #region 继承方法

    #region 加载二维码

    /// <summary>
    /// 加载二维码
    /// </summary>
    /// <param name="url"></param>
    /// <param name="text"></param>
    protected Task AppendQRCodeAsync(string? url, string text = "用 [ 微信 ] 扫一扫")
    {
        if (string.IsNullOrEmpty(url)) return Task.CompletedTask;

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
    protected Task AppendQRCodeAsync(byte[]? bytes, string text = "用 [ 微信 ] 扫一扫")
    {
        if (bytes == null) return Task.CompletedTask;

        InvokeOnUIThread(() =>
        {
            pbQRCode.Image = Image.FromStream(new MemoryStream(bytes));
            lblMessage.Text = text;
        });

        return Task.CompletedTask;
    }

    #endregion

    #endregion

    #region 私有方法

    #region 检查登录

    private Task CheckLogin()
    {
        return Task.Run(async () =>
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
    }

    #endregion

    #endregion

    #region 窗体加载

    /// <summary>
    /// 窗体加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken)
    {
        if (IsCenterImage)
        {
            pbQRCode.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        return Task.WhenAll(LoadQRCodeAsync(), CheckLogin());
    }

    #endregion
}
