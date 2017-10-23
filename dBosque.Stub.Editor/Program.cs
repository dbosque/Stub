using dBosque.Stub.Editor.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace dBosque.Stub.Editor
{
    static class Program
    {
        private static IServiceProvider _provider;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            _provider = SetupDependencies();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;
            Application.Run(_provider.GetService<Forms.Splashscreen>());
            
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        public static void Start()
        {
            Application.Run(_provider.GetService<Forms.MainFormDockPanel>());

        }

        private static IServiceProvider SetupDependencies()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var provider = new ServiceCollection()
              .AddScoped<IConfiguration>(s => configuration)
              .AddEditorModule(configuration)
              .AddRepositoryModule(configuration)
              .BuildServiceProvider();

            GlobalSettings.Instance.Configure(provider);
            return provider;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).ToString(), "Error");
            Application.Exit();
        }
    }
}
