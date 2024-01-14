using CommunityToolkit.Mvvm.Input;
using SWAPI.Models;
using SWAPI.Services;
using SWAPI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SWAPI.ViewModels
{
    public class MainViewModel : PrimeViewModel
    {
        private bool _IsLoading;
        public bool IsLoading { get=>_IsLoading; set=>SetProperty(ref _IsLoading, value); }

        private ObservableCollection<Planet> _Planets;
        public ObservableCollection<Planet> Planets { get => _Planets; set => SetProperty(ref _Planets, value); }

        private List<Planet> _AllPlanets;

        private int _MaxPerPage = 10;

        private List<int> _Pages;
        public List<int> Pages { get => _Pages; set => SetProperty(ref _Pages,value); }

        private int _SelectedPage;
        public int SelectedPage { get => _SelectedPage; set => SetProperty(ref _SelectedPage,value); }


        public ICommand Refresh { get; private set; }
        public ICommand DisplayPage { get; private set; }
        public ICommand SearchPlanet { get; private set; }
        public ICommand DisplayPlanet { get; private set; }

        public MainViewModel() 
        {
            Init();
        }

        private void Init()
        {
            Preload();
            _SelectedPage = 1;

            RelayCommands();
        }

        private void RelayCommands()
        {
            Refresh = new Command(LoadPlanets);
            DisplayPage = new Command(async()=>await DisplayPageHandler());
            SearchPlanet = new Command(FilterHandler);
            DisplayPlanet = new Command(DisplayPlanetHandler);
        }

        private async void DisplayPlanetHandler(object planet)
        {
            await Shell.Current.Navigation.PushModalAsync(new PlanetView((Planet) planet));
        }

        private void FilterHandler()
        {
            throw new NotImplementedException();
        }

        private async void Preload()
        {
            IsLoading = true;
            await PlanetService.PreLoadAllPlanets();

            _AllPlanets = new List<Planet>();
            _AllPlanets = PlanetService.AllPlanets;

            LoadPlanets();
        }

        private async Task DisplayPageHandler()
        {
                Planets = new ObservableCollection<Planet>();
                var p = _AllPlanets.Skip((_SelectedPage - 1) * _MaxPerPage).Take(_MaxPerPage);
                p.ToList().ForEach(planet => Planets.Add(planet));
        }

        private async void LoadPlanets()
        {
            IsLoading = true;

            await DisplayPageHandler();
            await SetPages();
            
            IsLoading = false;
        }

        private async Task SetPages()
        {
            Pages = new List<int>();
            int pagesCount = (int)Math.Ceiling((decimal)_AllPlanets.Count() / _MaxPerPage);

            for (int i = 1; i <= pagesCount; i++)
                Pages.Add(i);
        }

    }
}
