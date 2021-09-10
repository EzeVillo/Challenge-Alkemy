using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.Peliculas_Series
{
    public class Peliculas_SeriesGetRequestViewModel
    {
        public string name { get; set; }
        public int genre { get; set; }
        public string order { get; set; }
    }
}
