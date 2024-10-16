using Core.DataAccess.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Extensions;

public static class QueryablePaginateExtension
{
    public static Paginate<TData> ToPaginate<TData>(this IQueryable<TData> queryable, int index = default, int size = default)
    {
        int count = queryable.Count();

        if (index == default || index < 0) index = 0;
        if (size == default || size <= 0) size = count;

        List<TData> items = queryable.Skip(index * size).Take(size).ToList();
        Paginate<TData> list = new()
        {
            Page = index,
            PageSize = size,
            DataCount = count,
            Data = items,
            PageCount = (count <= 0 || size <= 0) ? 0 : (int)Math.Ceiling(count / (double)size)
        };
        return list;
    }


    public static async Task<Paginate<TData>> ToPaginateAsync<TData>(this IQueryable<TData> queryable, int index = default, int size = default, CancellationToken cancellationToken = default)
    {
        int count = await queryable.CountAsync(cancellationToken).ConfigureAwait(false);

        if (index == default || index < 0) index = 0;
        if (size == default || size <= 0) size = count;

        List<TData> items = await queryable.Skip(index * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);
        Paginate<TData> list = new()
        {
            Page = index,
            PageSize = size,
            DataCount = count,
            Data = items,
            PageCount = (count <= 0 || size <= 0) ? 0 : (int)Math.Ceiling(count / (double)size)
        };
        return list;
    }
}
