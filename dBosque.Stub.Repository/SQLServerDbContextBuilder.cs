﻿using dBosque.Stub.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace dBosque.Stub.Repository
{
    public class SQLServerDbContextBuilder : IDbContextBuilder
    {
        bool IDbContextBuilder.CanHandle(string providerType) => string.Compare(providerType, "SQLServer", true) == 0;      

        T IDbContextBuilder.CreateDbContext<T>(string connectionString)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(connectionString);
            object[] args = new object[] { dbContextBuilder.Options };
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }

    
}
