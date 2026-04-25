using Microsoft.EntityFrameworkCore;
using Services.Core.DomainCore;
using Services.Core.Entities;
using Services.Interfaces;
using System.Linq.Expressions;

namespace Messages.Database.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly MessagesDbContext _dbContext;
        private DbSet<T> _dbSet = null;
        private DbSet<T> table = null;
        public Repository(MessagesDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            table = _dbContext.Set<T>();
        }

        public void Delete(T entity)
        {
            entity.DateDeleted = DateTime.Now;
            //_dbContext.Entry(entity).State = EntityState.Modified;
            //_dbSet.Remove(entity);
        }

        public T Find(params object[] keyValues)
        {

            return _dbSet.Find(keyValues);
        }

        public IEnumerable<T> FindAll()
        {
            return _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            entity.DateEntered = DateTime.Now;
            table.Add(entity);
        }

        public IQueryable<T> Queryable()
        {
            return _dbSet;
        }

        public void Update(T entity)
        {
            entity.DateUpdated = DateTime.Now;
            //_dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
            return;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
            return;
        }

        public void Clear()
        {
            table.RemoveRange(table);
        }

        public async Task<T> FindByIdAsync(int id, Expression<Func<T, object>> includes = null)
        {
            if (includes == null) return await table.Where(b => b.DateDeleted == default(DateTime) && b.Id == id).SingleOrDefaultAsync();
            return await table.Where(b => b.DateDeleted == default(DateTime) && b.Id == id).Include(includes).SingleOrDefaultAsync();
        }

        public async Task<T> FindBySurrogateAsync(Expression<Func<T, bool>> wheres, Expression<Func<T, object>> includes = null)
        {
            if (includes != null) return await table.ActiveRows().Where(wheres).Include(includes).SingleOrDefaultAsync();

            return await table.ActiveRows().Where(wheres).SingleOrDefaultAsync();
        }
    }
}
