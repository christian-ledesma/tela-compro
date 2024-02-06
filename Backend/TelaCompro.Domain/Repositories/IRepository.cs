namespace TelaCompro.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
