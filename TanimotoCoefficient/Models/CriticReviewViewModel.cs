using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TanimotoCoefficient.Models
{
    public class CriticReviewViewModel
    {
        public List<Critic> MoviesRatedByCritic { get; set; } //list of all movies that one critic rated
        public SelectList Critics { get; set; } //list of critics to select from
        public string CriticName { get; set; } //contains chosen critic's name
        public string SearchString { get; set; } //query
    }
}
