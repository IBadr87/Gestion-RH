using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Info;

namespace GestionRH
{
    public class Program
    {
        static void Main(string[] args)
        {
            Entreprise Ent = new Entreprise("");
            Ent.LoadData();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nMenu Principal:");
                Console.WriteLine("1. Ajouter un nouvel employé");
                Console.WriteLine("2. Afficher la liste des employés");
                Console.WriteLine("3. Récupérer un employé existant");
                Console.WriteLine("4. Modifier les informaion d'un employé");
                Console.WriteLine("5. Supprimer un employé");
                Console.WriteLine("6. Afficher les Statistiques");
                Console.WriteLine("7. Quitter\n");

                Console.Write("Veuillez sélectionner une option (1-7): \n");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("\nOption 1: Ajouter un nouvel employé\n");
                        Ent.AddNewEmploye();
                        break;

                    case "2":

                        Console.WriteLine("\nOption 2: Afficher la liste des employés\n");
                        Ent.displayList();
                        break;

                    case "3":
                        Console.WriteLine("\nOption 3: Récupérer un employé existant\n");
                        Ent.EmployeExiste();
                        break;

                    case "4":
                        Console.WriteLine("\nOption 4: Modifier les informaion d'un employé\n");
                        Ent.ModifyEmployeeInfo();
                        break;

                    case "5":
                        Console.WriteLine("\nOption 5: Supprimer un employé\n");
                        int matriculeToRemove = 1;
                        Ent.DeleteEmployee(matriculeToRemove);
                        break;

                    case "6":
                        Console.WriteLine("\nOption 6: Afficher les Statistiquess\n");
                        Ent.DisplayStatistics();
                        break;

                    case "7":
                        Console.WriteLine("\nOption 7: Quitter\n");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Option non valide. Veuillez sélectionner une option valide (1-7).");
                        break;
                }
            }

            Console.WriteLine("\nAppuyez sur n'importe quelle touche pour quitter...\nAu revoir");
            Console.ReadKey();
        }
    }
}