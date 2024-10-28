# Dialog manager demo in AvaloniaUI
My attempt to add dialogs or message boxes to my AvaloniaUI application project.

* MVVM
* Can show dialog within the dialog
* Create your own kind of dialog window and it's content

<img src="https://github.com/user-attachments/assets/ad5a21d6-64e0-4c5f-ae84-ba71bb43911e" width="385px" align="center">

## How it's used in the demo

```C#
using Avalonia.Collections;
using AvaloniaDialogManagerDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AvaloniaDialogManagerDemo.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ItemModel? selectedItem;
        [ObservableProperty]
        private AvaloniaList<ItemModel> items;
        [ObservableProperty]
        private bool isItemSelected = false;

        public MainWindowViewModel()
        {
            items = new AvaloniaList<ItemModel>();
        }

        partial void OnSelectedItemChanged(ItemModel? value)
        {
            IsItemSelected = value != null;
        }

        [RelayCommand]
        private async Task New()
        {
            var result = await DialogManager.ShowDialog(new DialogItemViewModel(null), "Create new item", ["Ok", "Cancel"]);
            if (result.ButtonPressed == "Ok" && result.ReturningObject != null)
            {
                Items.Add((ItemModel)result.ReturningObject);
            }
        }

        [RelayCommand]
        private async Task Edit()
        {
            if (SelectedItem == null) return;

            var result = await DialogManager.ShowDialog(new DialogItemViewModel(SelectedItem), "Edit item", ["Ok", "Cancel"]);

            string oldId = SelectedItem.ID;

            if (result.ButtonPressed == "Ok" && result.ReturningObject != null)
            {
                for(int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].ID == oldId)
                    {
                        Items.RemoveAt(i);
                        Items.Insert(i, (ItemModel)result.ReturningObject);
                        SelectedItem = (ItemModel)result.ReturningObject;
                        break;
                    }
                }
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            if (SelectedItem == null) return;

            var result = await DialogManager.ShowDialog(new DialogMessageViewModel($"Delete {SelectedItem.Name}?"), "Delete item", ["Ok", "Cancel"]);

            if(result.ButtonPressed == "Ok")
            {
                Items.Remove(SelectedItem);
                SelectedItem = null;
            }
        }
    }
}
```
