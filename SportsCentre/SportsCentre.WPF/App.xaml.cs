using Microsoft.Extensions.DependencyInjection;
using SportsCentre.Services;
using SportsCentre.Domain.Interfaces;
using SportsCentre.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.State.Navigators;
using SportsCentre.WPF.ViewModels.Factories;

namespace SportsCentre.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<ICentreService, CentreService>();

            services.AddSingleton<ISportsCentreViewModelAbstractFactory, SportsCentreViewModelAbstractFactory>();
            services.AddSingleton<ISportsCentreViewModelFactory<CentresViewModel>, CentreViewModelFactory>();
            services.AddSingleton<ISportsCentreViewModelFactory<CourtsViewModel>, CourtsViewModelFactory>(); 

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
