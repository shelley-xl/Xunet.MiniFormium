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
                    HeaderNames.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36"
                },
                { HeaderNames.Cookie,"_qddaz=QD.379023527533497; pgv_pvid=8449783680; sensorsdata2015jssdkcross=%7B%22distinct_id%22%3A%22198bb12b9416d6-012006be0d10395-26011051-2073600-198bb12b9421173%22%2C%22first_id%22%3A%22%22%2C%22props%22%3A%7B%7D%2C%22identities%22%3A%22eyIkaWRlbnRpdHlfY29va2llX2lkIjoiMTk4YmIxMmI5NDE2ZDYtMDEyMDA2YmUwZDEwMzk1LTI2MDExMDUxLTIwNzM2MDAtMTk4YmIxMmI5NDIxMTczIn0%3D%22%2C%22history_login_id%22%3A%7B%22name%22%3A%22%22%2C%22value%22%3A%22%22%7D%7D; _gcl_au=1.1.714981096.1755485224; RK=REEVBtbPaH; ptcz=e7b12979be2dffb8cf813d7f001fcb9dd52c5813bb1c83c381278dc91f57b491; _hp2_id.1405110977=%7B%22userId%22%3A%226342771205353419%22%2C%22pageviewId%22%3A%224467450972837658%22%2C%22sessionId%22%3A%228553768329080723%22%2C%22identity%22%3Anull%2C%22trackerVersion%22%3A%224.0%22%7D; fs_uid=#o-1C2DZG-na1#c9f2a069-cdaa-4bf0-aa3f-aa1eb0375d08:ffe6a7ec-c7a4-4701-a1bd-986b45b9ec9c:1756977266774::1#/1788513268; _clck=3915660984|1|fz1|0; mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; refreshTimes=2" }
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
