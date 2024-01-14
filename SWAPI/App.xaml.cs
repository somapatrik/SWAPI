using Bumptech.Glide.Request.Target;
using SWAPI.Services;

namespace SWAPI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //PreLoad();
            MainPage = new AppShell();
            
        }

        public async void PreLoad()
        {
            await PlanetService.PreLoadAllPlanets();
        }
    }
}
