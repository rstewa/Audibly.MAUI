namespace Audibly.MAUI.Extensions
{
    public static class WindowExtensions
    {
        public static async Task SetWindowSize(this Window window, double width, double height)
        {
            // change window size.
            window.Width = width;
            window.Height = height;

            // give it some time to complete window resizing task.
            await window.Dispatcher.DispatchAsync(() => { });
        }
        
        public static async Task ResizeWindowToDensity(this Window window, double width, double height)
        {
            var disp = DeviceDisplay.Current.MainDisplayInfo;

            var w = width * disp.Density;
            var h = height * disp.Density;

            await window.SetWindowSize(w, h);
        }

        public static void CenterOnScreen(this Window window)
        {
            var disp = DeviceDisplay.Current.MainDisplayInfo;

            window.X = (disp.Width / disp.Density - window.Width) / 2;
            window.Y = (disp.Height / disp.Density - window.Height) / 2;
        }
    }
}
