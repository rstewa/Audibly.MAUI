#region usings
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

using Audibly.MAUI.Extensions;
using CommunityToolkit.Maui;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Controls;

#if WINDOWS || DEBUG
using WinUIEx;

#elif MACCATALYST
using CoreGraphics;
using UIKit;

#elif IOS
#elif ANDROID
#elif TIZEN

#endif
#endregion


namespace Audibly.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit MediaElement by adding the below line of code
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var width = 315;
        var height = 440;

        // ref: https://github.com/LanceMcCarthy/DevOpsExamples/blob/main/src/MAUI/MauiDemo/MauiProgram.cs
        builder.ConfigureLifecycleEvents(events =>
        {
#if WINDOWS || DEBUG

            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    window.SetIsResizable(false);
                    window.ExtendsContentIntoTitleBar = true;
                });
            });

#elif MACCATALYST

            // Using CoreGraphics and UIKit
            events.AddiOS(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.SceneWillConnect((scene, session, options) =>
                {
                    if (scene is UIWindowScene { SizeRestrictions: { } } windowScene)
                    {
                        windowScene.SizeRestrictions.MaximumSize = new CGSize(width, height);
                        windowScene.SizeRestrictions.MinimumSize = new CGSize(width, height);
                    }
                });

            });

#endif
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
