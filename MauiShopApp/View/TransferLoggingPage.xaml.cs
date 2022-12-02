using MauiShopApp.ViewModel;

namespace MauiShopApp.View;

public partial class TransferLoggingPage : ContentPage
{
    public TransferLoggingPage()
    {
        InitializeComponent();

        BindingContext = new TransferLoggingViewModel(Navigation, this);
    }
}
