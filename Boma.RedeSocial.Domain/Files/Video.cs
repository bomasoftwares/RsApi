using Boma.RedeSocial.Domain.Interfaces.Entities;

namespace Boma.RedeSocial.Domain.Files
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
