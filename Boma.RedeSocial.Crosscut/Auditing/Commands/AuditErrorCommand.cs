using System;

namespace Boma.RedeSocial.Crosscut.Auditing.Commands
{
    public class AuditErrorCommand : AuditCommand
    {
        public Exception Exception { get; set; }
        public AuditErrorCommand(string message, Exception exception)
        {
            Message = message;
            ExecutedDate = DateTime.UtcNow;
            Exception = exception;
            AuditObject = exception;
        }
    }
}
