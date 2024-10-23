using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AvaloniaDialogManagerDemo.ViewModels
{
    public abstract class DialogViewModelBase : ObservableObject
    {
        /// <summary>
        /// Button pressed method that is needed at DialogButtonsViewModel.cs
        /// </summary>
        /// <param name="button"></param>
        public abstract void ButtonPressed(string button);
        /// <summary>
        /// Action to close dialog window.
        /// </summary>
        public Action? CloseDialogAction { get; set; }

        private DialogResult? m_result;
        /// <summary>
        /// Dialog result. When result is set, dialog window closes.
        /// </summary>
        public DialogResult Result
        {
            get
            {
                if(m_result == null)
                {
                    m_result = new DialogResult("None");
                    return m_result;
                }

                return m_result;
            }
            set
            {
                m_result = value;
                CloseDialogAction?.Invoke();
            }
        }
    
    }
}