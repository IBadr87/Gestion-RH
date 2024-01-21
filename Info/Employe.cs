using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Info
{
    public class Employe : Personne, IDisposable
    {
        #region Attributs
        private int numero;
        private double salaire;
        public string fonction;
        #endregion


        #region Constructeurs
        public Employe(string nom, string prenom, string adresse, int age, string fonction, double salaire) : base(nom, prenom, adresse, age)
        {
            this.fonction = fonction;
            this.salaire = salaire;
        }

        public Employe(Personne p, string fonction, double salaire) : this(p.nom, p.prenom, p.adresse, p.age, fonction, salaire) { }
        #endregion


        #region Methodes
        public override string getInfo()
        {
                return base.getInfo() + ", " + this.fonction + ", " + this.salaire;
        }

        public void augmentation(double montant)
        {
            this.salaire += montant;
        }

        public void affectation(string nouvelle_fonction)
        {
            this.fonction = nouvelle_fonction;
        }

        public void affectation(string nouvelle_fonction, Action<String> methode)
        {
            methode(this.nom + " " + this.prenom + ", " + this.fonction + ": devient " + nouvelle_fonction);
            this.fonction = nouvelle_fonction;
        }

        public int setNumero(int matricule_N)
        {
            return this.numero += matricule_N;
        }

        public int getNumero()
        {
            return numero;
        }
        #endregion

        #region Propriétés
        public double Salaire
        {
            get { return salaire; }
            set { salaire = value; }
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
                    fonction = null;
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Employe()
        {
            Dispose(false);
        }
        #endregion
    }
}
