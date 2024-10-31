using BookshopManagement.BL.Interface;
using BookshopManagement.BL.Services;
using BookshopManagement.DAL.Data;
using BookshopManagement.DAL.Interfaces;
using BookshopManagement.DAL.Repositories;
using BookshopManagement.PL.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BookshopManagement
{
    public partial class App : Application
    {
        public static ServiceProvider _serviceProvider;

        public App() { }

        protected override void OnStartup(StartupEventArgs e)
        {
            //await AppHost.StartAsync();
            var serviceCollection = new ServiceCollection();

            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)  // Startup project directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            // Register BookshopContext with connection string from configuration
            serviceCollection.AddDbContext<BookshopContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BookshopDatabase")));

            // Register Core services
            serviceCollection.AddScoped<IBookRepository, BookRepository>();
            serviceCollection.AddScoped<IBookService, BookService>();
            serviceCollection.AddScoped<ISalesRepository, SalesRepository>();
            serviceCollection.AddScoped<ISalesService, SalesService>();

            // Register ViewModels
            serviceCollection.AddSingleton<BookManagementViewModel>();
            serviceCollection.AddSingleton<SalesViewModel>();

            _serviceProvider = serviceCollection.BuildServiceProvider();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider.Dispose();
            base.OnExit(e);
        }
    }

}
