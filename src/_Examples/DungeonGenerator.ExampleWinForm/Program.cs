using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetCraft.DevTools.DungeonGenerator.Business;
using Microsoft.Extensions.DependencyInjection;

namespace DungeonGenerator.ExampleWinForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serviceProvider = ConfigureServices();
            var mainForm = (Form1)serviceProvider.GetService(typeof(Form1));
            Application.Run(mainForm);
        }

        static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<Form1>();

            services.AddLogging();
            services.UseDungeonGenerator();
            
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
