using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.User
{
    public class UserViewModelBase
    {
        [Required]
        [MinLength(6)]
        public string name { get; set; }

        [Required]
        [MinLength(6)]
        public string password { get; set; }
    }
}
