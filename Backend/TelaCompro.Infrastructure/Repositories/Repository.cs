using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TelaCompro.Domain.Repositories;
using TelaCompro.Infrastructure.Persistence;

namespace TelaCompro.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly StoreContext _context;

        public Repository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Create(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity is not null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T?> Get(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var entities = await _context.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable<T>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            var entities = await query.ToListAsync();
            return entities;
        }

        public Task<IQueryable<T>> GetQueryable()
        {
            var queryable = _context.Set<T>().AsQueryable<T>();
            return Task.FromResult(queryable);
        }

        public async Task Update(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
