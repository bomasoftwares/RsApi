using System;

namespace Boma.RedeSocial.Crosscut.Exceptions
{
    public class DomainException :Exception
        
    {
        public DomainException() : base("Erro de domínio")
        {

        }

        public DomainException(string message): base(message)
        {
            
        }
    }
}
