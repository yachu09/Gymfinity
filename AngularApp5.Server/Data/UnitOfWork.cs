using AngularApp5.Server.Interfaces;
using AngularApp5.Server.Models;
using AngularApp5.Server.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace AngularApp5.Server.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;
        private Hashtable repositories;

        public UnitOfWork(StoreContext context)
        {
            this.context = context;
        }
        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<TModel> Repository<TModel>() where TModel : BaseModel
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(TModel).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositoryType.MakeGenericType(typeof(TModel)), context);

                repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TModel>) repositories[type];

        }
    }
}
