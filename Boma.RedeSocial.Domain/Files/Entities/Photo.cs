namespace Boma.RedeSocial.Domain.Files.Entities
{
    public class Photo: File
    {
        public Photo(string url, string name)
        {
            Name = name;
            Url = url;
        }
    }
}
