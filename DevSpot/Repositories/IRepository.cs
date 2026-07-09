namespace DevSpot.Respositories
{
    // Generic repository of type T where T shd be a class
    public interface IRespository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();//this returns as task of IEnumerable of genric type T
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);//enity is anything we can add to our db
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}