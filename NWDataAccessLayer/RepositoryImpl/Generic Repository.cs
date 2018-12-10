using DataAccessLayer.Context;
using DataAccessLayer.Interfaces.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer.RepositoryImpl
{
    public class Repository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private IDbSet<TEntity> _dbSet;
       
        public Repository(IDbcontext context)
        {
            _context = (NwDBcontext)context;
            _dbSet = _context.Set<TEntity>();           
        }        
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach((addentity) => _context.Set<TEntity>().Add(addentity));
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsEnumerable();
        }
        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }
        internal IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null)
        {

            IQueryable<TEntity> query = _dbSet;           
            IEnumerable<TEntity> queryResults;

                if (includeProperties != null)
                    includeProperties.ForEach(i => { query = query.Include(i); });

                if (filter != null)
                    query = query.Where(filter);

                if (orderBy != null)
                    query = orderBy(query);

                if (page != null && pageSize != null)
                    query = query
                        .Skip((page.Value - 1) * pageSize.Value)
                        .Take(pageSize.Value);

                queryResults = query.ToList();           

            return queryResults;
        }

        internal IQueryable<TEntity> GetAllChildsFor(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);

        }
        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void UpdateRange(IEnumerable<TEntity> Updates)
        {
            Updates.ToList().ForEach((upd) => _context.Set<TEntity>().Attach(upd));
            _context.Entry(Updates).State = EntityState.Modified;
        }
        public void Remove(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
        }
        public void Remove(int Id)
        {
            TEntity entityTobeRemoved = _dbSet.Find(Id);
            if (entityTobeRemoved != null)
            {
                _dbSet.Attach(entityTobeRemoved);
                _context.Entry(entityTobeRemoved).State = EntityState.Deleted;
            }
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach((e) => _dbSet.Attach(e));
            _context.Entry(entities).State = EntityState.Deleted;
        }
        public virtual RepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper =
                new RepositoryQuery<TEntity>(this);

            return repositoryGetFluentHelper;
        }
    }
}
