using MediatR;
using Persistence.Repositories;
using Persistence.Models;
using Persistence.Redis;

namespace Core.Features.Commands.CreateTableSpecifications;

public class CreateTableSpecificationsHandler : IRequestHandler<CreateTableSpecificationsCommand, CreateTableSpecificationsResponse>
{
    private readonly ITableSpecificationRepository _tableSpecificationRepository;
    private readonly ICacheService _cacheService;

    public CreateTableSpecificationsHandler(ITableSpecificationRepository tableSpecificationRepository, ICacheService cacheService)
    {
        _tableSpecificationRepository = tableSpecificationRepository;
        _cacheService = cacheService;
    }

    public async Task<CreateTableSpecificationsResponse> Handle(CreateTableSpecificationsCommand command, CancellationToken cancellationToken)
    {
        var newTableSpecification = new TableSpecification
        {
            TableId = Guid.NewGuid(),
            TableNumber = command.TableNumber,
            ChairNumber = command.ChairNumber,
            TablePic = command.TablePic,
            TableType = command.TableType
        };

        await _tableSpecificationRepository.AddAsync(newTableSpecification);
        if (_cacheService.IsRedisActive())
        {
            _cacheService.Add($"TableSpecification-{newTableSpecification.TableId}", newTableSpecification);
        }

        return new CreateTableSpecificationsResponse
        {
            TableId = newTableSpecification.TableId,
            TableNumber = newTableSpecification.TableNumber,
            ChairNumber = newTableSpecification.ChairNumber,
            TablePic = newTableSpecification.TablePic,
            TableType = newTableSpecification.TableType
        };
    }
}
