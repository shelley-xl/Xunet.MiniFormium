// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

/// <summary>
/// WebView2窗体
/// </summary>
partial class WebView2Form
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
        splitContainer = new SplitContainer();
        webView2 = new WebView2();
        rtbMessage = new RichTextBox();
        ((ISupportInitialize)splitContainer).BeginInit();
        splitContainer.Panel1.SuspendLayout();
        splitContainer.Panel2.SuspendLayout();
        splitContainer.SuspendLayout();
        ((ISupportInitialize)webView2).BeginInit();
        SuspendLayout();
        // 
        // splitContainer
        // 
        splitContainer.Dock = DockStyle.Fill;
        splitContainer.Location = new Point(0, 25);
        splitContainer.Name = "splitContainer";
        // 
        // splitContainer.Panel1
        // 
        splitContainer.Panel1.Controls.Add(webView2);
        // 
        // splitContainer.Panel2
        // 
        splitContainer.Panel2.Controls.Add(rtbMessage);
        splitContainer.Size = new Size(1200, 553);
        splitContainer.SplitterDistance = 800;
        splitContainer.TabIndex = 4;
        // 
        // webView2
        // 
        webView2.AllowExternalDrop = true;
        webView2.BackColor = Color.White;
        webView2.CreationProperties = null;
        webView2.DefaultBackgroundColor = Color.White;
        webView2.Dock = DockStyle.Fill;
        webView2.Location = new Point(0, 0);
        webView2.Name = "webView2";
        webView2.Size = new Size(800, 553);
        webView2.TabIndex = 0;
        webView2.ZoomFactor = 1D;
        webView2.CoreWebView2InitializationCompleted += WebView2_CoreWebView2InitializationCompleted;
        webView2.NavigationCompleted += WebView2_NavigationCompleted;
        webView2.WebMessageReceived += WebView2_WebMessageReceived;
        // 
        // rtbMessage
        // 
        rtbMessage.BackColor = Color.White;
        rtbMessage.BorderStyle = BorderStyle.None;
        rtbMessage.Dock = DockStyle.Fill;
        rtbMessage.Location = new Point(0, 0);
        rtbMessage.Name = "rtbMessage";
        rtbMessage.ReadOnly = true;
        rtbMessage.Size = new Size(396, 553);
        rtbMessage.TabIndex = 0;
        rtbMessage.Text = "";
        // 
        // WebView2Form
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1200, 700);
        Controls.Add(splitContainer);
        Name = "WebView2Form";
        Text = "WebView2窗体";
        Controls.SetChildIndex(splitContainer, 0);
        splitContainer.Panel1.ResumeLayout(false);
        splitContainer.Panel2.ResumeLayout(false);
        ((ISupportInitialize)splitContainer).EndInit();
        splitContainer.ResumeLayout(false);
        ((ISupportInitialize)webView2).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private SplitContainer splitContainer;
    private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
    private RichTextBox rtbMessage;
}