using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementTool.Models
{
    public class IssueItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string Type { get; set; }
        public enum EType
        {
            Test = 0,
            Task = 1,
            Bug = 2,
            NewFeature = 3,
            Improvement = 4,
        }

        public string Status { get; set; }
        public enum EStatus
        {
            New = 0,
            Closed = 1,
            InProgress = 2,
            OnHold = 3,
        }
        
        public string Priority { get; set; }
        public enum EPriority
        {
            Blocker = 0,
            Major = 1,
            High = 2,
            Medium = 3,
            Normal = 4,
            Low = 5,
            Minor = 6,
            None = 7
        }
    }
}
