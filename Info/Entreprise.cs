using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Info
{
    public class Entreprise : IDisposable, IEnumerable<Employe>
    {
        #region Attributs
        public string nomEnt;

        // TODO : Exercice 1.5.1 (Utilisation d’une collection)
        private List<Employe> employes;
        
        public event Action<Object, EntEventArgs> InfoEffectif = null;
        private static string filePath = "../../../../../data.txt";
        #endregion


        #region Constructeurs
        public Entreprise(string nomEnt)
        {
            this.nomEnt = nomEnt;

            // TODO : Exercice 1.5.2 (Utilisation d’une collection)
            employes = new List<Employe>();
        }

        public Entreprise() : this("Hema")
        {
        }
        #endregion


        #region Methodes
        public static string getString(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        public static int getInt(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            int Output;

            if (int.TryParse(input, out Output))
            {
                return Output;
            }
            else
            {
                throw new Exception("La valeur saisie n'est pas valide.");
            }
        }

        public void LoadData()
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string ligne = "";

                while ((ligne = sr.ReadLine()) != null)
                {
                    string[] data = (from l in ligne.Split(new[] { ':', ',' })
                        where !string.IsNullOrEmpty(l) 
                        select l.Trim()).ToArray();
                                              

                    if (data.Length >= 5)
                    {
                        string[] nameParts = data[1].Trim().Split(' ');
                        string nom = nameParts[0];
                        string prenom = nameParts[1];

                        embauche(new Employe(int.Parse(data[0]), nom, prenom, data[2], int.Parse(data[3]), data[4], double.Parse(data[5])));
                    }
                }

                sr.Close();
            }
            else
            {
                Console.WriteLine("Le fichier de données n'existe pas.");
            }
        }

        public void embauche(Employe emp)
        {

            // TODO: Exercice 1.5.3 (Utilisation d’une collection)
            employes.Add(emp);
        }

        public void writeDataFile(Employe emp)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                string employeeInfo = $"{emp.Numero}: {emp.Nom} {emp.Prenom}, {emp.Adresse}, {emp.Age}, {emp.Fonction}, {emp.Salaire}";
                writer.WriteLine(employeeInfo);
            }
        }

        // TODO : Exercice 1.2 (Recrutement d’un nouvel employé)
        public void AddNewEmploye()
        {
            int newNumero = 0;

            for (int i = 0; i < employes.Count; i++)
            {
                if (employes[i] != null && i < employes.Count)
                {
                    if (((Employe)employes[i]).Numero > newNumero)
                    {
                        newNumero = ((Employe)employes[i]).Numero;
                    }
                }
            }

            string nom = getString("Bonjour,\n\nVeuillez saisir le nom du nouvel employe: ");
            string prenom = getString("\nVeuillez saisir le prénom du nouvel employe: ");
            string adresse = getString("\nVeuillez saisir l'adresse du nouvel employe: ");
            string fonction = getString("\nVeuillez saisir la fonction du nouvel employe: ");

            int age = getInt("\nVeuillez saisir l'âge du nouvel employe: ");
            int salaire = getInt("\nVeuillez saisir le salaire du nouvel employe: ");

            Employe emp = new Employe(newNumero + 1, nom, prenom, adresse, age, fonction, salaire);
            embauche(emp);
            Console.WriteLine($"\nNouvel employé embauché, Matricule_N{emp.Numero}: {emp.Nom} {emp.Prenom}");

            writeDataFile(emp);
        }
/*
        // TODO : Exercice 1.3 (Récupération d’un employé existant)
        public void EmployeExiste()
        {
            string searchName = getString("Veuillez saisir le nom de l'employé à rechercher: ").ToLower();

            bool found = false;

            for (int i = 0; i < employes.Count; i++)
            {
                if (employes[i] is Employe && ((Employe)employes[i]).Nom.ToLower() == searchName)
                {
                    Employe emp = (Employe)employes[i];
                    Console.WriteLine($"Employé trouvé, Matricule_N{emp.Numero}: {emp.getInfo()}");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Aucun employé trouvé avec le nom '{searchName}'.");
            }
        }
*/
        // TODO : Exercice 1.6 (Recherche d’employé par nom)
        public void searchEmpNom()
        {
            string nomRecherche = getString("Veuillez saisir le nom à rechercher: ");

            // Appeler la méthode rechercheParNom
            List<Employe> employesTrouves = rechercheParNom (nomRecherche);

            // Afficher les informations des employés correspondants
            if (employesTrouves.Count > 0)
            {
                Console.WriteLine($"Employés trouvés pour le nom '{nomRecherche}':");

                foreach (Employe emp in employesTrouves)
                {
                    Console.WriteLine($"Matricule_N{emp.Numero}: {emp.getInfo()}");
                }
            }
            else
            {
                Console.WriteLine($"Aucun employé trouvé avec le nom '{nomRecherche}'.");
            }
        }

        public List<Employe> rechercheParNom (string nom)
        {
            var nomSpe = from emp in employes
                                            where emp.Nom.ToUpper().Contains(nom.ToUpper()) // recherche partial
                                            select emp;

            return nomSpe.ToList();
        }

        // TODO : Exercice 1.7 (Recherche d’employé par fonction)
        public void searchEmpFonc()
        {
            string foncRecherche = getString("Veuillez saisir la fonction à rechercher: ");

            // Appeler la méthode rechercheParNom
            List<Employe> employesTrouves = rechercheParFonction (foncRecherche);

            // Afficher les informations des employés correspondants
            if (employesTrouves.Count > 0)
            {
                Console.WriteLine($"Employés trouvés pour la fonction '{foncRecherche}':");

                foreach (Employe emp in employesTrouves)
                {
                    Console.WriteLine($"Matricule_N{emp.Numero}: {emp.getInfo()}");
                }
            }
            else
            {
                Console.WriteLine($"Aucun employé trouvé avec la fonction '{foncRecherche}'.");
            }
        }

        public List<Employe> rechercheParFonction(string fonction)
        {
            var foncSpe = from emp in employes
                              //where emp.Fonction.ToUpper().Contains(fonction.ToUpper()) recherche partial
                          where emp.Fonction.ToUpper() == fonction.ToUpper() // recherche exacte
                          select emp;

            return foncSpe.ToList();
        }

        private void deleteDataFile(int matricule, List<Employe> allEmployees)
        {
            // Read the existing file contents into a list of lines
            List<string> lines = File.ReadAllLines(filePath).ToList();

            // Find the index of the line to be deleted
            int indexToDelete = -1;

            for (int i = 0; i < lines.Count; i++)
            {
                string[] data = lines[i].Split(':');
                if (int.Parse(data[0]) == matricule)
                {
                    indexToDelete = i;
                    break; // Found the line to delete, exit the loop
                }
            }

            // Remove the line if it was found
            if (indexToDelete >= 0)
            {
                lines.RemoveAt(indexToDelete);
            }

            // Rewrite the modified lines to the file
            File.WriteAllLines(filePath, lines.ToArray());

            Console.WriteLine($"Les données de l'employé matricule_N{matricule} ont été supprimées.");
        }


        public void DeleteEmployee(int matricule)
        {
            matricule = getInt("Veuillez saisir le matricule numero de l'employé à supprimer: ");

            int indexToDelete = -1;

            // Find the index of the employee with the specified matricule
            for (int i = 0; i < employes.Count; i++)
            {
                if (employes[i] is Employe && ((Employe)employes[i]).Numero == matricule)
                {
                    indexToDelete = i;
                    break; // Found the employee, exit the loop
                }
            }

            if (indexToDelete >= 0)
            {
                // Shift the elements in the array to remove the employee
                employes.RemoveAt(indexToDelete);

                // Call the method to delete data from the file
                deleteDataFile(matricule, new List<Employe>());
            }
            else
            {
                Console.WriteLine($"Aucun employe trouve avec le Matricule_N{matricule}.");
            }
        }

        public void ModifyDataFile(Employe emp)
        {
            // Read the existing file contents into a list of lines
            List<string> lines = File.ReadAllLines(filePath).ToList();

            // Find the index of the line to be modified
            int indexToModify = -1;

            for (int i = 0; i < lines.Count; i++)
            {
                string[] data = lines[i].Split(':');
                if (int.Parse(data[0]) == emp.Numero)
                {
                    indexToModify = i;
                    break; // Found the line to modify, exit the loop
                }
            }

            // Modify the line if it was found
            if (indexToModify >= 0)
            {
                lines[indexToModify] = $"{emp.Numero}: {emp.Nom} {emp.Prenom}, {emp.Adresse}, {emp.Age}, {emp.Fonction}, {emp.Salaire}";
            }

            // Rewrite the modified lines to the file
            File.WriteAllLines(filePath, lines.ToArray());
        }

        public void ModifyEmployeeInfo()
        {
            string searchName = getString("Veuillez saisir le nom de l'employé: ").ToLower();

            bool found = false;

            for (int i = 0; i < employes.Count; i++)
            {
                if (employes[i] is Employe && ((Employe)employes[i]).Nom.ToLower() == searchName)
                {
                    found = true;

                    Console.WriteLine($"Employé avec le nom {searchName} trouvé et prêt à être modifié.");

                    int choice;

                    do
                    {
                        Console.WriteLine("\nSélectionnez ce que vous souhaitez faire:");
                        Console.WriteLine("1. Change Address.");
                        Console.WriteLine("2. Change Fonction.");
                        Console.WriteLine("3. Augmentation Salaire.");
                        Console.WriteLine("4. Arrêter.\n");

                        if (int.TryParse(Console.ReadLine(), out choice))
                        {
                            switch (choice)
                            {
                                case 1:
                                    string newAddress = getString("\nSaisissez la nouvelle adresse: ");
                                    ((Employe)employes[i]).Adresse = newAddress;
                                    Console.WriteLine($"L'address de l'employe a été changé.\n");
                                    break;

                                case 2:
                                    string nouvelle_fonction = getString("Faites l'affectation: ");
                                    ((Employe)employes[i]).affectation(nouvelle_fonction,x => Console.WriteLine(x.ToUpper()));
                                    break;

                                case 3:
                                    double salaryIncrease = double.Parse(getString("Saisissez le montant de l'augmentation: "));
                                    ((Employe)employes[i]).augmentation(salaryIncrease);
                                    Console.WriteLine($"Le salaire de l'employe a été augmenté.\n");
                                    break;

                                case 4:
                                    Console.WriteLine("Arrêter.");
                                    break;

                                default:
                                    Console.WriteLine("Choix non valide");
                                    break;
                            }
                        }
                    } 
                    while (choice != 4);

                    ModifyDataFile(((Employe)employes[i]));
                }
            }

            if (!found)
            {
                Console.WriteLine($"Aucun employé trouvé avec le nom {searchName}.");
            }
        }

        // TODO :  Exercice 1.1 (Liste des employés)
        public void displayList()
        {
            if (employes != null)
            {
                Console.WriteLine("Liste des employés:");

                foreach (Employe e in employes)  // Utilisation de l'interface IEnumerable
                {
                    Console.WriteLine($"Matricule_N{e.Numero}: {e.getInfo()}");
                }
            }
            else
            {
                Console.WriteLine("Aucun employé existé.");
            }
        }

        public void DisplayStatistics()
        {
            Console.WriteLine($"Nombre d'employés : {employes.Count}");
            Console.WriteLine($"Total salaire : {ChargeSalariale}");
            Console.WriteLine($"Salaire moyen : {(ChargeSalariale / employes.Count)}");

            // TODO : Exercice 1.8 (Recherche de la charge salariale pour une fonction recherchée)
            // Call chargeSalarialeParFonction to get statistics for a specific function
            string fonctionToCheck = getString("\nVeuillez saisir la fonction à rechercher:");
            double chargeSalarialeForFunction;

            int employeeCountForFunction = chargeSalarialeParFonction(fonctionToCheck, out chargeSalarialeForFunction);

            if (employeeCountForFunction > 0)
            {
                Console.WriteLine($"\nNombre des employes avec la fonction '{fonctionToCheck}': {employeeCountForFunction}");
                Console.WriteLine($"Total salaire pour la fonction '{fonctionToCheck}': {chargeSalarialeForFunction}");
                Console.WriteLine($"Salaire moyen pour la fonction '{fonctionToCheck}': {chargeSalarialeForFunction / employeeCountForFunction}");
            }
            else
            {
                Console.WriteLine($"Aucun employe trouve avec cette fonction {fonctionToCheck}");
            }

        }

        public int chargeSalarialeParFonction (string fonction, out double chargeSalariale)
        {
            // Use LINQ to filter employees by the specified function
            var employeesWithFunction = from emp in employes
                                                            where emp.Fonction.ToUpper() == fonction.ToUpper()
                                                            select emp;

            // Count the number of employees with the specified function
            int employeeCount = employeesWithFunction.Count();

            // Calculate the total salary for employees with the specified function
            chargeSalariale = employeesWithFunction.Sum(emp => emp.Salaire);

            // Return the number of employees
            return employeeCount;
        }

        #endregion

        #region Propriétés
        public int Effectif
        {
            get { return employes.Count; }
        }
        public Employe this[int index]
        {
            get
            {
                if (index >= 0 && index <= employes.Capacity)
                {
                    return (Employe)employes[index];
                }

                return null;
            }
            set
            {
                if (index >= 0 && index <= employes.Capacity)
                {
                    employes[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Indice en dehors des limites du tableau employes.");
                }
            }
        }

        // TODO :  Exercice 1.4 (Calculer les salaires)
        public double ChargeSalariale
        {
            get
            {
                double total = 0;

                foreach (Employe e in employes)
                {
                    if (e is Employe)
                    {
                        Employe emp = (Employe)e;
                        total += emp.Salaire;
                    }
                }

                return total;
            }

            private set { }
        }
        #endregion

        #region Dispose
        private bool isDisposed = false;

        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    if (employes != null)
                    {
                        foreach (Employe e in employes)
                        {
                            nomEnt = null;
                        }
                    }
                }

                isDisposed = true;
            }
        }
        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #region Enumerable
        public IEnumerator<Employe> GetEnumerator()
        {
            return employes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return employes.GetEnumerator();
        }
        #endregion

        #region Destructor
        ~Entreprise()
        {
            Dispose(false);
        }
        #endregion
    }
}