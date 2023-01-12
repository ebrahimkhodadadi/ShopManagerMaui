using MauiShopApp.ViewModel;

namespace MauiShopApp.View;

public partial class UserPage : ContentPage
{
    public UserPage()
    {
        InitializeComponent();

        BindingContext = new UserViewModel(Navigation, this);
    }
}
