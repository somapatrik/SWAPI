
using SWAPI.Models;

namespace SWAPI.ViewModels
{
    public class PlanetViewModel : PrimeViewModel
    {
        private Planet _Planet;

        public Planet Planet { get => _Planet; set => SetProperty(ref _Planet, value); }
        public PlanetViewModel(Planet planet) 
        {
            Planet = planet;
        }
    }
}
