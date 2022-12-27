
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Text;

namespace MauiShopApp.ViewModel;

public partial class TransferLoggingViewModel : BaseViewModel
{
    private INavigation _navigationService;
    private Page _pageService;
    
    private ApiService apiService;
    IConfiguration configuration;

    public ObservableCollection<TransportLogger> TransportLoggerList { get; set; } = new();
    
    public TransferLoggingViewModel(INavigation navigationService, Page pageService, IConfiguration config)
    {
        _navigationService = navigationService;
        _pageService = pageService;

        apiService = new ApiService();
        configuration = config;
    }

    public async void GetTransferList()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            TransportLoggerList.Clear();

            var urlApi = await ReadJsonFile.ReadJson<Settings>();
            var transferLoggers = await apiService.SendRequestAsync<List<TransportLogger>>(urlApi.ApiUrl + $"Transfer/GetAllTransfers/1", null, HttpMethod.Get);

            if (transferLoggers == null)
                return;
            if (!transferLoggers.Any())
                return;

            foreach (var transferlog in transferLoggers.OrderByDescending(x => x.Id))
            {
                TransportLoggerList.Add(transferlog);
            }

        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            await Shell.Current.DisplayAlert("Error!",
                $"Unable to get List: {e.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async void ShowDetail(TransportLogger transportLogger)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var urlApi = await ReadJsonFile.ReadJson<Settings>();
            var transferLoggers = await apiService.SendRequestAsync<List<KeyValues>>(urlApi.ApiUrl + $"Transfer/GetTransferDetails?userID=1&transferID={transportLogger.Id}", null, HttpMethod.Get);

            StringBuilder stringBuilder = new();
            
            foreach (var transferlog in transferLoggers)
            {
                stringBuilder.Append($"{transferlog.Key} : {transferlog.Value} {Environment.NewLine}");
            }

            await _pageService.DisplayAlert("Product Transfered Details", stringBuilder.ToString(), "Ok");
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            await Shell.Current.DisplayAlert("Error!",
                $"Unable to get List: {e.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
