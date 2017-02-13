using Boma.RedeSocial.Domain.Files.Interfaces;

namespace Boma.RedeSocial.Domain.Files.Entities
{
    public class Video: File, IVideo
    {
        public Video(string url, string name)
        {
            Name = name;
            Url = url;
        }
    }
}
