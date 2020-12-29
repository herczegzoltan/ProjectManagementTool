using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementTool.Areas.User.Controllers
{
    [Area("User")]
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
