using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TanimotoCoefficient.Data;
using System;
using System.Linq;
using TanimotoCoefficient.Models;
using System.Collections.Generic;

namespace TanimotoCoefficient.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TanimotoCoefficientContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TanimotoCoefficientContext>>()))
            {
                // Look for any movies.
                if (context.Critic.Any())
                {
                    return;   // DB has been seeded
                }

                context.Critic.AddRange(
                    new Critic
                    {
                        FullName = "John Adams",
                        MovieName = "Titanic",
                        Rating = 5.0
                    },

                    new Critic
                    {
                        FullName = "John Adams",
                        MovieName = "Star Wars",
                        Rating = 4.0
                    },

                    new Critic
                    {
                        FullName = "Bruce Swift",
                        MovieName = "Titanic",
                        Rating = 4.0
                    },

                    new Critic
                    {
                        FullName = "Bruce Swift",
                        MovieName = "Star Wars",
                        Rating = 3.0
                    }
                ); ;
                context.SaveChanges();
            }
        }
    }
}
