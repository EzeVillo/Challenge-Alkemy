using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Entities
{
    public class Pelicula_Serie
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        [Range(1,5)]
        public int Calificacion { get; set; }
        public ICollection<Personaje> Personajes { get; set; } = new List<Personaje>();
        public Genero Genero { get; set; }
    }
}
