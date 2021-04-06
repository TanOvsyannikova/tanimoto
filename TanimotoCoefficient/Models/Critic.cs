using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TanimotoCoefficient.Models
{
    public class Critic
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Movie Title")]
        public string MovieName { get; set; }

        public double Rating { get; set; }
    }
}
