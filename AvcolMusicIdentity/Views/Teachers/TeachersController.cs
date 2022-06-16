using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvcolMusicIdentity.Models;
using AvcolMusicIdentity.Areas.Identity.Data;

namespace AvcolMusicIdentity.Views.Teachers
{
    public class TeachersController : Controller
    {
        private readonly MusicContext _context;

        public TeachersController(MusicContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int searchInt, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewData["SurnameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Surname_desc" : "";
            ViewData["FirstnameSortParm"] = sortOrder == "Firstname" ? "Firstname_desc" : "Firstname";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var teachers = from s in _context.Teacher
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                teachers = teachers.Where(s => s.Surname.Contains(searchString)
                                       || s.Firstname.Contains(searchString)
                                       || s.TeacherID.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Surname_desc":
                    teachers = teachers.OrderByDescending(s => s.Surname);
                    break;
                case "ID":
                    teachers = teachers.OrderBy(s => s.TeacherID);
                    break;
                case "ID_desc":
                    teachers = teachers.OrderByDescending(s => s.TeacherID);
                    break;
                case "Firstname":
                    teachers = teachers.OrderBy(s => s.Firstname);
                    break;
                case "Firstname_desc":
                    teachers = teachers.OrderByDescending(s => s.Firstname);
                    break;
                default:
                    teachers = teachers.OrderBy(s => s.Surname);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Teacher>.CreateAsync(teachers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .Include(c => c.Classes)
                    .ThenInclude(s => s.Student)
                    .ThenInclude(c => c.MusicTimetables)
                .ThenInclude(g => g.Group)
                    .ThenInclude(l => l.Lessons)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TeacherID == id);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherID,Surname,Firstname")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TeacherID,Surname,Firstname")] Teacher teacher)
        {
            if (id != teacher.TeacherID)
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
                    if (!TeacherExists(teacher.TeacherID))
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

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(string id)
        {
            return _context.Teacher.Any(e => e.TeacherID == id);
        }
    }
}
