using MediatR;
using Persistence.Redis;
using Persistence.Repositories;
using System.Diagnostics;

namespace Core.Features.Queries.GetTodoDetails;

public class GetTodoDetailsHandler : IRequestHandler<GetTodoDetailsQuery, GetTodoDetailsResponse>
{
    private readonly ITodoDetailRepository _todoDetailRepository;
    private readonly ICacheService _cacheService;


    public GetTodoDetailsHandler(ITodoDetailRepository todoDetailRepository, ICacheService cacheService)
    {
        _todoDetailRepository = todoDetailRepository;
        _cacheService = cacheService;
    }

    public async Task<GetTodoDetailsResponse> Handle(GetTodoDetailsQuery query, CancellationToken cancellationToken)
    {
        bool isRedisActive = _cacheService.IsRedisActive();
        string cacheKey = $"TodoDetail-{query.Id}";

        if (isRedisActive)
        {
            var cachedResponse = _cacheService.Get<GetTodoDetailsResponse>(cacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }
        }

        var todos = await _todoDetailRepository.GetByIdAsync(query.Id);

        if (todos is null)
        {
            return new GetTodoDetailsResponse();
        }

        var response = new GetTodoDetailsResponse
        {
            Activity = todos.Activity,
            DetailNote = todos.DetailNote,
            Category = todos.Category
        };

        if (isRedisActive)
        {
            _cacheService.Add(cacheKey, response);
        }

        return response;
    }
}