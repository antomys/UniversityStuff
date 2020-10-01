using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Course_Project.Data;
using Course_Project.Models;

namespace Course_Project.Controllers
{
    public class LabWorksController : Controller
    {
        private readonly Course_ProjectContext _context;

        public LabWorksController(Course_ProjectContext context)
        {
            _context = context;
        }

        // GET: Products
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchStringName, string searchStringType)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "type_desc" : "Type";

            var labwork = from s in _context.LabWorks
                           select s;
            if (!String.IsNullOrEmpty(searchStringName))
            {
                labwork = labwork.Where(s => s.Name.Contains(searchStringName));
                ViewData["CurrentNameFilter"] = searchStringName;
            }
            if (!String.IsNullOrEmpty(searchStringType))
            {
                labwork = labwork.Where(s => s.Type.Contains(searchStringType));
                ViewData["CurrentTypeFilter"] = searchStringType;
            }
            switch (sortOrder)
            {
                case "name_desc":
                    labwork = labwork.OrderByDescending(s => s.Name);
                    break;
                case "Type":
                    labwork = labwork.OrderByDescending(s => s.Type);
                    break;
                case "type_desc":
                    labwork = labwork.OrderBy(s => s.Type);
                    break;
                default:
                    labwork = labwork.OrderBy(s => s.Name);
                    break;
            }

            return View(await labwork.AsNoTracking().ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labwork = await _context.LabWorks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (labwork == null)
            {
                return NotFound();
            }

            return View(labwork);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Type")] LabWork product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labwork = await _context.LabWorks.FindAsync(id);
            if (labwork == null)
            {
                return NotFound();
            }
            return View(labwork);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Type")] LabWork labwork)
        {
            if (id != labwork.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labwork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabWorkExists(labwork.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(labwork);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labwork = await _context.LabWorks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (labwork == null)
            {
                return NotFound();
            }

            return View(labwork);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.LabWorks.FindAsync(id);
            _context.LabWorks.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabWorkExists(int id)
        {
            return _context.LabWorks.Any(e => e.ID == id);
        }
    }
}
