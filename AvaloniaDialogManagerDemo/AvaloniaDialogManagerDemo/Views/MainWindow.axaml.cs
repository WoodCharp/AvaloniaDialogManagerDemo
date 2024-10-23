using Avalonia.Controls;

namespace AvaloniaDialogManagerDemo.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DialogManager.SetMainWindow(this);
        }
    }
}