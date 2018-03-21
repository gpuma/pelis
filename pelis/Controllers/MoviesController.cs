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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        [HttpGet("[action]")]
        public IActionResult Add()
        {
            return View();
        }

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