using Services.Core.Entities;

namespace Services.Core.DomainCore
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> ActiveRows<T>(this IQueryable<T> query) where T : Entity
        {
            return query.Where(x => x.DateDeleted == default);
        }

        public static IQueryable<T> PageQuery<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
