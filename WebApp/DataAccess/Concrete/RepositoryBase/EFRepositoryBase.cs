using Core.DataAccess.DynamicQueries;
using Core.DataAccess.Pagination;
using Core.Model;
using DataAccess.Abstract.RepositoryBase;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.DataAccess.Extensions;

namespace DataAccess.Concrete.RepositoryBase;

public class EFRepositoryBase<TEntity, TContext> : IRepository<TEntity>, IRepositoryAsync<TEntity>
    where TEntity : class, IEntity
    where TContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    protected TContext _context { get; set; }
    public EFRepositoryBase(TContext context) => _context = context;



    // ************* SYNC PROCESSES **************
    public TEntity Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (withDeleted) queryable = queryable.IgnoreQueryFilters();
        if (include != null) queryable = include(queryable);
        return queryable.FirstOrDefault(filter)!;
    }

    public TEntity Add(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Added;
        _context.SaveChanges();
        return entity;
    }

    public void Delete(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public void DeleteByFilter(Expression<Func<TEntity, bool>> filter)
    {
        var entity = _context.Set<TEntity>().Where(filter).FirstOrDefault();
        if (entity == null) throw new InvalidOperationException("The specified entity to delete could not be found.");
        _context.Entry(entity).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public TEntity Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
        return entity;
    }

    public bool IsExist(Expression<Func<TEntity, bool>> filter, bool withDeleted = false)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (withDeleted) queryable = queryable.IgnoreQueryFilters();
        return queryable.Any(filter);
    }

    public ICollection<TEntity> GetAll(
        Filter? filter = null,
        Sort? sort = null,
        Expression<Func<TEntity, bool>>? where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (withDeleted) queryable = queryable.IgnoreQueryFilters();
        if (include != null) queryable = include(queryable);
        if (filter != null) queryable = queryable.ToFilter(filter);
        if (sort != null) queryable = queryable.ToSort(sort);
        if (where != null) queryable = queryable.Where(where);
        if (orderBy != null)
            return orderBy(queryable).ToList();
        return queryable.ToList();
    }


    public Paginate<TEntity> GetPaginatedList(
        Filter? filter = null,
        Sort? sort = null,
        Expression<Func<TEntity, bool>>? where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int page = default,
        int pageSize = default,
        bool withDeleted = false,
        bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (withDeleted) queryable = queryable.IgnoreQueryFilters();
        if (include != null) queryable = include(queryable);
        if (filter != null) queryable = queryable.ToFilter(filter);
        if (sort != null) queryable = queryable.ToSort(sort);
        if (where != null) queryable = queryable.Where(where);
        if (orderBy != null)
            return orderBy(queryable).ToPaginate(page, pageSize);
        return queryable.ToPaginate(page, pageSize);
    }


    // ************* ASYNC PROCESSES **************

    public async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (withDeleted) queryable = queryable.IgnoreQueryFilters();
        if (include != null) queryable = include(queryable);
        var result = await queryable.FirstOrDefaultAsync(filter, cancellationToken);
        return result!;
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Added;
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteByFilterAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
        if (entity == null) throw new InvalidOperationException("The specified entity to delete could not be found.");
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter, bool withDeleted = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (withDeleted) queryable = queryable.IgnoreQueryFilters();
        return await queryable.AnyAsync(filter, cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetAllAsync(
        Filter? filter = null,
        Sort? sort = null,
        Expression<Func<TEntity, bool>>? where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (withDeleted) queryable = queryable.IgnoreQueryFilters();
        if (include != null) queryable = include(queryable);
        if (filter != null) queryable = queryable.ToFilter(filter);
        if (sort != null) queryable = queryable.ToSort(sort);
        if (where != null) queryable = queryable.Where(where);
        if (orderBy != null)
            return await orderBy(queryable).ToListAsync(cancellationToken);
        return await queryable.ToListAsync(cancellationToken);
    }

    public async Task<Paginate<TEntity>> GetPaginatedListAsync(
        Filter? filter = null,
        Sort? sort = null,
        Expression<Func<TEntity, bool>>? where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int page = default,
        int pageSize = default,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (withDeleted) queryable = queryable.IgnoreQueryFilters();
        if (include != null) queryable = include(queryable);
        if (filter != null) queryable = queryable.ToFilter(filter);
        if (sort != null) queryable = queryable.ToSort(sort);
        if (where != null) queryable = queryable.Where(where);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(page, pageSize, cancellationToken);
        return await queryable.ToPaginateAsync(page, pageSize, cancellationToken);
    }

}