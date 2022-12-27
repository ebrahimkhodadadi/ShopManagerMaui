using MauiShopApp.ViewModel;

namespace MauiShopApp.View;

public partial class BasketPage : ContentPage
{
    BasketViewModel viewModel;

    public BasketPage(IConfiguration config)
    {
        InitializeComponent();

        BindingContext = viewModel = new BasketViewModel(Navigation, this, config);
        
        transferBtn.IsEnabled = false;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.GetBaksetList();
    }

    public void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        //var stepper = (Stepper)sender;

        viewModel.StepperProduct();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        viewModel.TransferBasket();
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (viewModel?.basketlist.Count >= 0 && viewModel.store != null)
            transferBtn.IsEnabled = viewModel.transferbtnenabled = true;
        else
            transferBtn.IsEnabled = viewModel.transferbtnenabled = false;
    }
}
