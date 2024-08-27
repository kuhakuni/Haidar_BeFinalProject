using Persistence.DatabaseContext;

namespace Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly TableContext _context;

    public GenericRepository(TableContext context)
    {
        _context = context;
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? GetById(Guid id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Remove(T itemToRemove)
    {
        _context.Set<T>().Remove(itemToRemove);
    }
    public async Task<T> Create(T itemToCreate)
    {
        await _context.Set<T>().AddAsync(itemToCreate);
        return itemToCreate;
    }

    public async Task<T> Update(T itemToUpdate)
    {
        await Task.Run(() => _context.Set<T>().Update(itemToUpdate));
        return itemToUpdate;
    }

    public void AddAll(IEnumerable<T> itemsToAdd)
    {
         _context.Set<T>().AddRange(itemsToAdd);
    }
}