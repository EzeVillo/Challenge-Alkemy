using Challenge.ViewModels.Peliculas_Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.Personajes
{
    public class PersonajesPostRequestViewModel : PersonajesViewModelBase
    {
        public int Edad { get; set; }
        public int Peso { get; set; }
        public string Historia { get; set; }
    }
}
