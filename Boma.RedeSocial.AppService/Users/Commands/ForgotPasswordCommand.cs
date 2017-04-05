using System.ComponentModel.DataAnnotations;

namespace Boma.RedeSocial.AppService.Users.Commands
{
    public class ForgotPasswordCommand
    {
        [Required]
        public string Email { get; set; }
    }
}
