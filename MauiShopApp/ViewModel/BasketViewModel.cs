

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace MauiShopApp.ViewModel;

public partial class BasketViewModel : BaseViewModel
{
    private INavigation _navigationService;
    private Page _pageService;

    private ApiService apiService;
    IConfiguration configuration;

    public ObservableCollection<Basket> BasketList { get; } = new();

    public BasketViewModel(INavigation navigationService, Page pageService, IConfiguration config)
    {
        _navigationService = navigationService;
        _pageService = pageService;

        apiService = new ApiService();
        configuration = config;
    }


    [RelayCommand]
    public async void GetBaksetList()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            var urlApi = await ReadJsonFile.ReadJson<Settings>();
            var baksetLists = await apiService.SendRequestAsync<List<Basket>>(urlApi.ApiUrl + "Basket/GetAll", string.Empty, HttpMethod.Get);

            if (baksetLists.Count != 0)
                baksetLists.Clear();

            foreach (var basket in baksetLists)
            {
                BasketList.Add(basket);
            }

        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            await Shell.Current.DisplayAlert("Error!",
                $"Unable to get ProductLists: {e.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
