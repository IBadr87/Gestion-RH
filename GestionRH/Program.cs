using System;
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
                Console.WriteLine("3. Chercher un employé par nom");
                Console.WriteLine("4. Chercher un employé par fonction");
                Console.WriteLine("5. Modifier les informaion d'un employé");
                Console.WriteLine("6. Supprimer un employé");
                Console.WriteLine("7. Afficher les Statistiques");
                Console.WriteLine("8. Quitter\n");

                Console.Write("Veuillez sélectionner une option (1-8): \n");
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
                        Console.WriteLine("\nOption 3: Chercher un employé par nom\n");
                        Ent.searchEmpNom();
                        break;

                    case "4":
                        Console.WriteLine("\nOption 4: Chercher un employé par fonction\n");
                        Ent.searchEmpFonc();
                        break;

                    case "5":
                        Console.WriteLine("\nOption 5: Modifier les informaion d'un employé\n");
                        Ent.ModifyEmployeeInfo();
                        break;

                    case "6":
                        Console.WriteLine("\nOption 6: Supprimer un employé\n");
                        int matriculeToRemove = 1;
                        Ent.DeleteEmployee(matriculeToRemove);
                        break;

                    case "7":
                        Console.WriteLine("\nOption 7: Afficher les Statistiquess\n");
                        Ent.DisplayStatistics();
                        break;

                    case "8":
                        Console.WriteLine("\nOption 8: Quitter\n");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Option non valide. Veuillez sélectionner une option valide (1-8).");
                        break;
                }
            }

            Console.WriteLine("\nAppuyez sur n'importe quelle touche pour quitter...\nAu revoir");
            Console.ReadKey();
        }
    }
}