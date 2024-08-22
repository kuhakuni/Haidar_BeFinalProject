using Persistence.Models;

namespace Persistence.Repositories;

public interface ITableSpecificationRepository : IGenericRepository<TableSpecification>
{
    Task AddAsync(TableSpecification tableSpecification); // Add this method
    Task<TableSpecification> GetByIdAsync(Guid tableId);
    Task UpdateAsync(TableSpecification tableSpecification);
    Task DeleteAsync(TableSpecification tableSpecification);
}