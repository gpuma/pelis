using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace pelis.Models
{
    public class Movie
    {
        public int ID { get; set; }
        
        [Required]
        //longest movie title is about 200 characters AFAIK
        [StringLength(300)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [Range(1800, 3000)]
        [Display(Name = "Year")]
        public int ReleaseYear { get; set; }
        
        [Required]
        // constraint: movie can have only one director
        public string Director { get; set; }

        //joint entity (what actors appear in this movie)
        public IEnumerable<MovieActor> ActorMovies { get; set; }

        //for View purposes
        [NotMapped]
        public IEnumerable<Actor> Actors { get; set; }
    }
}
