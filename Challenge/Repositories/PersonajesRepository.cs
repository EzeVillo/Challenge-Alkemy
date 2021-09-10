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
    public class PersonajesRepository : BaseRepository<Personaje, DisneyContext>, IPersonajesRepository
    {
        public PersonajesRepository(DisneyContext dbContext) : base(dbContext)
        {
        }
        public Personaje GetPersonaje(int id)
        {
            return DbSet.Include(x => x.Peliculas_Series).FirstOrDefault(x => x.Id == id);
        }
        public List<Personaje> GetPersonajes()
        {
            return DbSet.Include(x => x.Peliculas_Series).ToList();
        }
    }
}

