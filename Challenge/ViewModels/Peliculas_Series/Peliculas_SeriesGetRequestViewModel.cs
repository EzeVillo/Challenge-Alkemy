using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.Peliculas_Series
{
    public class Peliculas_SeriesGetRequestViewModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string name { get; set; }
        public int genre { get; set; }
        public string order { get; set; }
    }
}
