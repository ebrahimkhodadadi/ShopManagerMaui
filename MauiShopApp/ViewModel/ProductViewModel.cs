
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace MauiShopApp.ViewModel;

public partial class ProductViewModel : BaseViewModel
{
    private INavigation _navigationService;
    private Page _pageService;
    private ApiService apiService;
    IConfiguration configuration;

    public ObservableCollection<Item> ProductLists { get; } = new();
    public ObservableCollection<Stores> StoreList { get; set; } = new();

    public ProductViewModel(INavigation navigationService, Page pageService, IConfiguration config)
    {
        _navigationService = navigationService;
        _pageService = pageService;

        apiService = new ApiService();
        configuration = config;
    }

    [RelayCommand]
    public async void GetProductList(int storeID)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            ProductLists.Clear();

            var urlApi = await ReadJsonFile.ReadJson<Settings>();
            var productLists = await apiService.SendRequestAsync<List<Products>>(urlApi.ApiUrl + $"Product/GetAll/{storeID}", string.Empty, HttpMethod.Get);

            if (!productLists.Any())
                return;

            foreach (var product in productLists)
            {
                ProductLists.Add(new Item()
                {
                    Title = product.Name,
                    Image = product.Image ??= "image_not_found.png",
                });
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

    [RelayCommand]
    public async void GetStoreList()
    {
        try
        {
            StoreList.Clear();

            var urlApi = await ReadJsonFile.ReadJson<Settings>();
            var list = await apiService.SendRequestAsync<List<Stores>>(urlApi.ApiUrl + "Store/GetAll", string.Empty, HttpMethod.Get);

            if (!list.Any())
                return;

            foreach (var item in list)
            {
                StoreList.Add(item);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            await Shell.Current.DisplayAlert("Error!",
                $"Unable to get Store list: {e.Message}", "Ok");
        }
        finally
        {
        }
    }
}
