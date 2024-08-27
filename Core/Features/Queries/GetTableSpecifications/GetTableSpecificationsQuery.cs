using MediatR;

namespace Core.Features.Queries.GetTableSpecifications;

public class GetTableSpesificationsQuery : IRequest<GetTableSpesificationsResponse>
{
    public Guid TableSpecificationId { get; set; }
}