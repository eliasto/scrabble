using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    /* <summary>
     * La classe utils comporte des fonctions permettant d'aider au bon fonctionnement du jeu.
     * Les fonctions ici présentes ne sont pas adaptées pour se trouver dans d'autres classes.
     * </summary>
     */
    public class Utils
    {
        static public List<Joueur> joueurs = new List<Joueur>(); //Liste des joueurs
        static public bool isStart = false; //Etat de la partie (si elle a commencé ou pas
        static public void Welcome() //Affiche un message de bienvenue
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" ___   ___  ____    __    ____  ____  __    ____ ");
            Console.WriteLine("/ __) / __)(  _ \\  /__\\  (  _ \\(  _ \\(  )  ( ___)");
            Console.WriteLine("\\__ \\( (__  )   / /(__)\\  ) _ < ) _ < )(__  )__) ");
            Console.WriteLine("(___/ \\___)(_)\\_)(__)(__)(____/(____/(____)(____)");
            Console.WriteLine("                                                 ");
            Console.ResetColor();
            Console.WriteLine("   --- Bienvenue sur ce jeu de Scrabble ---    ");
            Console.WriteLine(" ");
        }

        static public void MenuPrincipal() //Affiche le menu principal
        {
            int OptionSelectionne = 0; //Sous-menu sélectionné
            string[] menu = { "Créer un joueur", "Supprimer un joueur", "Exporter les paramètres de la partie", "Importer une partie", "Rénitialiser la partie", "Lancer le jeu" }; //Sous-menus
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (key.KeyChar != 13) //Si la touche tapée est différente de la touche entrée
            {
                Console.Clear();
                Welcome();
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("               Menu principal               ");
                Console.ResetColor();

                for (int i = 0; i < menu.Length; i++) //affiche les sous-menus
                {
                    if (OptionSelectionne == i) 
                    {
                        Console.Write(">>");
                        Console.WriteLine(" " + (i+1) + ". " + menu[i]); //si l'id du sous menu selectionné vaut le i de la boucle, alors on affiche ">>" devant
                    }
                    else
                    {
                        Console.WriteLine("   "+(i+1)+". "+menu[i]);
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAppuyez sur entrée pour valider votre choix.");
                Console.ResetColor();
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Etat actuel de la partie: ");
                string joueurTexte = Utils.joueurs.Count == 0 ? "joueur" : "joueurs";
                Console.WriteLine("Il y a " + Utils.joueurs.Count + " " + joueurTexte + "."); //Retourne le nombre de joueur
                if(Utils.joueurs.Count != 0)
                {
                    foreach (Joueur joueur in joueurs)
                    {
                        Console.WriteLine("  - " + joueur.Nom+", score: "+joueur.Score); //Retourne le score de chaque joueur
                    }
                }
                Console.ResetColor();

                key = Console.ReadKey(true);
                if (key.Key.ToString() == "DownArrow") //Si on appuie sur la flèche du bas, alors optionselectionnee--
                {
                    OptionSelectionne++;
                    if (OptionSelectionne > menu.Length - 1)
                    {
                        OptionSelectionne = 0;
                    }
                }
                else if (key.Key.ToString() == "UpArrow") //Si on appuie sur la flèche du haut, alors optionselectionnee++
                {
                    OptionSelectionne--;
                    if (OptionSelectionne < 0) {
                        OptionSelectionne = menu.Length - 1;
                    }
                }
            }
            Console.Clear();
            Welcome();
            switch (OptionSelectionne) //switch pour ouvrir chaque sous-menu
            {
                case 0: //Permet d'ajouter un joueur
                    if (isStart) Program.Main(); //on ne peut pas sélectionné l'option si la partie est lancé
                    if(joueurs.Count > 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("La limite de joueur est de 4. Pour ajouter un autre joueur vous devez en supprimer un.");
                        Console.ResetColor();
                        Console.WriteLine(" ");
                        Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                        Console.ReadKey();
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Veuillez écrire le nom de votre joueur: ");
                        string nom = Console.ReadLine();
                        if(joueurs.Find(r=> r.Nom.Contains(nom)) != null) //si le joueur exite déjà
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ce joueur existe déjà.");
                            Console.ResetColor();
                            Console.WriteLine(" ");
                            Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                            Console.ReadKey();

                        } else //sinon
                        {
                            joueurs.Add(new Joueur(nom)); //ajoute un joueur
                            Console.ResetColor();

                        }
                    }
                    
                    break;
                case 1: //supprimer un joueur
                    if (joueurs.Count < 1 || isStart == true) Program.Main(); //s'il n'y a pas de joueur ou que la partie a commencé
                    
                    ConsoleKeyInfo keySuppression = new ConsoleKeyInfo();
                    int OptionSelectionneSuppression = 0;

                    while (keySuppression.KeyChar != 13 && keySuppression.KeyChar != 27)
                    {
                        Console.Clear();
                        Welcome();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Quel joueur voulez-vous supprimer ?");
                        Console.WriteLine(" ");
                        Console.ResetColor();

                        for (int i = 0; i < joueurs.Count; i++)
                        {
                            if (OptionSelectionneSuppression == i)
                            {
                                Console.Write(">>");
                                Console.WriteLine(" " + (i + 1) + ". " + joueurs[i].Nom);
                            }
                            else
                            {
                                Console.WriteLine("   " + (i + 1) + ". " + joueurs[i].Nom);
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nAppuyez sur entrée pour valider votre choix.\nAppuyez sur ECHAP pour revenir au menu précédent.");
                        Console.ResetColor();

                        keySuppression = Console.ReadKey(true);
                        if (keySuppression.Key.ToString() == "DownArrow")
                        {
                            OptionSelectionneSuppression++;
                            if (OptionSelectionneSuppression > joueurs.Count - 1)
                            {
                                OptionSelectionneSuppression = 0;
                            }
                        }
                        else if (keySuppression.Key.ToString() == "UpArrow")
                        {
                            OptionSelectionneSuppression--;
                            if (OptionSelectionneSuppression < 0)
                            {
                                OptionSelectionneSuppression = joueurs.Count - 1;
                            }
                        }
                    } //même chose que pour les sous-menus
                    if(keySuppression.KeyChar != 27)
                    {
                        joueurs.RemoveAll(r => r.Nom == joueurs[OptionSelectionneSuppression].Nom); //supprime le joueur sélectionné
                    }
                    break;
                case 2: //exporter les paramètres de jeu
                    Console.Clear();
                    Console.WriteLine("Comment voulez-vous appeler le fichier qui exportera les paramètres de la partie ?");
                    Console.WriteLine("Exemple: partieScrabble");
                    string value = Console.ReadLine(); //nom du fichier
                    try
                    {
                        List<string> dataPlayers = new List<string>(); //liste qui contiendra les données des joueurs
                        foreach(Joueur joueur in joueurs)
                        {
                            dataPlayers.Add(joueur.Nom + ";" + joueur.Score + ";"); //écrit une ligne pour chaque joueur

                            string valueWords = "";
                            if(joueur.MotsTrouves.Count > 0)
                            {
                                for (int i = 0; i < joueur.MotsTrouves.Count - 1; i++)
                                {
                                    valueWords += joueur.MotsTrouves[i] + ";"; //écrit sur une ligne chaque mots trouvés par le joueur
                                }
                            }
                            valueWords += joueur.MotsTrouves[joueur.MotsTrouves.Count - 1];
                            dataPlayers.Add(valueWords); //ajoute dans la liste

                            valueWords = "";
                            if(joueur.MainCourante.Count > 0)
                            {
                                for (int i = 0; i < joueur.MainCourante.Count - 1; i++)
                                {
                                    valueWords += joueur.MainCourante[i].Lettre + ";"; //pour chaque lettre dans la main
                                }
                            }
                            valueWords += joueur.MainCourante[joueur.MainCourante.Count - 1].Lettre;
                            dataPlayers.Add(valueWords); //ajoute dans la liste
                        }

                        //Partie plateau;
                        List<string> plateau = new List<string>();
                        /* <summary>
                         * Convertit le plateau dans un fichier avec les espaces en _
                         * </summary>
                         */
                        for(int i = 0; i < Plateau.lettres.GetLength(0); i++)
                        {
                            string valueLettre = "";
                            for(int j = 0; j < Plateau.lettres.GetLength(1)-1; j++)
                            {
                                if(Plateau.lettres[i,j]==' ')
                                {
                                    valueLettre += "_;";
                                } else
                                {
                                    valueLettre += Plateau.lettres[i, j] + ";";
                                }
                            }
                            if (Plateau.lettres[i, Plateau.lettres.GetLength(1) - 1] == ' ')
                            {
                                valueLettre += "_";
                            }
                            else
                            {
                                valueLettre += Plateau.lettres[i, Plateau.lettres.GetLength(1) - 1] ;
                            }
                            plateau.Add(valueLettre);
                        }

                        //Ecrit les fichiers
                        File.WriteAllLines(value+"_joueurs.txt", dataPlayers);
                        File.WriteAllLines(value + "_plateau.txt", plateau);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Partie sauvegardé sur le fichier '"+value+ "' ! Appuyez sur n'importe quelle touche pour revenir au menu principal.");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    catch (Exception e) //Affiche les erreurs
                    {
                        Console.WriteLine("Une erreur est survenue: " + e);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Appuyez sur n'importe quelle touche pour revenir au menu principal.");
                        Console.ResetColor();
                        Console.ReadKey();
                        Program.Main();
                    }
                    break;
                case 3: //importer un fichier
                    Console.Clear();
                    Console.WriteLine("Comment s'appelle le fichier que vous voulez importer ?");
                    Console.WriteLine("Exemple: partieScrabble");
                    string valueName = Console.ReadLine();
                    if(joueurs.Count != 0) //S'il y a déjà des joueurs
                    {
                        Console.WriteLine("Impossible d'importer une partie dans une partie en cours. Appuyez sur une touche.");
                        Console.ReadKey();
                    } else
                    {
                        try
                        {
                            //Lit les fichiers
                            string[] lectureFichier = File.ReadAllLines(valueName + "_joueurs.txt");
                            string[] plateau = File.ReadAllLines(valueName + "_plateau.txt");
                            
                            //Convertit le fichier plateau en un plateau
                            for(int i = 0; i < 15; i++)
                            {
                                string[] plateauLigne = plateau[i].Split(";");
                                for(int j = 0; j < plateauLigne.Length; j++)
                                {
                                    if (Convert.ToChar(plateauLigne[j]) == '_')
                                    {
                                        Plateau.lettres[i, j] = ' ';
                                    } else
                                    {
                                        Plateau.lettres[i, j] = Convert.ToChar(plateauLigne[j]);
                                    }
                                }
                            }
                            Plateau.tableauRemplis = true;

                            if (lectureFichier.Length % 3 != 0) //Si la structure du fichier n'est pas valide
                            {
                                throw new Exception("Le fichier n'est pas valide. Impossible d'exporter la partie."); //On créé une erreur
                            } 
                            else //sinon
                            {
                                //On importe les joueurs
                                for (int i = 0; i < lectureFichier.Length / 3; i++)
                                {
                                    joueurs.Add(new Joueur(lectureFichier[i*3].Split(";")[0]));
                                    joueurs[i].Add_Score(Convert.ToInt32(lectureFichier[i*3].Split(";")[1]));
                                    foreach(string mot in lectureFichier[i * 3 + 1].Split(";")) //On sépare chaque ";" pour récupérer les infos sur le joueur
                                    {
                                        joueurs[i].add_Mot(mot);
                                    }
                                    foreach (string jeton in lectureFichier[i * 3 + 2].Split(";"))
                                    {
                                        char jetonLettre = Convert.ToChar(jeton);
                                        joueurs[i].Add_Main_Courante(Program.sac.Jetons.Find(r => r.Lettre == jetonLettre));
                                    }
                                }
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Partie importé depuis le fichier '" + valueName + "' ! Appuyez sur n'importe quelle touche pour revenir au menu principal.");
                                Console.ResetColor();
                                Console.ReadKey();
                            }
                        }
                        catch (Exception e) //S'il y a une erreur
                        {
                            Console.WriteLine("Une erreur est survenue: " + e);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Appuyez sur n'importe quelle touche pour revenir au menu principal.");
                            Console.ResetColor();
                            Console.ReadKey();
                            Program.Main();
                        }
                    }
                    break;
                case 4: //On rénitialise la partie
                    supprimerPartie();
                    break;
                case 5: //On lance la partie
                    if (joueurs.Count < 1) Program.Main(); //S'il n'y a pas de joueur, on retourne au menu principal
                    isStart = true; //On dit que la partie commence
                    for (int i = 0; i < joueurs.Count; i++)
                    {
                        while (joueurs[i].MainCourante.Count < 7)
                        {
                            joueurs[i].Add_Main_Courante(Program.sac.Retire_Jeton(Program.r)); //On ajoute 7 jetons dans la main de chaque joueurs
                        }
                    }
                    foreach (Joueur joueur in Utils.joueurs)
                    {
                        Program.ordreDeJeu.Enqueue(joueur); //On ajoute les joueurs dans la file de jeu
                    }
                    Program.Main(); //On lance la partie
                    break;
                default:
                    break;
            }
            Console.Clear();
            MenuPrincipal();

        }

        static public void supprimerPartie() //Rénitialise les différentes classes et variables utilisées pour une partie
        {
            isStart = false;
            joueurs.Clear();
            Program.sac = new Sac_Jetons();
            Console.Clear();
            Program.ordreDeJeu.Clear();
            Program.plateau = new Plateau();
            Plateau.mot = "";
            Program.Main();
        }
    }
}
