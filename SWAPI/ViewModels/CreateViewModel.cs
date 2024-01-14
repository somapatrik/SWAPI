using CommunityToolkit.Mvvm.Messaging;
using SWAPI.Models;
using System.Windows.Input;

namespace SWAPI.ViewModels
{
    public class CreateViewModel : PrimeViewModel
    {
        private string _Name;
        public string Name 
        {
            get => _Name;
            set 
            { 
                _Name = value;
                RefreshCans(); 
            } 
        }
        private string _Terrain;
        public string Terrain 
        { 
            get => _Terrain; 
            set
            {
                _Terrain = value;
                RefreshCans();
            }
        }

        public int Population { get; set; }

        public ICommand CreatePlanet { get; private set; }

        public CreateViewModel() 
        {
            CreatePlanet = new Command(CreateHandler, CanSave);
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

        private void RefreshCans()
        {
            ((Command)CreatePlanet).ChangeCanExecute();
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Terrain);
        }
    }
}
