using System.Linq.Expressions;

namespace Services.Interfaces
{
    public interface IRepository<T>
    {
        T Find(params object[] keyValues);
        Task<T> FindByIdAsync(int id, Expression<Func<T, object>> includes = null);

        Task<T> FindBySurrogateAsync(Expression<Func<T, bool>> wheres, Expression<Func<T, object>> includes = null);

        IEnumerable<T> FindAll();
        void Add(T entity);
        void Update(T entity);
        void Clear();
        void Delete(T entity);
        void SaveChanges();
        Task SaveChangesAsync();
        IQueryable<T> Queryable();
    }
}
