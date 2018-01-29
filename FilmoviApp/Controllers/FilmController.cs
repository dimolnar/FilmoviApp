using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FilmoviApp.Data;
using FilmoviApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace FilmoviApp.Controllers
{
    [Authorize]
    public class FilmController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilmController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Film
        public async Task<IActionResult> Index(string naziv,string zanr)
        {
            var GenreLst = new List<string>();

            var GenreQry = from f in _context.Film.Include(f=>f.Zanr)
                           orderby f.Zanr.Naziv
                           select f.Zanr.Naziv;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.zanr = new SelectList(GenreLst);
            var filmovi = from f in _context.Film.Include(f=>f.Zanr)
                          select f;
            if (!String.IsNullOrEmpty(naziv))
            {
                filmovi = filmovi.Where(f => f.Naziv.Contains(naziv));
            }
            if (!string.IsNullOrEmpty(zanr))
            {
                filmovi = filmovi.Where(x => x.Zanr.Naziv == zanr);
            }
            //var applicationDbContext = _context.Film.Include(f => f.Redatelj).Include(f => f.Zanr);
            return View(filmovi.ToList());
        }

        // GET: Film/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.Redatelj)
                .Include(f => f.Zanr)
                .SingleOrDefaultAsync(m => m.FilmId == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Film/Create
       
        public IActionResult Create()
        {
            ViewData["RedateljId"] = new SelectList(_context.Redatelj, "RedateljId", "ImeIPrezime");
            ViewData["ZanrId"] = new SelectList(_context.Zanr, "ZanrId", "Naziv");
            return View();
        }

        // POST: Film/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmId,Naziv,IzvorniNaziv,Godina,Trajanje,ZanrId,RedateljId")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RedateljId"] = new SelectList(_context.Redatelj, "RedateljId", "ImeIPrezime", film.RedateljId);
            ViewData["ZanrId"] = new SelectList(_context.Zanr, "ZanrId", "Naziv", film.ZanrId);
            return View(film);
        }

        // GET: Film/Edit/5
     
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.SingleOrDefaultAsync(m => m.FilmId == id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["RedateljId"] = new SelectList(_context.Redatelj, "RedateljId", "ImeIPrezime", film.RedateljId);
            ViewData["ZanrId"] = new SelectList(_context.Zanr, "ZanrId", "Naziv", film.ZanrId);
            return View(film);
        }

        // POST: Film/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmId,Naziv,IzvorniNaziv,Godina,Trajanje,ZanrId,RedateljId")] Film film)
        {
            if (id != film.FilmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.FilmId))
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
            ViewData["RedateljId"] = new SelectList(_context.Redatelj, "RedateljId", "ImeIPrezime", film.RedateljId);
            ViewData["ZanrId"] = new SelectList(_context.Zanr, "ZanrId", "Naziv", film.ZanrId);
            return View(film);
        }

        // GET: Film/Delete/5
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.Redatelj)
                .Include(f => f.Zanr)
                .SingleOrDefaultAsync(m => m.FilmId == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Film/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Film.SingleOrDefaultAsync(m => m.FilmId == id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.FilmId == id);
        }
    }
}
