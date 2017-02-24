using System;

namespace Boma.RedeSocial.Crosscut.Auditing.Commands
{
    public abstract class AuditCommand
    {
        public string Message { get; set; }
        public DateTime ExecutedDate { get; set; }
        public object AuditObject { get; set; }
    }
}
