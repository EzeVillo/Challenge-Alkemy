using Challenge.Entities;
using Challenge.ViewModels.Personajes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.Peliculas_Series
{
    public class Peliculas_SeriesPostRequestViewModel : Peliculas_PersonajesViewModelBase
    {
        [Range(1, 5)]
        public int Calificacion { get; set; }
    }
}
