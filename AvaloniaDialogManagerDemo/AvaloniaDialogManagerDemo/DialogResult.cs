namespace AvaloniaDialogManagerDemo
{
    public class DialogResult
    {
        /// <summary>
        /// What button was pressed
        /// </summary>
        public string ButtonPressed { get; }
        /// <summary>
        /// Returning data from the dialog
        /// </summary>
        public object? ReturningObject { get; }

        public DialogResult(string buttonPressed, object? returningObject = null)
        {
            ButtonPressed = buttonPressed;
            ReturningObject = returningObject;
        }
    }
}