using Appccelerate.EventBroker;
using dBosque.Stub.Editor;
using dBosque.Stub.Editor.Controls;
using dBosque.Stub.Editor.Controls.Interfaces;
using dBosque.Stub.Editor.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// DependencyInjection module
    /// </summary>
    public static class Module
    {
        public static IServiceCollection AddEditorModule(this IServiceCollection service, IConfiguration configuration, string sectionName = "Version")
        {
            service.AddSingleton(new LoggerFactory())
                   .AddLogging()
                   .AddSingleton<IControlFactory,Factory>()
                   .AddSingleton<IEventBroker, EventBroker>()
                   .AddSingleton<MessageTypeViewControl>()
                   .AddSingleton<PropertyEditor>()
                   .AddSingleton<MessageEditor>()
                   .AddSingleton<LoggingControl>()
                   .AddSingleton<Splashscreen>()
                   .AddSingleton<MainFormDockPanel>()
                   .AddTransient<TemplateTreeviewControl>()
                   .AddSingleton<TemplateViewController>()
                   .AddSingleton<DockPanelController>()
                   .AddSingleton<ConnectionStringSelection>()
                   .Configure<Version>(configuration.GetSection(sectionName));

            return service;

        }
    }
}
