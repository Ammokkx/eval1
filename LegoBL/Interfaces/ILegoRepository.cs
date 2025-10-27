using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoBL.Interfaces
{
    public interface ILegoRepository
    {
        LegoTheme GetLegoTheme(string name);
        void WriteLegoThemes(List<LegoTheme> legoThemes);
    }
}
