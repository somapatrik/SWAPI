
using CommunityToolkit.Mvvm.Messaging.Messages;
using SWAPI.Models;

namespace SWAPI.Messages
{
    public class CreatePlanetMessage : ValueChangedMessage<Planet>
    {
        public CreatePlanetMessage(Planet value) : base(value)
        {
        }
    }
}
