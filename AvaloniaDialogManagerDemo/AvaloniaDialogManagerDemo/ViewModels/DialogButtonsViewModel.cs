using Avalonia.Controls;
using Avalonia.Layout;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaDialogManagerDemo.ViewModels
{
    public partial class DialogButtonsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private StackPanel sPanel;

        public DialogButtonsViewModel(DialogViewModelBase dialogVM, string[] buttons)
        {
            sPanel = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Orientation = Orientation.Horizontal
            };

            var method = typeof(DialogViewModelBase).GetMethod("ButtonPressed");

            foreach (var button in buttons)
            {
                var btn = GetButton(button);
                btn.Click += (s, e) => { method?.Invoke(dialogVM, new object[] { button }); };

                sPanel.Children.Add(btn);
            }
        }

        private Button GetButton(string txt)
        {
            var b = new Button()
            {
                Content = txt,
                Margin = new Avalonia.Thickness(5)
            };

            return b;
        }
    }
}