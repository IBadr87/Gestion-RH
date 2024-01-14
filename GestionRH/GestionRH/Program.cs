using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Info;

namespace GestionRH
{
    public class Program
    {
        static void Main(string[] args)
        {
            Employe em1 = new Employe("BADR", "Ibrahim", "Lyon", 36, ", Software_Developer, ", 1700.00);
            Console.WriteLine(em1.getInfo());

            em1.affectation(", Alternant, ");
            em1.augmentation(100);
            Console.WriteLine(em1.getInfo());

            using (Employe em2 = new Employe("SAID", "Ali", "Paris", 30, ", Ingénieur, ", 3000.0))
            {
                Console.WriteLine(em2.getInfo());
            }

            using (Entreprise e = new Entreprise("Conduent"))
            {
                e.embauche(em1);

                //Employe em3 = new Employe("Adel", "Marie, ", "Marseille, ", 25, ", Assistant, ", 2500.0);
                //e.embauche(em3);

                for (int i = 0; i < e.effectif; i++)
                {
                    Employe emp = e.employes[i];
                    Console.WriteLine(emp.getInfo());
                }
            }

            Console.WriteLine("Appuyez sur n'importe quelle touche pour quitter...\nAu revoir");
            Console.ReadKey();
        }
    }
}