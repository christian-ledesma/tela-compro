using System.Linq.Expressions;

namespace TelaCompro.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> Get(int id);
        Task<IQueryable<T>> GetQueryable();
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
