using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SupportTicketSystem.Application;
using SupportTicketSystem.Infrastructure;
using SupportTicketSystem.Presentation.Navigation;
using SupportTicketSystem.Presentation.ViewModels;
using System;

namespace SupportTicketSystem.Presentation
{
                               // V - För att bara "Application" funkar inte
    public partial class App : Microsoft.UI.Xaml.Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        private Window? _window;

        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddApplication();
            services.AddInfrastructure();

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddTransient<HomeViewModel>();

            services.AddTransient<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = ServiceProvider.GetRequiredService<MainWindow>();
            _window.Activate();
        }
    }
}
