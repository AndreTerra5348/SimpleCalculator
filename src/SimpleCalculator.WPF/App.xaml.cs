using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleCalculator.Core;
using SimpleCalculator.Core.Contexties;
using SimpleCalculator.WPF.Formatter;
using SimpleCalculator.WPF.ViewModels;
using SimpleCalculator.WPF.Views;
using System.Windows;

namespace SimpleCalculator.WPF;

public partial class App : Application
{
    private static IHost AppHost { get; set; }
    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services
                    .AddScoped<IOperationContext, OperationContext>()
                    .AddScoped<ICalculator, Calculator>();

                services
                    .AddTransient<ICultureNumberFormatter, CultureNumberFormatter>()
                    .AddTransient<CalculatorViewModel>()
                    .AddTransient<MainWindow>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost.StartAsync();

        var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();

        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync();

        base.OnExit(e);
    }
}
