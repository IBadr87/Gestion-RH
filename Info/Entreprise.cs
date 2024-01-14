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

        public void embauche(Employe e)
        {
            if (this.effectif < employes.Length)
            {
                employes[this.effectif] = e;
                e.setNumero(this.effectif + 1);
                this.effectif++; 

                Console.WriteLine($"Nouvel employé embauché : {e.nom}");
            }
            else
            {
                throw new Exception("Le nombre maximal d'employés est atteint. Impossible d'embaucher plus d'employés.");
            }
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
                            e.Dispose();
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
