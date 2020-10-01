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
    public class StudentsController : Controller
    {
        private readonly DataBaseContext _context;

        public StudentsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index(string sortOrder, string SearchStringFullName, string SearchStringGroup, DateTime SearchStringDateOfBirth)
        {
            ViewData["FullNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GroupSortParm"] = sortOrder == "Group" ? "group_desc" : "Group";
            ViewData["DateOfBirthSortParm"] = sortOrder == "DateOfBirth" ? "dateofbirth_desc" : "DateOfBirth";

            var student = from s in _context.Student
                            select s;

            if (!String.IsNullOrEmpty(SearchStringFullName))
            {
                student = student.Where(s => s.FullName.Contains(SearchStringFullName));
                ViewData["CurrentFullNameFilter"] = SearchStringFullName;
            }
            if (!String.IsNullOrEmpty(SearchStringGroup))
            {
                student = student.Where(s => s.Group.Contains(SearchStringGroup));
                ViewData["CurrentGroupFilter"] = SearchStringGroup;
            }
            if (SearchStringDateOfBirth != null && SearchStringDateOfBirth != DateTime.Parse("1/1/0001 12:00:00 AM"))
            {
                student = student.Where(s => s.DateOfBirth.Equals(SearchStringDateOfBirth));
                ViewData["CurrentDateOfBirthFilter"] = SearchStringDateOfBirth.ToShortDateString();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    student = student.OrderByDescending(s => s.FullName);
                    break;
                case "Group":
                    student = student.OrderByDescending(s => s.Group);
                    break;
                case "group_desc":
                    student = student.OrderBy(s => s.Group);
                    break;
                case "DateOfBirth":
                    student = student.OrderBy(s => s.DateOfBirth);
                    break;
                case "dateofbirth_desc":
                    student = student.OrderByDescending(s => s.DateOfBirth);
                    break;
                default:
                    student = student.OrderBy(s => s.FullName);
                    break;
            }

            return View(await student.AsNoTracking().ToListAsync());
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FullName,Group,DateOfBirth")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FullName,Group,DateOfBirth")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
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
            return View(student);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.ID == id);
        }
    }
}
