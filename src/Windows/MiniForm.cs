// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) ���� ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// ���㴰��
/// </summary>
public partial class MiniForm : BaseForm
{
    #region ��д����

    /// <summary>
    /// ��ʾ�˵�
    /// </summary>
    protected override bool ShowMenu => true;

    /// <summary>
    /// ��ʾ״̬��
    /// </summary>
    protected override bool ShowStatus => true;

    /// <summary>
    /// ��־�ؼ�
    /// </summary>
    protected override RichTextBox OutputTextBox => rtbMessage;

    #endregion

    #region ���캯��

    /// <summary>
    /// ���캯��
    /// </summary>
    public MiniForm()
    {
        InitializeComponent();
    }

    #endregion
}
