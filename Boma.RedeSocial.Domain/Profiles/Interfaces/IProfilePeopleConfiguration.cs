using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boma.RedeSocial.Domain.Profiles.Interfaces
{
    public interface IProfilePeopleConfiguration
    {
        string Name { get; set; }
        DateTime BirthDate { get; set; }
        int HairColor { get; set; }
        string HairColorDescription { get; set; }
        int EyesColor { get; set; }
        string EyesColorDescription { get; set; }
        int Height { get; set; }
        int Weight { get; set; }
        bool ASmoker { get; set; }
        bool ADrinker { get; set; }
    }
}
