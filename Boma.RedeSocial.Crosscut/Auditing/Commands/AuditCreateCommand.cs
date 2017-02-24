using System;

namespace Boma.RedeSocial.Crosscut.Auditing.Commands
{
    public class AuditCreateCommand: AuditCommand
    {
        public AuditCreateCommand(string message, object @object)
        {
            Message = message;
            ExecutedDate = DateTime.UtcNow;
            AuditObject = @object;
        }
    }
}
