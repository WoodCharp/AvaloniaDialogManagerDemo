using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaDialogManagerDemo.ViewModels
{
    public partial class DialogMessageViewModel : DialogViewModelBase
    {
        [ObservableProperty]
        private string message;

        public DialogMessageViewModel(string msg)
        {
            message = msg;
        }

        public override void ButtonPressed(string button)
        {
            Result = new DialogResult(button);
        }
    }
}