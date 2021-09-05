using Challenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Interfaces
{
    public interface IPersonajesRepository : IRepository<Personaje>
    {
        Personaje GetPersonaje(int id);
    }
}
