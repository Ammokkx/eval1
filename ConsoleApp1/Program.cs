using LegoBL.Beheerder;
using LegoBL.Interfaces;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World! Het is Nick hier met LINQ query.");
            LegoRepository repo = new LegoRepository("Data Source=MSI\\MOXXY;Initial Catalog=LegoEvaluatie;Integrated Security=True;Trust Server Certificate=True");

            FileReader reader = new FileReader();

            LegoBeheerder buh = new LegoBeheerder(repo, reader);

            List <LegoTheme> themes = buh.ReadFile("testpath");

            Console.WriteLine("\nTop 3 Leg themas met meeste sets :");
            var q1 = themes
                .OrderBy(x => x.LegoSets.Count())
                .Reverse()
                .Select(x => $"{x.Name}, Amount of Themes: {x.LegoSets.Count()}")
                .Take(10);
            foreach (var x in q1) Console.WriteLine(x);

            Console.WriteLine("\nLego sets met minstens 25 mini figs en minstens 300 blokjes :");
            var q2 = themes
                .Where(x => (x.LegoSets));
foreach (var x in q2) Console.WriteLine(x);

            Console.WriteLine("\nThema met de langste naam :");
            var q3 = themes.OrderBy(x => x.Name.Length)
                .Reverse()
                .Select(x => x.Name)
                .First();
Console.WriteLine(q3);
            Console.WriteLine("the end");

        }
    }
}
