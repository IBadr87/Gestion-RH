using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Info;


namespace JeuPoM
{
    class Joueur : Personne
    {
        #region Attributs
        public Partie [] Parties;
        #endregion

        #region Constructeurs
        public Joueur(string nom, string prenom) : base (nom, prenom)

        {
            Parties = new Partie[20];
        }
        #endregion
    }
}