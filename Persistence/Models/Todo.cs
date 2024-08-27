using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    [Table("Todo")]
    public class Todo
    {
        [Key]
        [Required]
        public Guid TodoId { get; set; }

        [Required]
        public string Day { get; set; }

        [Required]
        public DateTime TodayDate { get; set; }

        [Required]
        public string Note { get; set; }

        [Required]
        public int DetailCount { get; set; }

        public ICollection<TodoDetail> TodoDetails { get; set; }
    }
}
