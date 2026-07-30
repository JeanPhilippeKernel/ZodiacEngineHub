using System;
using System.Threading;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Controls.Primitives;

namespace Panzerfaust;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        // Named mutex ensures only one instance runs at a time, cross-platform.
        // The name is prefixed with "Global\" on Windows for cross-session detection;
        // on macOS/Linux the prefix is ignored but the name still works per-user.
        const string mutexName = "Global\\Panzerfaust_SingleInstance_8F3A2C1D";
        using var mutex = new Mutex(initiallyOwned: true, mutexName, out bool createdNew);
        if (!createdNew)
            return; // Another instance is already running — exit silently.

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .UseReactiveUI()
            .LogToTrace();
}