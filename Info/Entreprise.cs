using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public string nom;
        private Employe[] employes;

        #endregion


        #region Constructeurs

        public Entreprise(string nom)
        {
            this.nom = nom;
            this.effectif = 0;
            employes = new Employe [100];
        }

        public Entreprise() : this("")
        {
        }

        #endregion


        #region Methodes

        static string getString(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        static int getInt(string message)
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

        public void embauche(Employe e)
        {
            string filePath = "C:/Projets/VS/2022/Exercises/Module_11/data.txt";

            int lastEmployeeNumber = 0;

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    Match match = Regex.Match(line, @"Matricule_N(\d+):");

                    if (match.Success)
                    {
                        int number = int.Parse(match.Groups[1].Value);

                        if (number > lastEmployeeNumber)
                        {
                            lastEmployeeNumber = number;
                        }
                    }
                }
            }

            if (this.effectif >= employes.Length)
            {
                throw new Exception("Le nombre maximal d'employés est atteint. Impossible d'embaucher plus d'employés.");
            }

            int newEmployeeNumber = lastEmployeeNumber + 1;

            employes[this.effectif] = e;
            e.setNumero(newEmployeeNumber);  

            Console.WriteLine($"\nNouvel employé embauché, Matricule_N{e.getNumero()}: {e.nom} {e.prenom}");

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                string employeeInfo = $"Matricule_N{e.getNumero()}: {e.nom} {e.prenom}, {e.adresse}, {e.age}, {e.fonction}, {e.Salaire}";
                writer.WriteLine(employeeInfo);
            }
        }


        // TODO : Exercice 1.2 (Recrutement d’un nouvel employé)
        public void AddNewEmploye()
        {
            string nom = getString("Bonjour,\n\nVeuillez saisir le nom du nouvel employe: ");
            string prenom = getString("\nVeuillez saisir le prénom du nouvel employe: ");
            string adresse = getString("\nVeuillez saisir l'adresse du nouvel employe: ");
            string fonction = getString("\nVeuillez saisir la fonction du nouvel employe: ");

            int age = getInt("\nVeuillez saisir l'âge du nouvel employe: ");
            int salaire = getInt("\nVeuillez saisir le salaire du nouvel employe: ");

            Employe recrue = new Employe(nom, prenom, adresse, age, fonction, (double)salaire);

            embauche(recrue);
        }

        // TODO : Exercice 1.3 (Récupération d’un employé existant)
        public void EmployeExiste()
        {
            int matricule_N = getInt("\nVeuillez saisir le matricule_N de l'employé à récupérer : ");

            string filePath = "C:/Projets/VS/2022/Exercises/Module_11/data.txt";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                bool matchFound = false;

                foreach (string line in lines)
                {
                    Match match = Regex.Match(line, @"Matricule_N(\d+):");

                    if (match.Success)
                    {
                        int employeeMatricule = int.Parse(match.Groups[1].Value);

                        if (employeeMatricule == matricule_N)
                        {
                            string employeeInfo = line.Substring(match.Index + match.Length).Trim();

                            Console.WriteLine(employeeInfo);
                            matchFound = true;
                        }
                    }
                }
                if (!matchFound)
                {
                    Console.WriteLine("Aucun employé trouvé avec le matricule spécifié.");
                }
            }
        }

        public void DeleteEmployee(int matriculeToDelete)
        {
            string filePath = "C:/Projets/VS/2022/Exercises/Module_11/data.txt";

            string[] lines = File.ReadAllLines(filePath);

            List<string> updatedLines = new List<string>();

            bool found = false;
            foreach (string line in lines)
            {
                string[] data = line.Split(new[] { ':', ',' });

                if (data.Length >= 6)
                {
                    if (int.TryParse(data[0].Trim().Substring(11), out int matricule))
                    {
                        if (matricule == matriculeToDelete)
                        {
                            found = true;
                            Console.WriteLine($"Employe avec Matricule_N{matriculeToDelete} a été supprimé.");
                        }
                        else
                        {
                            updatedLines.Add(line);
                        }
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine($"Aucun employé trouvé avec Matricule_N{matriculeToDelete}.");
            }
            else
            {
                File.WriteAllLines(filePath, updatedLines);
            }
        }

        public void ModifyEmployeeInfo()
        {
            string filePath = "C:/Projets/VS/2022/Exercises/Module_11/data.txt";

            bool continueModifying = true;

            while (continueModifying)
            {
                string[] lines = File.ReadAllLines(filePath);

                Console.Write("Saisiez le matricule_N de l’employé pour modifier: ");
                if (int.TryParse(Console.ReadLine(), out int matriculeToModify))
                {
                    bool found = false;

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string line = lines[i];

                        string[] data = line.Split(new[] { ':', ',' });

                        if (data.Length >= 6)
                        {
                            if (int.TryParse(data[0].Trim().Substring(11), out int matricule))
                            {
                                if (matricule == matriculeToModify)
                                {
                                    found = true;
                                    Console.WriteLine($"Employé avec Matricule_N{matriculeToModify} trouvé et prêt à être modifié.");

                                    Console.WriteLine("\nSélectionnez ce que vous souhaitez faire: \n");
                                    Console.WriteLine("1. Change Address");
                                    Console.WriteLine("2. Change Fonction");
                                    Console.WriteLine("3. Augmentation Salaire");
                                    Console.WriteLine("4. Arrêter sans apporter de changements \n");

                                    if (int.TryParse(Console.ReadLine(), out int choice))
                                    {
                                        switch (choice)
                                        {
                                            case 1:
                                                string newAddress = getString("Saisissez la nouvelle adresse : ");
                                                data[2] = newAddress;
                                                lines[i] =
                                                    $"Matricule_N{matricule}:{data[1]}, {data[2]},{data[3]},{data[4]},{data[5]}";
                                                Console.WriteLine($"L'adresse de l'employé a été mise à jour.");
                                                break;

                                            case 2:
                                                string nouvelle_fonction = getString("Faites l'affectation: ");

                                                Employe employee = new Employe("John", "Doe", "Suiss", 30, "Manager",
                                                    500);
                                                employee.nom = data[1];
                                                employee.prenom = data[2];
                                                employee.fonction = data[4];

                                                employee.affectation(nouvelle_fonction,
                                                    x => Console.WriteLine(x.ToUpper()));

                                                lines[i] =
                                                    $"Matricule_N{matricule}:{data[1]},{data[2]},{data[3]}, {nouvelle_fonction},{data[5]}";
                                                Console.WriteLine("La fonction de l'employé a été mise à jour.");
                                                break;

                                            case 3:
                                                if (double.TryParse(data[5].Trim(), out double currentSalary))
                                                {
                                                    double salaryIncrease =
                                                        double.Parse(getString(
                                                            "Saisissez le montant de l'augmentation de salaire: "));
                                                    currentSalary += salaryIncrease;
                                                    data[5] = currentSalary.ToString();

                                                    lines[i] =
                                                        $"Matricule_N{matricule}:{data[1]},{data[2]},{data[3]},{data[4]}, {currentSalary}";
                                                    Console.WriteLine(
                                                        $"Le salaire de l'employé a été augmenté à {data[5]}");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Erreur: Le salaire actuel n'est pas valide.");
                                                }

                                                break;

                                            case 4:
                                                Console.WriteLine("Aucune modification n'a été effectuée.");
                                                break;

                                            default:
                                                Console.WriteLine("Choix non valide");
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine($"Aucun employé trouvé avec Matricule_N{matriculeToModify}.");
                    }
                    else
                    {
                        File.WriteAllLines(filePath, lines);
                    }

                    Console.WriteLine("\nVoulez-vous continuer à modifier d'autres employés? (Oui/Non)");
                    string continueChoice = Console.ReadLine();
                    continueModifying = continueChoice.Equals("Oui", StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    Console.WriteLine("Matricule_N non valide");
                }
            }
        }

        // TODO :  Exercice 1.1 (Liste des employés)
        public void ReadAndDisplay()
        {
            FileStream f = File.OpenRead("C:/Projets/VS/2022/Exercises/Module_11/data.txt");
            StreamReader sr = new StreamReader(f);
            string ligne = "";

            while ((ligne = sr.ReadLine()) != null)
            {
                string[] data = ligne.Split(new[] { ':', ',' });

                if (data.Length >= 5)
                {
                    Console.WriteLine($"{ligne}");
                }
            }

            sr.Close();
            f.Close();
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
                double totalSalaire = 0;
                effectif = 0;

                using (StreamReader reader = new StreamReader("C:/Projets/VS/2022/Exercises/Module_11/data.txt"))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(new[] { ':', ',' });

                        if (parts.Length >= 6)
                        {
                            if (double.TryParse(parts[5], out double salaire))
                            {
                                totalSalaire += salaire;
                            }
                        }

                        effectif++;
                    }
                }

                return totalSalaire;
            }

            private set {}
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
                            nom = null;
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