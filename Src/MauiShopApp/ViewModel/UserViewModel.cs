
namespace MauiShopApp.ViewModel;

public partial class UserViewModel : BaseViewModel
{
    private INavigation _navigationService;
    private Page _pageService;
    public UserViewModel(INavigation navigationService, Page pageService)
    {
        _navigationService = navigationService;
        _pageService = pageService;
    }
}
