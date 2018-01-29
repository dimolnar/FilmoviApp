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
    public class ZanrController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZanrController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zanr
        public async Task<IActionResult> Index(string naziv)
        {
            var zanrovi = from z in _context.Zanr
                            select z;
            if (!String.IsNullOrEmpty(naziv))
            {
                zanrovi = zanrovi.Where(z => z.Naziv.Contains(naziv));
            }
            return View(zanrovi.ToList());
        }

        // GET: Zanr/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zanr = await _context.Zanr
                .SingleOrDefaultAsync(m => m.ZanrId == id);
            if (zanr == null)
            {
                return NotFound();
            }

            return View(zanr);
        }

        // GET: Zanr/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zanr/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZanrId,Naziv")] Zanr zanr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zanr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zanr);
        }

        // GET: Zanr/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zanr = await _context.Zanr.SingleOrDefaultAsync(m => m.ZanrId == id);
            if (zanr == null)
            {
                return NotFound();
            }
            return View(zanr);
        }

        // POST: Zanr/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZanrId,Naziv")] Zanr zanr)
        {
            if (id != zanr.ZanrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zanr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZanrExists(zanr.ZanrId))
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
            return View(zanr);
        }

        // GET: Zanr/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zanr = await _context.Zanr
                .SingleOrDefaultAsync(m => m.ZanrId == id);
            if (zanr == null)
            {
                return NotFound();
            }

            return View(zanr);
        }

        // POST: Zanr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zanr = await _context.Zanr.SingleOrDefaultAsync(m => m.ZanrId == id);
            _context.Zanr.Remove(zanr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZanrExists(int id)
        {
            return _context.Zanr.Any(e => e.ZanrId == id);
        }
    }
}
