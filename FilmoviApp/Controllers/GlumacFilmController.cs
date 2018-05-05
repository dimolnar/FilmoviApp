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
    public class GlumacFilmController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GlumacFilmController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GlumacFilm
        public async Task<IActionResult> Index(string film,string glumac)
        {
            var FilmL = new List<string>();
            var GlumacL = new List<string>();

            var FilmQ = from g in _context.GlumacFilm.Include(g => g.Film)
                        orderby g.Film.Naziv
                        select g.Film.Naziv;

            var GlumacQ = from g in _context.GlumacFilm.Include(g => g.Glumac)
                          orderby g.Glumac.ImeIPrezime
                          select g.Glumac.ImeIPrezime;

            FilmL.AddRange(FilmQ.Distinct());
            ViewBag.film = new SelectList(FilmL);

            GlumacL.AddRange(GlumacQ.Distinct());
            ViewBag.glumac = new SelectList(GlumacL);

            var glumciFilmovi = from g in _context.GlumacFilm.Include(g => g.Film).Include(g => g.Glumac)
                                select g;

            if (!string.IsNullOrEmpty(film))
            {
                glumciFilmovi = glumciFilmovi.Where(x => x.Film.Naziv == film);
            }

            if (!string.IsNullOrEmpty(glumac))
            {
                glumciFilmovi = glumciFilmovi.Where(x => x.Glumac.ImeIPrezime == glumac);
            }
            //var applicationDbContext = _context.GlumacFilm.Include(g => g.Film).Include(g => g.Glumac);
            return View(glumciFilmovi.ToList());
        }

        // GET: GlumacFilm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glumacFilm = await _context.GlumacFilm
                .Include(g => g.Film)
                .Include(g => g.Glumac)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (glumacFilm == null)
            {
                return NotFound();
            }

            return View(glumacFilm);
        }

        // GET: GlumacFilm/Create
        public IActionResult Create()
        {
            ViewData["FilmId"] = new SelectList(_context.Film, "FilmId", "Naziv");
            ViewData["GlumacId"] = new SelectList(_context.Glumac, "GlumacId", "ImeIPrezime");
            return View();
        }

        // POST: GlumacFilm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FilmId,GlumacId")] GlumacFilm glumacFilm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(glumacFilm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmId"] = new SelectList(_context.Film, "FilmId", "Naziv", glumacFilm.FilmId);
            ViewData["GlumacId"] = new SelectList(_context.Glumac, "GlumacId", "ImeIPrezime", glumacFilm.GlumacId);
            return View(glumacFilm);
        }

        // GET: GlumacFilm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glumacFilm = await _context.GlumacFilm.SingleOrDefaultAsync(m => m.Id == id);
            if (glumacFilm == null)
            {
                return NotFound();
            }
            ViewData["FilmId"] = new SelectList(_context.Film, "FilmId", "Naziv", glumacFilm.FilmId);
            ViewData["GlumacId"] = new SelectList(_context.Glumac, "GlumacId", "ImeIPrezime", glumacFilm.GlumacId);
            return View(glumacFilm);
        }

        // POST: GlumacFilm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FilmId,GlumacId")] GlumacFilm glumacFilm)
        {
            if (id != glumacFilm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(glumacFilm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlumacFilmExists(glumacFilm.Id))
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
            ViewData["FilmId"] = new SelectList(_context.Film, "FilmId", "Naziv", glumacFilm.FilmId);
            ViewData["GlumacId"] = new SelectList(_context.Glumac, "GlumacId", "ImeIPrezime", glumacFilm.GlumacId);
            return View(glumacFilm);
        }

        // GET: GlumacFilm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glumacFilm = await _context.GlumacFilm
                .Include(g => g.Film)
                .Include(g => g.Glumac)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (glumacFilm == null)
            {
                return NotFound();
            }

            return View(glumacFilm);
        }

        // POST: GlumacFilm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var glumacFilm = await _context.GlumacFilm.SingleOrDefaultAsync(m => m.Id == id);
            _context.GlumacFilm.Remove(glumacFilm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GlumacFilmExists(int id)
        {
            return _context.GlumacFilm.Any(e => e.Id == id);
        }
    }
}
