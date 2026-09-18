namespace AvaloniaDialogManagerDemo.Core.Dialogs
{
    public enum DialogButtonResult
    {
        None,
        Ok,
        Yes,
        No,
        Cancel,
        Custom
    }

    public static class DialogButton
    {
        public static bool IsPrimaryAction(string label)
        {
            var lower = label.ToLowerInvariant();
            return lower is "ok" or "save" or "yes" or "accept";
        }

        public static bool IsCancelAction(string label)
        {
            var lower = label.ToLowerInvariant();
            return lower is "cancel" or "no" or "close";
        }
    }

    public class DialogResult<T>
    {
        public DialogButtonResult Result { get; }
        public string? CustomButtonText { get; }
        public T? Data { get; }

        public bool IsSuccess
        {
            get
            {
                return Result == DialogButtonResult.Ok || Result == DialogButtonResult.Yes;
            }
        }

        public DialogResult(DialogButtonResult result, T? data = default, string? customButtonText = null)
        {
            Result = result;
            Data = data;
            CustomButtonText = customButtonText;
        }

        public static DialogResult<T> Ok(T data)
        {
            return new(DialogButtonResult.Ok, data);
        }

        public static DialogResult<T> Cancel()
        {
            return new(DialogButtonResult.Cancel);
        }

        public static DialogResult<T> Custom(string buttonText, T? data = default)
        {
            return new(DialogButtonResult.Custom, data, buttonText);
        }
    }
}