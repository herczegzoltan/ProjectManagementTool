using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementTool.Data;

namespace ProjectManagementTool.Areas.User.Controllers
{
    [Area("User")]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public ProjectController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _db.Projects.ToListAsync());
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }
    }
}
