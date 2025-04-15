using System.Globalization;

namespace sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("cs-CZ");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("cs-CZ");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        public void PreloadCalendarPage(IServiceProvider services)
        {
            Task.Run(async () =>
            {
                var calendarPage = services.GetService<MainPage>();
                if (calendarPage != null)
                {
                    // Preload calendar page
                    Application.Current.Dispatcher.DispatchAsync(() =>
                    {
                        var content = calendarPage.Content;
                    });
                }
            });
        }
    }
}