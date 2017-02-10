namespace Boma.RedeSocial.Domain.Files
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
