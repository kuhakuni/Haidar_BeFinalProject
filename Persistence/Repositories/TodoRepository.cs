
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Persistence.Models;

namespace Persistence.Repositories;

public class TodoRepository : GenericRepository<Todo>, ITodoRepository
{
    private readonly TableContext _context;
    public TodoRepository(TableContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Todo> GetAllAsync()
    {
        return _context.Todo.Include(Todo => Todo.TodoDetails).OrderByDescending(Todo => Todo.TodayDate);
    }

    public async Task<Todo> GetById(Guid id)
    {
        return await _context.Todo.FindAsync(id);
    }

    public async Task AddAsync(Todo todo)
    {
        await _context.Todo.AddAsync(todo);
        await _context.SaveChangesAsync();
    }

    public async Task<Todo> GetByIdAsync(Guid tableId)
    {
        return await _context.Todo.FindAsync(tableId);
    }

    public async Task UpdateAsync(Todo todo)
    {
        _context.Todo.Update(todo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Todo todo)
    {
        _context.Todo.Remove(todo);
        await _context.SaveChangesAsync();
    }
}