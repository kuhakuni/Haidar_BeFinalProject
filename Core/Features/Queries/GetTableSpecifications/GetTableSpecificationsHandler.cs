using MediatR;
using Persistence.Redis;
using Persistence.Repositories;

namespace Core.Features.Queries.GetTableSpecifications;

public class GetTableSpesificationsHandler : IRequestHandler<GetTableSpesificationsQuery, GetTableSpesificationsResponse>
{
    private readonly ITableSpecificationRepository _tableSpecificationRepository;
    private readonly ICacheService _cacheService;


    public GetTableSpesificationsHandler(ITableSpecificationRepository tableSpecificationRepository, ICacheService cacheService)
    {
        _tableSpecificationRepository = tableSpecificationRepository;
        _cacheService = cacheService;
    }

    public async Task<GetTableSpesificationsResponse> Handle(GetTableSpesificationsQuery query, CancellationToken cancellationToken)
    {
        bool isRedisActive = _cacheService.IsRedisActive();
        string cacheKey = $"TableSpecification-{query.TableSpecificationId}";

        if (isRedisActive)
        {
            var cachedResponse = _cacheService.Get<GetTableSpesificationsResponse>(cacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }
        }

        var tableSpecification = await _tableSpecificationRepository.GetByIdAsync(query.TableSpecificationId);

        if (tableSpecification is null)
        {
            return new GetTableSpesificationsResponse();
        }

        var response = new GetTableSpesificationsResponse
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