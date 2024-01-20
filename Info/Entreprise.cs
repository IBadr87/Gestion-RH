using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Info
{
    public class Entreprise : IDisposable
    {
        #region Attributs
        public int effectif;
        public string nom;
        private Employe [] employes;
        #endregion


        #region Constructeurs
        public Entreprise(string nom)
        {
            this.nom = nom;
            this.effectif = 0;
            employes = new Employe [100];
        }

        public Entreprise() : this("") { }
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
            if (this.effectif < employes.Length)
            {
                employes[this.effectif] = e;
                e.setNumero(this.effectif + 1);
                this.effectif++; 

                Console.WriteLine($" \nNouvel employé embauché : {e.nom} {e.prenom} \n");
            }
            else
            {
                throw new Exception("Le nombre maximal d'employés est atteint. Impossible d'embaucher plus d'employés.");
            }
        }

        // TODO : Exercice 1.2 (Recrutement d’un nouvel employé)
        public void AddNewEmploye()
        {
            using (Entreprise En = new Entreprise("Conduent"))
            {
                string nom = getString("Bonjour,\n\nVeuillez saisir le nom du nouvel employe : ");
                string prenom = getString("\nVeuillez saisir le prénom du nouvel employe : ");
                string adresse = getString("\nVeuillez saisir l'adresse du nouvel employe : ");
                string fonction = getString("\nVeuillez saisir la fonction du nouvel employe : ");

                int age = getInt("\nVeuillez saisir l'âge du nouvel employe : ");
                int salaire = getInt("\nVeuillez saisir le salaire du nouvel employe : ");

                Employe recrue = new Employe(nom, prenom, adresse, age, fonction, (double)salaire);
                En.embauche(recrue);

                UpdateList(recrue);
            }
        }

        public void UpdateList(Employe employe)
        {
            if (effectif < employes.Length)
            {
                employes[effectif] = employe;
                employe.setNumero(effectif + 1);
                effectif++;
            }
            else
            {
                throw new Exception("Le nombre maximal d'employés est atteint. Impossible d'embaucher plus d'employés.");
            }
        }

        // TODO :  Exercice 1.1 (Liste des employés)
        public void DisplayEmployeeList()
        {
            Console.WriteLine($"Employes List: ");

            if (effectif == 0)
            {
                Console.WriteLine("Aucun employé n'est présent dans l'entreprise.");
            }
            else
            {
                for (int i = 0; i < effectif; i++)
                {
                    Employe emp = employes[i];
                    if (emp != null) 
                    {
                        Console.WriteLine($"matricule N{emp.setNumero(i + 1)} : {emp.getInfo()}");
                    }
                }
            }
        }

        // TODO : Exercice 1.3 (Récupération d’un employé existant)
        public void EmployeExiste()
        {
            int numero = getInt("\nVeuillez saisir le numéro de l'employé à récupérer : ");
            Employe emp = null;

            for (int i = 0; i < effectif; i++)
            {
                if (employes[i] != null && employes[i].setNumero(i + 1) == numero)
                {
                    emp = employes[i];
                    Console.WriteLine($"matricule N {numero}: " + emp.getInfo());
                    break;
                }
            }

            if (emp == null)
            {
                Console.WriteLine($"\nAucun employé trouvé avec le numéro {numero}. Retour au Menu Principal.");
            }
        }

        public bool DeleteEmployee()
        {
            int matricule = getInt("Enter the matricule of the employee to delete: ");
            Employe emp = null;
            int indexToDelete = -1;

            for (int i = 0; i < effectif; i++)
            {
                if (employes[i] != null && employes[i].setNumero(i + 1) == matricule)
                {
                    emp = employes[i];
                    indexToDelete = i;
                    Console.WriteLine($"Matricule N : {matricule}: " + emp.getInfo());
                    break;
                }
            }

            if (emp != null)
            {
                for (int i = indexToDelete; i < effectif - 1; i++)
                {
                    employes[i] = employes[i + 1];
                }
                employes[effectif - 1] = null;

                effectif--;

                Console.WriteLine($"Employee with matricule N : {matricule} has been deleted.");
                return true;
            }
            else
            {
                Console.WriteLine($"No employee found with matricule N : {matricule}. Returning to the main menu.");
                return false;
            }
        }


        public void ModifyEmployeeInfo()
        {
            int matricule = getInt("Veuillez saisir le matricule N de votre employee: ");
            Employe emp = null;

            for (int i = 0; i < effectif; i++)
            {
                if (employes[i] != null && employes[i].setNumero(i + 1) == matricule)
                {
                    emp = employes[i];
                    Console.WriteLine($"Matricule N : {matricule}: " + emp.getInfo());

                    emp.adresse = getString("Veuillez saisir la nouvelle adresse: ");
                    emp.fonction = getString("Veuillez saisir la nouvelle function: ");
                    emp.Salaire = getInt("EVeuillez saisir le nouveau salaire: ");
                    Console.WriteLine("La mise à jour de l'information de votre employé a été effectuée avec succès.");
                    break;
                }
            }

            if (emp == null)
            {
                Console.WriteLine($"Aucun matricule N: {matricule} trouver.");
            }
        }

        public void SaveEmployeeInfoToFile()
        {
            using (StreamWriter writer = new StreamWriter($"C:/Projets/VS/2022/Exercises/Module_11/data.txt", true))
            {
                int currentEmployeeNumber = 1;

                foreach (Employe emp in employes)
                {
                    if (emp != null)
                    {
                        emp.setNumero(currentEmployeeNumber);

                        string employeeInfo = $"Matricule N : {emp.setNumero(currentEmployeeNumber)} : {emp.nom}, {emp.prenom}, {emp.adresse}, {emp.age}, {emp.fonction}, {emp.Salaire}";
                        writer.WriteLine(employeeInfo);

                        currentEmployeeNumber++;
                    }
                }
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
                double totalSalaire = 0;
                for (int i = 0; i < this.effectif; i++)
                {
                    Employe employe = this[i];
                    if (employe != null)
                    {
                        totalSalaire += this.employes[i].Salaire;
                    }
                }
                return totalSalaire;
            }
        }

        public void DisplayStatistics()
        {
            Console.WriteLine($"Nombre d'employés : {effectif}");
            Console.WriteLine($"Charge salariale : {ChargeSalariale}");
            Console.WriteLine($"Salaire moyen (arrondi) : {Convert.ToInt32(ChargeSalariale/this.effectif)}");
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