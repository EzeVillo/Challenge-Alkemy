using Challenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Interfaces
{
    public interface IPeliculas_SeriesRepository : IRepository<Pelicula_Serie>
    {
        public Pelicula_Serie GetPelicula_Serie(int id);
    }
}
