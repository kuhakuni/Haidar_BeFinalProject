using Core.Features.Queries.GetTableSpecifications;
using MediatR;
using Persistence.DatabaseContext;
using Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using Core.Features.Commands.CreateTableSpecifications;
using Core.Features.Commands.UpdateTableSpecification;
using Core.Features.Commands.DeleteTableSpecification;
using Core.Features.Queries.GetTodo;


namespace Application.Controllers;

public class TodoController : BaseController
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("v1/todo/all")]
    public async Task<GetTodoResponse> GetAllTodos(int page)
    {
        var request = new GetTodoQuery()
        {
            currentPage= page
        };
        var response = await _mediator.Send(request);
        return response;
    }
    //[HttpPost("v1/table/specification")]
    //public async Task<CreateTableSpecificationsResponse> CreateTableSpecifications([FromBody] CreateTableSpecificationsCommand command)
    //{
    //    var response = await _mediator.Send(command);
    //    return response;
    //}
    //[HttpPut("v1/table/specification/{id}")]
    //public async Task<IActionResult> UpdateTableSpecification(Guid id, [FromBody] UpdateTableSpecificationCommand command)
    //{
    //    if (command == null || id != command.TableId)
    //    {
    //        return BadRequest("Invalid table specification data.");
    //    }

    //    var response = await _mediator.Send(command);

    //    if (response.Success)
    //    {
    //        return Ok(response);
    //    }

    //    return BadRequest(response.Message);
    //}

    //[HttpDelete("v1/table/specification/{id}")]
    //public async Task<IActionResult> DeleteTableSpecification(Guid id)
    //{
    //    var command = new DeleteTableSpecificationCommand
    //    {
    //        TableId = id
    //    };

    //    var response = await _mediator.Send(command);

    //    if (response.Success)
    //    {
    //        return Ok(response);
    //    }

    //    return BadRequest(response.Message);
    //}
}