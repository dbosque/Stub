using dBosque.Stub.Editor.Controls.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace dBosque.Stub.Editor.Controls
{
    public class Factory : IControlFactory
    {
        private IServiceProvider _provider;
        private TemplateViewController _controller;
        public Factory(IServiceProvider provider, TemplateViewController controller)
        {
            _provider = provider;
            _controller = controller;
        }


        ///<summary>
        ///Create a specific control
        ///</summary>
        ///<typeparam name="T"></typeparam>
        ///<returns></returns>      
        public T Create<T>() => _provider.GetService<T>();

        ///<summary>
        ///Create a specific control from a persisString
        ///</summary>
        ///<param name="persistString"></param>
        ///<returns></returns>
        public IDockContent CreateFrom(string persistString)
        {
            if (persistString == typeof(LoggingControl).ToString())
                return Create<LoggingControl>();
            else if (persistString == typeof(PropertyEditor).ToString())
                return Create<PropertyEditor>();
            else if (persistString == typeof(MessageTypeViewControl).ToString())
                return Create<MessageTypeViewControl>();
            else if (persistString == typeof(MessageEditor).ToString())
                return Create<MessageEditor>();
            else if (persistString.StartsWith(typeof(TemplateTreeviewControl).ToString(), StringComparison.Ordinal))
                return _controller.CreateFrom(persistString);               
            else
                return null;
        }
    }
}
