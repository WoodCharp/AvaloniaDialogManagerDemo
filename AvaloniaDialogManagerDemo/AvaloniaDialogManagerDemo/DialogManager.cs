using Avalonia.Controls;
using AvaloniaDialogManagerDemo.ViewModels;
using AvaloniaDialogManagerDemo.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaloniaDialogManagerDemo
{
    public class DialogManager
    {
        private static Window? _mainWindow;
        private static List<DialogWindow> _dialogWindows = new();
        private static List<DialogWindowViewModel> _dialogVMs = new();

        /// <summary>
        /// Set _mainWindow at MainWindow.axaml.cs
        /// </summary>
        /// <param name="window">First dialog owner</param>
        public static void SetMainWindow(Window window)
        { 
            _mainWindow = window;
        }

        /// <summary>
        /// This is called when current dialog windows result has been set.
        /// </summary>
        private static void CloseDialog()
        {
            _dialogWindows[_dialogWindows.Count - 1].Close();
            _dialogWindows.RemoveAt(_dialogWindows.Count - 1);
            _dialogVMs.RemoveAt(_dialogVMs.Count - 1);
        }

        /// <summary>
        /// Show dialog
        /// </summary>
        /// <param name="dialogVM">Dialog view model</param>
        /// <param name="title">Dialog title</param>
        /// <param name="buttons">"Which buttons to display in dialog</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<DialogResult> ShowDialog(DialogViewModelBase dialogVM, string title, string[] buttons)
        {
            if (_mainWindow == null)
            {
                throw new ArgumentException("_mainWindow cannot be null");
            }

            //Set action for closing the current dialog window
            dialogVM.CloseDialogAction = CloseDialog;

            //Create VMs for the dialog
            var buttonsVM = new DialogButtonsViewModel(dialogVM, buttons);
            var windowVM = new DialogWindowViewModel(title, dialogVM, buttonsVM);

            //Add windowVM and DialogWindow to lists
            _dialogVMs.Add(windowVM);
            _dialogWindows.Add(new DialogWindow() { DataContext = windowVM });            

            //Wait for the dialog result
            await _dialogWindows[_dialogWindows.Count - 1].ShowDialog(_dialogWindows.Count > 1 ? _dialogWindows[_dialogWindows.Count - 2] : _mainWindow);

            //Return dialog result
            return _dialogVMs[_dialogVMs.Count - 1].DialogViewModel.Result;
        }
    }
}