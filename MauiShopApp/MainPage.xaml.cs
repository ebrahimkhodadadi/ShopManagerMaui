using MauiShopApp.ViewModel;

namespace MauiShopApp;

public partial class MainPage : ContentPage
{
    ProductViewModel viewModel;
    
    public MainPage(IConfiguration config)
    {
        InitializeComponent();

        BindingContext = viewModel = new ProductViewModel(Navigation, this, config);

        viewModel.GetStoreList();
    }

    public void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            int id = ((Stores)picker.ItemsSource[selectedIndex]).Id;
            viewModel.GetProductList(id);
        }
    }
}