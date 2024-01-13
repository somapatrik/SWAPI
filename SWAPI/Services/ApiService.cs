
using Newtonsoft.Json;
using SWAPI.Models;

namespace SWAPI.Services
{
    public class ApiService
    {
        public ApiService() { }

        public async Task<List<ApiResult>> GetAllPlanets()
        {
            var x = await GetResuls();
            return x;
        }

        public async Task<List<ApiResult>> GetPlanetsByName(string name)
        {
            var x = await GetResuls(name);
            return x;
        }

        private async Task<List<ApiResult>> GetResuls(string name = "")
        {
            List<ApiResult> results = new List<ApiResult>();
            int page = 1;
            bool done = false;

            using (var client = new HttpClient())
            {
                while (!done)
                {

                    string searchUrl = string.IsNullOrEmpty(name) ? new RequestBuilder(page).Url : new RequestBuilder(page, name).Url;

                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(searchUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            results.Add(JsonConvert.DeserializeObject<ApiResult>(content));
                            page++;
                        }
                        else
                        {
                            done = true;
                        }
                    }
                    catch
                    {
                        done = true;
                    }
                }
            }

            return results;
        }

    }
}
