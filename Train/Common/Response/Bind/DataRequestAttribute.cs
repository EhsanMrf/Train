using Microsoft.AspNetCore.Mvc;

namespace Common.Response.Bind;

public class DataRequestAttribute : ModelBinderAttribute
{
    public DataRequestAttribute() => this.BinderType = typeof(DataRequestModelBinder);
}