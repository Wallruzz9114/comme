using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.Configuration;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Core.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseModel
    {
        private readonly DatabaseContext _databaseContext;

        public GenericService(DatabaseContext databaseContext) => _databaseContext = databaseContext;

        public async Task<T> GetByIdAsync(int id) => await _databaseContext.Set<T>().FindAsync(id);

        public async Task<T> GetOneWithSpecification(ISpecificationService<T> specification) =>
            await ApplySpecification(specification).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> ListAllAsync() => await _databaseContext.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> ListAllWithSpecificationAsync(ISpecificationService<T> specification) =>
            await ApplySpecification(specification).ToListAsync();

        public async Task<int> CountAsync(ISpecificationService<T> specification) =>
            await ApplySpecification(specification).CountAsync();

        private IQueryable<T> ApplySpecification(ISpecificationService<T> specification) =>
            SpecificationEvaluator<T>.GetQuery(_databaseContext.Set<T>().AsQueryable(), specification);
    }
}