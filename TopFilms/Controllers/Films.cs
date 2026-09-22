using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TopFilms.Models;

namespace TopFilms.Controllers
{
    public class FilmsController(FilmsContext context) : Controller
    {
        private readonly FilmsContext _context = context;

        public async Task<IActionResult> Index() =>
            View(await _context.Films.AsNoTracking().ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
        
            var film = await _context.Films
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            return film is null ? NotFound() : View(film);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Director,Genre,Year,Poster,Info")] Film film)
        {
            if (!ModelState.IsValid) return View(film);

            _context.Add(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var film = await _context.Films.FindAsync(id);
            return film is null ? NotFound() : View(film);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Director,Genre,Year,Poster,Info")] Film film)
        {
            if (id != film.Id) return NotFound();
            if (!ModelState.IsValid) return View(film);

            try
            {
                _context.Update(film);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
                if (!FilmExists(film.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var film = await _context.Films
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            return film is null ? NotFound() : View(film);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film is not null)
            {
                _context.Films.Remove(film);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id) => _context.Films.Any(e => e.Id == id);
    }
}
