
using Microsoft.Extensions.DependencyInjection;
using MyLibrary2.Application.Interfaces;
using MyLibrary2.Application;
using MyLibrary2.Infrastructure;
using MyLibrary2.Presentation;

namespace MyLibrary2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 1) Configure DI container
            var services = new ServiceCollection();

            // Register repository & services
            services.AddSingleton<IBookRepository, InMemoryBookRepository>(); // single store for entire app lifetime
            services.AddScoped<IBookService, BookService>();

            // UI
            services.AddTransient<ConsoleMenu>();

            // 2) Build provider and run
            using var sp = services.BuildServiceProvider();
            var menu = sp.GetRequiredService<ConsoleMenu>();
            menu.Run();
        }
    }
}
