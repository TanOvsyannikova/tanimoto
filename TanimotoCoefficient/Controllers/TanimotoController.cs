using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TanimotoCoefficient.Data;
using TanimotoCoefficient.Models;

namespace TanimotoCoefficient.Controllers
{
    public class TanimotoController : Controller
    {
        private readonly TanimotoCoefficientContext _context;

        public TanimotoController(TanimotoCoefficientContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string criticName1, string criticName2)
        {
            // Use LINQ to get list of critics.
            IQueryable<string> criticQuery = from m in _context.Critic
                                             orderby m.FullName
                                             select m.FullName;


            //MARK: this part of the code doesn't work properly yet


            var firstCritic = from m in _context.Critic
                              select m;

            ////Dictionary<string, Dictionary<string, double>> firstDic = new();
            //Dictionary<string, Movie> firstDic = new();

            if (!string.IsNullOrEmpty(criticName1))
            {
                firstCritic = firstCritic.Where(s => s.FullName == criticName1);
            }

            //firstDic = await firstCritic.ToDictionaryAsync(p => p.FullName, c => new Movie(c.MovieName, c.Rating));

            var secondCritic = from m in _context.Critic
                              select m;

            //Dictionary<string, Movie> secondDic = new();

            if (!string.IsNullOrEmpty(criticName1))
            {
                secondCritic = secondCritic.Where(s => s.FullName == criticName2);
            }

            //secondDic = await secondCritic.ToDictionaryAsync(p => p.FullName, c => new Movie(c.MovieName, c.Rating));

            //foreach (var movie in firstDic)
            //{
            //    if (movie.Value.Rating < 3)
            //    {
            //        movie.Value.Rating = 0;
            //    } else
            //    {
            //        movie.Value.Rating = 1;
            //    }
            //}

            //foreach (var movie in secondDic)
            //{
            //    if (movie.Value.Rating < 3)
            //    {
            //        movie.Value.Rating = 0;
            //    }
            //    else
            //    {
            //        movie.Value.Rating = 1;
            //    }
            //}

            //int a = firstDic.Count;
            //int b = secondDic.Count;
            //double c = 0.0;

            //c = (firstDic.Values).Intersect(secondDic.Values).Count();

            //double coef = c / (a + b - c);


            var tanimotoCoefficientVM = new TanimotoCoefficientViewModel
            {
                Critics = new SelectList(await criticQuery.Distinct().ToListAsync()),
                FirstCritic = await firstCritic.ToListAsync(),
                SecondCritic = await secondCritic.ToListAsync(),
                Coefficient = 0
            };

            return View(tanimotoCoefficientVM);
        }

    }
}
