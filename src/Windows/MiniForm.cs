// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// 迷你窗体
/// </summary>
public partial class MiniForm : BaseForm
{
    #region 重写属性

    /// <summary>
    /// 显示菜单
    /// </summary>
    protected override bool ShowMenu => true;

    /// <summary>
    /// 显示状态栏
    /// </summary>
    protected override bool ShowStatus => true;

    /// <summary>
    /// 日志控件
    /// </summary>
    protected override RichTextBox OutputTextBox => rtbMessage;

    #endregion

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public MiniForm()
    {
        InitializeComponent();
    }

    #endregion
}
