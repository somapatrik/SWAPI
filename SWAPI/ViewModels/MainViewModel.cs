using SWAPI.Models;
using SWAPI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SWAPI.ViewModels
{
    public class MainViewModel :PrimeViewModel
    {
        private bool _IsLoading;
        public bool IsLoading { get=>_IsLoading; set=>SetProperty(ref _IsLoading, value); }

        private ObservableCollection<Planet> _Planets;
        public ObservableCollection<Planet> Planets { get => _Planets; set => SetProperty(ref _Planets, value); }

        private List<Planet> _AllPlanets;

        public ICommand Refresh {  get; private set; }

        private int _MaxPerPage = 10;
        private int _SelectedPage = 2;

        public MainViewModel() 
        {
           Preload();
           Refresh = new Command(LoadPlanets);
        }

        private async void Preload()
        {
            IsLoading = true;
            await PlanetService.PreLoadAllPlanets();

            _AllPlanets = new List<Planet>();
            _AllPlanets = PlanetService.AllPlanets;

            LoadPlanets();
        }

        private async void LoadPlanets()
        {
            IsLoading = true;

            await Task.Run(() =>
            {
                Planets = new ObservableCollection<Planet>();
                var p = _AllPlanets.Skip((_SelectedPage - 1)* _MaxPerPage ).Take(_MaxPerPage);
                p.ToList().ForEach(planet => Planets.Add(planet));
            });

            IsLoading = false;
        }

    }
}
