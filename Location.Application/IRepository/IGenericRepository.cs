

namespace Location.Application.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>1 si la création s'est deroulée correctement </returns>
        Task<int> Add(T student);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>The all dataset.</returns>
        List<T> GetAll();

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>The object dataset.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task<int> UpdateAsync(T location);

        /// <summary>
        /// Deletes the entity by the specified primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        void DeleteAsync(int Id);
 
       
    }
}
