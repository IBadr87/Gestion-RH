using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuPoM
{
    public static class Utilitaire
    {
        // TODO : Exercise 1.1 (Modularisation de l’affichage d’historique)

        public static void afficheHistorique(this Partie[] tab, int compteur)
        {
            for (int i = 0; i <= compteur; i++)
            {
                Console.WriteLine("Partie N°{0}, " + tab[i].info(), i + 1);
            }
        }

        // TODO Mo.5 (Génération d’un fichier d’historique)
        public static void afficheHistorique(this Partie[] tab, int compteur, string nomFichier)
        {
            try
            {
                if (tab != null)  // Check if tab is not null
                {
                    FileStream fs = new FileStream(nomFichier, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);

                    sw.WriteLine("vos parties : ");

                    for (int i = 0; i <= compteur; i++)
                    {
                        if (tab[i] != null)  // Check if the element at index i is not null
                        {
                            sw.WriteLine("Partie N°{0}, {1}", i + 1, tab[i].info()); 
                        }
                        else
                        {
                            sw.WriteLine("Partie N°{0}, N/A", i + 1); // Handle null element
                        }
                    }

                    sw.Close();
                    fs.Close();
                }
                else
                {
                    Console.WriteLine("The tab array is null.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }
        }
    }
}
