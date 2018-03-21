using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pelis.Models
{
    public class PelisContext : DbContext
    {
        public PelisContext(DbContextOptions<PelisContext> options):
            base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //clave compuesta
            modelBuilder.Entity<MovieActor>()
                .HasKey(x => new { x.MovieId, x.ActorId });

            //modelBuilder.Entity<MovieActor>()
            //    .HasOne(ma => ma.Movie)
            //    .WithMany(m => m.Actors)
            //    .HasForeignKey(ma => ma.MovieId);

            //modelBuilder.Entity<MovieActor>()
            //    .HasOne(ma => ma.Actor)
            //    .WithMany(a => a.Movies)
            //    .HasForeignKey(ma => ma.ActorId);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }
}
