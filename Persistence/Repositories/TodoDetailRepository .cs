using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Persistence.Models;

namespace Persistence.Repositories;

public class TodoDetailRepository : GenericRepository<TodoDetail>, ITodoDetailRepository
{
    private readonly TableContext _context;
    public TodoDetailRepository(TableContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<TodoDetail>> GetAllAsync()
    {
        return await _context.TodoDetail.ToListAsync();
    }
    public async Task AddAsync(TodoDetail todoDetail)
    {
        await _context.TodoDetail.AddAsync(todoDetail);
        await _context.SaveChangesAsync();
    }

    public async Task<TodoDetail> GetByIdAsync(Guid tableId)
    {
        return await _context.TodoDetail.FindAsync(tableId);
    }

    public async Task UpdateAsync(TodoDetail todoDetail)
    {
        _context.TodoDetail.Update(todoDetail);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TodoDetail todoDetail)
    {
        _context.TodoDetail.Remove(todoDetail);
        await _context.SaveChangesAsync();
    }

    public async Task AddAllAsync(IEnumerable<TodoDetail> todoDetails)
    {
        if (todoDetails == null || !todoDetails.Any())
        {
            throw new ArgumentException("The collection of todo details cannot be null or empty.", nameof(todoDetails));
        }

        _context.TodoDetail.AddRange(todoDetails);
        await _context.SaveChangesAsync(); 
    }
}
