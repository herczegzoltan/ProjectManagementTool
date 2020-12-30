using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementTool.Data;
using ProjectManagementTool.Models;
using ProjectManagementTool.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace ProjectManagementTool.Areas.User.Controllers
{
    [Area("User")]
    public class IssueItemController : Controller
    {
        private readonly ApplicationDbContext _db;

        ////[BindProperty]
        public IssueItemViewModel IssueItemViewModel { get; set; }

        public IssueItemController(ApplicationDbContext db)
        {
            _db = db;
            IssueItemViewModel = new IssueItemViewModel();
        }

        // GET
        public async Task<IActionResult> Index(int id)
        {
            // TODO in case id is null i need to validate
            
            var issueItemsByProductId = await _db.Projects.Where(p=>p.Id == id).SelectMany(i=>i.IssueItems).ToListAsync();

            IssueItemViewModel.Project = await _db.Projects.FindAsync(id);
            IssueItemViewModel.IssueItems = issueItemsByProductId;

            HttpContext.Session.SetInt32("selectedProjectId", id);

            return View(IssueItemViewModel);
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }  
        
        // POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IssueItem issueItem)
        {
            if (ModelState.IsValid)
            {
                var selectedProjectId = HttpContext.Session.GetInt32("selectedProjectId");

                if (selectedProjectId != null)
                {
                    var selectedProject = await _db.Projects.FindAsync(selectedProjectId);

                    if (selectedProject.IssueItems == null)
                    {
                        selectedProject.IssueItems = new List<IssueItem>() {issueItem};
                    }
                    else
                    {
                        selectedProject.IssueItems.Add(issueItem);
                    }
                }

                await _db.SaveChangesAsync();
                return RedirectToAction("Index",new {id = selectedProjectId});
            }

            return View(issueItem);
        }
    }
}
