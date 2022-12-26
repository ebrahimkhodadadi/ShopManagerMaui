
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Entities;
using System.Diagnostics;

namespace MauiShopApp.ViewModel;

//[QueryProperty("BasketList", "BasketList")]
public partial class BasketViewModel : BaseViewModel
{
    private INavigation _navigationService;
    private Page _pageService;

    private ApiService apiService;
    IConfiguration configuration;

    public Stores store { get; set; }
    public string Description { get; set; }

    public ObservableCollection<Basket> basketlist { get; set; } = new();
    public ObservableCollection<Stores> StoreList { get; set; } = new();

    [ObservableProperty]
    public bool transferbtnenabled;

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

            basketlist.Clear();

            var urlApi = await ReadJsonFile.ReadJson<Settings>();
            var baksetLists = await apiService.SendRequestAsync<List<BasketListDto>>(urlApi.ApiUrl + "Basket/GetAll/1", null, HttpMethod.Get);

            if (baksetLists == null)
                return;
            if (!baksetLists.Any())
                return;

            foreach (var basket in baksetLists)
            {
                basket.Image ??= "image_not_found.png";
                basketlist.Add(new Basket()
                {
                    Id = basket.Id,
                    Count = basket.BasketCount,
                    UserId = basket.UserId,
                    StoredProductId = basket.StoreProductID,
                    StoredProduct = new StoredProducts()
                    {
                        Id = basket.StoreProductID,
                        Count = basket.StoredProductCount,
                        ProductId = basket.ProductId,
                        StoreId = basket.StoreID,
                        Product = new Products()
                        {
                            Id = basket.ProductId,
                            Name = $"{basket.Name} تعداد موجود {basket.StoredProductCount}",
                            Image = basket.Image
                        }
                    }
                });
            }

            GetStoreList();
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

    public async void StepperProduct()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            var urlApi = await ReadJsonFile.ReadJson<Settings>();
            var baksetLists = await apiService.SendRequestAsync<List<BasketListDto>>(
                urlApi.ApiUrl + "Basket/UpdateBasket",
                basketlist.Select(x => new BasketDto { Id = x.Id, Count = x.Count, UserId = x.UserId, StoreProductID = x.StoredProductId }),
                HttpMethod.Post);

            basketlist.Clear();
            foreach (var basket in baksetLists)
            {
                basket.Image ??= "image_not_found.png";
                basketlist.Add(new Basket()
                {
                    Id = basket.Id,
                    Count = basket.BasketCount,
                    UserId = basket.UserId,
                    StoredProductId = basket.StoreProductID,
                    StoredProduct = new StoredProducts()
                    {
                        Id = basket.StoreProductID,
                        Count = basket.StoredProductCount,
                        ProductId = basket.ProductId,
                        StoreId = basket.StoreID,
                        Product = new Products()
                        {
                            Id = basket.ProductId,
                            Name = $"{basket.Name} تعداد موجود {basket.StoredProductCount}",
                            Image = basket.Image
                        }
                    }
                });
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            await Shell.Current.DisplayAlert("Error!",
                $"Detail: {e.Message}", "Ok");
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

            if(basketlist.Any())
                list.Remove(list.Single(x => x.Id == basketlist.First().StoredProduct.StoreId));

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

    public async void TransferBasket()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            var urlApi = await ReadJsonFile.ReadJson<Settings>();
            var result = await apiService.SendRequestAsync<bool>(
                urlApi.ApiUrl + "Transfer/TransferBasket",
                basketlist.Select(x => new TransferBasketDto { UserId = 1, Description = Description, DestinaionStore = store }),
                HttpMethod.Post);

            GetBaksetList();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            await Shell.Current.DisplayAlert("Error!",
                $"Detail: {e.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
