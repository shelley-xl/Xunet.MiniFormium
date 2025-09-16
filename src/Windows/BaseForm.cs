// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) 徐来 ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Windows;

using HtmlAgilityPack;
using Jint;

/// <summary>
/// 基础窗体
/// </summary>
public partial class BaseForm : Form, IMiniFormium
{
    #region 重写属性

    /// <summary>
    /// 窗体标题
    /// </summary>
    protected virtual string Title { get; } = "基础窗体";

    /// <summary>
    /// 显示系统托盘
    /// </summary>
    protected virtual bool ShowTray { get; } = false;

    /// <summary>
    /// 最小化到系统托盘
    /// </summary>
    protected virtual bool IsTray { get; } = false;

    /// <summary>
    /// 禁用关于窗体
    /// </summary>
    protected virtual bool DisabledAboutForm { get; } = false;

    #endregion

    #region 继承属性

    #region 取消令牌

    /// <summary>
    /// 取消令牌
    /// </summary>
    protected static CancellationTokenSource TokenSource { get; set; } = new();

    #endregion

    #region 版本号

    /// <summary>
    /// 版本号
    /// </summary>
    protected static string Version
    {
        get
        {
            return $"v{Assembly.GetEntryAssembly()?.GetName().Version}";
        }
    }

    #endregion

    #region SDK版本号

    /// <summary>
    /// SDK版本号
    /// </summary>
    protected static string SDKVersion
    {
        get
        {
            return $"v{Assembly.GetExecutingAssembly().GetName().Version}";
        }
    }

    #endregion

    #region 默认请求客户端

    /// <summary>
    /// 默认请求客户端
    /// </summary>
    protected static HttpClient DefaultClient
    {
        get
        {
            return DependencyResolver.Current?.GetRequiredService<IHttpClientFactory>()?.CreateClient("default") ?? throw new InvalidOperationException("No headers for type 'StartupOptions.RequestHeaders' has been initialized.");
        }
    }

    #endregion

    #region 数据库访问

    /// <summary>
    /// 数据库访问
    /// </summary>
    protected static ISqlSugarClient Db
    {
        get
        {
            return DependencyResolver.Current?.GetRequiredService<ISqlSugarClient>() ?? throw new InvalidOperationException("No storage for type 'StartupOptions.SqliteStorage' has been initialized.");
        }
    }

    #endregion

    #region 屏幕缩放比例

    /// <summary>
    /// 屏幕缩放比例
    /// </summary>
    protected static float ScalingFactor
    {
        get
        {
            return DpiHelper.GetDpiScalingFactor();
        }
    }

    #endregion

    #endregion

    #region 私有属性

    /// <summary>
    /// 配置
    /// </summary>
    private static IConfigurationRoot Configuration
    {
        get
        {
            return DependencyResolver.Current?.GetRequiredService<IConfigurationRoot>() ?? throw new InvalidOperationException("No service for type 'IConfigurationRoot' has been registered.");
        }
    }

    /// <summary>
    /// HTML文档
    /// </summary>
    private static HtmlDocument? HtmlDocument { get; set; }

    #endregion

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public BaseForm()
    {
        InitializeComponent();
    }

    #endregion

    #region 重写方法

    #region 窗体加载

    /// <summary>
    /// 窗体加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    #endregion

    #region 窗体关闭

    /// <summary>
    /// 窗体关闭
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task OnCloseAsync(object sender, FormClosingEventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    #endregion

    #endregion

    #region 继承方法

    #region 获取配置

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    protected static T? GetConfigValue<T>(string key)
    {
        return Configuration.GetSection(key).Get<T>();
    }

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected static string? GetConfigValue(string key)
    {
        return Configuration[key];
    }

    #endregion

    #region 序列化

    private static JsonSerializerOptions SerializerOptions(JsonNamingPolicy? namingPolicy = null, string dateFormat = "yyyy-MM-dd HH:mm:ss")
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = namingPolicy ?? JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
        };

        options.Converters.Add(new DateTimeJsonConverter(dateFormat));

        return options;
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="value">对象值</param>
    /// <param name="namingPolicy">命名策略</param>
    /// <param name="dateFormat">时间格式，默认：yyyy-MM-dd HH:mm:ss</param>
    /// <returns></returns>
    protected static string JsonSerializeObject(object? value, JsonNamingPolicy? namingPolicy = null, string dateFormat = "yyyy-MM-dd HH:mm:ss")
    {
        return JsonSerializer.Serialize(value, SerializerOptions(namingPolicy, dateFormat));
    }

    #endregion

    #region 反序列化

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json">JSON字符串</param>
    /// <param name="namingPolicy">命名策略</param>
    /// <param name="dateFormat">时间格式，默认：yyyy-MM-dd HH:mm:ss</param>
    /// <returns></returns>
    protected static T? JsonDeserializeObject<T>(string json, JsonNamingPolicy? namingPolicy = null, string dateFormat = "yyyy-MM-dd HH:mm:ss")
    {
        return JsonSerializer.Deserialize<T>(json, SerializerOptions(namingPolicy, dateFormat));
    }

    #endregion

    #region 创建雪花Id

    /// <summary>
    /// 创建雪花Id
    /// </summary>
    /// <returns></returns>
    protected static long CreateNextId()
    {
        return SnowFlakeSingle.Instance.NextId();
    }

    /// <summary>
    /// 创建雪花Id
    /// </summary>
    /// <returns></returns>
    protected static string CreateNextIdString()
    {
        return SnowFlakeSingle.Instance.NextId().ToString();
    }

    #endregion

    #region 打开窗体

    /// <summary>
    /// 打开窗体
    /// </summary>
    /// <param name="main"></param>
    protected Task ShowFormAsync(Form main)
    {
        InvokeOnUIThread(() =>
        {
            Hide();
            main.Show();
            main.Activate();
        });

        return Task.CompletedTask;
    }

    #endregion

    #region 在UI线程上异步执行Action

    /// <summary>
    /// 在UI线程上异步执行Action
    /// </summary>
    /// <param name="action"></param>
    protected void InvokeOnUIThread(Action action)
    {
        if (IsDisposed || TokenSource.IsCancellationRequested) return;

        if (InvokeRequired)
        {
            Invoke(new System.Windows.Forms.MethodInvoker(action));
        }
        else
        {
            action.Invoke();
        }
    }

    #endregion

    #region HtmlAgilityPack

    #region 创建HTML文档

    /// <summary>
    /// 创建HTML文档
    /// </summary>
    /// <param name="html"></param>
    protected static void CreateHtmlDocument(string html)
    {
        HtmlDocument = new HtmlDocument();

        HtmlDocument.LoadHtml(html);
    }

    #endregion

    #region 通过XPath查找单个元素

    /// <summary>
    /// 通过XPath查找单个元素
    /// </summary>
    /// <param name="xpath">xpath</param>
    /// <returns></returns>
    protected static object? FindElementByXPath(string xpath)
    {
        return HtmlDocument?.DocumentNode.SelectSingleNode(xpath);
    }

    #endregion

    #region 通过XPath查找集合元素

    /// <summary>
    /// 通过XPath查找集合元素
    /// </summary>
    /// <param name="xpath">xpath</param>
    /// <returns></returns>
    protected static List<object> FindElementsByXPath(string xpath)
    {
        return HtmlDocument?.DocumentNode.SelectNodes(xpath)?.ToList<object>() ?? [];
    }

    #endregion

    #region 通过XPath检查元素是否存在

    /// <summary>
    /// 通过XPath检查元素是否存在
    /// </summary>
    /// <param name="xpath">xpath</param>
    /// <returns></returns>
    protected static bool IsElementExist(string xpath)
    {
        return (HtmlDocument?.DocumentNode.SelectNodes(xpath)?.Count ?? 0) != 0;
    }

    #endregion

    #region 通过XPath查找指定元素对象的单个元素

    /// <summary>
    /// 通过XPath查找指定元素对象的单个元素
    /// </summary>
    /// <param name="element">元素对象</param>
    /// <param name="xpath">xpath</param>
    /// <returns></returns>
    protected static object? FindElementByXPath(object? element, string xpath)
    {
        return (element as HtmlNode)?.SelectSingleNode(xpath);
    }

    #endregion

    #region 通过XPath查找指定元素对象的集合元素

    /// <summary>
    /// 通过XPath查找指定元素对象的集合元素
    /// </summary>
    /// <param name="element">元素对象</param>
    /// <param name="xpath">xpath</param>
    /// <returns></returns>
    protected static List<object> FindElementsByXPath(object? element, string xpath)
    {
        return (element as HtmlNode)?.SelectNodes(xpath)?.ToList<object>() ?? [];
    }

    #endregion

    #region 通过XPath检查指定元素对象的元素是否存在

    /// <summary>
    /// 通过XPath检查指定元素对象的元素是否存在
    /// </summary>
    /// <param name="element">元素对象</param>
    /// <param name="xpath">xpath</param>
    /// <returns></returns>
    protected static bool IsElementExist(object? element, string xpath)
    {
        return ((element as HtmlNode)?.SelectNodes(xpath)?.Count ?? 0) != 0;
    }

    #endregion

    #region 查找元素对象的文本值

    /// <summary>
    /// 查找元素对象的文本值
    /// </summary>
    /// <param name="element">元素对象</param>
    /// <returns></returns>
    protected static string FindText(object? element)
    {
        return (element as HtmlNode)?.InnerText ?? string.Empty;
    }

    #endregion

    #region 查找元素对象的隐藏文本值

    /// <summary>
    /// 查找元素对象的隐藏文本值
    /// </summary>
    /// <param name="element">元素对象</param>
    /// <returns></returns>
    protected static string FindHiddenText(object? element)
    {
        return (element as HtmlNode)?.GetAttributeValue("textContent", "") ?? string.Empty;
    }

    #endregion

    #region 查找元素对象的属性值

    /// <summary>
    /// 查找元素对象的属性值
    /// </summary>
    /// <param name="element">元素对象</param>
    /// <param name="attribute">属性名称</param>
    /// <returns></returns>
    protected static string FindAttributeValue(object? element, string attribute)
    {
        return (element as HtmlNode)?.GetAttributeValue(attribute, "") ?? string.Empty;
    }

    #endregion

    #region 去首尾空格换行制表符

    /// <summary>
    /// 去首尾空格换行制表符
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    protected static string Trim(string text)
    {
        return text.Trim([' ', '\t', '\r', '\n']);
    }

    #endregion

    #endregion

    #region 执行JavaScript脚本

    /// <summary>
    /// 执行JavaScript脚本
    /// </summary>
    /// <param name="code"></param>
    /// <param name="method"></param>
    /// <param name="arguments"></param>
    protected static Task<object?> ExecuteJavaScriptAsync(string code, string method, params object?[] arguments)
    {
        return Task.Run(() =>
        {
            using var engine = new Engine();

            engine.Execute(code);

            var value = engine.Invoke(method, arguments);

            return value switch
            {
                Jint.Native.JsValue x when x.IsNumber() => x.AsNumber(),
                Jint.Native.JsValue x when x.IsString() => x.AsString(),
                Jint.Native.JsValue x when x.IsBoolean() => x.AsBoolean(),
                Jint.Native.JsValue x when x.IsDate() => x.AsDate(),
                Jint.Native.JsValue x when x.IsObject() => x.AsObject(),
                Jint.Native.JsValue x when x.IsArray() => x.AsArray(),
                _ => (object?)null,
            };
        });
    }

    #endregion

    #endregion

    #region 窗体加载事件

    private void BaseForm_Load(object sender, EventArgs e)
    {
        Text = Title;
        MinimumSize = Size;

        if (ShowTray)
        {
            notifyIcon.Visible = true;
            notifyIcon.Text = Title;
        }

        if (DisabledAboutForm)
        {
            cmsMenu.Items.Remove(tsmiAboutMe2);
        }

        OnLoadAsync(sender, e, TokenSource.Token);
    }

    #endregion

    #region 窗体关闭事件

    private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (IsTray)
        {
            Hide();
            e.Cancel = true;
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1000, Title, "程序已最小化到系统托盘", ToolTipIcon.Info);
        }

        OnCloseAsync(sender, e, TokenSource.Token);
    }

    #endregion

    #region 系统托盘事件

    #region 系统托盘

    private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            Show();
            Activate();
        }
    }

    #endregion

    #region 主窗体

    private void TsmiMainForm_Click(object sender, EventArgs e)
    {
        Show();
        Activate();
    }

    #endregion

    #region 关于

    private void TsmiAboutMe2_Click(object sender, EventArgs e)
    {
        DependencyResolver.Current?.GetService<AboutForm>()?.ShowDialog();
    }

    #endregion

    #region 退出

    private void TsmiExit_Click(object sender, EventArgs e)
    {
        TokenSource.Cancel();
        TokenSource = new CancellationTokenSource();
        notifyIcon.Dispose();
        Environment.Exit(0);
    }

    #endregion

    #endregion
}
