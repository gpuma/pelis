using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using pelis.Models;

namespace pelis.Controllers
{
    [Route("[controller]")]
    public class MoviesController : Controller
    {
        private readonly PelisContext _context;

        public MoviesController(PelisContext context)
        {
            _context = context;
        }

        //GET: movies/
        //no need to explicitly say action since it's the index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        //GET: movies/id
        [HttpGet("{movieId}")]
        public async Task<IActionResult> Details(int movieId)
        {
            var movie = await _context.Movies
                //related entities (two-levels)
                .Include(m => m.ActorMovies)
                .ThenInclude(am => am.Actor)
                .SingleOrDefaultAsync(m => m.ID == movieId);

            if (movie == null)
            {
                return NotFound();
            }
            var vm = new MovieDetailsViewModel();
            // get the actors in a nice collection for the View
            movie.Actors = movie.ActorMovies.Select(x => x.Actor);
            vm.Movie = movie;
            //actors that don't appear in the movie already
            vm.AvailableActors = await _context.Actors.Except(movie.Actors).ToListAsync();
            vm.SelectedMovieId = movieId;
            return View(vm);
        }

        //GET: movies/add
        [HttpGet("[action]")]
        public IActionResult Add()
        {
            return View();
        }

        //POST: movies/add
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        //form data is bound to argument automatically
        public async Task<IActionResult> Add(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                //returning the page with the same data
                //for an unsuccessful post
                return View(movie);
            }
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}