using SWAPI.Models;
using SWAPI.Services;

namespace SWAPI.ViewModels
{
    public class MainViewModel
    {
        private List<Planet> _Planets;



        public MainViewModel() 
        {
            LoadPlanets();   
        }

        private async void LoadPlanets()
        {
            _Planets = new List<Planet>();

            _Planets = await PlanetService.GetAllPlanets();
        }

    }
}
