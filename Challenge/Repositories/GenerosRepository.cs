using Challenge.Contexts;
using Challenge.Entities;
using Challenge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Repositories
{
    public class GenerosRepository : BaseRepository<Genero, DisneyContext>, IGenerosRepository
    {
        public GenerosRepository(DisneyContext dbContext) : base(dbContext)
        {
        }
    }
}
