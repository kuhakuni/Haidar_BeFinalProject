using MediatR;
using Persistence.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.Features.Commands.CreateTodoDetails;

public class CreateTodoDetailsCommand : IRequest<CreateTodoDetailsResponse>
{
    public List<TodoDetailCommand> TodoDetails { get; set; }
}

public class TodoDetailCommand
{
    public Guid TodoId { get; set; }
    public string Activity { get; set; }
    public string Category { get; set; }
    public string DetailNote { get; set; }
}
