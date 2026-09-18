using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaDialogManagerDemo.Core.Dialogs;
using AvaloniaDialogManagerDemo.Services;
using AvaloniaDialogManagerDemo.ViewModels;
using AvaloniaDialogManagerDemo.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaDialogManagerDemo
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<IDialogService, DialogService>();
            collection.AddTransient<MainWindowViewModel>();

            var services = collection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = services.GetRequiredService<MainWindowViewModel>()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}