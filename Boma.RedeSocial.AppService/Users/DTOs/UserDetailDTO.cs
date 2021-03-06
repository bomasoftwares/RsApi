﻿using Boma.RedeSocial.AppService.Users.Profiles.DTOs;
using System;

namespace Boma.RedeSocial.AppService.Users.DTOs
{
    public class UserDetailDTO
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string UrlProfilePhoto { get; set; }
        public int AccountType { get; set; }
        public string AccountTypeDescription { get; set; }
        
    }
}
