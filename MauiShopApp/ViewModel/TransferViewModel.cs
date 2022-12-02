
namespace MauiShopApp.ViewModel;

public partial class TransferViewModel : BaseViewModel
{
    private INavigation _navigationService;
    private Page _pageService;
    public TransferViewModel(INavigation navigationService, Page pageService)
    {
        _navigationService = navigationService;
        _pageService = pageService;
    }
}
