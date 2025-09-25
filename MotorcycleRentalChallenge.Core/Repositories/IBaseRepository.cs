using MotorcycleRentalChallenge.Core.Entities;

namespace MotorcycleRentalChallenge.Core.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);

        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);
    }
}
