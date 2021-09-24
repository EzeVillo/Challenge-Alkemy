using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Entities
{
    public class Genero : Entity
    {
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public ICollection<Pelicula_Serie> Peliculas_Series { get; set; }
    }
}
