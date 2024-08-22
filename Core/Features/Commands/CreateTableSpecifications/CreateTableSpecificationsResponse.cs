using MediatR;
using Persistence.Repositories;
using Persistence.Models;

namespace Core.Features.Commands.CreateTableSpecifications;

public class CreateTableSpecificationsHandler : IRequestHandler<CreateTableSpecificationsCommand, CreateTableSpecificationsResponse>
{
    private readonly ITableSpecificationRepository _tableSpecificationRepository;

    public CreateTableSpecificationsHandler(ITableSpecificationRepository tableSpecificationRepository)
    {
        _tableSpecificationRepository = tableSpecificationRepository;
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
