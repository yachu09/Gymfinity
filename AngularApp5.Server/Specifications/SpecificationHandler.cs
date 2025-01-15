using AngularApp5.Server.Interfaces;
using AngularApp5.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularApp5.Server.Specifications
{
    public class SpecificationHandler<TModel> where TModel : BaseModel
    {
        public static IQueryable<TModel> GetQuery(IQueryable<TModel> input,
            ISpecification<TModel> spec)
        {
            var query = input;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) 
                => current.Include(include));

            return query;
        }
    }
}
