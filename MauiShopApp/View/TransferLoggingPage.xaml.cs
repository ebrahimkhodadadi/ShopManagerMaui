
using MauiShopApp.ViewModel;

namespace MauiShopApp.View;

public partial class TransferLoggingPage : ContentPage
{
    TransferLoggingViewModel viewModel;

    public TransferLoggingPage(IConfiguration config)
    {
        InitializeComponent();

        BindingContext = viewModel = new TransferLoggingViewModel(Navigation, this, config);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.GetTransferList();
    }
}
