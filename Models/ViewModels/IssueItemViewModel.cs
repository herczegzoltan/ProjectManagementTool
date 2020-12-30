using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementTool.Models.ViewModels
{
    public class IssueItemViewModel
    {
        public Project Project { get; set; }
        public ICollection<IssueItem> IssueItems { get; set; }
    }
}
