using Persistence.Models;

namespace Persistence.Repositories;

public interface ITodoRepository : IGenericRepository<Todo>
{
    Task AddAsync(Todo todo);
    IQueryable<Todo> GetAllAsync();
    Task<Todo> GetByIdAsync(Guid tableId);
    Task DeleteAsync(Todo todo);
}