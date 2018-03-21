using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using pelis.Models;

namespace pelis.Controllers
{
    [Route("[controller]")]
    public class ActorsController : Controller
    {
        private readonly PelisContext _context;

        public ActorsController(PelisContext context)
        {
            _context = context;
        }

        //GET: /actors/
        //no need to explicitly say action since it's the index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actors.ToListAsync());
        }

        //GET: /actors/id
        [HttpGet("{actorId}")]
        public async Task<IActionResult> Details(int actorId)
        {
            var actor = await _context.Actors
                .Include(a => a.ActorMovies)
                .ThenInclude(am => am.Movie)
                .SingleOrDefaultAsync(a => a.ID == actorId);

            if (actor == null)
            {
                return NotFound();
            }
            var vm = new ActorDetailsViewModel();
            // get the actors in a nice collection for the View
            // TODO: maybe move this to the ViewModel?
            actor.Movies = actor.ActorMovies.Select(x => x.Movie);
            vm.Actor = actor;
            //gets all movies where the actor doesn't appear in
            vm.AllMovies = await _context.Movies.Except(actor.Movies).ToListAsync();
            return View(vm);
        }

        //POST: /actors/add
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        //form data is bound to argument automatically
        public async Task<IActionResult> Add(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                //returning the page with the same data
                //for an unsuccessful post
                return View(actor);
            }
            _context.Add(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //POST: /actors/{actorId}/AddMovieToActor
        [HttpPost("{actorId}/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovieToActor(int actorId, int SelectedMovieId)
        {
            var actor = await _context.Actors.SingleOrDefaultAsync(a => a.ID == actorId);
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.ID == SelectedMovieId);
            if (actor == null || movie == null)
            {
                return NotFound();
            }
            //trying to add a movie already asssigned to an actor
            if (MovieActorPairExists(actorId, SelectedMovieId))
            {
                return BadRequest();
            }

            _context.MovieActors.Add(new MovieActor { ActorId = actorId, MovieId = SelectedMovieId });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), actorId);
        }

        public bool MovieActorPairExists(int actorId, int movieId)
        {
            var moviePair = _context.MovieActors.SingleOrDefault(
                ma => ma.ActorId == actorId && ma.MovieId == movieId
            );
            return moviePair != null;
        }

        //GET: actors/add
        [HttpGet("[action]")]
        public IActionResult Add()
        {
            return View();
        }
    }
}