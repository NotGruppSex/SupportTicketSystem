using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SupportTicketSystem.Application;
using SupportTicketSystem.Infrastructure;
using System;

namespace SupportTicketSystem.Presentation
{
                               // V - För att bara "Application" funkar inte
    public partial class App : Microsoft.UI.Xaml.Application
    {
        public static IServiceProvider Provider { get; private set; } = null!;

        private Window? _window;

        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddApplication();
            services.AddInfrastructure();

            services.AddTransient<MainWindow>();

            Provider = services.BuildServiceProvider();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = Provider.GetRequiredService<MainWindow>();
            _window.Activate();
        }
    }
}
