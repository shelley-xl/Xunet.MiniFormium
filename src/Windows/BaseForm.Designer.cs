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
        cmsMenu.SuspendLayout();
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
        // BaseForm
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(384, 361);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "BaseForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "基础窗体";
        FormClosing += BaseForm_FormClosing;
        Load += BaseForm_Load;
        cmsMenu.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private ContextMenuStrip cmsMenu;
    private ToolStripMenuItem tsmiMainForm;
    private ToolStripSeparator tss1;
    private ToolStripMenuItem tsmiAboutMe2;
    private ToolStripMenuItem tsmiExit;
    private NotifyIcon notifyIcon;
}