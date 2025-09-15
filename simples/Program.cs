// THIS FILE IS PART OF Xunet.MiniFormium PROJECT
// THE Xunet.MiniFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) –Ï¿¥ ALL RIGHTS RESERVED.
// GITHUB: https://github.com/shelley-xl/Xunet.MiniFormium

namespace Xunet.MiniFormium.Simples;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        var builder = MiniFormiumApplication.CreateBuilder();

        builder.Services.AddMiniFormium<LoginForm>(options =>
        {
            options.Headers = new()
            {
                {
                    HeaderNames.UserAgent, "Mozilla/5.0 (iPhone; CPU iPhone OS 6_1_3 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Mobile/10B329 MicroMessenger/5.0.1"
                }
            };
            options.Storage = new()
            {
                DataVersion = "25.9.15.1036",
                DbName = "Xunet.MiniFormium.Simples",
                EntityTypes = [],
            };
            options.Snowflake = new()
            {
                WorkerId = 1,
                DataCenterId = 1,
            };
        });

        builder.Services.AddWebApi((provider, services) =>
        {
            var db = provider.GetRequiredService<ISqlSugarClient>();

            services.AddSingleton(db);
        });

        var app = builder.Build();

        app.UseMiniFormium();

        app.UseSingleApp();

        app.UseWebApi();

        app.Run();
    }
}
