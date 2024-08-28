using Common.Response.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common.Response.Bind;

public class DataRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var request = bindingContext.HttpContext.Request;

        var filter = new Filter
        {
            Field = request.Query["filter[field]"],
            Type = request.Query["filter[type]"],
            Value = request.Query["filter[value]"],
            Operator = request.Query["filter[operator]"],
            FilterType = request.Query["filter[filterType]"],
        };

       
        var pageNumber = int.TryParse(request.Query["pageNumber"], out var page) ? page : 0;
        var pageSize = int.TryParse(request.Query["pageSize"], out var size) ? size : 10;


        var dataRequest = new DataRequest
        {
            Filter = filter.Field!=null? new List<Filter> { filter }:null,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        bindingContext.Result = ModelBindingResult.Success(dataRequest);
        return Task.CompletedTask;
    }
}