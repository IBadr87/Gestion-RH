using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Info
{
    public class Entreprise : IDisposable
    {
        #region Attributs
        public int effectif;
        public string nomEnt;
        private Employe[] employes;
        public event Action<Object, EntEventArgs> InfoEffectif = null;
        private static string filePath = "C:/Projets/VS/2022/Exercises/Module_11/Ex_2/data.txt";
        #endregion


        #region Constructeurs
        public Entreprise(string nomEnt)
        {
            this.nomEnt = nomEnt;
            employes = new Employe [100];
            this.effectif = 0;
        }

        public Entreprise() : this("Conduent")
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
                    string[] data = ligne.Split(new[] { ':', ',' });

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
            if (this.effectif < employes.Length)
            {
                employes[this.effectif] = emp;
                this.effectif++;
            }
            else
            {
                throw new Exception("Le nombre maximal d'employés est atteint. Impossible d'embaucher plus d'employés.");
            }

            // TODO : Exercice 2.1 (Création d’un événement)
            int nbPosteRestant = employes.Length - this.effectif;
            if (InfoEffectif != null)
            {
                EntEventArgs eventArgs = new EntEventArgs(nbPosteRestant);
                InfoEffectif(this, eventArgs);
            }

            // TODO : Exercice 2.2 (Gestion de l’événement)
            //InfoEffectif += new Action<Object, EntEventArgs>((o, e) => Console.WriteLine(e.posteRestant));
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

            for (int i = 0; i < employes.Length; i++)
            {
                if (employes[i] != null && employes[i].Numero > newNumero)
                {
                    newNumero = employes[i].Numero;
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

        // TODO : Exercice 1.3 (Récupération d’un employé existant)
        public void EmployeExiste()
        {
            string searchName = getString("Veuillez saisir le nom de l'employé à rechercher: ").ToLower();

            bool found = false;

            for (int i = 0; i < effectif; i++)
            {
                Employe emp = employes[i];

                if (emp != null && emp.Nom.ToLower() == searchName)
                {
                    Console.WriteLine($"Employé trouvé, Matricule_N{emp.Numero}: {emp.getInfo()}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Aucun employé trouvé avec le nom '{searchName}'.");
            }
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
            for (int i = 0; i < effectif; i++)
            {
                if (employes[i] != null && employes[i].Numero == matricule)
                {
                    indexToDelete = i;
                    break; // Found the employee, exit the loop
                }
            }

            if (indexToDelete >= 0)
            {
                // Shift the elements in the array to remove the employee
                for (int i = indexToDelete; i < effectif - 1; i++)
                {
                    employes[i] = employes[i + 1];
                }

                // Set the last element to null to clear the reference
                employes[effectif - 1] = null;

                // Decrease the employee count
                effectif--;

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

            for (int i = 0; i < effectif; i++)
            {
                Employe emp = employes[i];

                if (emp != null && emp.Nom.ToLower() == searchName)
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
                                    emp.Adresse = newAddress;
                                    Console.WriteLine($"L'address de l'employe a été changé.\n");
                                    break;

                                case 2:
                                    string nouvelle_fonction = getString("Faites l'affectation: ");
                                    emp.affectation(nouvelle_fonction,x => Console.WriteLine(x.ToUpper()));
                                    break;

                                case 3:
                                    double salaryIncrease = double.Parse(getString("Saisissez le montant de l'augmentation: "));
                                    emp.augmentation(salaryIncrease);
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

                    ModifyDataFile(emp);
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

                for (int i = 0; i < effectif; i++)
                {
                    Employe emp = employes[i];

                    Console.WriteLine($"Matricule_N{emp.Numero}: {emp.getInfo()}");
                }
            }
            else
            {
                Console.WriteLine("Aucun employé existé.");
            }
        }
        #endregion

        #region Propriétés
        public Employe this[int index]
        {
            get
            {
                if (index >= 0 && index <= this.effectif)
                {
                    return this.employes[index];
                }

                return null;
            }
            set
            {
                if (index >= 0 && index <= this.effectif)
                {
                    this.employes[index] = value;
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

                for (int i = 0; i < this.effectif; i++)
                {
                    total += this.employes[i].Salaire;
                }

                return total;
            }

            private set { }
        }

        public void DisplayStatistics()
        {
            Console.WriteLine($"Charge salariale : {ChargeSalariale}");
            Console.WriteLine($"Nombre d'employés : {effectif}");
            Console.WriteLine($"Salaire moyen : {(ChargeSalariale / effectif)}");
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Entreprise()
        {
            Dispose(false);
        }
        #endregion
    }
}