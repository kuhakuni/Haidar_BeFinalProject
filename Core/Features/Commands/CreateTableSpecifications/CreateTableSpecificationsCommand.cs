using MediatR;

namespace Core.Features.Commands.CreateTableSpecifications;

public class CreateTableSpecificationsCommand : IRequest<CreateTableSpecificationsResponse>
{
    public int TableNumber { get; set; }
    public int ChairNumber { get; set; }
    public string TablePic { get; set; }
    public string TableType { get; set; }
}
