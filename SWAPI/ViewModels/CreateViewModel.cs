using CommunityToolkit.Mvvm.Messaging;
using SWAPI.Models;
using System.Windows.Input;

namespace SWAPI.ViewModels
{
    public class CreateViewModel : PrimeViewModel
    {
        public string Name { get; set; }
        public string Terrain { get; set; }
        public int Population { get; set; }

        public ICommand CreatePlanet { get; private set; }

        public CreateViewModel() 
        {
            CreatePlanet = new Command(CreateHandler);
        }

        private async void CreateHandler()
        {
            Planet planet = new Planet()
            {
               Name = Name,
               Terrain = Terrain,
                Population = Population.ToString()
            };

            WeakReferenceMessenger.Default.Send(new Messages.CreatePlanetMessage(planet));
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
