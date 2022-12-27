using Common.API;
using MauiShopApp.View;
using Microsoft.Extensions.Configuration;
using System.Reflection;

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

            //#region App Settings

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            //#endregion

            builder.Services.AddSingleton<UserPage>();
            builder.Services.AddSingleton<TransferLoggingPage>();
            builder.Services.AddSingleton<BasketPage>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<ApiService>();
            
            return builder.Build();
        }
    }
}