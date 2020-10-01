using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuppliersProducts.Data;
using SuppliersProducts.Models;

namespace SuppliersProducts.Controllers
{
    public class PassingsController : Controller
    {
        private readonly DataBaseContext _context;

        public PassingsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string sortOrder, string searchStringName, DateTime searchStringDate, string searchStringPlace)
        {
            var dataBaseContext = _context.Passings.Include(o => o.Teacher).Include(o => o.LabWork);

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PlaceSortParm"] = sortOrder == "Place" ? "place_desc" : "Place";
            ViewData["LabWorkSortParm"] = sortOrder == "LabWork" ? "labwork_desc" : "LabWork";
            ViewData["TeacherSortParm"] = sortOrder == "Teacher" ? "teacher_desc" : "teacher";


            var passing = from s in dataBaseContext
                               select s;

            if (!String.IsNullOrEmpty(searchStringName))
            {
                passing = passing.Where(s => s.Name.Contains(searchStringName));
                ViewData["CurrentNameFilter"] = searchStringName;
            }
            if (searchStringDate != null && searchStringDate != DateTime.Parse("1/1/0001 12:00:00 AM"))
            {
                passing = passing.Where(s => s.Date.Equals(searchStringDate));
                ViewData["CurrentDateFilter"] = searchStringDate.ToShortDateString();
            }
            if (!String.IsNullOrEmpty(searchStringPlace))
            {
                passing = passing.Where(s => s.Place.Contains(searchStringPlace));
                ViewData["CurrentPlaceFilter"] = searchStringPlace;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    passing = passing.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    passing = passing.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    passing = passing.OrderByDescending(s => s.Date);
                    break;
                case "Place":
                    passing = passing.OrderBy(s => s.Place);
                    break;
                case "place_desc":
                    passing = passing.OrderByDescending(s => s.Place);
                    break;
                case "LabWork":
                    passing = passing.OrderBy(s => s.LabWork.Name);
                    break;
                case "labwork_desc":
                    passing = passing.OrderByDescending(s => s.LabWork.Name);
                    break;
                case "Teacher":
                    passing = passing.OrderBy(s => s.Teacher.FullName);
                    break;
                case "teacher_desc":
                    passing = passing.OrderByDescending(s => s.Teacher.FullName);
                    break;

                default:
                    passing = passing.OrderBy(s => s.Name);
                    break;
            }


            return View(await passing.AsNoTracking().ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Passings
                .Include(o => o.Teacher)
                .Include(o => o.LabWork)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "ID", "Address");
            ViewData["LabWorkID"] = new SelectList(_context.LabWorks, "ID", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LabWorkID,TeacherID,Name,Date,Place")] Passing passing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "ID", "Address", passing.TeacherID);
            ViewData["LabWorkID"] = new SelectList(_context.LabWorks, "ID", "Name", passing.LabWorkID);
            return View(passing);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passing = await _context.Passings.FindAsync(id);
            if (passing == null)
            {
                return NotFound();
            }
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "ID", "Address", passing.TeacherID);
            ViewData["LabWorkID"] = new SelectList(_context.LabWorks, "ID", "Name", passing.LabWorkID);
            return View(passing);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LabWorkID,TeacherID,Name,Date,Place")] Passing passing)
        {
            if (id != passing.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassingExists(passing.ID))
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
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "ID", "Address", passing.TeacherID);
            ViewData["LabWorkID"] = new SelectList(_context.LabWorks, "ID", "Name", passing.LabWorkID);
            return View(passing);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passing = await _context.Passings
                .Include(o => o.Teacher)
                .Include(o => o.LabWork)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (passing == null)
            {
                return NotFound();
            }

            return View(passing);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passing = await _context.Passings.FindAsync(id);
            _context.Passings.Remove(passing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassingExists(int id)
        {
            return _context.Passings.Any(e => e.ID == id);
        }
    }
}
