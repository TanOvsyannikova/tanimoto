using System;
using System.Collections;
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

            

            Dictionary<string, List<CriticMovieList>> dic = new();

            if (!string.IsNullOrEmpty(criticName1) && !string.IsNullOrEmpty(criticName2) && criticName1 != criticName2)
            {
                var selection = _context.Critic.Where(critic => (critic.FullName == criticName1) || (critic.FullName == criticName2))
                                               .AsEnumerable()
                                               .OrderBy(critic => critic.FullName)
                                               .ThenBy(m => m.MovieName)
                                               .GroupBy(critic => critic.FullName)
                                               .ToDictionary(i => i.Key, i => i
                                                                          .Select(a => new CriticMovieList { Title = a.MovieName, Rating = a.Rating })
                                                                          .ToList());
                dic = selection;
            }

            foreach (var critic in dic.Keys)
            {
                foreach (var film in dic[critic])
                {
                    if (film.Rating > 3)
                    {
                        film.Rating = 1; //liked this film
                    } else
                    {
                        film.Rating = 0; //disliked the film
                    }
                }
            }

            double coef = 0;

            if (dic.Count() != 0)
            {
                int a = dic[criticName1].Count();
                int b = dic[criticName2].Count();

                int c = dic[criticName1].Select(x => new { x.Title, x.Rating })
                     .Intersect(dic[criticName2].Select(x => new { x.Title, x.Rating })).Count();


                //for (int film = 0; film < dic.Count; film++)
                //{
                //    if (dic[criticName1][film] == dic[criticName1][film])
                //    {
                //        c += 1;
                //    }
                //}

                coef = c / ((double)a + (double)b - c);
            }
            

            var tanimotoCoefficientVM = new TanimotoCoefficientViewModel
            {
                Critics = new SelectList(await criticQuery.Distinct().ToListAsync()),
                ListOfCritics = dic,
                Coefficient = coef
                //Coefficient = string.Format("{0:0.00}", coef) ?? ""
            };

            return View(tanimotoCoefficientVM);
        }


    }
}
