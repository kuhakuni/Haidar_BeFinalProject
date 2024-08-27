using MediatR;
using Persistence.Repositories;
using Persistence.Models;
using Persistence.Redis;

namespace Core.Features.Commands.CreateTodoDetails;

public class CreateTodoDetailsHandler : IRequestHandler<CreateTodoDetailsCommand, CreateTodoDetailsResponse>
{
    private readonly ITodoDetailRepository _todoRepository;
    private readonly ICacheService _cacheService;

    public CreateTodoDetailsHandler(ITodoDetailRepository todoRepository, ICacheService cacheService)
    {
        _todoRepository = todoRepository;
        _cacheService = cacheService;
    }

    public async Task<CreateTodoDetailsResponse> Handle(CreateTodoDetailsCommand command, CancellationToken cancellationToken)
    {
        if (command.TodoDetails == null || !command.TodoDetails.Any())
        {
            return new CreateTodoDetailsResponse
            {
                Success = false,
                Message = "No Todo details provided."
            };
        }

        // Create a new list of TodoDetail entities
        var newTodos = command.TodoDetails.Select(detail => new TodoDetail
        {
            TodoDetailId = Guid.NewGuid(),
            TodoId = detail.TodoId,
            Activity = detail.Activity,
            Category = detail.Category,
            DetailNote = detail.DetailNote,
        }).ToList();

        // Add all TodoDetail entities in bulk
        await _todoRepository.AddAllAsync(newTodos);

        return new CreateTodoDetailsResponse
        {
            Success = true,
            Message = "Todo created successfully."
        };
    }
}
