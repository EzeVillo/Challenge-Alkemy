using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.Personajes
{
    public class PersonajesDetallesResponseViewModel : PersonajesViewModelBase
    {
        public int Id { get; set; }
        public int Edad { get; set; }
        public int Peso { get; set; }
        public string Historia { get; set; }
        public ICollection<string> Peliculas_Series { get; set; }
    }
}
