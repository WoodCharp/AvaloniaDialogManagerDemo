namespace AvaloniaDialogManagerDemo.Core.Dialogs
{
    public interface IDialogViewModel<T>
    {
        T? GetResultData();

        DialogValidation GetValidation();
    }
}