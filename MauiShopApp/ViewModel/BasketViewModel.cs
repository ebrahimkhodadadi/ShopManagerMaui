

namespace MauiShopApp.ViewModel;

public partial class BasketViewModel : BaseViewModel
{
    private INavigation _navigationService;
    private Page _pageService;
    public BasketViewModel(INavigation navigationService, Page pageService)
    {
        _navigationService = navigationService;
        _pageService = pageService;
    }
}
