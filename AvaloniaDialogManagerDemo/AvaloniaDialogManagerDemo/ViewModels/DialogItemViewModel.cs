using AvaloniaDialogManagerDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace AvaloniaDialogManagerDemo.ViewModels
{
    public partial class DialogItemViewModel : DialogViewModelBase
    {
        [ObservableProperty]
        private string name = "";
        [ObservableProperty]
        private double unitPrice = 0;
        [ObservableProperty]
        private double amount = 0;
        [ObservableProperty]
        private string measuringUnit = "";


        public DialogItemViewModel(ItemModel? item)
        {
            if (item != null)
            {
                name = item.Name;
                unitPrice = item.UnitPrice;
                amount = item.Amount;
                measuringUnit = item.MeasuringUnit;
            }
        }

        public override void ButtonPressed(string button)
        {
            if(button != "Cancel" && string.IsNullOrEmpty(Name) ||
                button != "Cancel" && string.IsNullOrEmpty(MeasuringUnit))
            {
                _ = FillAllFieldsMessage();
                return;
            }

            Result = new DialogResult(button, new ItemModel(Name, UnitPrice, Amount, MeasuringUnit));
        }

        private async Task FillAllFieldsMessage()
        {
            await DialogManager.ShowDialog(new DialogMessageViewModel("Fill all fields"), "Item", ["Ok"]);
        }
    }
}