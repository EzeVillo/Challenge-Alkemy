using Challenge.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Contexts
{
    public class DisneyContext : DbContext
    {
        public DisneyContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Genero> Generos { get; set; } = null!;
        public DbSet<Personaje> Personajes { get; set; } = null!;
        public DbSet<Pelicula_Serie> Peliculas_Series { get; set; } = null!;
    }
}
