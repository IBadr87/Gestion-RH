using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Info;

namespace GestionRH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employe Ib = new Employe("BADR", "Ibrahim", "Lyon", 36, ", Software_Developer, ", 1700.00);
            Console.WriteLine(Ib.getInfo());

            Ib.affectation(", Alternant, ");
            Ib.augmentation(100);
            Console.WriteLine(Ib.getInfo());

            Console.WriteLine("Appuyez sur n'importe quelle touche pour quitter...\nAu revoir");
            Console.ReadKey();
        }
    }
}
