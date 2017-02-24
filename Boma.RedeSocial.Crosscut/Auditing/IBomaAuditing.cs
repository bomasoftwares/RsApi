using Boma.RedeSocial.Crosscut.Auditing.Commands;

namespace Boma.RedeSocial.Crosscut.Auditing
{
    public interface IBomaAuditing
    {
        void Audit(AuditCreateCommand obj);
        void AuditError(AuditErrorCommand obj);
    }
}
