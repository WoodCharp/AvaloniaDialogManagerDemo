using AvaloniaDialogManagerDemo.Core.Dialogs;
using AvaloniaDialogManagerDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AvaloniaDialogManagerDemo.ViewModels
{
    public partial class DialogItemViewModel : ViewModelBase, IDialogViewModel<ItemModel>
    {
        [ObservableProperty]
        private string _name = "";
        [ObservableProperty]
        private double _unitPrice = 0;
        [ObservableProperty]
        private double _amount = 0;
        [ObservableProperty]
        private string _measuringUnit = "";

        private string _id = "";
        public DialogItemViewModel(ItemModel? item)
        {
            if (item != null)
            {
                _name = item.Name;
                _unitPrice = item.UnitPrice;
                _amount = item.Amount;
                _measuringUnit = item.MeasuringUnit;
                _id = item.ID;
            }
            else
                _id = Guid.NewGuid().ToString();
        }

        public ItemModel? GetResultData()
        {
            return new ItemModel(Name, UnitPrice, Amount, MeasuringUnit, _id);
        }

        public DialogValidation GetValidation()
        {
            if(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(MeasuringUnit))
            {
                return new DialogValidation(false, "Fill all fields");
            }

            return new DialogValidation(true);
        }
    }
}