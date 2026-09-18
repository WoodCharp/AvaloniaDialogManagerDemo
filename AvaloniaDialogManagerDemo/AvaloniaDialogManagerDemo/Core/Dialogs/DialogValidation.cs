namespace AvaloniaDialogManagerDemo.Core.Dialogs
{
    public class DialogValidation
    {
        public bool IsValid { get; }
        public string? InvalidMessage { get; }

        public DialogValidation(bool isValid, string? invalidMessage = null)
        {
            IsValid = isValid;
            InvalidMessage = invalidMessage;
        }
    }
}