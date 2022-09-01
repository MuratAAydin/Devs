using System.Linq.Dynamic.Core;
using System.Text;

namespace Core.Persistence.Dynamic;

public static class IQueryableDynamicFilterExtensions
{
    private static readonly IDictionary<string, string>
        Operators = new Dictionary<string, string>
        {
            { "eq", "=" },
            { "neq", "!=" },
            { "lt", "<" },
            { "lte", "<=" },
            { "gt", ">" },
            { "gte", ">=" },
            { "isnull", "== null" },
            { "isnotnull", "!= null" },
            { "startswith", "StartsWith" },
            { "endswith", "EndsWith" },
            { "contains", "Contains" },
            { "doesnotcontain", "Contains" }
        };

    public static IQueryable<T> ToDynamic<T>(
        this IQueryable<T> query, Dynamic dynamic)
    {
        if (dynamic.Filter is not null) query = Filter(query, dynamic.Filter);
        if (dynamic.Sort is not null && dynamic.Sort.Any()) query = Sort(query, dynamic.Sort);
        return query;
    }

    private static IQueryable<T> Filter<T>(
        IQueryable<T> queryable, Filter filter)
    {
        var filters = GetAllFilters(filter);
        var values = filters.Select(f => f.Value).ToArray();
        var where = Transform(filter, filters);
        queryable = queryable.Where(where, values);

        return queryable;
    }

    private static IQueryable<T> Sort<T>(
        IQueryable<T> queryable, IEnumerable<Sort> sort)
    {
        if (sort.Any())
        {
            var ordering = string.Join(",", sort.Select(s => $"{s.Field} {s.Dir}"));
            return queryable.OrderBy(ordering);
        }

        return queryable;
    }

    public static IList<Filter> GetAllFilters(Filter filter)
    {
        List<Filter> filters = new();
        GetFilters(filter, filters);
        return filters;
    }

    private static void GetFilters(Filter filter, IList<Filter> filters)
    {
        filters.Add(filter);
        if (filter.Filters is not null && filter.Filters.Any())
            foreach (var item in filter.Filters)
                GetFilters(item, filters);
    }

    public static string Transform(Filter filter, IList<Filter> filters)
    {
        var index = filters.IndexOf(filter);
        var comparison = Operators[filter.Operator];
        StringBuilder where = new();

        if (!string.IsNullOrEmpty(filter.Value))
        {
            if (filter.Operator == "doesnotcontain")
                where.Append($"(!np({filter.Field}).{comparison}(@{index}))");
            else if (comparison == "StartsWith" ||
                     comparison == "EndsWith" ||
                     comparison == "Contains")
                where.Append($"(np({filter.Field}).{comparison}(@{index}))");
            else
                where.Append($"np({filter.Field}) {comparison} @{index}");
        }
        else if (filter.Operator == "isnull" || filter.Operator == "isnotnull")
        {
            where.Append($"np({filter.Field}) {comparison}");
        }

        if (filter.Logic is not null && filter.Filters is not null && filter.Filters.Any())
            return
                $"{where} {filter.Logic} ({string.Join($" {filter.Logic} ", filter.Filters.Select(f => Transform(f, filters)).ToArray())})";

        return where.ToString();
    }
}