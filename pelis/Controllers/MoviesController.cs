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
    }
}