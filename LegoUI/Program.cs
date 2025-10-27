using LegoBL.Beheerder;

namespace LegoUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World! Het is Nick hier.");

            LegoRepository repo = new LegoRepository("Data Source=MSI\\MOXXY;Initial Catalog=LegoEvaluatie;Integrated Security=True;Trust Server Certificate=True");

            FileReader reader = new FileReader();

            LegoBeheerder buh = new LegoBeheerder(repo, reader);
          
            buh.WriteLegoThemes(buh.ReadFile("C:\\Users\\Admin\\Documents\\HoGent\\Programmeren_Gevorderd\\Evaluaties\\Ev1Data\\lego_sets - Copy.csv"));

            var res = buh.GetLegoTheme("Minitalia");
            Console.WriteLine(res);
        }
    }
}
