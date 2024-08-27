using MediatR;
using Persistence.Repositories;
using Persistence.Models;
using Persistence.Redis;

namespace Core.Features.Commands.CreateTodo;

public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, CreateTodoResponse>
{
    private readonly ITodoRepository _todoRepository;
    private readonly ICacheService _cacheService;

    public CreateTodoHandler(ITodoRepository todoRepository, ICacheService cacheService)
    {
        _todoRepository = todoRepository;
        _cacheService = cacheService;
    }

    public async Task<CreateTodoResponse> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var newTodo = new Todo
        {
            TodoId = Guid.NewGuid(),
            TodayDate = DateTime.Now,
            Day = command.Day,
            Note = command.Note,
            DetailCount = command.DetailCount
        };

        await _todoRepository.AddAsync(newTodo);
        if (_cacheService.IsRedisActive())
        {
            _cacheService.Add($"Todo-{newTodo.TodoId}", newTodo);
        }

        return new CreateTodoResponse
        {
            Success = true,
            Message = "Todo created successfully."
        };
    }
}
