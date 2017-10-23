
using Microsoft.EntityFrameworkCore;

namespace dBosque.Stub.Repository.Interfaces
{
    /// <summary>
    /// Separate ContextBuilder implementation
    /// </summary>
    public interface IDbContextBuilder
    {
        bool CanHandle(string providerType);
        T CreateDbContext<T>(string connectionString) where T : DbContext, new();
    }
}
