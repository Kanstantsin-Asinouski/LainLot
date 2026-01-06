namespace DatabaseRepository.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetById(int id);
        IQueryable<T> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}