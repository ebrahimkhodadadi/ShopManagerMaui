using MauiShopApp.View;

namespace MauiShopApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
            Routing.RegisterRoute(nameof(TransferLoggingPage), typeof(TransferLoggingPage));
            Routing.RegisterRoute(nameof(BasketPage), typeof(BasketPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}