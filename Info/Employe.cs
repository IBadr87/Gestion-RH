using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Info
{
    public class Employe : Personne
    {
        #region Attributs
        private int numero;
        private double salaire;
        public string fonction;
        #endregion


        #region Constructeurs
        public Employe(string nom, string prenom, string adresse, int age, string fonction,
            double salaire) : base(nom, prenom, adresse, age)
        {
            this.fonction = fonction;
            this.salaire = salaire;
        }

        public Employe(Personne p, string fonction, double salaire) : this(p.nom, p.prenom, p.adresse, p.age, fonction, salaire) { }
        #endregion


        #region Méthodes

        public override string getInfo()
        {
                return base.getInfo() + this.fonction + this.salaire;
        }

        public void augmentation(double montant)
        {
            this.salaire += montant;
        }

        public void affectation(string nouvelle_fonction)
        {
            this.fonction = nouvelle_fonction;
        }
        #endregion
    }
}
