namespace MauiShopApp.ViewModel;

public partial class TransferLoggingViewModel : BaseViewModel
{
    private INavigation _navigationService;
    private Page _pageService;
    public TransferLoggingViewModel(INavigation navigationService, Page pageService)
    {
        _navigationService = navigationService;
        _pageService = pageService;
    }
}
