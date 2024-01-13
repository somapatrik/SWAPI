using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAPI.Models
{
    public class ApiResult
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<PlanetResult> results { get; set; }
    }
}
