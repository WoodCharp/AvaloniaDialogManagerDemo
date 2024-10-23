using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaDialogManagerDemo.ViewModels
{
    public partial class DialogWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string windowTitle;
        [ObservableProperty]
        private DialogViewModelBase dialogViewModel;
        [ObservableProperty]
        private DialogButtonsViewModel buttonsViewModel;

        public DialogWindowViewModel(string title, DialogViewModelBase dialogVM, DialogButtonsViewModel buttonsVM)
        {
            windowTitle = title;
            dialogViewModel = dialogVM;
            buttonsViewModel = buttonsVM;
        }
    }
}