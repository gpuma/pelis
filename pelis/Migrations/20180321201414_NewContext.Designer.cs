﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using pelis.Models;
using System;

namespace pelis.Migrations
{
    [DbContext(typeof(PelisContext))]
    [Migration("20180321201414_NewContext")]
    partial class NewContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("pelis.Models.Actor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DOB");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("pelis.Models.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Director")
                        .IsRequired();

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("ReleaseYear");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("ID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("pelis.Models.MovieActor", b =>
                {
                    b.Property<int>("MovieId");

                    b.Property<int>("ActorId");

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("MovieActors");
                });

            modelBuilder.Entity("pelis.Models.MovieActor", b =>
                {
                    b.HasOne("pelis.Models.Actor", "Actor")
                        .WithMany("ActorMovies")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("pelis.Models.Movie", "Movie")
                        .WithMany("ActorMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
