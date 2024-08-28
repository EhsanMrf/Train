using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Common.Response;

public class Filter
{

    [DataMember(Name = "field")] public string? Field { get; set; }
    public string Logic { get; set; } = "and";
    [DataMember(Name = "type")] public string? Type { get; set; }

    [DataMember(Name = "value")] public string? Value { get; set; }

    [DataMember(Name = "@operator")] public string? Operator { get; set; }

    [DataMember(Name = "filterType")] public string? FilterType { get; set; }

    [DataMember(Name = "filters")] public IList<Filter>? Conditions { get; set; }

    public Filter()
    {
    }

    public Filter(string field, string type, object value, string @operator)
    {
        Field = field;
        Type = type;
        Value = value.ToString();
        Operator = @operator;
    }

    public static readonly IDictionary<string, string> Operators = new Dictionary<string, string>
    {
        {"equals", "="},
        {"notEqual", "!="},
        {"lessThan", "<"},
        {"lessThanOrEqual", "<="},
        {"greaterThan", ">"},
        {"greaterThanOrEqual", ">="},
        {"startsWith", "StartsWith"},
        {"endsWith", "EndsWith"},
        {"contains", "Contains"},
        {"notContains", "Contains"},
        {"blank", "IS NULL"},
        {"notBlank", "IS NOT NULL"},
        {"inRange", "BETWEEN"},
    };

    string BetweenNumber(Filter filter)
    {
        var numbers = filter.Value.Split(",");
        return $">={numbers[0]} AND {filter.Field}<={numbers[1]}";
    }

    string ApplyTypeQuery(Filter filter)
    {
        return filter.Type switch
        {
            "notContains" => $"!{filter.Field}.{Operators[filter.Type]}({Case(filter)})",
            "contains" => $"{ContainsWithoutList(filter)} ",
            "endsWith" => $"{filter.Field}.{Operators[filter.Type]}({Case(filter)}) ",
            "startsWith" => $"{filter.Field}.{Operators[filter.Type]}({Case(filter)}) ",
            "inRange" => $"{filter.Field}{BetweenNumber(filter)} ",
            "blank" => $"{filter.Field}=(null) ",
            "notBlank" => $"{filter.Field}!=(null) ",
            _ => $"{filter.Field}{Operators[filter.Type]}({Case(filter)})"
        };
    }

    private string ContainsOnList(Filter filter)
    {
        string generateQuery = string.Empty;
        var numbers = filter.Value!.Split(",");
        byte count = 0;
        foreach (var number in numbers)
        {
            count++;
            generateQuery += $"{filter.Field}.Any(l=>l=={number}) ";
            if (count < numbers.Count())
            {
                generateQuery += " || ";
            }
        }

        return generateQuery;
    }

    private string ContainsWithoutList(Filter filter)
    {
        return filter.FilterType == "list"
            ? ContainsOnList(filter)
            : $"{filter.Field}.{Operators[filter.Type]}({Case(filter)}) ";
    }

    private bool SetOperatorComparison(Filter filter, string par)
    {
        return Operators[filter.Type] is "StartsWith" or "EndsWith" or "Contains";
    }

    private string ConvertLogic()
    {
        return Logic switch
        {
            "and" => "and",
            "And" => "and",
            "or" => "||",
            "OR" => "||",
            // _ =>/* throw new QueryFilterExceptionParameterInvalid()*/
        };
    }


    private string GenerateNested(IList<Filter> filters, Filter filterItem)
    {
        var setQuery = "(";

        var count = 1;
        foreach (var filter in filters)
        {
            filter.Field = filterItem.Field;
            setQuery += ApplyTypeQuery(filter);

            count++;
            if (count == filters.Count || count < filters.Count)
                setQuery += $" {filterItem.Operator} ";
        }

        return setQuery += ")";
    }


    object? Case(Filter filter)
    {
        if (filter.Value == null)
        {
            string? nullable = filter.Value;
            return nullable;
        }

        return filter.Value.GetType().FullName == "System.String" ? " \"" + filter.Value + "\"" : filter.Value;
    }



    public string CodeGenerate(IList<Filter> filters)
    {
        var setQuery = "(";
        foreach (var filter in filters)
        {
            var countItem = filters.IndexOf(filter) + 1;
            if (filter.Conditions != null && filter.Conditions!.Count == 0 || filter.Conditions == null)
            {
                if (SetOperatorComparison(filter, setQuery))
                {
                    setQuery += ApplyTypeQuery(filter);
                    if (filters.Count > countItem)
                        setQuery += $"{ConvertLogic()} ";
                    continue;
                }

                if (filters.Any(x => x.Conditions is {Count: > 0}))
                {
                    setQuery += $"({filter.Field} {Operators[filter.Type]} {Case(filter)})";
                    if (filters.Count > countItem)
                        setQuery += $"{ConvertLogic()} ";
                }
                else
                {
                    setQuery += ApplyTypeQuery(filter);
                    if (filters.Count > countItem)
                        setQuery += $"{ConvertLogic()} ";
                }

            }

            if (filter.Conditions is not {Count: > 0}) continue;
            setQuery += GenerateNested(filter.Conditions, filter);
            if (filters.Count > countItem)
                setQuery += $"{ConvertLogic()} ";
        }

        setQuery += ")";
        var pattern = @"\band\s*\)";
        var result = Regex.Replace(setQuery, pattern, string.Empty);

        return result;
    }

}