
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;

namespace MauiShopApp.ViewModel;

public partial class ProductViewModel : BaseViewModel
{
    private INavigation _navigationService;
    private Page _pageService;
    private ApiService apiService;
    IConfiguration configuration;
    public ObservableCollection<Item> ProductLists { get; } = new();

    public ProductViewModel(INavigation navigationService, Page pageService, IConfiguration config)
    {
        _navigationService = navigationService;
        _pageService = pageService;

        apiService = new ApiService();
        configuration = config;
    }

    [RelayCommand]
    public async void GetProductList()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var urlApi = await ReadJsonFile.ReadJson<Settings>();
            var productLists = await apiService.SendRequestAsync<List<Products>>(urlApi.ApiUrl + "Product/GetAll", string.Empty, HttpMethod.Get);

            if (ProductLists.Count != 0)
                ProductLists.Clear();

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
}
