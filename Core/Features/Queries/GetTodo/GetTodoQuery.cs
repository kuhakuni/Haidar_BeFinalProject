using MediatR;

namespace Core.Features.Queries.GetTodo;

public class GetTodoQuery : IRequest<GetTodoResponse>
{
    public int currentPage { get; set; }
}