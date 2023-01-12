using MauiShopApp.ViewModel;
using System.ComponentModel;

namespace MauiShopApp;

public partial class MainPage : ContentPage
{
    ProductViewModel viewModel;

    public MainPage(IConfiguration config)
    {
        InitializeComponent();

        BindingContext = viewModel = new ProductViewModel(Navigation, this, config);
        viewModel.IsEmptyChanged += (s, e) => imgEmpty.IsVisible = viewModel.IsEmpty;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (viewModel.StoreList.Count <= 0)
            viewModel.GetStoreList();

        viewModel.GetProductList();
    }

    public void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        viewModel.GetProductList();
    }
}