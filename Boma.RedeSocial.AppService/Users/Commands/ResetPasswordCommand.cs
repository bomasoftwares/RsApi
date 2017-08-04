using System.ComponentModel.DataAnnotations;

namespace Boma.RedeSocial.AppService.Users.Commands
{
    public class ResetPasswordCommand
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordResetKey { get; set; }
        public string NewPassword { get; set; }
    }
}
