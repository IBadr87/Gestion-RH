using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Info
{
    public class Personne
    {
        #region Attributs
        public string nom, prenom, adresse;
        public int age;
        #endregion


        #region Constructeurs
        public Personne() : this("Badr", "Ibrahim", "Lyon", 36) { }

        public Personne(string nom, string prenom) : this(nom, prenom, "", 0) { }

        public Personne(string nom, string prenom, string adresse, int age)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.age = age;
        }
        #endregion


        #region Methodes
        public virtual string getInfo()
        {
            if (age != 0 && !adresse.Equals(""))
            {
                return this.nom + " " + this.prenom + ", " + "habite a " + this.adresse + ", " + this.age + " ans";
            }
            else
            {
                return this.nom + " " + this.prenom + ", aucune autre information disponible";
            }
        }
    }
    #endregion
}
