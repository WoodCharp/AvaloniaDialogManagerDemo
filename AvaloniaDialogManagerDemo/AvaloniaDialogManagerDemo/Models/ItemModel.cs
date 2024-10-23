using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AvaloniaDialogManagerDemo.Models
{
    public class ItemModel : ObservableObject
    {
        public string ID { get; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public double Amount { get; set; }
        public string MeasuringUnit { get; set; }

        public ItemModel(string name, double unitPrice, double amount, string measuringUnit)
        {
            ID = Guid.NewGuid().ToString();
            Name = name;
            UnitPrice = unitPrice;
            Amount = amount;
            MeasuringUnit = measuringUnit;
        }
    }
}