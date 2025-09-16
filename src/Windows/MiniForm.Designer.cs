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
        msMenu = new MenuStrip();
        tsmiWork = new ToolStripMenuItem();
        tsmiStart = new ToolStripMenuItem();
        tsmiStop = new ToolStripMenuItem();
        tsmiTask = new ToolStripMenuItem();
        tsmiExecute = new ToolStripMenuItem();
        tsmiCancel = new ToolStripMenuItem();
        tsmiLog = new ToolStripMenuItem();
        tsmiExportLog = new ToolStripMenuItem();
        tsmiClearLog = new ToolStripMenuItem();
        tsmiAbout = new ToolStripMenuItem();
        tsmiAboutMe = new ToolStripMenuItem();
        tsStatus = new StatusStrip();
        tsslStatus = new ToolStripStatusLabel();
        rtbMessage = new RichTextBox();
        msMenu.SuspendLayout();
        tsStatus.SuspendLayout();
        SuspendLayout();
        // 
        // msMenu
        // 
        msMenu.Items.AddRange(new ToolStripItem[] { tsmiWork, tsmiTask, tsmiLog, tsmiAbout });
        msMenu.Location = new Point(0, 0);
        msMenu.Name = "msMenu";
        msMenu.Size = new Size(584, 25);
        msMenu.TabIndex = 0;
        msMenu.Text = "菜单";
        // 
        // tsmiWork
        // 
        tsmiWork.DropDownItems.AddRange(new ToolStripItem[] { tsmiStart, tsmiStop });
        tsmiWork.Name = "tsmiWork";
        tsmiWork.Size = new Size(44, 21);
        tsmiWork.Text = "作业";
        // 
        // tsmiStart
        // 
        tsmiStart.Enabled = false;
        tsmiStart.Name = "tsmiStart";
        tsmiStart.Size = new Size(100, 22);
        tsmiStart.Text = "开启";
        tsmiStart.Click += TsmiStart_Click;
        // 
        // tsmiStop
        // 
        tsmiStop.Enabled = false;
        tsmiStop.Name = "tsmiStop";
        tsmiStop.Size = new Size(100, 22);
        tsmiStop.Text = "停止";
        tsmiStop.Click += TsmiStop_Click;
        // 
        // tsmiTask
        // 
        tsmiTask.DropDownItems.AddRange(new ToolStripItem[] { tsmiExecute, tsmiCancel });
        tsmiTask.Name = "tsmiTask";
        tsmiTask.Size = new Size(44, 21);
        tsmiTask.Text = "任务";
        // 
        // tsmiExecute
        // 
        tsmiExecute.Enabled = false;
        tsmiExecute.Name = "tsmiExecute";
        tsmiExecute.Size = new Size(100, 22);
        tsmiExecute.Text = "运行";
        tsmiExecute.Click += TsmiExecute_Click;
        // 
        // tsmiCancel
        // 
        tsmiCancel.Enabled = false;
        tsmiCancel.Name = "tsmiCancel";
        tsmiCancel.Size = new Size(100, 22);
        tsmiCancel.Text = "取消";
        tsmiCancel.Click += TsmiCancel_Click;
        // 
        // tsmiLog
        // 
        tsmiLog.DropDownItems.AddRange(new ToolStripItem[] { tsmiExportLog, tsmiClearLog });
        tsmiLog.Name = "tsmiLog";
        tsmiLog.Size = new Size(44, 21);
        tsmiLog.Text = "日志";
        // 
        // tsmiExportLog
        // 
        tsmiExportLog.Name = "tsmiExportLog";
        tsmiExportLog.Size = new Size(100, 22);
        tsmiExportLog.Text = "导出";
        tsmiExportLog.Click += TsmiExportLog_Click;
        // 
        // tsmiClearLog
        // 
        tsmiClearLog.Name = "tsmiClearLog";
        tsmiClearLog.Size = new Size(100, 22);
        tsmiClearLog.Text = "清空";
        tsmiClearLog.Click += TsmiClearLog_Click;
        // 
        // tsmiAbout
        // 
        tsmiAbout.DropDownItems.AddRange(new ToolStripItem[] { tsmiAboutMe });
        tsmiAbout.Name = "tsmiAbout";
        tsmiAbout.Size = new Size(44, 21);
        tsmiAbout.Text = "关于";
        // 
        // tsmiAboutMe
        // 
        tsmiAboutMe.Name = "tsmiAboutMe";
        tsmiAboutMe.Size = new Size(124, 22);
        tsmiAboutMe.Text = "关于软件";
        tsmiAboutMe.Click += TsmiAboutMe_Click;
        // 
        // tsStatus
        // 
        tsStatus.Items.AddRange(new ToolStripItem[] { tsslStatus });
        tsStatus.Location = new Point(0, 339);
        tsStatus.Name = "tsStatus";
        tsStatus.Size = new Size(584, 22);
        tsStatus.TabIndex = 1;
        tsStatus.Text = "状态栏";
        // 
        // tsslStatus
        // 
        tsslStatus.Name = "tsslStatus";
        tsslStatus.Size = new Size(105, 17);
        tsslStatus.Text = "加载中，请稍后 ...";
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
        Controls.Add(tsStatus);
        Controls.Add(msMenu);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MainMenuStrip = msMenu;
        Name = "MiniForm";
        msMenu.ResumeLayout(false);
        msMenu.PerformLayout();
        tsStatus.ResumeLayout(false);
        tsStatus.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip msMenu;
    private ToolStripMenuItem tsmiWork;
    private ToolStripMenuItem tsmiStart;
    private ToolStripMenuItem tsmiStop;
    private StatusStrip tsStatus;
    private ToolStripStatusLabel tsslStatus;
    private RichTextBox rtbMessage;
    private ToolStripMenuItem tsmiTask;
    private ToolStripMenuItem tsmiLog;
    private ToolStripMenuItem tsmiAbout;
    private ToolStripMenuItem tsmiExecute;
    private ToolStripMenuItem tsmiCancel;
    private ToolStripMenuItem tsmiExportLog;
    private ToolStripMenuItem tsmiClearLog;
    private ToolStripMenuItem tsmiAboutMe;
}
