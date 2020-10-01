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
    public class TeachersController : Controller
    {
        private readonly Course_ProjectContext _context;

        public TeachersController(Course_ProjectContext context)
        {
            _context = context;
        }

        // GET: Buyers
        public async Task<IActionResult> Index(string sortOrder, string SearchStringName, string SearchStringGroup)
        {
            ViewData["FullNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "address_desc" : "Address";

            var teachers = from s in _context.Teachers
                             select s;

            if (!String.IsNullOrEmpty(SearchStringName))
            {
                teachers = teachers.Where(s => s.FullName.Contains(SearchStringName));
                ViewData["CurrentFullNameFilter"] = SearchStringName;
            }
            if (!String.IsNullOrEmpty(SearchStringGroup))
            {
                teachers = teachers.Where(s => s.Address.Contains(SearchStringGroup));
                ViewData["CurrentGroupFilter"] = SearchStringGroup;
            }
            switch (sortOrder)
            {
                case "fullname_desc":
                    teachers = teachers.OrderByDescending(s => s.FullName);
                    break;
                case "Date":
                    teachers = teachers.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    teachers = teachers.OrderByDescending(s => s.Date);
                    break;
                case "Address":
                    teachers = teachers.OrderBy(s => s.Address);
                    break;
                case "address_desc":
                    teachers = teachers.OrderByDescending(s => s.Address);
                    break;
                default:
                    teachers = teachers.OrderBy(s => s.FullName);
                    break;
            }

            return View(await teachers.AsNoTracking().ToListAsync());
        }

        // GET: Buyers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Teachers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        // GET: Buyers/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buyers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,FullName,Date,Address")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Buyers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Teachers.FindAsync(id);
            if (buyer == null)
            {
                return NotFound();
            }
            return View(buyer);
        }

        // POST: Buyers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FullName,Date,Address")] Teacher teacher)
        {
            if (id != teacher.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.ID))
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
            return View(teacher);
        }

        // GET: Buyers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Teachers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        // POST: Buyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buyer = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(buyer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.ID == id);
        }
    }
}
