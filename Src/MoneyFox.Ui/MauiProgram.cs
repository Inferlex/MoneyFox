namespace MoneyFox.Ui;

using CommunityToolkit.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;

#if IOS
using MoneyFox.Ui.Platforms.iOS.Renderer;
using Microsoft.Maui.Controls.Handlers.Compatibility;
#endif

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .ConfigureFonts(
                fonts =>
                {
                    fonts.AddFont(filename: "OpenSans-Regular.ttf", alias: "OpenSansRegular");
                    fonts.AddFont(filename: "OpenSans-Semibold.ttf", alias: "OpenSansSemibold");
                    fonts.AddFont(filename: "ProductSans-Regular.ttf", alias: "Product");
                    fonts.AddFont(filename: "MaterialIconsRound-Regular.otf", alias: "MaterialIconsRound");
                })
            .ConfigureMauiHandlers(handlers => {
                handlers.AddHandler(typeof(Shell), typeof(CustomShellRenderer));
            })
            .UseSkiaSharp(true)
            .UseMauiCommunityToolkit();

        return builder.Build();
    }
}
