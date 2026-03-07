using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using SmartPole.Inventory.MobileCore.ViewModels;

namespace SmartPole.Inventory.App.Services;

public class DialogService : IDialogService
{
    public Task ShowErrorAsync(string title, string message, string buttonText)
    {
        if (Application.Current?.MainPage != null)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, buttonText);
        }
        return Task.CompletedTask;
    }
}
