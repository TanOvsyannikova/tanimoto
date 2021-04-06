using System;
namespace TanimotoCoefficient.Models
{
    public class Movie
    {
        public Movie(string movieName, double rating)
        {
            MovieName = movieName;
            Rating = rating;
        }

        public string MovieName { get; set; }

        public double Rating { get; set; }
    }
}
