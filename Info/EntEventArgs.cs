using System;

namespace Info
{
    public class EntEventArgs : EventArgs
    {
        #region Attributs
        public string posteRestant { get; }
        #endregion

        #region Constructeurs
        public EntEventArgs(int nbPosteRestant) : base()
        {
            this.posteRestant = "Il reste " + nbPosteRestant + " poste(s) libre.";
        }
        #endregion
    }
}