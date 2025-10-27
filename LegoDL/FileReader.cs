
using LegoBL.Interfaces;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

public class FileReader : IFileReader
{
    public List<LegoTheme> ReadFile(string path)
    {
        using (StreamReader sr = new StreamReader(path))
        {
            Dictionary<string, LegoTheme> data = new();
            string line;
            sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                try
                {
                    string[] ss = line.Split('|');
                    string set_id = ss[0];
                    string name = ss[1];
                    int year = int.Parse(ss[2]);
                    string theme = ss[3];
                    int pieces;
                    int fortesting;
                    bool test = int.TryParse(ss[7], out pieces);
                  


                    int miniFigs;
                    test = int.TryParse(ss[8], out miniFigs);
                    string imageUrl = ss[13];
                    double? retailPriceUSD = null;

                    double fortesting2;
                    test = double.TryParse(ss[10], out fortesting2);
                    if (test)
                    {
                        retailPriceUSD = fortesting2;
                    }
                    int? minAge = null;
                    test = int.TryParse(ss[9], out fortesting);
                    if (test)
                    {
                        minAge = fortesting;
                    }

                    LegoSet set = MakeSet(set_id, name, year, pieces, miniFigs, minAge, imageUrl, retailPriceUSD);

                        if (data.ContainsKey(theme))
                        {
                            data[theme].AddLegoSet(set);
                        }
                        else
                        {
                        MakeTheme(theme, data);
                        data[theme].AddLegoSet(set);

                    }
                    

                   


                }
                catch (LegoException ex)
                {
                    
                }   

            }
            return data.Values.ToList();

            
        }
    }

    private LegoSet MakeSet(string id, string name, int year, int pieces, int miniFigs, int? minAge, string imageUrl, double? retailPriceUSD)
    {
        LegoSet set = null;

        try
        {
            set = new LegoSet(id, name, year, pieces, miniFigs, minAge, imageUrl, retailPriceUSD);
            return set;
        }
        catch (LegoException ex) 
        { 
        }
        return set;
    }
    private LegoTheme MakeTheme(string theme, Dictionary<string, LegoTheme> data) 
    {
        LegoTheme legoTheme = null;
        try
        {
            legoTheme = new LegoTheme(theme);
            data.Add(theme, legoTheme);
            return legoTheme;
        }
        catch (LegoException ex)
        {
        }


        return legoTheme;
        
    }
}