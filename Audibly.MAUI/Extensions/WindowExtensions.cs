namespace Audibly.MAUI.Extensions
{
    public static class WindowExtensions
    {
        public static async Task SetWindowSize(this Window window, int width, int height)
        {
            // change window size.
            window.Width = width;
            window.Height = height;

            // give it some time to complete window resizing task.
            await window.Dispatcher.DispatchAsync(() => { });
        }
        
        public static async Task ResizeWindowToDensity(this Window window)
        {
            var disp = DeviceDisplay.Current.MainDisplayInfo;

            // change window size.
            window.Width *= disp.Density;
            window.Height *= disp.Density;

            // give it some time to complete window resizing task.
            await window.Dispatcher.DispatchAsync(() => { });
        }

        // TODO => may need to add await window.Dispatcher.DispatchAsync(() => { });
        public static void CenterOnScreen(this Window window)
        {
            var disp = DeviceDisplay.Current.MainDisplayInfo;

            window.X = (disp.Width / disp.Density - window.Width) / 2;
            window.Y = (disp.Height / disp.Density - window.Height) / 2;
        }
    }
}
