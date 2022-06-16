using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvcolMusicIdentity.Models;
using AvcolMusicIdentity.Areas.Identity.Data;

namespace AvcolMusicIdentity.Views.MusicTimetables
{
    public class MusicTimetablesController : Controller
    {
        private readonly MusicContext _context;

        public MusicTimetablesController(MusicContext context)
        {
            _context = context;
        }

        // GET: MusicTimetables
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var musicTimetables = from m in _context.MusicTimetable.Include(m => m.Group).Include(m => m.Student)
                                  select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                musicTimetables = musicTimetables.Where(s => s.Student.Surname.Contains(searchString)
                                       || s.Student.FirstName.Contains(searchString)
                                       || s.Group.Instrument.Contains(searchString));
            }

            int pageSize = 10;
            return View(await PaginatedList<MusicTimetable>.CreateAsync(musicTimetables.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: MusicTimetables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicTimetable = await _context.MusicTimetable
                .Include(m => m.Group)
                .Include(m => m.Student)
                .FirstOrDefaultAsync(m => m.MusicTimetableID == id);
            if (musicTimetable == null)
            {
                return NotFound();
            }

            return View(musicTimetable);
        }

        // GET: MusicTimetables/Create
        public IActionResult Create()
        {
            ViewData["GroupID"] = new SelectList(_context.Group, "GroupID", "GroupID");
            ViewData["StudentID"] = new SelectList(_context.Set<Student>(), "StudentID", "StudentID");
            return View();
        }

        // POST: MusicTimetables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MusicTimetableID,StudentID,GroupID")] MusicTimetable musicTimetable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musicTimetable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupID"] = new SelectList(_context.Group, "GroupID", "GroupID", musicTimetable.GroupID);
            ViewData["StudentID"] = new SelectList(_context.Set<Student>(), "StudentID", "StudentID", musicTimetable.StudentID);
            return View(musicTimetable);
        }

        // GET: MusicTimetables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicTimetable = await _context.MusicTimetable.FindAsync(id);
            if (musicTimetable == null)
            {
                return NotFound();
            }
            ViewData["GroupID"] = new SelectList(_context.Group, "GroupID", "GroupID", musicTimetable.GroupID);
            ViewData["StudentID"] = new SelectList(_context.Set<Student>(), "StudentID", "StudentID", musicTimetable.StudentID);
            return View(musicTimetable);
        }

        // POST: MusicTimetables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MusicTimetableID,StudentID,GroupID")] MusicTimetable musicTimetable)
        {
            if (id != musicTimetable.MusicTimetableID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicTimetable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicTimetableExists(musicTimetable.MusicTimetableID))
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
            ViewData["GroupID"] = new SelectList(_context.Group, "GroupID", "GroupID", musicTimetable.GroupID);
            ViewData["StudentID"] = new SelectList(_context.Set<Student>(), "StudentID", "StudentID", musicTimetable.StudentID);
            return View(musicTimetable);
        }

        // GET: MusicTimetables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicTimetable = await _context.MusicTimetable
                .Include(m => m.Group)
                .Include(m => m.Student)
                .FirstOrDefaultAsync(m => m.MusicTimetableID == id);
            if (musicTimetable == null)
            {
                return NotFound();
            }

            return View(musicTimetable);
        }

        // POST: MusicTimetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicTimetable = await _context.MusicTimetable.FindAsync(id);
            _context.MusicTimetable.Remove(musicTimetable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicTimetableExists(int id)
        {
            return _context.MusicTimetable.Any(e => e.MusicTimetableID == id);
        }
    }
}
