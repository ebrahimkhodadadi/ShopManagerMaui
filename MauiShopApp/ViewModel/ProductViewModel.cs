
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiShopApp.View;
using System.Diagnostics;

namespace MauiShopApp.ViewModel;

public partial class ProductViewModel : BaseViewModel
{
    private INavigation _navigationService;
    private Page _pageService;
    private ApiService apiService;
    IConfiguration configuration;


    [ObservableProperty]
    private bool isEmpty = true;

    partial void OnIsEmptyChanged(bool value) =>
    IsEmptyChanged?.Invoke(this, new EventArgs());
    public event EventHandler IsEmptyChanged;
    
    public Stores store { get; set; }

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
    public async void GetProductList()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            ProductLists.Clear();

            if (store == null)
                return;

            var urlApi = await ReadJsonFile.ReadJson<Settings>();
            var productLists = await apiService.SendRequestAsync<List<Products>>(urlApi.ApiUrl + $"Product/GetAll?storeID={store.Id}&userID=1", null, HttpMethod.Get);

            if (productLists == null)
                return;
            if (!productLists.Any())
                return;

            foreach (var product in productLists)
            {
                ProductLists.Add(new Item()
                {
                    Id = product.Id,
                    Title = product.Name,
                    Image = product.Image ??= "image_not_found.png",
                });
            }

            isEmpty = ProductLists.Any() ? false : true;
            OnIsEmptyChanged(isEmpty);
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
            var list = await apiService.SendRequestAsync<List<Stores>>(urlApi.ApiUrl + "Store/GetAll", null, HttpMethod.Get);

            if (!list.Any())
                return;

            foreach (var item in list)
            {
                StoreList.Add(item);
            }

            isEmpty = ProductLists.Any() ? false : true;
            OnIsEmptyChanged(isEmpty);
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

    [RelayCommand]
    public async void AddProductToBasket(Item product)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var urlApi = await ReadJsonFile.ReadJson<Settings>();
            var result = await apiService.SendRequestAsync<bool>(urlApi.ApiUrl + $"Product/AddToBasket?storeID={store.Id}&productId={product.Id}&userID=1", null, HttpMethod.Get);

            if (result)
            {
                await _pageService.DisplayAlert("آماده برای انتقال!", "محصول به سبد انتقالات اضافه شد", "باشه");

                await _navigationService.PushAsync(new BasketPage(configuration));
            }
            else
            {
                await _pageService.DisplayAlert("خطا!", "انتقال محصول با خطا مواجه شد", "Ok");
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            await Shell.Current.DisplayAlert("Error!",
                $"Decription: {e.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
