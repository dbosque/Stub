using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace dBosque.Stub.Interfaces
{
    public interface IStubService
    {
        string Description { get; }

        IStubService Start(Action<IServiceCollection> collectionAction, IConfiguration configuration);

        IStubService Stop();
    }
}
