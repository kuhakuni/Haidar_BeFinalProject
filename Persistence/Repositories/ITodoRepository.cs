using Persistence.Models;

namespace Persistence.Repositories;

public interface ITodoRepository : IGenericRepository<Todo>
{
    Task AddAsync(Todo todo);
    IQueryable<Todo> GetAll();
    Task<Todo> GetByIdAsync(Guid tableId);
    Task DeleteAsync(Todo todo);
}