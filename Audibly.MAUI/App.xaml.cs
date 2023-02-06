#if WINDOWS
using Audibly.MAUI.Extensions;
using WinUIEx;
#endif

namespace Audibly.MAUI;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);
        window.Activated += Window_Activated;
        window.DisplayDensityChanged += Window_DisplayDensityChanged;

        var tmp = window.DisplayDensity;

        return window;
    }

    private async void Window_DisplayDensityChanged(object sender, DisplayDensityChangedEventArgs e)
    {
        var window = sender as Window;
        await window.ResizeWindowToDensity();
    }

    private async void Window_Activated(object sender, EventArgs e)
    {
        const int width = 315;
        const int height = 440;

#if WINDOWS

        var window = sender as Window;

        await window.SetWindowSize(width, height);

        if (DeviceDisplay.Current.MainDisplayInfo.Density != 1)
            await window.ResizeWindowToDensity();

        // move to screen center
        window.CenterOnScreen();
                    
#elif MACCATALYST

        var window = sender as Window;
        window.MinimumWidth = width;
        window.MaximumWidth = width;
        window.MinimumHeight = height;
        window.MaximumHeight = height;

#endif
    }
}
