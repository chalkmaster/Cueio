using System;
using Superstar.Html;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Superstar.Html.Linq;

namespace ZeCueio
{
    class Program
    {
        const string Uri = @"https://www.google.com.br/search?q={0}";
        static void Main(string[] args)
        {
            Console.WriteLine("Informe o consulta:");
            var q = Console.ReadLine();
            var searchResult = HDocument.Load(string.Format(Uri, q));

            var resultItens = searchResult.Descendants("span").Where(s => s.Attribute("class") != null 
                                                             && s.Attribute("class").Value == "st");


            var lqArgs = new List<double>();
            var lrArgs = new List<double>();
            var liArgs = new List<double>();

            resultItens.ToList().ForEach(e =>
                                             {
                                                 var relevantResults = new List<string>();
                                                 if (e.Descendants("b") != null)
                                                 {                                                     
                                                     relevantResults.AddRange(
                                                         String.Join(" ", 
                                                            e.Descendants("b").Select(b => b.Value).ToArray())
                                                         .Split(' ')
                                                         );
                                                 }

                                                 var qArgs = (double)q.Split(' ').Count();
                                                 var rArgs = (double)relevantResults.Count();
                                                 var iArgs = (double)string.Join(" ", resultItens.Select(r => r.Value)).Split(' ').Count() - rArgs;
                                                 var sim = (rArgs/qArgs);
                                                 lqArgs.Add(qArgs);
                                                 lrArgs.Add(rArgs);
                                                 liArgs.Add(iArgs);

                                                 Console.WriteLine(qArgs);
                                                 Console.WriteLine(rArgs);
                                                 Console.WriteLine(iArgs);

                                                 Console.WriteLine("Similaridade: {0}", sim);

                                             });

            Console.WriteLine(lqArgs.Average());
            Console.WriteLine(lrArgs.Average());
            Console.WriteLine(liArgs.Average());

            Console.WriteLine(lrArgs.Average() / lqArgs.Average());
            Console.WriteLine(lrArgs.Max() / lqArgs.Average());


            Console.ReadKey();
        }
    }
}
