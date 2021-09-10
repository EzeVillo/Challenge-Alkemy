using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.ViewModels.User
{
    public class UserRegisterRequestViewModel : UserViewModelBase
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}
