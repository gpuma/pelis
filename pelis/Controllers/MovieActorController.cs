using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pelis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pelis.Controllers
{
    [Route("[controller]")]
    public class MovieActorController : Controller
    {
        private readonly PelisContext _context;

        public MovieActorController(PelisContext context)
        {
            _context = context;
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovieToActor(int SelectedActorId, int SelectedMovieId)
        {
            var actor = await _context.Actors.SingleOrDefaultAsync(a => a.ID == SelectedActorId);
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.ID == SelectedMovieId);
            if (actor == null || movie == null)
            {
                return NotFound();
            }
            //trying to add a movie already asssigned to an actor
            if (MovieActorPairExists(SelectedActorId, SelectedMovieId))
            {
                return BadRequest();
            }

            _context.MovieActors.Add(new MovieActor { ActorId = SelectedActorId, MovieId = SelectedMovieId });
            await _context.SaveChangesAsync();

            //TODO: FIX THIS
            return RedirectToAction(nameof(MoviesController.Index));
        }

        public bool MovieActorPairExists(int actorId, int movieId)
        {
            var moviePair = _context.MovieActors.SingleOrDefault(
                ma => ma.ActorId == actorId && ma.MovieId == movieId
            );
            return moviePair != null;
        }
    }
}
