
namespace CleanArchJobs.Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        Task<T> GetByIdAsync(int Id);
        Task <int> AddAsync(T item);
        Task<int> UpdateAsync(T item);
        void DeleteAsync(int Id);
    }
}
