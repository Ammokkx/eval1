using LegoBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoBL.Beheerder
{
    public class LegoBeheerder
    {
        private IFileReader reader;
        private ILegoRepository legoRepository;

        public LegoBeheerder(ILegoRepository legoRepository, IFileReader reader )
        {
           
            this.legoRepository = legoRepository;
            this.reader = reader;
        }

        public List<LegoTheme> ReadFile(string file)
        {
            return reader.ReadFile(file);
        }

        public void WriteLegoThemes(List<LegoTheme> legoThemes)
        {
            legoRepository.WriteLegoThemes(legoThemes);
        }

        public LegoTheme GetLegoTheme(string name)
        {
            return legoRepository.GetLegoTheme(name);   
        }

    }
}
