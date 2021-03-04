using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cv4
{
    class Program
    {
        static void Main(string[] args)
        {
            string testovaciText = "Toto je retezec predstavovany nekolika radky,\n"
                + "ktere jsou od sebe oddeleny znakem LF (Line Feed).\n"
                + "Je tu i nejaky ten vykricnik! Pro ucely testovani i otaznik?\n"
                + "Toto je jen zkratka zkr. ale ne konec vety. A toto je\n"
                + "posledni veta!";
            StringStatistics text = new StringStatistics(testovaciText);

            Console.WriteLine(testovaciText + "\n");
            Console.WriteLine("Pocet slov: {0}", text.CountWords());
            Console.WriteLine("Pocet radku: {0}", text.CountRows());
            Console.WriteLine("Pocet vet: {0}", text.CountSentences());
            Console.WriteLine("Nejdelsi slova: {0}", String.Join(", ", text.LongestWords()));
            Console.WriteLine("Nejkratsi slova: {0}", String.Join(", ", text.ShortestWords()));
            Console.WriteLine("Nejcetnejsi slova: {0}", String.Join(", ", text.MostCommonWords()));
            Console.WriteLine("Slova dle abecedy: {0}", String.Join(", ", text.AlphSort()));
        }
    }
}
