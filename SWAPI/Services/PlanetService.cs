

using SWAPI.Models;

namespace SWAPI.Services
{
    public static class PlanetService
    {

        public static List<Planet> AllPlanets;

        public static async Task PreLoadAllPlanets()
        {
            AllPlanets = new List<Planet>();
            AllPlanets = await GetAllPlanets();
        }

        public static async Task<List<Planet>> GetAllPlanets()
        {
            var apiService = new ApiService();
            List<ApiResult> apiPlanets = await apiService.GetAllPlanets();
            var planets = apiPlanets.SelectMany(x => x.results).Select(p => new Planet()
            {
                Climate = p.Climate,
                Created = p.Created,
                Diameter = p.Diameter,
                Edited = p.Edited,
                Films = p.Films,
                Gravity = p.Gravity,
                Name = p.Name,
                OrbitalPeriod = p.OrbitalPeriod,
                Population = p.Population,
                Residents = p.Residents,
                RotationPeriod = p.RotationPeriod,
                SurfaceWater = p.SurfaceWater,
                Terrain = p.Terrain,
                Url = p.Url
            }).ToList();

            return planets;
        }
    }
}
