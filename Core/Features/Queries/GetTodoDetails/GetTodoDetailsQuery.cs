using Core.Features.Queries.GetTodo;
using MediatR;

namespace Core.Features.Queries.GetTodoDetails;

public class GetTodoDetailsQuery : IRequest<GetTodoDetailsResponse>
{
    public Guid Id {  get; set; }
}