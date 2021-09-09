using Challenge.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.Peliculas_Series
{
    public class Peliculas_SeriesDetallesResponseViewModel : Peliculas_SeriesViewModelBase
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Calificacion { get; set; }
        public List<string> Personajes { get; set; }
        public string Genero { get; set; }
    }
}
