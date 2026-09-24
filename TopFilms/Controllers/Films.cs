using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Numerics;
using TopFilms.Models;

namespace TopFilms.Controllers
{
    public class FilmsController(FilmsContext context, IWebHostEnvironment appEnvironment) : Controller
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

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckFilm(string name, string director, int year)
        {
            
            return Json(!_context.Films.Any(f => f.Name == name && f.Director == director && f.Year == year));
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Director,Genre,Year,Poster,Info")] Film film,IFormFile? posterFile)
        {
            if (!ModelState.IsValid) return View(film);


            if (posterFile is not null && posterFile.Length > 0)
            {
                var fileName = Path.GetFileName(posterFile.FileName);
                var relativePath = $"/posters/{fileName}";
                var absolutePath = Path.Combine(appEnvironment.WebRootPath, "posters", fileName);

                await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await posterFile.CopyToAsync(fileStream);
                }
                film.Poster = relativePath;
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Director,Genre,Year,Poster,Info")] Film film, IFormFile? posterFile)
        {
            if (id != film.Id) return NotFound();
            if (!ModelState.IsValid) return View(film);

            var oldFilm = await _context.Films.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

            if (oldFilm is null) return NotFound();

            if (posterFile is not null && posterFile.Length > 0)
            {
                var fileName = Path.GetFileName(posterFile.FileName);
                var relativePath = $"/posters/{fileName}";
                var absolutePath = Path.Combine(appEnvironment.WebRootPath, "posters", fileName);

                await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await posterFile.CopyToAsync(fileStream);
                }
                film.Poster = relativePath;
            }
            else
            {
                film.Poster = oldFilm.Poster;
            }
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

        public async Task<IActionResult> About()
        {
            return View();
        }

        public async Task<IActionResult> Contact()
        {
            return View();
        }
        private bool FilmExists(int id) => _context.Films.Any(e => e.Id == id);
    }
}
