using Challenge.Entities;
using Challenge.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Interfaces
{
    public interface IMailService
    {
        Task SendMail(UserRegisterRequestViewModel User);
    }
}
