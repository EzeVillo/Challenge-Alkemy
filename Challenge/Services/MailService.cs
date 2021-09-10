using Challenge.Entities;
using Challenge.Interfaces;
using Challenge.ViewModels.User;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Services
{
    public class MailService : IMailService
    {
        private readonly ISendGridClient _client;
        private const string Mail = "ezequielvillordo92@gmail.com";
        private const string Name = "Martin Ezequiel Villordo";

        public MailService(ISendGridClient client)
        {
            _client = client;
        }

        public async Task SendMail(UserRegisterRequestViewModel User)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Mail, Name),
                Subject = "Usted se ha registrado correctamente",
                PlainTextContent = $"Usted se ha creado un usuario con nombre {User.name}"
            };
            msg.AddTo(new EmailAddress(User.email, User.name));
            await _client.SendEmailAsync(msg);
        }
    }
}
