using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.Personajes
{
    public class PersonajesPutRequestViewModel
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Peso { get; set; }
        public string Historia { get; set; }
    }
}
