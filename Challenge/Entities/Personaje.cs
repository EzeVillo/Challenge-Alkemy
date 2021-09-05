﻿using Challenge.ViewModels.Peliculas_Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Entities
{
    public class Personaje
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Peso { get; set; }
        public string Historia { get; set; }
        public ICollection<Pelicula_Serie> Peliculas_Series { get; set; }
    }
}
