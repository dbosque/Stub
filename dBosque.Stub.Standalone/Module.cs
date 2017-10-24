﻿using dBosque.Stub.AspNet.Combined;
using dBosque.Stub.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddHostingModule(this IServiceCollection service, IConfiguration configuration, string sectionName = "Hosting")
        {
            service.AddTransient<IServiceRegister, OneEndpointServiceRegister>()
                    .Configure<dBosque.Stub.AspNet.Combined.Configuration.Hosting>(configuration.GetSection(sectionName));
            return service;
        }
    }
}