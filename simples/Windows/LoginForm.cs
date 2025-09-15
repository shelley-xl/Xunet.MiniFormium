// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Simples.Windows;

/// <summary>
/// 登录窗体
/// </summary>
public class LoginForm(MainForm mainForm) : QRCodeForm
{
    protected override string Title => "扫码登录";

    protected override async Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken)
    {
        await AppendQRCodeAsync("https://login.weixin.qq.com/qrcode/YbRxa9j-Qw==");
    }

    protected override async Task<bool> CheckLoginAsync()
    {
        await Task.CompletedTask;

        return false;
    }

    protected override async Task LoginSuccessAsync()
    {
        await ShowMainFormAsync(mainForm);
    }
}
