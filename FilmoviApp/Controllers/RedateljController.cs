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
    public class RedateljController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RedateljController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Redatelj
        public async Task<IActionResult> Index(string ime, string prezime)
        {
            var redatelji = from r in _context.Redatelj
                         select r;
            if (!String.IsNullOrEmpty(ime))
            {
                redatelji = redatelji.Where(r => r.Ime.Contains(ime));
            }
            if (!String.IsNullOrEmpty(prezime))
            {
                redatelji = redatelji.Where(r => r.Prezime.Contains(prezime));
            }
            return View(redatelji.ToList());
        }

        // GET: Redatelj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var redatelj = await _context.Redatelj
                .SingleOrDefaultAsync(m => m.RedateljId == id);
            if (redatelj == null)
            {
                return NotFound();
            }

            return View(redatelj);
        }

        // GET: Redatelj/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Redatelj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RedateljId,Ime,Prezime,DatRod,MjestoRod")] Redatelj redatelj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(redatelj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(redatelj);
        }

        // GET: Redatelj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var redatelj = await _context.Redatelj.SingleOrDefaultAsync(m => m.RedateljId == id);
            if (redatelj == null)
            {
                return NotFound();
            }
            return View(redatelj);
        }

        // POST: Redatelj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RedateljId,Ime,Prezime,DatRod,MjestoRod")] Redatelj redatelj)
        {
            if (id != redatelj.RedateljId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(redatelj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RedateljExists(redatelj.RedateljId))
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
            return View(redatelj);
        }

        // GET: Redatelj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var redatelj = await _context.Redatelj
                .SingleOrDefaultAsync(m => m.RedateljId == id);
            if (redatelj == null)
            {
                return NotFound();
            }

            return View(redatelj);
        }

        // POST: Redatelj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var redatelj = await _context.Redatelj.SingleOrDefaultAsync(m => m.RedateljId == id);
            _context.Redatelj.Remove(redatelj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RedateljExists(int id)
        {
            return _context.Redatelj.Any(e => e.RedateljId == id);
        }
    }
}
