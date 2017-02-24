using System;
using Boma.RedeSocial.Crosscut.Auditing;
using Boma.RedeSocial.Crosscut.Auditing.Commands;

namespace Boma.RedeSocial.Infrastructure.Auditing
{
    public class SexMoveAuditing : IBomaAuditing
    {
        public void Audit(AuditCreateCommand obj)
        {
            throw new NotImplementedException();
        }

        public void AuditError(AuditErrorCommand obj)
        {
            throw new NotImplementedException();
        }
    }
}
