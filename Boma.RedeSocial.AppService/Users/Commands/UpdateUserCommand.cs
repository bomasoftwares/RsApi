using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boma.RedeSocial.AppService.Users.Commands
{
    public class UpdateUserCommand
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UrlProfilePhoto { get; set; }
        public int? AccountType { get; set; }
    }
}
