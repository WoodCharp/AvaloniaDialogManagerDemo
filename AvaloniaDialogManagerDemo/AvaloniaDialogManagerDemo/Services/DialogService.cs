using AvaloniaDialogManagerDemo.Core.Dialogs;
using AvaloniaDialogManagerDemo.ViewModels;
using System.Threading.Tasks;

namespace AvaloniaDialogManagerDemo.Services
{
    public class DialogService : IDialogService
    {
        public async Task<DialogResult<TData>> ShowDialogAsync<TData>(object contentViewModel, string title, string[] buttons, DialogWindowSettings? settings = null, IDialogService? dialogService = null)
        {
            return await DialogManager.ShowDialogAsync<TData>(contentViewModel, title, buttons, settings, dialogService);
        }

        public async Task<DialogButtonResult> ShowInfoAsync(string message, string title, string[] buttons, DialogWindowSettings? settings = null)
        {
            var vm = new DialogInfoViewModel(message);
            var result = await ShowDialogAsync<object>(vm, title, buttons, settings);
            return result.Result;
        }
    }
}