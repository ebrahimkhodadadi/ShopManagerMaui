using MauiShopApp.ViewModel;

namespace MauiShopApp.View;

public partial class TransgerPage : ContentPage
{
    public TransgerPage()
    {
        InitializeComponent();

        BindingContext = new TransferViewModel(Navigation, this);
    }
}
