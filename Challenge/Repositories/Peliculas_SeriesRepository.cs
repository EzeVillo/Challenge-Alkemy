using Challenge.Contexts;
using Challenge.Entities;
using Challenge.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Repositories
{
    public class Peliculas_SeriesRepository : BaseRepository<Pelicula_Serie, DisneyContext>, IPeliculas_SeriesRepository
    {
        public Peliculas_SeriesRepository(DisneyContext dbContext) : base(dbContext)
        {
        }

        public Pelicula_Serie GetPelicula_Serie(int id)
        {
            return DbSet.Include(x => x.Personajes).FirstOrDefault(x => x.Id == id);
        }
    }
}
