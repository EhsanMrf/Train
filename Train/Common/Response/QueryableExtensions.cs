using Common.Exception;
using Common.Response.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;

namespace Common.Response;

public static class QueryableExtensions
{
    public static async Task<DataList<T>> ToDataSourceResultAsync<T>(this IQueryable<T> queryable,DataRequest request)
    {
        if (request?.Filter?.Count() > 0)
            queryable = Filter(queryable, request.Filter.ToList(), request.AliasNameProperty);

        try
        {
            var total = await queryable.CountAsync();
            queryable = PageSize(queryable, request!.PageNumber,
                (request?.PageSize ?? 0) == 0 ? 15 : request!.PageSize);

            var data = await queryable.ToListAsync();
            var dataList = new DataList<T>(data, request!.PageNumber, request.PageSize);
            dataList.SetTotalCount(total);
            return dataList;

        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    static IQueryable<T> Filter<T>(this IQueryable<T> queryable, List<Filter> filter, AliasNameProperty aliasNameProperty = null)
    {
        try
        {
            if (filter is not { Count: > 0 }) return queryable;
            var changeFilters = TransformedPropertyName.ChangeFilters(filter, aliasNameProperty);
            var first = changeFilters.First();
            var codeGenerate = first.CodeGenerate(changeFilters);
            queryable = queryable.Where(codeGenerate);
            return queryable;
        }
        catch (System.Exception e)
        {
            throw new BaseException( e.Message);
        }
    }

     static IQueryable<T> PageSize<T>(IQueryable<T> queryable, int page, int pageSize)
    {
        return queryable.Skip(page * pageSize).Take(pageSize);
    }
}