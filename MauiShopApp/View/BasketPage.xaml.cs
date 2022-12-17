using MauiShopApp.ViewModel;

namespace MauiShopApp.View;

public partial class BasketPage : ContentPage
{
    BasketViewModel viewModel;
    
    public BasketPage(IConfiguration config)
    {
        InitializeComponent();

        BindingContext = viewModel = new BasketViewModel(Navigation, this, config);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.GetBaksetList();
    }
}
