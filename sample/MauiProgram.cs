using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.Maui.LifecycleEvents;

namespace sample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                    events.AddAndroid(android =>
                    {
                        android.OnCreate((activity, bundle) =>
                        {
                            var window = activity.Window;
                            window.SetStatusBarColor( Android.Graphics.Color.ParseColor("#0466cf"));
                        });
                    });
#endif
            }).UseMauiCommunityToolkit();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<App>();
            var app = builder.Build();

            var appInstance = app.Services.GetService<App>();
            if (appInstance != null)
            {
                appInstance.PreloadCalendarPage(app.Services);
            }
            else
            {
                Console.WriteLine("App instance is null");
            }

            return app;
        }
    }
}