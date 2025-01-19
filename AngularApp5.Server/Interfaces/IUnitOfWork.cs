using AngularApp5.Server.Models;

namespace AngularApp5.Server.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel;

        Task<int> Complete();

    }
}
