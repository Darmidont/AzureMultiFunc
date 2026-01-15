using System.Linq.Expressions;

namespace BusinessLogic.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> query,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }

        public static async Task<IQueryable<T>> WhereIfAsync<T>(
            this IQueryable<T> query,
            Func<Task<bool>> condition,
            Expression<Func<T, bool>> predicate)
        {
            if (await condition())
                return query.Where(predicate);
            return query;
        }

        public static IQueryable<T> Apply<T>(
            this IQueryable<T> query,
            params Func<IQueryable<T>, IQueryable<T>>[] steps)
        {
            foreach (var step in steps)
            {
                query = step(query);
            }
            return query;
        }
    }
}
