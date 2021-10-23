public static class IQueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool should, Expression<Func<T, bool>> expression)
        => should
            ? query.Where(expression)
            : query;
}