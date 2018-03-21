using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pelis.Models
{
    public class Actor
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        [StringLength(100)]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Date of birth")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [NotMapped]
        public int Age
        {
            get { return DateTime.Today.Year - this.DOB.Year; }
        }
        
        //joint entity (what movies the actor has appeared in)
        public IEnumerable<MovieActor> ActorMovies { get; set; }

        //for View purposes
        //MAYBE: move to ViewModel?
        [NotMapped]
        public IEnumerable<Movie> Movies { get; set; }
    }
}
