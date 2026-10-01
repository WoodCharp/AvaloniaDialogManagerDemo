using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Interactivity;
using AvaloniaDialogManagerDemo.Core.Dialogs;
using System.Reflection;
using System.Threading.Tasks;

namespace AvaloniaDialogManagerDemo.Views
{
    public partial class DialogWindow : Window
    {
        public string? ClickedButtonText { get; private set; }
        private object? _vm;
        public IDialogService? dialogService;

        public DialogWindow()
        {
            InitializeComponent();

            DataContextChanged += (s, e) =>
            {
                _vm = DataContext;
            };
        }

        public void BuildButtons(string[] buttonLabels)
        {
            ButtonPanel.Children.Clear();

            if (buttonLabels.Length == 0)
            {
                ButtonPanel.IsVisible = false;
                return;
            }

            foreach (var label in buttonLabels)
            {
                var button = new Button
                {
                    Content = label,
                    MinWidth = 75,
                    Margin = new Thickness(5),
                    HorizontalContentAlignment = HorizontalAlignment.Center
                };

                if (DialogButton.IsPrimaryAction(label))
                {
                    button.IsDefault = true;
                }
                else if (DialogButton.IsCancelAction(label))
                {
                    button.IsCancel = true;
                }

                button.Click += OnDialogButtonClick;
                ButtonPanel.Children.Add(button);
            }
        }

        private async void OnDialogButtonClick(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Content is string text)
            {
                if (button.IsDefault && !await IsValid())
                    return;

                ClickedButtonText = text;
                Close();
            }
        }

        private async Task<bool> IsValid()
        {
            if (_vm == null)
                return true;

            MethodInfo? mi = _vm.GetType().GetMethod("GetValidation");

            if(mi != null)
            {
                object? result = mi.Invoke(_vm, null);
                if(result != null && result is DialogValidation)
                {
                    DialogValidation dv = (DialogValidation)result;
                    if (!dv.IsValid && dialogService != null)
                    {
                        await dialogService.ShowInfoAsync(dv.InvalidMessage ?? "InvalidMessage was not found.",
                            "Information", new[] { "Ok" }, DialogWindowSettings.SizeToContent());
                        return false;
                    }
                }
            }

            return true;
        }

        public void ApplySettings(DialogWindowSettings settings)
        {
            CanResize = settings.CanResize;
            ShowInTaskbar = settings.ShowInTaskBar;

            if (settings.Width.HasValue) Width = settings.Width.Value;
            if (settings.Height.HasValue) Height = settings.Height.Value;
            if (settings.MinWidth.HasValue) MinWidth = settings.MinWidth.Value;
            if (settings.MinHeight.HasValue) MinHeight = settings.MinHeight.Value;
            if (settings.MaxWidth.HasValue) MaxWidth = settings.MaxWidth.Value;
            if (settings.MaxHeight.HasValue) MaxHeight = settings.MaxHeight.Value;

            SizeToContent = settings.CanResize ? SizeToContent.Manual : SizeToContent.WidthAndHeight;
            CanMaximize = settings.CanResize;
            CanMinimize = settings.CanResize;
        }
    }
}