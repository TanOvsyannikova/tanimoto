using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace laba
{
    class Program
    {
        static void Main(string[] args)
        {
            var critics = new Dictionary<string, Dictionary<string, double>>();

            critics.Add("first critic", new Dictionary<string, double>());
            critics.Add("second critic", new Dictionary<string, double>());

            critics["first critic"].Add("Superman Returns", 5.0);
            critics["first critic"].Add("You, Me and Dupree", 3.5);
            critics["first critic"].Add("The Night Listener", 3.5);

            critics["second critic"].Add("Superman Returns", 5.0);
            critics["second critic"].Add("You, Me and Dupree", 3.5);
            critics["second critic"].Add("The Night Listener", 3.5);

            Console.WriteLine(tanimoto(critics, "first critic", "second critic"));
        }

        

        static Double tanimoto(Dictionary<string, Dictionary<string, double>> critics, string critic1, string critic2)
        {
            double a = critics[critic1].Count;
            double b = critics[critic2].Count;

            double c = critics[critic1].Intersect(critics[critic2]).Count();

            return c / (a + b - c); //returns value from 0.0 to 1.0
        }

        







    }
}
