using System;
using System.IO;
using System.Windows.Forms;
using Info;

namespace JeuPoM
{
    class Jeu
    {
        static int valeurSecrete, valeurSaisie;
        static string reponse;
        static int tentatives = 0;
        static int meilleurScore = int.MaxValue;

        static string GetString(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        static int GetInt(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int output))
            {
                return output;
            }
            else
            {
                throw new Exception("La valeur saisie n'est pas valide.");
            }
        }

        static void LeJeu(Joueur player)
        {
            Random rnd = new Random();
            valeurSecrete = rnd.Next(100);

            do
            {
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
                    Console.WriteLine("Désolé, la valeur est trop grande.\n");
                }
                else if (valeurSaisie < valeurSecrete)
                {
                    Console.WriteLine("Désolé, la valeur est trop petite.\n");
                }
                else
                {
                    Console.WriteLine("Félicitations ! Vous avez trouvé la valeur correcte.\n");

                    Console.WriteLine("Nombre de Tentatives : " + tentatives);

                    if (tentatives < meilleurScore)
                    {
                        meilleurScore = tentatives;
                    }

                    Console.WriteLine("Meilleur score : " + meilleurScore + "\n");

                    Partie partie = new Partie(valeurSecrete, tentatives);
                    player.Parties[Partie.getNbParties() - 1] = partie;
                }
            } while (valeurSaisie != valeurSecrete);
        }

        static void ReLeJeu(Joueur player)
        {
            Random rnd = new Random();
            valeurSecrete = rnd.Next(100);

            bool continuerJeu = false;
            while (!continuerJeu)
            {
                reponse = GetString("Voulez-vous rejouer une nouvelle partie ? (O/N):\n");
                try
                {
                    if (reponse.ToLower() == "o")
                    {
                        tentatives = 0;

                        LeJeu(player);

                        valeurSecrete = rnd.Next(100);
                    }
                    else if (reponse.ToLower() == "n")
                    {
                        continuerJeu = true;
                        Console.WriteLine("Le Meilleur score est : " + meilleurScore + "\n");
                    }
                    else
                    {
                        throw new Exception("La valeur saisie n'est pas valide, SVP entrez 'O' ou 'N'.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                    continue;
                }
            }

            InfoSaveInFile(player);
        }

        static void InfoSaveInFile(Joueur player)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.FileName = "";
            saveFileDialog.Title = "Save As";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Utilitaire.afficheHistorique(player.Parties, Partie.getNbParties(), saveFileDialog.FileName);
            }
            else
            {
                Utilitaire.afficheHistorique(player.Parties, Partie.getNbParties());
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            Joueur player = null;
            string nom = GetString("Bonjour, Quel est votre nom ?");
            string prenom = GetString("Quel est votre prénom ?");
            player = new Joueur(nom, prenom);

            LeJeu(player);
            ReLeJeu(player);

            Console.WriteLine("Appuyez sur n'importe quelle touche pour quitter...\nAu revoir");
            Console.ReadKey();
        }
    }
}
