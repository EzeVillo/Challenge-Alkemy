using Challenge.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.Peliculas_Series
{
    public class Peliculas_SeriesPutRequestViewModel : Peliculas_PersonajesViewModelBase
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Calificacion { get; set; }
    }
}
