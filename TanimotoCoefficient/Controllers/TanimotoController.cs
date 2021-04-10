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
            Dictionary<string, List<CriticMovieList>> viewForChosen = new();

            if (!string.IsNullOrEmpty(criticName1) && !string.IsNullOrEmpty(criticName2) && criticName1 != criticName2)
            {
                var selection1 = _context.Critic
                                               .AsEnumerable()
                                               .OrderBy(critic => critic.FullName)
                                               .ThenBy(m => m.MovieName)
                                               .GroupBy(critic => critic.FullName);


                var selection2 = _context.Critic
                                               .Where(critic => (critic.FullName == criticName1) || (critic.FullName == criticName2))
                                               .AsEnumerable()
                                               .OrderBy(critic => critic.FullName)
                                               .ThenBy(m => m.MovieName)
                                               .GroupBy(critic => critic.FullName);

                viewForChosen = selection2.ToDictionary(i => i.Key, i => i
                                                                          .Select(a => new CriticMovieList { Title = a.MovieName, Rating = a.Rating })
                                                                          .Distinct(new ItemEqualityComparer())
                                                                          .ToList()); ;

                dic = selection1.ToDictionary(i => i.Key, i => i
                                                                          .Select(a => new CriticMovieList { Title = a.MovieName, Rating = a.Rating })
                                                                          .Distinct(new ItemEqualityComparer())
                                                                          .ToList()); ;
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
            double coefToView = 0;

            double findTanimoto(string name1, string name2)
            {
                if (dic.Count() != 0)
                {
                    int a = dic[name1].Count();
                    int b = dic[name2].Count();

                    int c = dic[name1].Select(x => new { x.Title, x.Rating })
                         .Intersect(dic[name2].Select(x => new { x.Title, x.Rating })).Count();

                    coef = c / ((double)a + (double)b - c);
                }

                return coef;
            }

            List<Tuple<string, double>> similar = new();
            foreach (var critic in dic.Keys)
            {
                if (criticName1 != critic)
                {
                    double coe = findTanimoto(criticName1, critic);


                    //Recommend critics with Tanimoto Coefficient > 0.3 only

                    //if (coe > 0.3)
                    //{
                    //    similar.Add((critic, coe).ToTuple());
                    //}

                    //Recommend all critics in DB
                    similar.Add((critic, coe).ToTuple());

                    if (critic == criticName2)
                    {
                        coefToView = coe;
                    }
                }

                
                
            }
            similar.Sort((x, y) => y.Item2.CompareTo(x.Item2));


            var tanimotoCoefficientVM = new TanimotoCoefficientViewModel
            {
                Critics = new SelectList(await criticQuery.Distinct().ToListAsync()),
                ListOfCritics = viewForChosen,
                Coefficient = coefToView,
                Similarities = similar
            };

            return View(tanimotoCoefficientVM);
        }


    }
}
