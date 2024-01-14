using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SWAPI.Messages;
using SWAPI.Models;
using SWAPI.Services;
using SWAPI.Views;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Windows.Input;

namespace SWAPI.ViewModels
{
    public class MainViewModel : PrimeViewModel
    {
        private List<Planet> _AllPlanets;
        private int _MaxPerPage = 10;

        private bool _IsLoading;
        public bool IsLoading { get=>_IsLoading; set=>SetProperty(ref _IsLoading, value); }


        private ObservableCollection<Planet> _Planets;
        public ObservableCollection<Planet> Planets { get => _Planets; set => SetProperty(ref _Planets, value); }


        private List<int> _Pages;
        public List<int> Pages { get => _Pages; set => SetProperty(ref _Pages,value); }


        private int _SelectedPage;
        public int SelectedPage { get => _SelectedPage; set => SetProperty(ref _SelectedPage,value); }


        private string _SearchName;
        public string SearchName { get => _SearchName; set => SetProperty(ref _SearchName, value); }

        public ICommand Refresh { get; private set; }
        public ICommand DisplayPage { get; private set; }
        public ICommand SearchPlanet { get; private set; }
        public ICommand DisplayPlanet { get; private set; }
        public ICommand SyncApi { get; private set; }

        public ICommand AddPlanet { get; private set; }

        public MainViewModel() 
        {
            Init();
        }

        private void Init()
        {
            Preload();
            RelayCommands();
            WeakReferenceMessenger.Default.Register<CreatePlanetMessage>(this, (a, b) => { AddPlanetToList(b.Value); });
        }

        private void RelayCommands()
        {
            Refresh = new Command(LoadPlanets);
            DisplayPage = new Command(async()=>await DisplayPageHandler());
            SearchPlanet = new Command(FilterHandler);
            DisplayPlanet = new Command(DisplayPlanetHandler);
            SyncApi = new Command(SyncHandler);
            AddPlanet = new Command(AddPlanetHandler);
        }

        private async void AddPlanetHandler(object obj)
        {
            await Shell.Current.Navigation.PushModalAsync(new CreateView());
        }

        private void AddPlanetToList(Planet planet)
        {
            PlanetService.AllPlanets.Add(planet);
            FilterHandler();
        }

        private async void DisplayPlanetHandler(object planet)
        {
            await Shell.Current.Navigation.PushModalAsync(new PlanetView((Planet) planet));
        }

        private void FilterHandler()
        {
            if (string.IsNullOrEmpty(SearchName))
                _AllPlanets = PlanetService.AllPlanets;
            else
                _AllPlanets = PlanetService.AllPlanets.Where(x => x.Name.ToLower().Contains(SearchName.ToLower().Trim())).ToList();

            LoadPlanets();
        }

        private async void SyncHandler()
        {
            SearchName = "";
            Planets = new ObservableCollection<Planet>();
            await Task.Run(()=>Preload());
        }

        private async void Preload()
        {
            IsLoading = true;

            await SyncData();

            _AllPlanets = new List<Planet>();
            _AllPlanets = PlanetService.AllPlanets;

            LoadPlanets();
        }

        private async Task SyncData()
        {
            await PlanetService.PreLoadAllPlanets();
        }

        private async void LoadPlanets()
        {
            IsLoading = true;

            await DisplayPageHandler();
            await SetPages();
            
            IsLoading = false;
        }

        private async Task DisplayPageHandler()
        {
            Planets = new ObservableCollection<Planet>();
            IEnumerable<Planet> p;
            p = _AllPlanets.Skip((_SelectedPage - 1) * _MaxPerPage).Take(_MaxPerPage).OrderBy(p=>p.Name);

            p.ToList().ForEach(planet => Planets.Add(planet));
        }

        private async Task SetPages()
        {
            SelectedPage = 1;
            Pages = new List<int>();
            int pagesCount = (int)Math.Ceiling((decimal)_AllPlanets.Count() / _MaxPerPage);

            for (int i = 1; i <= pagesCount; i++)
                Pages.Add(i);
        }

    }
}
