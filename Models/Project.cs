using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ProjectManagementTool.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Status { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
