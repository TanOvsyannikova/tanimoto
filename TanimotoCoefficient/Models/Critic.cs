using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TanimotoCoefficient.Models
{
    public class Critic
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Name should start with a capital letter")]
        public string FullName { get; set; }

        [Display(Name = "Movie Title")]
        [Required]
        [RegularExpression(@"^[A-Z0-9]+[a-zA-Z0-9\s]*$", ErrorMessage ="Title should start with a capital letter and consist only letters and numbers")]
        public string MovieName { get; set; }

        [Range(1, 5)]
        [Required]
        public double Rating { get; set; }
    }
}
