using dBosque.Stub.Interfaces;
using Microsoft.Extensions.Logging;

namespace dBosque.Stub.Server.AspNetCore.Host
{
    public abstract class ServiceRegisterBase<T> : IServiceRegister where T: class
    {
        protected ServiceRegisterBase(ILogger logger, string name, string url)
        { 
            _service = new WebApiHost<T>(url, name, logger);
        }

        public bool Enabled { get; protected set; }

        protected IStubService _service;

        IStubService IServiceRegister.Service => _service;
    }
}
