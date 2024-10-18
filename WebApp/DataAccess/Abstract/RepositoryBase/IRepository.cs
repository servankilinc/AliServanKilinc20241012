using Core.DataAccess.DynamicQueries;
using Core.DataAccess.Pagination;
using Core.Model;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Abstract.RepositoryBase;

public interface IRepository<TEntity> where TEntity : IEntity
{
    TEntity Get(
        Expression<Func<TEntity, bool>> filter, 
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false
    );
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
    void DeleteByFilter(Expression<Func<TEntity, bool>> filter);
    bool IsExist(Expression<Func<TEntity, bool>> filter, bool withDeleted = false);

    ICollection<TEntity> GetAll(
        Filter? filter = null,
        Sort? sort = null,
        Expression<Func<TEntity, bool>>? where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true
    );

    Paginate<TEntity> GetPaginatedList(
        Filter? filter = null,
        Sort? sort = null,
        Expression<Func<TEntity, bool>>? where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int page = default,
        int pageSize = default,
        bool withDeleted = false,
        bool enableTracking = true
    );
}
