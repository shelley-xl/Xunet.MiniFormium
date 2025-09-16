// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// 迷你窗体
/// </summary>
partial class MiniForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(MiniForm));
        rtbMessage = new RichTextBox();
        SuspendLayout();
        // 
        // rtbMessage
        // 
        rtbMessage.BackColor = Color.White;
        rtbMessage.BorderStyle = BorderStyle.None;
        rtbMessage.Dock = DockStyle.Fill;
        rtbMessage.Location = new Point(0, 25);
        rtbMessage.Name = "rtbMessage";
        rtbMessage.ReadOnly = true;
        rtbMessage.Size = new Size(584, 314);
        rtbMessage.TabIndex = 2;
        rtbMessage.Text = "";
        // 
        // MiniForm
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(584, 361);
        Controls.Add(rtbMessage);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "MiniForm";
        Text = "迷你窗体";
        Controls.SetChildIndex(rtbMessage, 0);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private RichTextBox rtbMessage;
}
