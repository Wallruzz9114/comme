using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.Contexts;
using Models;

namespace Core.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;
        private Hashtable _services;

        public UnitOfWork(DatabaseContext databaseContext) => _databaseContext = databaseContext;

        public IGenericService<T> Service<T>() where T : BaseModel
        {
            if (_services == null) _services = new Hashtable();

            if (!_services.ContainsKey(typeof(T).Name))
            {
                var serviceType = typeof(GenericService<>);
                var serviceInstance = Activator.CreateInstance(serviceType.MakeGenericType(typeof(T)), _databaseContext);

                _services.Add(serviceType, serviceInstance);
            }

            return (IGenericService<T>)_services[typeof(T).Name];
        }

        public async Task<int> Save() => await _databaseContext.SaveChangesAsync();

        public void Dispose() => _databaseContext.Dispose();
    }
}