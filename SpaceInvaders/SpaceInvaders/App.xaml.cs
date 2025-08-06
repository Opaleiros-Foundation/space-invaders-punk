using Windows.System;
using Uno.Resizetizer;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Services;
using Microsoft.EntityFrameworkCore;
using SpaceInvaders.Data;
using Microsoft.Extensions.Configuration;

namespace SpaceInvaders;

public partial class App : Application
{
    /// <summary>
    /// Initializes the singleton application object. This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
        System.Diagnostics.Debug.WriteLine("Application constructor initialized.");
    }

    protected Window? MainWindow { get; private set; }
    public static IHost? Host { get; private set; }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var builder = this.CreateBuilder(args)
            // Add navigation support for toolkit controls such as TabBar and NavigationView
            .UseToolkitNavigation()
            .Configure(host => host
#if DEBUG
                // Switch to Development environment when running in DEBUG
                .UseEnvironment(Environments.Development)
#endif
                .UseLogging(configure: (context, logBuilder) =>
                {
                    // Configure log levels for different categories of logging
                    logBuilder
                        .SetMinimumLevel(
                            context.HostingEnvironment.IsDevelopment() ? LogLevel.Information : LogLevel.Warning)

                        // Default filters for core Uno Platform namespaces
                        .CoreLogLevel(LogLevel.Warning);

                    // Uno Platform namespace filter groups
                    // Uncomment individual methods to see more detailed logging
                    //// Generic Xaml events
                    //logBuilder.XamlLogLevel(LogLevel.Debug);
                    //// Layout specific messages
                    //logBuilder.XamlLayoutLogLevel(LogLevel.Debug);
                    //// Storage messages
                    //logBuilder.StorageLogLevel(LogLevel.Debug);
                    //// Binding related messages
                    //logBuilder.XamlBindingLogLevel(LogLevel.Debug);
                    //// Binder memory references tracking
                    //logBuilder.BinderMemoryReferenceLogLevel(LogLevel.Debug);
                    //// DevServer and HotReload related
                    //logBuilder.HotReloadCoreLogLevel(LogLevel.Information);
                    //// Debug JS interop
                    //logBuilder.WebAssemblyLogLevel(LogLevel.Debug);
                }, enableUnoLogging: true)
                .UseConfiguration(configure: configBuilder =>
                    configBuilder
                        .EmbeddedSource<App>()
                        .Section<AppConfig>()
                )
                // Enable localization (see appsettings.json for supported languages)
                .UseLocalization()
                .UseHttp((context, services) =>
                {
#if DEBUG
                    // DelegatingHandler will be automatically injected
                    services.AddTransient<DelegatingHandler, DebugHttpHandler>();
#endif
                })
                .ConfigureServices((context, services) => {
                    services.AddSingleton<ISoundService, SoundService>();
                    services.AddSingleton<IPlayerService, PlayerService>();
                    services.AddSingleton<IScoreService, ScoreService>();
                    services.AddSingleton<Player>();
                    services.AddTransient<ScoreViewModel>();
                    services.AddTransient<ScorePage>();
                    services.AddDbContext<SpaceInvadersDbContext>(options =>
                        options.UseNpgsql(context.Configuration.GetConnectionString("SpaceInvadersDb")));
                })
                .UseNavigation(RegisterRoutes)
            );
        MainWindow = builder.Window;
        
        Host = await builder.NavigateAsync<Shell>();
    }

    private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {
        views.Register(
            new ViewMap(ViewModel: typeof(ShellViewModel)),
            new ViewMap<MainPage, MainViewModel>(),
            new DataViewMap<ControllersPage, ControllersViewModel, Player>(),
            new ViewMap<ScorePage, ScoreViewModel>(),
            new DataViewMap<GameStartPage, GameStartPageViewModel, Player>(),
            new DataViewMap<GameOver, GameOverViewModel, Player>()
        );

        routes.Register(
            new RouteMap("", View: views.FindByViewModel<ShellViewModel>(),
                Nested:
                [
                    new RouteMap(
                        Path: "Main",
                        View: views.FindByViewModel<MainViewModel>(), IsDefault: true
                    ),
                    new RouteMap(
                        Path: "Controller",
                        View: views.FindByViewModel<ControllersViewModel>()
                    ),
                    new RouteMap(
                        Path: "Score",
                        View: views.FindByViewModel<ScoreViewModel>()
                    ),
                    new RouteMap(
                        Path: "GameStart",
                        View: views.FindByViewModel<GameStartPageViewModel>()
                    ),
                    new RouteMap(
                        Path: "GameOver",
                        View: views.FindByViewModel<GameOverViewModel>()
                    )
                ]
            )
        );
    }
}
