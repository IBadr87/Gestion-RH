using System;

namespace Info
{
    public class Employe : Personne, IDisposable
    {
        #region Attributs
        private int numero;
        private double salaire;
        #endregion


        #region Constructeurs
        public Employe(int numero, string nom, string prenom, string adresse, int age, string fonction, double salaire) : base(nom, prenom, adresse, age)
        {
            this.numero = numero;
            this.Fonction = fonction;
            this.Salaire = salaire;
        }

        public Employe(Personne p, int numero, string fonction, double salaire) : this(numero, p.Nom, p.Prenom, p.Adresse, p.Age, fonction, salaire) { }
        #endregion


        #region Methodes
        public override string getInfo()
        {
                return base.getInfo() + this.Fonction + ", " + this.Salaire;
        }

        public void augmentation(double montant)
        {
            this.Salaire += montant;
        }

        public void affectation(string nouvelle_fonction, Action<String> methode)
        {
            methode(this.Nom + " " + this.Prenom + "," + this.Fonction + ": devient " + nouvelle_fonction);
            this.Fonction = nouvelle_fonction;
        }
        #endregion


        #region Propriétés
        public double Salaire
        {
            get { return salaire; }
            private set
            {
                if (value <= 0)
                {
                    throw new Exception("le salaire ne peut pas etre null ou negative");
                }
                salaire = value;
            }
        }

        public int Numero
        {
            get { return numero; }
        }

        public string Fonction { get; private set; }
        #endregion

        #region Dispose 
        private bool isDisposed = false;

        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    Fonction = null;
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
