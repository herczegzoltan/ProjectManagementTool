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
using ProjectManagementTool.Utility;

namespace ProjectManagementTool.Areas.User.Controllers
{
    [Area("User")]
    public class IssueItemController : Controller
    {
        private readonly ApplicationDbContext _db;

        public IssueItemViewModel IssueItemViewModel { get; set; }

        public IssueItemController(ApplicationDbContext db)
        {
            _db = db;
            IssueItemViewModel = new IssueItemViewModel();
        }

        // GET
        public async Task<IActionResult> Index(int? id)
        {
            // TODO in case id is null i need to validate

            if (id == null)
            {
                id = HttpContext.Session.GetInt32(SD.SelectedProjectId);
            }
            var issueItemsByProjectId = await _db.IssueItems.Where(i => i.ProjectId == id).ToListAsync();
        
            IssueItemViewModel.Project = await _db.Projects.FindAsync(id);
            IssueItemViewModel.IssueItems = issueItemsByProjectId;

            HttpContext.Session.SetInt32(SD.SelectedProjectId, (int)id);

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
                var selectedProjectId = HttpContext.Session.GetInt32(SD.SelectedProjectId);

                if (selectedProjectId != null)
                {
                    issueItem.ProjectId = (int)selectedProjectId;

                    _db.IssueItems.Add(issueItem);
                }

                await _db.SaveChangesAsync();
                return RedirectToAction("Index",new {id = selectedProjectId});
            }

            return View(issueItem);
        }

        // GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueItem = await _db.IssueItems.FindAsync(id);

            if (issueItem == null)
            {
                return NotFound();
            }

            return View(issueItem);
        }

        // POST - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IssueItem issueItem)
        {
            if (ModelState.IsValid)
            {
                var selectedProjectId = HttpContext.Session.GetInt32(SD.SelectedProjectId);

                var issueDb = await _db.IssueItems.FindAsync(issueItem.Id);
                issueDb.Title = issueItem.Title;
                issueDb.Description = issueItem.Description;
                issueDb.Priority = issueItem.Priority;
                issueDb.Status = issueItem.Status;
                issueDb.Type = issueItem.Type;
                
                await _db.SaveChangesAsync();

                return RedirectToAction("Index", new { id = selectedProjectId });
            }

            return View(issueItem);
        }

        // GET - DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueItem = await _db.IssueItems.FindAsync(id);

            if (issueItem == null)
            {
                return NotFound();
            }

            return View(issueItem);
        }

    }
}
