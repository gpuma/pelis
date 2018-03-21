using System.Collections.Generic;

namespace pelis.Models
{
    public class ActorDetailsViewModel
    {
        public Actor Actor { get; set; }
        public IEnumerable<Movie> AllMovies { get; set; }
        public int SelectedMovieId { get; set; }
    }
}
