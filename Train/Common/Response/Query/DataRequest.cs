namespace Common.Response.Query;

public class DataRequest
{
    public IEnumerable<Filter>? Filter { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 0;
    public AliasNameProperty? AliasNameProperty { get; set; }
}