using System;
using System.Threading.Tasks;
using Models;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericService<T> Service<T>() where T : BaseModel;
        Task<int> Save();
    }
}