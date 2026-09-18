using System.Threading.Tasks;

namespace AvaloniaDialogManagerDemo.Core.Dialogs
{
    public interface IDialogService
    {
        Task<DialogResult<TData>> ShowDialogAsync<TData>(
            object contentViewModel,
            string title,
            string[] buttons,
            DialogWindowSettings? settings = null,
            IDialogService? dialogService = null);

        Task<DialogButtonResult> ShowInfoAsync(
            string message,
            string title,
            string[] buttons,
            DialogWindowSettings? settings = null);
    }
}