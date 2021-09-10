using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.User
{
    public class UserLoginResponseViewModel
    {
        public string Token { get; set; }
        public DateTime ValidoHasta { get; set; }
    }
}
