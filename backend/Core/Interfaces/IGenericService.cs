using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Core.Interfaces
{
    public interface IGenericService<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(int modelId);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetOneWithSpecification(ISpecificationService<T> specification);
        Task<IReadOnlyList<T>> ListAllWithSpecificationAsync(ISpecificationService<T> specification);
        Task<int> CountAsync(ISpecificationService<T> specification);
        void Create(T model);
        void Update(T model);
        void Delete(T model);
    }
}