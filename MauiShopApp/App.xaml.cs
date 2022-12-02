namespace MauiShopApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Application.Current.UserAppTheme = AppTheme.Light; // or AppTheme.Dark

            MainPage = new AppShell();
        }
    }
}