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
            
            // get the actors in a nice collection for the View
            actor.Movies = actor.ActorMovies.Select(x => x.Movie);
            //TODO: probably shouldn't go here
            //gets all movies where the actor doesn't appear in
            actor.AllMovies = await _context.Movies.Except(actor.Movies).ToListAsync();
            return View(actor);
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
            //TODO: check if movie has been added already to the actor

            _context.MovieActors.Add(new MovieActor { ActorId = actorId, MovieId = SelectedMovieId });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), actorId);
        }

        //GET: actors/add
        [HttpGet("[action]")]
        public IActionResult Add()
        {
            return View();
        }
    }
}