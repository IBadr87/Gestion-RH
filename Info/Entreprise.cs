using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Info
{
    public class Entreprise : IDisposable
    {
        #region Attributs
        
        public int effectif;
        public string nom;
        public Employe [] employes;

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
            Console.WriteLine($"List of Employes : ");

            if (effectif == 0)
            {
                Console.WriteLine("No employes in the company.");
            }
            else
            {
                for (int i = 0; i < effectif; i++)
                {
                    Employe emp = employes[i];
                    Console.WriteLine(emp.getInfo());
                }
            }
        }

        // TODO : Exercice 1.3 (Récupération d’un employé existant)
        public void EmployeExiste()
        {
            int numero = getInt("\nVeuillez saisir le numéro de l'employé à récupérer : ");
            Employe emp = null;

            foreach (Employe employe in employes)
            {
                if (employe != null && employe.numero == numero)
                {
                    emp = employe;
                    Console.WriteLine(employe.getInfo());
                    break;
                }
                else
                {
                    Console.WriteLine("\nAucun employé trouvé avec le numéro spécifié. Retour au Menu Principal.");
                }
            }
        }

        #endregion

        #region Propriétés
        // TODO :  Exercice 1.4 (Calculer les salaires)
        public double ChargeSalariale
        {
            get
            {
                double totalSalaire = 0;
                foreach (Employe employe in employes)
                {
                    if (employe != null)
                    {
                        totalSalaire += employe.Salaire;
                    }
                }
                return totalSalaire;
            }
        }

        public void DisplayStatistics()
        {
            Console.WriteLine($"Nombre d'employés : {effectif}");
            Console.WriteLine($"Charge salariale : {ChargeSalariale}");
            Console.WriteLine($"Salaire moyen (arrondi) : {Convert.ToInt32(ChargeSalariale/2)}");
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
