
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APImovies.Data
{
    public partial class MovieTrailerContext : DbContext
    {
       

        public MovieTrailerContext(DbContextOptions<MovieTrailerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MovieURL> MovieURLs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieURL>(entity =>
            {
                entity.ToTable("MovieURL");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.VideoUrl)
                    .HasMaxLength(500)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

