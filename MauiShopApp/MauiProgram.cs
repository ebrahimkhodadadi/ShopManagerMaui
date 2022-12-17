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

            //using var stream = Assembly.GetExecutingAssembly()
            //    .GetManifestResourceStream("MauiShopApp.appsettings.json");
            //var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
            //builder.Configuration.AddConfiguration(config);

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