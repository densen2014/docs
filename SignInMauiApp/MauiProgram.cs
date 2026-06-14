// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using FreeSql;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text;

namespace SignInMauiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var cultureInfo = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        AppContext.SetSwitch("System.Reflection.NullabilityInfoContext.IsSupported", true);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });



#if ANDROID || IOS || MACCATALYST
        string dbpath = Path.Combine(FileSystem.AppDataDirectory, "signindb.db"); 
        Microsoft.Data.Sqlite.SqliteConnection _database = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={dbpath}");
        var fsql = new FreeSql.FreeSqlBuilder()
            .UseConnectionFactory(FreeSql.DataType.Sqlite, () => _database, typeof(FreeSql.Sqlite.SqliteProvider<>))
            .UseNoneCommandParameter(true)
            .Build();
        builder.Services.AddSingleton(fsql);
#else
        var fsql = new FreeSqlBuilder()
            .UseConnectionString(DataType.Sqlite, "Data Source=signindb.db")
            .UseNoneCommandParameter(true)
            .Build();
        builder.Services.AddSingleton(fsql);
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
