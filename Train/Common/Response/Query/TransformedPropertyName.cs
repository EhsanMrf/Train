namespace Common.Response.Query;

public static class TransformedPropertyName
{

    public static List<Filter> ChangeFilters(List<Filter> filters, AliasNameProperty aliasNameProperty)
    {
        if (aliasNameProperty == null || aliasNameProperty.PropertyMappings.Count == 0)
            return filters;

        var dictionary =
            aliasNameProperty.PropertyMappings
                .ToDictionary(item => item.Key, item => item.Value);
        
        foreach (var filter in filters)
        {
            filter.Field = GetTransformedPropertyName(dictionary, filter.Field);
            if (filter.Conditions is not {Count: > 0}) continue;
            foreach (var filterFilter in filter.Conditions)
            {
                if (filterFilter.Field != null)
                    filterFilter.Field = GetTransformedPropertyName(dictionary, filterFilter.Field);
            }
        }
        return filters;
    }


    static string? GetTransformedPropertyName(Dictionary<string, string> propertyMappings, string propertyName)
    {
        return propertyMappings.ContainsKey(propertyName)
            ? propertyMappings.GetValueOrDefault(propertyName)
            : propertyName;
    }
}

public class AliasNameProperty
{
    public Dictionary<string, string> PropertyMappings { get; set; } = new();
}