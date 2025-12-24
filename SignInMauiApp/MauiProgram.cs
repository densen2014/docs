// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using FreeSql;
using Microsoft.Extensions.Logging;
using System.Text;

namespace SignInMauiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
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

        // 注册 FreeSql ORM，使用 SQLite 数据库
        var fsql = new FreeSqlBuilder()
#if DEBUG
 .UseAutoSyncStructure(true)
#endif
          .UseConnectionString(DataType.Sqlite, "Data Source=signindb.db")
            .Build();
        builder.Services.AddSingleton(fsql);

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
