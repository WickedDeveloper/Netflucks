using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Netflucks.Models;

namespace Netflucks.Controllers
{
    public class MoviesController : Controller
    {
        private readonly netflucksContext _context;

        public MoviesController(netflucksContext context)
        {
            _context = context;
        }

        // Create Comment
        public IActionResult CreateComment()
        {
            return RedirectToAction("Create", "Comments");
        }

        // GET: Comments
        //public IActionResult GetComments()
        //{
        //    CommentsController comments = new CommentsController(_context);
        //    if (comments.Create() != null)
        //    {
        //        return Vi
        //    } else
        //    {
        //        return PartialView("No Comments");
        //    }
        //}

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Movie.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //Movie mov = new Movie();
            //Comment c = new Comment();
            //CommentsController cm = new CommentsController(_context);
            //ViewBag.Comments = await _context.Movie.SingleOrDefaultAsync(m =>)
            //ViewBag.Comments = GetComments();
            //var comments = 
            //ViewData["Comment"] = new SelectList(_context.Comment, "MovieId", "MovieComment");
            //IEnumerable<SelectListItem> items = _context.Comment.Select(c => new SelectListItem
            //{
            //    Value = c.MovieId.ToString(),
            //    Text = c.MovieComment
            //});
            ViewData["Comments"] = _context.Comment.Where(c => c.MovieId == id).Select(c => c.MovieComment);

            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .SingleOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,ReleaseDate,Genre,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,ReleaseDate,Genre,Rating")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.MovieId == id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }
    }
}
