using Microsoft.Maui.Controls;
using SmartPole.Inventory.MobileCore.ViewModels;

namespace SmartPole.Inventory.App.Views;

public partial class InventoryInspectionPage : ContentPage
{
    public InventoryInspectionPage(InventoryInspectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
