using Boma.RedeSocial.Domain.Users.Interfaces;
using System;

namespace Boma.Rs.Api.Context
{
    public class User : IUser
    {
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public User(Guid id, string email, string userName)
        {
            Id = id;
            Email = email;
            UserName = userName;
        }

    }
}