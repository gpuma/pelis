using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public int ReleaseYear { get; set; }
        
        [Required]
        // constraint: movie can have only one director
        public string Director { get; set; }

        public List<MovieActor> Actors { get; set; }
    }
}
