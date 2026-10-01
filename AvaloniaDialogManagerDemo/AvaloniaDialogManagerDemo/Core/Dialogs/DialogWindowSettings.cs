namespace AvaloniaDialogManagerDemo.Core.Dialogs
{
    public class DialogWindowSettings
    {
        public bool CanResize { get; set; } = false;
        public bool ShowInTaskBar { get; set; } = false;
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? MinWidth { get; set; }
        public double? MinHeight { get; set; }
        public double? MaxWidth { get; set; }
        public double? MaxHeight { get; set; }

        public static DialogWindowSettings Fixed(
            double width = 400, double height = 280,
            bool showInTaskBar = false) => new()
        {
            CanResize = false,
            ShowInTaskBar = showInTaskBar,
            Width = width,
            Height = height,
            MinWidth = width,
            MaxWidth = width,
            MinHeight = height,
            MaxHeight = height
        };

        public static DialogWindowSettings Resizable(
            double width = 500, double height = 400,
            double minWidth = 250, double minHeight = 200,
            double maxWidth = 800, double maxHeight = 600,
            bool showInTaskBar = false) => new()
            {
                CanResize = true,
                ShowInTaskBar = showInTaskBar,
                Width = width,
                Height = height,
                MinWidth = minWidth,
                MinHeight = minHeight,
                MaxWidth = maxWidth,
                MaxHeight = maxHeight
            };

        public static DialogWindowSettings SizeToContent() => new()
        {
            CanResize = false,
            ShowInTaskBar = false
        };
    }
}