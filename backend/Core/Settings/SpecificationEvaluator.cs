using System.Linq;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Configuration
{
    public class SpecificationEvaluator<T> where T : BaseModel
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecificationService<T> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);
            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);
            if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);
            if (specification.PagingEnabled)
                query = query.Skip(specification.Skip).Take(specification.Take);

            return specification.Includes.Aggregate(query, (current, expression) => current.Include(expression));
        }
    }
}