using System;
using System.Threading.Tasks;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
    }
}