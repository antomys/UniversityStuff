using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuppliersProducts.Data;
using SuppliersProducts.Models;

namespace SuppliersProducts.Controllers
{
    public class StudentLabWorksController : Controller
    {
        private readonly DataBaseContext _context;

        public StudentLabWorksController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: SupplierProducts
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.StudentLabWork.Include(s => s.LabWork).Include(s => s.Student);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: SupplierProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentLabWork = await _context.StudentLabWork
                .Include(s => s.LabWork)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentLabWork == null)
            {
                return NotFound();
            }

            return View(studentLabWork);
        }

        // GET: SupplierProducts/Create
        public IActionResult Create()
        {
            ViewData["LabWorkID"] = new SelectList(_context.LabWorks, "ID", "Name");
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "FullName");
            return View();
        }

        // POST: SupplierProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LabWorkID,StudentID")] StudentLabWork studentLabWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentLabWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LabWorkID"] = new SelectList(_context.LabWorks, "ID", "Name", studentLabWork.LabWorkID);
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "FullName", studentLabWork.StudentID);
            return View(studentLabWork);
        }

        // GET: SupplierProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentLabWork = await _context.StudentLabWork.FindAsync(id);
            if (studentLabWork == null)
            {
                return NotFound();
            }
            ViewData["LabWorkID"] = new SelectList(_context.LabWorks, "ID", "Name", studentLabWork.LabWorkID);
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "FullName", studentLabWork.StudentID);
            return View(studentLabWork);
        }

        // POST: SupplierProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LabWorkID,StudentID")] StudentLabWork studentLabWork)
        {
            if (id != studentLabWork.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentLabWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentLabWorkExists(studentLabWork.ID))
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
            ViewData["LabWorkID"] = new SelectList(_context.LabWorks, "ID", "Name", studentLabWork.LabWorkID);
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "FullName", studentLabWork.StudentID);
            return View(studentLabWork);
        }

        // GET: SupplierProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentLabWork = await _context.StudentLabWork
                .Include(s => s.LabWork)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentLabWork == null)
            {
                return NotFound();
            }

            return View(studentLabWork);
        }

        // POST: SupplierProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentLabWork = await _context.StudentLabWork.FindAsync(id);
            _context.StudentLabWork.Remove(studentLabWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentLabWorkExists(int id)
        {
            return _context.StudentLabWork.Any(e => e.ID == id);
        }
    }
}
