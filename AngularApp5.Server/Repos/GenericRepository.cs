using AngularApp5.Server.Data;
using AngularApp5.Server.Interfaces;
using AngularApp5.Server.Models;
using AngularApp5.Server.Specifications;
using Microsoft.EntityFrameworkCore;

namespace AngularApp5.Server.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetModelWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecs(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecs(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecs(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecs(ISpecification<T> spec)
        {
            return SpecificationHandler<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
