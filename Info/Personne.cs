using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Info
{
    public class Personne
    {
        #region Attributs

        private string nom;
        private string prenom;
        private int age;

        #endregion


        #region Constructeurs

        public Personne() : this("Badr", "Ibrahim", "Lyon", 36)
        {
        }

        public Personne(string nom, string prenom) : this(nom, prenom, "", 0)
        {
        }

        public Personne(string nom, string prenom, string adresse, int age)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Adresse = adresse;
            this.Age = age;
        }

        #endregion

        #region Methodes

        public virtual string getInfo()
        {
            if (age != 0 && !Adresse.Equals(""))
            {
                return this.Nom + " " + this.Prenom + ", habite a " + this.Adresse + ", " + this.Age + " ans";
            }
            else
            {
                return this.Nom + " " + this.Prenom + ", aucune autre information disponible";
            }
        }

        #endregion


        #region Propriétés

        public int Age
        {
            get { return age; }
            private set
            {
                if (value <= 0 && value >= 150)
                {
                    throw new Exception("l'age doit etre entre 0 et 150 ans");
                }

                age = value;
            }
        }

        public string Nom
        {
            get { return nom.ToUpper();}

            set { nom = value; }
        }

        public string Prenom
        {
            get
            {
                var initial = prenom.Substring(0, 1).ToUpper();
                return initial + prenom.Substring(1).ToLower();
            }

            set { prenom = value; }
        }
        
        public string Adresse { get; set; }
        #endregion
    }
}
