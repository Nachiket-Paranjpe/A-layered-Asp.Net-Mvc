using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer.RepositoryImpl
{
    public sealed class RepositoryQuery<TEntity>  where TEntity:class
    {
        private readonly List<Expression<Func<TEntity, object>>> _includeProperties;
        private readonly Repository<TEntity> _repository;
        private Expression<Func<TEntity, bool>> _filter;
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;
        private int? _page = null;
        private int? pageSize = null;

        public RepositoryQuery(Repository<TEntity> repository)
        {
            _repository = repository;
            _includeProperties =
                new List<Expression<Func<TEntity, object>>>();
        }

        public RepositoryQuery<TEntity> Filter ( Expression<Func<TEntity, bool>> filter)
        {
            _filter = filter;
            return this;

        }

        public RepositoryQuery<TEntity> OrderBy(
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy)
        {
            _orderBy = orderBy;
            return this;
        }

        public RepositoryQuery<TEntity> Include(
        Expression<Func<TEntity, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        public IEnumerable<TEntity> GetAll ()
        {
            return _repository.GetAll(_filter, _orderBy, _includeProperties, _page, pageSize);
        }

        public TEntity GetChildEntities(Expression<Func<TEntity,bool>> predicate)
        {
            return _repository.GetAllChildsFor(predicate).Single();
        }
    }
}
