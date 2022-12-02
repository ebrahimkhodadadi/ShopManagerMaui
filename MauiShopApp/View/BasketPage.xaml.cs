using MauiShopApp.ViewModel;

namespace MauiShopApp.View;

public partial class BasketPage : ContentPage
{
    public BasketPage()
    {
        InitializeComponent();

        BindingContext = new BasketViewModel(Navigation, this);
    }
}
