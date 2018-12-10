using DataAccessLayer.Interfaces.Repository;
using DataAccessLayer.RepositoryImpl;

namespace DataAccessLayer
{
    public interface IGenericRepository<TEntity> : IRepository<TEntity> where TEntity: class
    {
        RepositoryQuery<TEntity> Query();
    }
}
