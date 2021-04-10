using System;
using System.Collections;
using System.Collections.Generic;

namespace TanimotoCoefficient.Models
{
    public class CriticMovieList
    {
        public string Title { get; set; }
        public double Rating { get; set; }
    }

    class ItemEqualityComparer : IEqualityComparer<CriticMovieList>
    {
        public bool Equals(CriticMovieList item1, CriticMovieList item2)
        {
            // Two items are equal if their titles are equal.
            return item1.Title == item2.Title;
        }

        public int GetHashCode(CriticMovieList obj)
        {
            return obj.Title.GetHashCode();
        }
    }

    
}
