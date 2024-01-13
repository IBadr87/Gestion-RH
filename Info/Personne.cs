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
        public Personne() : this("Do", "John", "", 0) { }

        public Personne(string nom, string prenom) : this(nom, prenom, "", 0) { }

        public Personne(string nom, string prenom, string adresse, int age)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.age = age;
        }
        #endregion


        #region Méthodes
        public virtual string getInfo()
        {
            if (age != 0 && !adresse.Equals(""))
            {
                return nom + " " + prenom + ", " + " habite a " + adresse + ", " + age + " ans ";
            }
            else
            {
                return nom + " " + prenom + ", aucune autre information disponible";
            }
        }
    }
    #endregion
}
