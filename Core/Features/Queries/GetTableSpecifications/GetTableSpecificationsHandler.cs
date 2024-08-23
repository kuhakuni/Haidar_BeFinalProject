using MediatR;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.GetTableSpecifications;

public class GetTableSpecificationsHandler : IRequestHandler<GetTableSpecificationsQuery, GetTableSpecificationsResponse>
{
    private readonly ITableSpecificationRepository _tableSpecificationRepository;
    private readonly ICacheService _cacheService;


    public GetTableSpecificationsHandler(ITableSpecificationRepository tableSpecificationRepository, ICacheService cacheService)
    {
        _tableSpecificationRepository = tableSpecificationRepository;
        _cacheService = cacheService;
    }

    public async Task<GetTableSpecificationsResponse> Handle(GetTableSpecificationsQuery query, CancellationToken cancellationToken)
    {
        bool isRedisActive = _cacheService.IsRedisActive();
        string cacheKey = $"TableSpecification-{query.TableSpecificationId}";

        if (isRedisActive)
        {
            var cachedResponse = _cacheService.Get<GetTableSpecificationsResponse>(cacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }
        }

        var tableSpecification = await _tableSpecificationRepository.GetByIdAsync(query.TableSpecificationId);

        if (tableSpecification is null)
        {
            return new GetTableSpecificationsResponse();
        }

        var response = new GetTableSpecificationsResponse
        {
            TableId = tableSpecification.TableId,
            ChairNumber = tableSpecification.ChairNumber,
            TableNumber = tableSpecification.TableNumber,
            TablePic = tableSpecification.TablePic,
            TableType = tableSpecification.TableType
        };

        if (isRedisActive)
        {
            _cacheService.Add(cacheKey, response);
        }

        return response;
    }
}