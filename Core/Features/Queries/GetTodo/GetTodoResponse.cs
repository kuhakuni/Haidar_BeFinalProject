using Persistence.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Features.Queries.GetTodo;

public class TodoItemResponse
{

    public string Day { get; set; }
    public DateTime TodayDate { get; set; }
    public string Note { get; set; }
    public int DetailCount { get; set; }
    public List<TodoDetailItemResponse> details { get; set; }
}

public class TodoDetailItemResponse
{
    public string Activity { get; set; }
    public string DetailNote { get; set; }
    public string Category { get; set; }
}

public class GetTodoResponse
{
    public List<TodoItemResponse> Todos { get; set; }
}