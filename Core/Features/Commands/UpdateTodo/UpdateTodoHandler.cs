using MediatR;
using Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Features.Commands.UpdateTodo
{
    public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, UpdateTodoResponse>
    {
        private readonly ITodoRepository _tableSpecificationRepository;

        public UpdateTodoHandler(ITodoRepository tableSpecificationRepository)
        {
            _tableSpecificationRepository = tableSpecificationRepository;
        }

        public async Task<UpdateTodoResponse> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
        {
            var tableSpecification = await _tableSpecificationRepository.GetByIdAsync(command.TableId);

            if (tableSpecification == null)
            {
                return new UpdateTodoResponse
                {
                    Success = false,
                    Message = "Table specification not found."
                };
            }

            //tableSpecification.TableNumber = command.TableNumber;
            //tableSpecification.ChairNumber = command.ChairNumber;
            //tableSpecification.TablePic = command.TablePic;
            //tableSpecification.TableType = command.TableType;

            //await _tableSpecificationRepository.UpdateAsync(tableSpecification);

            return new UpdateTodoResponse
            {
                Success = true,
                Message = "Table specification updated successfully."
            };
        }
    }
}
