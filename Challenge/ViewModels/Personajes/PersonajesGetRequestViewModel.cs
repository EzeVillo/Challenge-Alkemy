using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.Personajes
{
    public class PersonajesGetRequestViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public int idMovie { get; set; }
    }
}
