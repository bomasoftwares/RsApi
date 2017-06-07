namespace Boma.RedeSocial.Domain.Common.Enums
{
    public enum AccountType
    {
        Normal = 0,         // Normal
        Seller = 1,         // Comerciante
        Companion = 2       // Acompanhante
    }

    public enum SubscriptionType
    {
        Gratuity = 0,       // Gratuito
        Normal = 1          // Normal
    }

    public enum DocumentType
    {
        Cpf = 0,
        Cnpj = 1
    }

    public enum MaritalStatus
    {
        Married = 0,        // Casado
        Single = 1,         // Solteiro
        Dating = 2,         // Namorando
        Engaged = 3,        // Noivo
        Widower = 4         // Viúvo
    }

    public enum TypePerson
    {
        Man = 0,            // Homem
        Woman = 1,          // Mulher
        HeAndHe = 2,        // Casal - Ele x Ele
        HeAndShe = 3,       // Casal - Ele x Ela
        SheAndShe = 4       // Casal - Ela x Ela
    }

    public enum HairColor
    {
        Black = 0,          // Pretos
        Brown = 1,          // Castanhos
        RedHead = 2,        // Ruivos
        Blond = 3,          // Loiros
        Grey = 4            // Grisalho
    }

    public enum EyeColor
    {
        Other = 0,          // Outro
        Black = 1,          // Pretos
        Brown = 2,          // Castanhos
        Green = 3,          // Verdes
        Blue = 4,           // Azul
        Gray = 5            // Cinza
    }


}
