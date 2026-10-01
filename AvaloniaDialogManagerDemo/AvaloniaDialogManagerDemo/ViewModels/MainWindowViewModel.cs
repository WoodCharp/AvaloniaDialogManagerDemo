using Avalonia.Collections;
using AvaloniaDialogManagerDemo.Core.Dialogs;
using AvaloniaDialogManagerDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AvaloniaDialogManagerDemo.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private ItemModel? selectedItem;
        [ObservableProperty]
        private AvaloniaList<ItemModel> items;
        [ObservableProperty]
        private bool isItemSelected = false;

        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            items = new AvaloniaList<ItemModel>();
        }
        partial void OnSelectedItemChanged(ItemModel? value)
        {
            IsItemSelected = value != null;
        }

        [RelayCommand]
        private async Task New()
        {
            DialogResult<ItemModel> result = await _dialogService.ShowDialogAsync<ItemModel>(
                contentViewModel: new DialogItemViewModel(SelectedItem),
                title: "Create new item",
                new[] { "Ok", "Cancel" },
                DialogWindowSettings.SizeToContent(),
                dialogService: _dialogService);

            if(result.IsSuccess && result.Data != null)
            {
                Items.Add(result.Data);
            }
        }

        [RelayCommand]
        private async Task Edit()
        {
            DialogResult<ItemModel> result = await _dialogService.ShowDialogAsync<ItemModel>(
                contentViewModel: new DialogItemViewModel(SelectedItem),
                title: "Edit item",
                new[] { "Ok", "Cancel" },
                DialogWindowSettings.SizeToContent());

            if (result.IsSuccess && result.Data != null)
            {
                for(int i = 0; i <  Items.Count; i++)
                {
                    if (Items[i].ID == result.Data.ID)
                    {
                        Items.RemoveAt(i);
                        Items.Insert(i, result.Data);
                        SelectedItem = Items[i];
                        break;
                    }
                }
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            if (SelectedItem == null) return;

            DialogButtonResult result = await _dialogService.ShowInfoAsync(
                message: $"Delete {SelectedItem.Name} ?",
                buttons: new[] { "Yes", "No" },
                title: "Delete item",
                settings: DialogWindowSettings.SizeToContent());

            if(result == DialogButtonResult.Yes)
            {
                Items.Remove(SelectedItem);
                SelectedItem = null;
            }
        }
    }
}