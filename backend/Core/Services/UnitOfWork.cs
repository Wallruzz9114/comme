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

            var type = typeof(T);

            if (!_services.ContainsKey(type.Name))
            {
                var serviceType = typeof(GenericService<>);
                var serviceInstance = Activator.CreateInstance(serviceType.MakeGenericType(type), _databaseContext);

                _services.Add(type.Name, serviceInstance);
            }

            return (IGenericService<T>)_services[type.Name];
        }

        public async Task<int> Save() => await _databaseContext.SaveChangesAsync();

        public void Dispose() => _databaseContext.Dispose();
    }
}