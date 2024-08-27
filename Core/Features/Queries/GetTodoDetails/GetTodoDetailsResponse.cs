using Persistence.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Features.Queries.GetTodoDetails;

public class GetTodoDetailsResponse
{
    public string Activity { get; set; }
    public string DetailNote { get; set; }
    public string Category { get; set; }
}
