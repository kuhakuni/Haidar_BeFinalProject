using MediatR;

namespace Core.Features.Commands.UpdateTodo
{
    public class UpdateTodoCommand : IRequest<UpdateTodoResponse>
    {
        public Guid TableId { get; set; }
        public int TableNumber { get; set; }
        public int ChairNumber { get; set; }
        public string TablePic { get; set; }
        public string TableType { get; set; }
    }
}
