using Persistence.Models;

namespace Persistence.Repositories;

public interface ITodoDetailRepository : IGenericRepository<TodoDetail>
{
    Task AddAsync(TodoDetail todoDetail);
    Task AddAllAsync(IEnumerable<TodoDetail> todoDetails);
    Task<List<TodoDetail>> GetAllAsync();
    Task<TodoDetail> GetByIdAsync(Guid tableId);
    Task DeleteAsync(TodoDetail TodoDetail);
}