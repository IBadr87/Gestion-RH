using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Info;
using System.Net.NetworkInformation;

namespace JeuPoM
{
    class Jeu
    {
        // This code is designed to allow users to guess a random number and track their best score and game history.
        // The game loop ensures that the user can play multiple times if they wish. 


        // Variables declaration and initiation 
        static int valeurSecrete, valeurSaisie;
        static string reponse;

        static int tentatives = 0;
        static int meilleurScore = 50;

        static Partie[] historique = new Partie[20];
        static int Parties = 0;

        // TODO : Exercice 1.3 (Modularisation des interactions utilisateurs)
        static string GetString(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        static int GetInt(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            int Output;

            // TODO : Exercice 2 (Mise en œuvre de la levée d’exception)
            if (int.TryParse(input, out Output))
            {
                return Output;
            }
            else
            {
                throw new Exception("La valeur saisie n'est pas valide.");
            }
        }

        static void Jouer()
        {
            Random rnd = new Random();
            valeurSecrete = rnd.Next(100);

            do
            {
                // TODO : Exercice 1.5 (Modularisation des interactions utilisateurs)
                // TODO : Exercice 3 (Capture d’exception)
                try
                {
                    valeurSaisie = GetInt("Veuillez saisir un nombre entier entre 0 et 100: ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                    continue;
                }

                tentatives++;

                if (valeurSaisie > valeurSecrete)
                {
                    Console.WriteLine("Désolé, la valeur est trop grande. \n");
                }
                else if (valeurSaisie < valeurSecrete)
                {
                    Console.WriteLine("Désolé, la valeur est trop petite. \n");
                }
                else
                {
                    Console.WriteLine("Félicitations ! Vous avez trouvé la valeur correcte. \n");

                    
                    Console.WriteLine("Nombre du Tentatives : " + tentatives);

                    if (tentatives < meilleurScore)
                    {
                        meilleurScore = tentatives;
                    }

                    Console.WriteLine("Meilleur score : " + meilleurScore + "\n");

                    historique[Parties] = new Partie(valeurSecrete, tentatives);
                    Parties++;
                }
            } while (valeurSaisie != valeurSecrete);
        }

        static void ReJouer()
        {
            Random rnd = new Random();
            valeurSecrete = rnd.Next(100);

            // TODO : Exercice 1.4 (Modularisation des interactions utilisateurs)
            // TODO : Exercice 3.1 (Capture d’exception)

            bool continuerJeu = false;
            while (!continuerJeu)
            {
                reponse = GetString("Voulez-vous rejouer une nouvelle partie ? (O/N): \n");
                try
                {
                    if (reponse.ToLower() == "o")
                    {
                        tentatives = 0;

                        Jouer();

                        valeurSecrete = rnd.Next(100);

                    }
                    else if (reponse.ToLower() == "n")
                    {
                        continuerJeu = false;

                        Console.WriteLine("Le Meilleur score est : " + meilleurScore + "\n");

                        break;
                    }
                    else
                    {
                        throw new Exception("La valeur saisie n'est pas valide, SVP entrez 'O' ou 'N'");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                    continue;
                }
            }
            InfoSaveInFile();
        }

        static void InfoSaveInFile()
        {
            // TODO : Exercice 1.2 (Modularisation de l’affichage d’historique)

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.FileName = "GAM.txt";
            saveFileDialog.Title = "Save As";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Utilitaire.afficheHistorique(Parties, historique, saveFileDialog.FileName);
            }
            else
            {
                Utilitaire.afficheHistorique(Parties, historique);
            }
        }

        [STAThread]
        static void Main(string[] args)
        {

            Personne joueur = null;

            string reponse = GetString("Souhaitez-vous vous identifier? (O/N): ");

            if (reponse.ToLower() == "o")
            {
                string nom = GetString("\n Quel est votre nom ? ");
                string prenom = GetString("\n Quel est votre prenom ? ");

                joueur = new Personne(nom, prenom);
            }
            else if (reponse.ToLower() == "n")
            {
                Console.WriteLine("Vous avez choisi de ne pas vous identifier.\n");
            }
            else
            {
                Console.WriteLine("La valeur saisie n'est pas valide, SVP entrez 'O' ou 'N'");
            }

            Jouer();
            ReJouer();

            Console.WriteLine("Appuyez sur n'importe quelle touche pour quitter...\nAu revoir");
            Console.ReadKey();
        }
    }
}