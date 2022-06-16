using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvcolMusicIdentity.Areas.Identity.Data;
using AvcolMusicIdentity.Models;
using AvcolMusicIdentity;

namespace AvcolMusicIdentity.Views.Classes
{
    public class ClassesController : Controller
    {
        private readonly MusicContext _context;

        public ClassesController(MusicContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Date_asc" : "";
            ViewData["TeacherIDSortParm"] = sortOrder == "TeacherID" ? "TeacherID_desc" : "TeacherID";
            ViewData["StudentIDSortParm"] = sortOrder == "StudentID" ? "StudentID_desc" : "StudentID";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var classes = from c in _context.Class.Include(c => c.Student).Include(c => c.Teacher)
                          select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                classes = classes.Where(s => s.TeacherID.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Date_asc":
                    classes = classes.OrderBy(s => s.Date);
                    break;
                case "StudentID":
                    classes = classes.OrderBy(s => s.StudentID);
                    break;
                case "StudentID_desc":
                    classes = classes.OrderByDescending(s => s.StudentID);
                    break;
                case "TeacherID":
                    classes = classes.OrderBy(s => s.TeacherID);
                    break;
                case "TeacherID_desc":
                    classes = classes.OrderByDescending(s => s.TeacherID);
                    break;
                default:
                    classes = classes.OrderByDescending(s => s.Date);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Class>.CreateAsync(classes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .Include(c => c.Student)
                    .ThenInclude(m => m.MusicTimetables)
                    .ThenInclude(g => g.Group)
                    .ThenInclude(l => l.Lessons)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentID");
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "TeacherID", "TeacherID");
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassID,StudentID,TeacherID,Date,StartTime,EndTime")] Class @class)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentID", @class.StudentID);
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "TeacherID", "TeacherID", @class.TeacherID);
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentID", @class.StudentID);
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "TeacherID", "TeacherID", @class.TeacherID);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassID,StudentID,TeacherID,Date,StartTime,EndTime")] Class @class)
        {
            if (id != @class.ClassID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.ClassID))
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
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentID", @class.StudentID);
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "TeacherID", "TeacherID", @class.TeacherID);
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .Include(c => c.Student)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @class = await _context.Class.FindAsync(id);
            _context.Class.Remove(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Class.Any(e => e.ClassID == id);
        }
    }
}
