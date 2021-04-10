using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TanimotoCoefficient.Models
{
    public class TanimotoCoefficientViewModel
    {
        public Dictionary<string, List<CriticMovieList>> ListOfCritics { get; set; }
        public SelectList Critics { get; set; }
        public string CriticName1 { get; set; }
        public string CriticName2 { get; set; }
        public double Coefficient { get; set; }
        public List<Tuple<string, double>> Similarities { get; set; }
    }
}
