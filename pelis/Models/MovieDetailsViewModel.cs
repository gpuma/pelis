using System.Collections.Generic;

namespace pelis.Models
{
    public class MovieDetailsViewModel
    {
        public Movie Movie { get; set; }
        public IEnumerable<Actor> AvailableActors { get; set; }
        public int SelectedActorId { get; set; }
        public int SelectedMovieId { get; set; }
    }
}
