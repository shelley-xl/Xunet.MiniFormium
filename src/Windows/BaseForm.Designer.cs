// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// 基础窗体
/// </summary>
partial class BaseForm
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
        components = new Container();
        ComponentResourceManager resources = new ComponentResourceManager(typeof(BaseForm));
        cmsMenu = new ContextMenuStrip(components);
        tsmiMainForm = new ToolStripMenuItem();
        tss1 = new ToolStripSeparator();
        tsmiAboutMe2 = new ToolStripMenuItem();
        tsmiExit = new ToolStripMenuItem();
        notifyIcon = new NotifyIcon(components);
        tsStatus = new StatusStrip();
        tsslStatus = new ToolStripStatusLabel();
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
        cmsMenu.SuspendLayout();
        tsStatus.SuspendLayout();
        msMenu.SuspendLayout();
        SuspendLayout();
        // 
        // cmsMenu
        // 
        cmsMenu.Items.AddRange(new ToolStripItem[] { tsmiMainForm, tss1, tsmiAboutMe2, tsmiExit });
        cmsMenu.Name = "cmsMenu";
        cmsMenu.Size = new Size(113, 76);
        // 
        // tsmiMainForm
        // 
        tsmiMainForm.Name = "tsmiMainForm";
        tsmiMainForm.Size = new Size(112, 22);
        tsmiMainForm.Text = "主窗体";
        tsmiMainForm.Click += TsmiMainForm_Click;
        // 
        // tss1
        // 
        tss1.Name = "tss1";
        tss1.Size = new Size(109, 6);
        // 
        // tsmiAboutMe2
        // 
        tsmiAboutMe2.Name = "tsmiAboutMe2";
        tsmiAboutMe2.Size = new Size(112, 22);
        tsmiAboutMe2.Text = "关于";
        tsmiAboutMe2.Click += TsmiAboutMe2_Click;
        // 
        // tsmiExit
        // 
        tsmiExit.Name = "tsmiExit";
        tsmiExit.Size = new Size(112, 22);
        tsmiExit.Text = "退出";
        tsmiExit.Click += TsmiExit_Click;
        // 
        // notifyIcon
        // 
        notifyIcon.ContextMenuStrip = cmsMenu;
        notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
        notifyIcon.MouseClick += NotifyIcon_MouseClick;
        // 
        // tsStatus
        // 
        tsStatus.Items.AddRange(new ToolStripItem[] { tsslStatus });
        tsStatus.Location = new Point(0, 339);
        tsStatus.Name = "tsStatus";
        tsStatus.Size = new Size(584, 22);
        tsStatus.TabIndex = 3;
        tsStatus.Text = "状态栏";
        // 
        // tsslStatus
        // 
        tsslStatus.Name = "tsslStatus";
        tsslStatus.Size = new Size(105, 17);
        tsslStatus.Text = "加载中，请稍后 ...";
        // 
        // msMenu
        // 
        msMenu.Items.AddRange(new ToolStripItem[] { tsmiWork, tsmiTask, tsmiLog, tsmiAbout });
        msMenu.Location = new Point(0, 0);
        msMenu.Name = "msMenu";
        msMenu.Size = new Size(584, 25);
        msMenu.TabIndex = 2;
        msMenu.Text = "菜单";
        // 
        // tsmiWork
        // 
        tsmiWork.DropDownItems.AddRange(new ToolStripItem[] { tsmiStart, tsmiStop });
        tsmiWork.Name = "tsmiWork";
        tsmiWork.ShortcutKeyDisplayString = "";
        tsmiWork.Size = new Size(44, 21);
        tsmiWork.Text = "作业";
        // 
        // tsmiStart
        // 
        tsmiStart.Enabled = false;
        tsmiStart.Name = "tsmiStart";
        tsmiStart.Size = new Size(180, 22);
        tsmiStart.Text = "开启";
        tsmiStart.Click += TsmiStart_Click;
        // 
        // tsmiStop
        // 
        tsmiStop.Enabled = false;
        tsmiStop.Name = "tsmiStop";
        tsmiStop.Size = new Size(180, 22);
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
        // BaseForm
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(584, 361);
        Controls.Add(tsStatus);
        Controls.Add(msMenu);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "BaseForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "基础窗体";
        FormClosing += BaseForm_FormClosing;
        Load += BaseForm_Load;
        cmsMenu.ResumeLayout(false);
        tsStatus.ResumeLayout(false);
        tsStatus.PerformLayout();
        msMenu.ResumeLayout(false);
        msMenu.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ContextMenuStrip cmsMenu;
    private ToolStripMenuItem tsmiMainForm;
    private ToolStripSeparator tss1;
    private ToolStripMenuItem tsmiAboutMe2;
    private ToolStripMenuItem tsmiExit;
    private NotifyIcon notifyIcon;
    private ToolStripStatusLabel tsslStatus;
    private ToolStripMenuItem tsmiWork;
    private ToolStripMenuItem tsmiStart;
    private ToolStripMenuItem tsmiStop;
    private ToolStripMenuItem tsmiTask;
    private ToolStripMenuItem tsmiExecute;
    private ToolStripMenuItem tsmiCancel;
    private ToolStripMenuItem tsmiLog;
    private ToolStripMenuItem tsmiExportLog;
    private ToolStripMenuItem tsmiClearLog;
    private ToolStripMenuItem tsmiAbout;
    private ToolStripMenuItem tsmiAboutMe;

    /// <summary>
    /// msMenu
    /// </summary>
    protected internal MenuStrip msMenu;
    /// <summary>
    /// tsStatus
    /// </summary>
    protected internal StatusStrip tsStatus;
}