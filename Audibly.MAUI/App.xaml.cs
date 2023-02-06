#if WINDOWS || DEBUG
using Audibly.MAUI.Extensions;
using WinUIEx;
#endif

namespace Audibly.MAUI;

public partial class App : Application
{
    private const double _width = 315;
    private const double _height = 440;

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

        return window;
    }

    private async void Window_DisplayDensityChanged(object sender, DisplayDensityChangedEventArgs e)
    {
        var window = sender as Window;
        await window.ResizeWindowToDensity(_width, _height);
    }

    private async void Window_Activated(object sender, EventArgs e)
    {

#if WINDOWS || DEBUG

        var window = sender as Window;

        await window.SetWindowSize(_width, _height);

        if (DeviceDisplay.Current.MainDisplayInfo.Density != 1)
            await window.ResizeWindowToDensity(_width, _height);

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
