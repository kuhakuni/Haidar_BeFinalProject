using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    [Table("TodoDetail")]
    public class TodoDetail
    {
        [Key]
        [Required]
        public Guid TodoDetailId { get; set; }
        
        [Required]
        public Guid TodoId { get; set; }

        [Required]
        public string Activity { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string DetailNote { get; set; }

    }
}
