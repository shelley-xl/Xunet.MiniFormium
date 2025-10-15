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
    /// 窗体大小
    /// </summary>
    protected virtual Size WindowSize { get; } = Size.Empty;

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

    /// <summary>
    /// 工作频率（单位：秒），设置 0 时仅工作一次
    /// </summary>
    protected virtual int DoWorkInterval { get; } = 0;

    /// <summary>
    /// 日志控件
    /// </summary>
    protected virtual RichTextBox? OutputTextBox { get; }

    /// <summary>
    /// 显示菜单
    /// </summary>
    protected virtual bool ShowMenu { get; } = false;

    /// <summary>
    /// 显示状态栏
    /// </summary>
    protected virtual bool ShowStatus { get; } = false;

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
    /// 信号量
    /// </summary>
    private readonly static AsyncSemaphore AsyncSemaphore = new();

    /// <summary>
    /// 作业名称
    /// </summary>
    private string JobName => GetType()?.FullName ?? Guid.NewGuid().ToString();

    /// <summary>
    /// 接口应用
    /// </summary>
    private static WebApplication? WebApp
    {
        get
        {
            return DependencyResolver.Current?.GetService<WebApplication>();
        }
    }

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

    /// <summary>
    /// 窗体加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task OnLoadAsync(object sender, EventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// 窗体关闭
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task OnCloseAsync(object sender, FormClosingEventArgs e, CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// 任务执行
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task DoWorkAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// 任务取消
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected virtual Task DoCanceledAsync(OperationCanceledException ex) => Task.CompletedTask;

    /// <summary>
    /// 任务异常
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task DoExceptionAsync(Exception ex, CancellationToken cancellationToken) => Task.CompletedTask;

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
    /// <param name="forms"></param>
    protected Task ShowFormAsync(params Form[] forms)
    {
        InvokeOnUIThread(() =>
        {
            Hide();
            foreach (var form in forms)
            {
                form.Show();
                form.Activate();
            }
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

    /// <summary>
    /// 在UI线程上异步执行Action
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method"></param>
    /// <returns></returns>
    public T InvokeOnUIThread<T>(Func<T> method)
    {
        if (IsDisposed || TokenSource.IsCancellationRequested) return default!;

        if (InvokeRequired)
        {
            return Invoke(method);
        }

        return method.Invoke();
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

    #region 日志输出

    /// <summary>
    /// 日志输出
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    protected Task AppendBoxAsync(string text, Color? color = null)
    {
        if (OutputTextBox == null) return Task.CompletedTask;

        InvokeOnUIThread(() =>
        {
            if (OutputTextBox.Lines.Length > 0)
            {
                OutputTextBox.AppendText(Environment.NewLine);
            }
            ;
            OutputTextBox.SelectionStart = OutputTextBox.TextLength;
            OutputTextBox.SelectionLength = 0;
            OutputTextBox.SelectionColor = color ?? Color.Black;
            OutputTextBox.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}：{text}");
            OutputTextBox.SelectionColor = OutputTextBox.ForeColor;
            OutputTextBox.ScrollToCaret();
        });

        return Task.CompletedTask;
    }

    #endregion

    #region 创建随机数

    /// <summary>
    /// 创建随机数
    /// </summary>
    /// <param name="length">长度（默认：4位）</param>
    /// <returns></returns>
    protected static Task<string> CreateRandomNumberAsync(int length = 4)
    {
        if (length < 4) throw new ArgumentException("长度必须大于4", nameof(length));

        return Task.Run(() =>
        {
            // 定义字符集
            const string numbers1 = "123456789";
            const string numbers2 = "0123456789";

            // 使用加密学安全的随机数生成器
            using var generator = RandomNumberGenerator.Create();

            // 生成第一位数字，确保不以0开头
            byte[] randomBytes = new byte[1];
            generator.GetBytes(randomBytes);

            // 将字节转换为字符，使用取模确保索引在有效范围内
            var result1 = new string(randomBytes.Select(b => numbers1[b % numbers1.Length]).ToArray());

            // 生成后面数字，可以是任意数字
            randomBytes = new byte[length - 1];
            generator.GetBytes(randomBytes);

            // 将字节转换为字符，使用取模确保索引在有效范围内
            var result2 = new string(randomBytes.Select(b => numbers2[b % numbers2.Length]).ToArray());

            return result1 + result2;
        });
    }

    #endregion

    #region 创建随机字符串

    /// <summary>
    /// 创建随机字符串
    /// </summary>
    /// <param name="length">字符串长度（默认：8位）</param>
    /// <returns></returns>
    protected static Task<string> CreateRandomStringAsync(int length = 8)
    {
        return Task.Run(() =>
        {
            // 定义字符集
            const string numbers = "0123456789";
            const string lowerLetters = "abcdefghijklmnopqrstuvwxyz";
            const string upperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?/";

            // 合并所有字符集
            var allChars = numbers + lowerLetters + upperLetters + symbols;

            // 使用加密学安全的随机数生成器
            using var generator = RandomNumberGenerator.Create();

            // 生成指定字节随机数据
            byte[] randomBytes = new byte[length];
            generator.GetBytes(randomBytes);

            // 将字节转换为字符，使用取模确保索引在有效范围内
            var result = new string(randomBytes.Select(b => allChars[b % allChars.Length]).ToArray());

            return result;
        });
    }

    #endregion

    #region MD5加密

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="input">待加密字符串</param>
    /// <param name="toUpper">是否转换成大写（默认：小写）</param>
    /// <returns></returns>
    protected static Task<string> MD5EncryptAsync(string input, bool toUpper = false)
    {
        return Task.Run(() =>
        {
            var buffer = Encoding.UTF8.GetBytes(input);

            var MD5buffer = MD5.HashData(buffer);

            var builder = new StringBuilder();

            foreach (var item in MD5buffer)
            {
                if (toUpper) builder.Append(item.ToString("X2"));
                else builder.Append(item.ToString("x2"));
            }

            return builder.ToString();
        });
    }

    #endregion

    #endregion

    #region 私有方法

    #region 在线时长、内存占用情况显示

    private Task OnlineTimerAsync()
    {
        PerformanceCounter? counter = null;

        Task.Run(() =>
        {
            counter = new PerformanceCounter("Process", "Working Set - Private", Process.GetCurrentProcess().ProcessName);
        });

        Task.Run(async () =>
        {
            if (WebApp != null && WebApp.Urls.Count > 0)
            {
                InvokeOnUIThread(new Action(() =>
                {
                    var tsslStatus = tsStatus.Items[0];
                    var portLabel = new ToolStripStatusLabel
                    {
                        Text = $"监听端口：{string.Join(",", WebApp.Urls.Select(x => new Uri(x).Port))}",
                    };
                    tsStatus.Items.Clear();
                    tsStatus.Items.Add(portLabel);
                    tsStatus.Items.Add(new ToolStripSeparator());
                    tsStatus.Items.Add(tsslStatus);
                }));
            }

            var seconds = 0;

            while (!IsDisposed && !TokenSource.IsCancellationRequested)
            {
                var usedMemory = 0d;
                if (counter != null)
                {
                    usedMemory = Math.Round(counter.RawValue / 1024.0 / 1024.0, 1);
                }
                int hours = seconds / 3600;
                int minutes = seconds % 3600 / 60;
                int remainingSeconds = seconds % 3600 % 60;
                var nextRun = "未开启定时作业";

                if (JobManager.GetSchedule(JobName) is Schedule doWork)
                {
                    nextRun = $"下次执行还剩：{(int)(doWork.NextRun - DateTime.Now).TotalSeconds:00} 秒";
                }

                InvokeOnUIThread(new Action(() =>
                {
                    tsslStatus.Text = $"在线时长：{hours:00} 小时 {minutes:00} 分 {remainingSeconds:00} 秒，内存：{usedMemory:0.0} MB，{nextRun}";
                }));

                seconds++;

                await Task.Delay(1000);
            }
        });

        return Task.CompletedTask;
    }

    #endregion

    #region 添加周期作业

    private Task AddJobAsync()
    {
        JobManager.AddJob(DoWork, schedule =>
        {
            schedule.WithName(JobName);
            if (DoWorkInterval > 0)
            {
                schedule.ToRunNow().AndEvery(DoWorkInterval).Seconds();
                InvokeOnUIThread(() =>
                {
                    tsmiStop.Enabled = true;
                });
            }
            else
            {
                schedule.ToRunNow();
            }
        });

        return Task.CompletedTask;
    }

    private async void DoWork()
    {
        using (await AsyncSemaphore.WaitAsync())
        {
            try
            {
                InvokeOnUIThread(() =>
                {
                    tsmiExecute.Enabled = false;
                    tsmiCancel.Enabled = true;
                });
                await DoWorkAsync(TokenSource.Token);
            }
            catch (OperationCanceledException ex)
            {
                await DoCanceledAsync(ex);
            }
            catch (Exception ex)
            {
                await DoExceptionAsync(ex, TokenSource.Token);
            }
            finally
            {
                InvokeOnUIThread(() =>
                {
                    tsmiExecute.Enabled = true;
                    tsmiCancel.Enabled = false;
                });
            }
        }
    }

    #endregion

    #endregion

    #region 窗体加载事件

    private void BaseForm_Load(object sender, EventArgs e)
    {
        Text = Title;

        if (!WindowSize.IsEmpty)
        {
            ClientSize = WindowSize;
            MinimumSize = Size;
        }

        if (ShowTray)
        {
            notifyIcon.Visible = true;
            notifyIcon.Text = Title;
        }

        if (DisabledAboutForm)
        {
            msMenu.Items.Remove(tsmiAbout);
            cmsMenu.Items.Remove(tsmiAboutMe2);
        }

        if (ShowMenu)
        {
            msMenu.Visible = true;
        }

        if (ShowStatus)
        {
            tsStatus.Visible = true;
            Task.Run(OnlineTimerAsync);
        }

        Task.Run(AddJobAsync);

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

    #region 菜单事件

    #region 开启作业

    private void TsmiStart_Click(object sender, EventArgs e)
    {
        tsmiStart.Enabled = false;
        tsmiStop.Enabled = true;

        Task.Run(AddJobAsync);
    }

    #endregion

    #region 停止作业

    private void TsmiStop_Click(object sender, EventArgs e)
    {
        tsmiStart.Enabled = true;
        tsmiStop.Enabled = false;

        JobManager.RemoveJob(JobName);
    }

    #endregion

    #region 执行任务

    private void TsmiExecute_Click(object sender, EventArgs e)
    {
        Task.Run(DoWork);
    }

    #endregion

    #region 取消任务

    private void TsmiCancel_Click(object sender, EventArgs e)
    {
        TokenSource.Cancel();
        TokenSource = new CancellationTokenSource();
    }

    #endregion

    #region 导出日志

    private void TsmiExportLog_Click(object sender, EventArgs e)
    {
        var sfd = new SaveFileDialog
        {
            // 设置保存文件对话框的标题
            Title = "请选择要保存的文件路径",
            // 初始化保存目录，默认exe文件目录
            InitialDirectory = Application.StartupPath,
            // 设置保存文件的类型
            Filter = "日志文件|*.log",
            // 设置默认文件名
            FileName = $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.log"
        };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            // 获得保存文件的路径
            string filePath = sfd.FileName;
            // 保存
            using var writer = new StreamWriter(filePath);
            writer.Write(OutputTextBox?.Text);
            writer.Flush();
        }
    }

    #endregion

    #region 清空日志

    private void TsmiClearLog_Click(object sender, EventArgs e)
    {
        OutputTextBox?.Clear();
    }

    #endregion

    #region 关于软件

    private void TsmiAboutMe_Click(object sender, EventArgs e)
    {
        DependencyResolver.Current?.GetService<AboutForm>()?.ShowDialog();
    }

    #endregion

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
