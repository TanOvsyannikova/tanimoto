using System;
using Microsoft.EntityFrameworkCore;
using TanimotoCoefficient.Models;

namespace TanimotoCoefficient.Data
{
    public class TanimotoCoefficientContext: DbContext
    {
        public TanimotoCoefficientContext (DbContextOptions<TanimotoCoefficientContext> options)
            : base(options)
        {
        }

        public DbSet<Critic> Critic { get; set; }
    }
}
