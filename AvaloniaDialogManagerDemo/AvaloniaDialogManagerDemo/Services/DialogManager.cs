using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaDialogManagerDemo.Core.Dialogs;
using AvaloniaDialogManagerDemo.ViewModels;
using AvaloniaDialogManagerDemo.Views;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaDialogManagerDemo.Services
{
    public static class DialogManager
    {
        public static async Task<DialogResult<TData>> ShowDialogAsync<TData>(
            object contentViewModel, string title, string[] buttons,
            DialogWindowSettings? settings = null, IDialogService? dialogService = null)
        {
            if (settings == null)
                settings = DialogWindowSettings.Fixed();

            var ownerWindow = GetTopMostWindow();

            var dialogWindow = new DialogWindow
            {
                Title = title,
                DataContext = contentViewModel,
                dialogService = dialogService
            };

            dialogWindow.ApplySettings(settings);
            dialogWindow.BuildButtons(buttons);

            await dialogWindow.ShowDialog(ownerWindow);

            string pressedButton = dialogWindow.ClickedButtonText ?? "Cancel";
            var enumResult = MapStringToButtonResult(pressedButton);

            TData? data = default;
            if (contentViewModel is IDialogViewModel<TData> dialogVM && enumResult != DialogButtonResult.Cancel)
            {
                data = dialogVM.GetResultData();
            }

            return new DialogResult<TData>(
                result: enumResult,
                data: data,
                customButtonText: enumResult == DialogButtonResult.Custom ? pressedButton : null);
        }

        public static async Task<DialogButtonResult> ShowInfoAsync(string message, string[] buttons, string title = "Information")
        {
            var vm = new DialogInfoViewModel(message);

            var result = await ShowDialogAsync<object>(
                contentViewModel: vm,
                title: title,
                buttons: buttons
            );

            return result.Result;
        }

        private static DialogButtonResult MapStringToButtonResult(string text)
        {
            return text.ToLowerInvariant() switch
            {
                "ok" => DialogButtonResult.Ok,
                "cancel" => DialogButtonResult.Cancel,
                "yes" => DialogButtonResult.Yes,
                "no" => DialogButtonResult.No,
                _ => DialogButtonResult.Custom
            };
        }

        private static Window GetTopMostWindow()
        {
            var desktop = Avalonia.Application.Current?.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime;

            if (desktop == null)
                throw new InvalidOperationException("Desktop application lifetime not found.");

            var activeWindow = desktop.Windows.FirstOrDefault(w => w.IsActive)
                               ?? desktop.Windows.LastOrDefault()
                               ?? desktop.MainWindow;

            return activeWindow ?? throw new InvalidOperationException("No active owner window found.");
        }
    }
}