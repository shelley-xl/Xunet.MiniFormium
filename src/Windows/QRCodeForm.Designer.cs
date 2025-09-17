// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// 扫码窗体
/// </summary>
partial class QRCodeForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(QRCodeForm));
        pbQRCode = new PictureBox();
        lblMessage = new Label();
        ((ISupportInitialize)pbQRCode).BeginInit();
        SuspendLayout();
        // 
        // pbQRCode
        // 
        pbQRCode.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        pbQRCode.Location = new Point(0, 0);
        pbQRCode.Name = "pbQRCode";
        pbQRCode.Size = new Size(300, 300);
        pbQRCode.SizeMode = PictureBoxSizeMode.StretchImage;
        pbQRCode.TabIndex = 0;
        pbQRCode.TabStop = false;
        // 
        // lblMessage
        // 
        lblMessage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblMessage.Font = new Font("Microsoft YaHei UI", 10F);
        lblMessage.ForeColor = Color.Gray;
        lblMessage.Location = new Point(0, 303);
        lblMessage.Name = "lblMessage";
        lblMessage.Size = new Size(300, 57);
        lblMessage.TabIndex = 1;
        lblMessage.Text = "用 [ 微信 ] 扫一扫";
        lblMessage.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // QRCodeForm
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(300, 360);
        Controls.Add(lblMessage);
        Controls.Add(pbQRCode);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        MinimumSize = new Size(136, 39);
        Name = "QRCodeForm";
        Text = "扫码窗体";
        Controls.SetChildIndex(pbQRCode, 0);
        Controls.SetChildIndex(lblMessage, 0);
        ((ISupportInitialize)pbQRCode).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox pbQRCode;
    private Label lblMessage;
}