using AngularApp5.Server.Models;

namespace AngularApp5.Server.Interfaces
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetModelWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void Add(T model);
        void Update(T model);
        void Delete(T model);
    }
}
