﻿
namespace SWAPI.Models
{
    public class Planet
    {
        public string Climate { get; set; }
        public DateTime Created { get; set; }
        public string Diameter { get; set; }
        public DateTime Edited { get; set; }
        public List<string> Films { get; set; }
        public string Gravity { get; set; }
        public string Name { get; set; }
        public string OrbitalPeriod { get; set; }
        public string Population { get; set; }
        public List<string> Residents { get; set; }
        public string RotationPeriod { get; set; }
        public string SurfaceWater { get; set; }
        public string Terrain { get; set; }
        public string Url { get; set; }
    }
}
