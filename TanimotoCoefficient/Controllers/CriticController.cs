using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TanimotoCoefficient.Data;
using TanimotoCoefficient.Models;

namespace TanimotoCoefficient.Controllers
{
    public class CriticController : Controller
    {
        private readonly TanimotoCoefficientContext _context;

        public CriticController(TanimotoCoefficientContext context)
        {
            _context = context;
        }

        // GET: Critic
        public async Task<IActionResult> Index(string criticName, string searchString)
        {
            // Use LINQ to get list of critics.
            IQueryable<string> criticQuery = from m in _context.Critic
                                            orderby m.FullName
                                            select m.FullName;

            var movies = from m in _context.Critic
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.MovieName.Contains(searchString)).OrderBy(s => s.FullName).ThenBy(s => s.MovieName);
            }

            if (!string.IsNullOrEmpty(criticName))
            {
                movies = movies.Where(x => x.FullName == criticName).OrderBy(x => x.FullName).ThenBy(x => x.MovieName);
            }

            movies = movies.OrderBy(x => x.FullName).ThenBy(x => x.MovieName);

            var criticReviewVM = new CriticReviewViewModel
            {
                Critics = new SelectList(await criticQuery.Distinct().ToListAsync()),
                MoviesRatedByCritic = await movies.ToListAsync()
            };

            return View(criticReviewVM);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Critic/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var critic = await _context.Critic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (critic == null)
            {
                return NotFound();
            }

            return View(critic);
        }

        // GET: Critic/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Critic/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,MovieName,Rating")] Critic critic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(critic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(critic);
        }

        // GET: Critic/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var critic = await _context.Critic.FindAsync(id);
            if (critic == null)
            {
                return NotFound();
            }
            return View(critic);
        }

        // POST: Critic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,MovieName,Rating")] Critic critic)
        {
            if (id != critic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(critic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriticExists(critic.Id))
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
            return View(critic);
        }

        // GET: Critic/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var critic = await _context.Critic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (critic == null)
            {
                return NotFound();
            }

            return View(critic);
        }

        // POST: Critic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var critic = await _context.Critic.FindAsync(id);
            _context.Critic.Remove(critic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriticExists(int id)
        {
            return _context.Critic.Any(e => e.Id == id);
        }
    }
}
