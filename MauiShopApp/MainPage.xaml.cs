using MauiShopApp.ViewModel;

namespace MauiShopApp;

public partial class MainPage : ContentPage
{
    ProductViewModel viewModel;
    
    public MainPage(IConfiguration config)
    {
        InitializeComponent();

        BindingContext = viewModel = new ProductViewModel(Navigation, this, config);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        viewModel.GetProductList();
    }
}