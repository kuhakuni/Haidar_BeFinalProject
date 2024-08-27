using MediatR;
using Persistence.Redis;
using Persistence.Repositories;
using System.Diagnostics;

namespace Core.Features.Queries.GetTodo;

public class GetTodoHandler : IRequestHandler<GetTodoQuery, GetTodoResponse>
{
    private readonly ITodoRepository _todoRepository;
    private readonly ICacheService _cacheService;


    public GetTodoHandler(ITodoRepository todoRepository, ICacheService cacheService)
    {
        _todoRepository = todoRepository;
        _cacheService = cacheService;
    }

    public async Task<GetTodoResponse> Handle(GetTodoQuery query, CancellationToken cancellationToken)
    {
        bool isRedisActive = _cacheService.IsRedisActive();
        string cacheKey = $"Todo-{query.currentPage}";

        if (isRedisActive)
        {
            var cachedResponse = _cacheService.Get<GetTodoResponse>(cacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }
        }

        var todos =  _todoRepository.GetAllAsync();

        if (todos is null)
        {
            return new GetTodoResponse();
        }

        var response = new GetTodoResponse
        {
            Todos = todos.Select(todo => new TodoItemResponse
            {
                Day = todo.Day,
                TodayDate = todo.TodayDate,
                DetailCount = todo.DetailCount,
                Note = todo.Note,
                details = todo.TodoDetails.Select(item => new TodoDetailItemResponse
                {
                    Activity = item.Activity,
                    DetailNote = item.DetailNote,
                    Category = item.Category
                }).ToList()
            }).ToList()
        };

        if (isRedisActive)
        {
            _cacheService.Add(cacheKey, response);
        }

        return response;
    }
}