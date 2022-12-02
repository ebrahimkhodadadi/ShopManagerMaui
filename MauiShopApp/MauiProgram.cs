using MauiShopApp.View;

namespace MauiShopApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<UserPage>();
            builder.Services.AddSingleton<TransferLoggingPage>();
            builder.Services.AddSingleton<BasketPage>();
            builder.Services.AddSingleton<TransgerPage>();
            
            return builder.Build();
        }
    }
}