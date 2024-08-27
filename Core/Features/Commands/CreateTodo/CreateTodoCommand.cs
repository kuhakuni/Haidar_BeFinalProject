using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Core.Features.Commands.CreateTodo;

public class CreateTodoCommand : IRequest<CreateTodoResponse>
{
    public string Day { get; set; }
    public string Note { get; set; }
    public int DetailCount { get; set; }
}
