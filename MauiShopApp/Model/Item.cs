using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiShopApp.Model;

[INotifyPropertyChanged]
public partial class Item
{
    [ObservableProperty]
    int id;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    int quantity;

    [ObservableProperty]
    string image;

    [ObservableProperty]
    double price;

    partial void OnQuantityChanged(int value)
    {
        OnPropertyChanged(nameof(SubTotal));
    }


    public double SubTotal {
        get
        {
            return Price * Quantity;
        }
    }
}