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

            query = specification.Includes.Aggregate(query, (current, expression) => current.Include(expression));

            return query;
        }
    }
}