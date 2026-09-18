using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaDialogManagerDemo.ViewModels
{
    public partial class DialogInfoViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _message;
        public DialogInfoViewModel(string message)
        {
            _message = message;
        }
    }
}