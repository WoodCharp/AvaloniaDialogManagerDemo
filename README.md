# Dialog manager demo in AvaloniaUI
My attempt to add dialogs or message boxes to my AvaloniaUI application project.

* MVVM
* Can show dialog within the dialog
* Create your own kind of dialog window and it's content

<img width="627" height="539" alt="AvaloniaDialogManagerDemo_D8eG5LQeZS" src="https://github.com/user-attachments/assets/ea84f5d9-701b-41e8-a668-415dc2457d12" />


## How it's used in the demo

```C#
[RelayCommand]
private async Task New()
{
    DialogResult<ItemModel> result = await _dialogService.ShowDialogAsync<ItemModel>(
        contentViewModel: new DialogItemViewModel(SelectedItem),
        title: "Create new item",
        new[] { "Ok", "Cancel" },
        DialogWindowSettings.ItemDialog(),
        dialogService: _dialogService);

    if(result.IsSuccess && result.Data != null)
    {
        Items.Add(result.Data);
    }
}
```
