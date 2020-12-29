using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementTool.Data;
using ProjectManagementTool.Models;

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
        
        // POST -CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _db.Projects.Add(project);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }
    }
}
