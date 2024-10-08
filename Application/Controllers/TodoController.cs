﻿using Core.Features.Queries.GetTodo;
using MediatR;
using Persistence.DatabaseContext;
using Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using Core.Features.Commands.CreateTodo;
using Core.Features.Commands.UpdateTodo;
using Core.Features.Commands.DeleteTodo;
using Core.Features.Queries.GetTodo;
using Core.Features.Queries.GetTodoDetails;
using Core.Features.Commands.CreateTodoDetails;
using Microsoft.AspNetCore.Authorization;


namespace Application.Controllers;

[Authorize]
public class TodoController : BaseController
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
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

    [HttpGet("v1/todo/detail/{id}")]
    public async Task<GetTodoDetailsResponse> GetTodosDetailByID(Guid id)
    {
        var request = new GetTodoDetailsQuery()
        {
            Id = id
        };

        var response = await _mediator.Send(request);
        return response;
    }

    [HttpPost("v1/todo/add")]
    public async Task<CreateTodoResponse> CreateTodo([FromBody] CreateTodoCommand command)
    {
        var response = await _mediator.Send(command);
        return response;
    }

    [HttpPost("v1/todo/detail/add-bulk")]
    public async Task<CreateTodoDetailsResponse> CreateTodoDetailsBulk([FromBody] CreateTodoDetailsCommand command)
    {
        var response = await _mediator.Send(command);
        return response;
    }


    [HttpPut("v1/todo/{id}")]
    public async Task<IActionResult> UpdateTodo(Guid id, [FromBody] UpdateTodoCommand command)
    {
        if (command == null || id != command.TodoId)
        {
            return BadRequest("Invalid todo data.");
        }

        var response = await _mediator.Send(command);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response.Message);
    }

    [HttpDelete("v1/todo/delete/{id}")]
    public async Task<IActionResult> DeleteTodo(Guid id)
    {
        var command = new DeleteTodoCommand
        {
            TodoId = id
        };

        var response = await _mediator.Send(command);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response.Message);
    }
}