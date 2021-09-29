using Serilog.Sinks.Prism;
using PrismSinkDemo.Views;
using Prism.Ioc;
using Prism.Modularity;
using Serilog;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Prism.Events;
using Serilog.Formatting.Display;

namespace PrismSinkDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();

            var configuration = new ConfigurationBuilder()
				 .SetBasePath(Directory.GetCurrentDirectory())
				 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				 .Build();

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
                .WriteTo.EventAggregator(eventAggregator, "{Message: lj}")
                .CreateLogger();

            containerRegistry.RegisterInstance(Log.Logger);

        }
    }
}
