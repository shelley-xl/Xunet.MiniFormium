// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

partial class AboutForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(AboutForm));
        pbLogo = new PictureBox();
        lblAbout = new Label();
        ((ISupportInitialize)pbLogo).BeginInit();
        SuspendLayout();
        // 
        // pbLogo
        // 
        pbLogo.Dock = DockStyle.Top;
        pbLogo.Image = (Image)resources.GetObject("pbLogo.Image");
        pbLogo.Location = new Point(0, 0);
        pbLogo.Name = "pbLogo";
        pbLogo.Size = new Size(284, 94);
        pbLogo.SizeMode = PictureBoxSizeMode.CenterImage;
        pbLogo.TabIndex = 0;
        pbLogo.TabStop = false;
        pbLogo.Click += NavigateUrl_Click;
        // 
        // lblAbout
        // 
        lblAbout.Dock = DockStyle.Fill;
        lblAbout.Location = new Point(0, 94);
        lblAbout.Name = "lblAbout";
        lblAbout.Size = new Size(284, 167);
        lblAbout.TabIndex = 1;
        lblAbout.Text = "关于软件";
        lblAbout.TextAlign = ContentAlignment.TopCenter;
        lblAbout.Click += NavigateUrl_Click;
        // 
        // AboutForm
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(284, 261);
        Controls.Add(lblAbout);
        Controls.Add(pbLogo);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "AboutForm";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "关于软件";
        Load += AboutForm_Load;
        ((ISupportInitialize)pbLogo).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pbLogo;
    private Label lblAbout;
}