using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    public class Plateau
    {
        /* <summary>
         * Classe qui affiche le plateau, calcul le score de mots, permets de poser
         * les mots, orienter les mots. Elle est la plus grosse et principale classe du programme.
         * </summary>
         * */
        public static char[,] lettres = new char[15, 15] ; //Génère un plateau
        public static bool tableauRemplis = false; //Si un premier mot a déjà été posé
        private int[,] bonusMot; //coefficient pour le bonus d'un mot
        private int[,] bonusLettre; //coefficient pour le bonus d'une lettre
        public static string mot = ""; //mot que le joueur veut posé
        public static bool sensHorizontal = true; //sens du mot

        public Plateau()
        {
            //On génère des matrices de la taille du plateau
            this.bonusMot = new int[15, 15];
            this.bonusLettre = new int[15, 15];

            if(tableauRemplis == false)
            {
                //Le plateau est vide de base
                for (int i = 0; i < lettres.GetLength(0); i++)
                {
                    for (int j = 0; j < lettres.GetLength(1); j++)
                    {
                        lettres[i, j] = ' ';
                        this.bonusMot[i, j] = 1;
                        this.bonusLettre[i, j] = 1;
                    }
                }
            }

            //On rajoute les bonus pour chaque case manuellement
            #region bonus
            this.bonusLettre[0,3] = 2;
            this.bonusLettre[0, 11] = 2;
            this.bonusLettre[1, 5] = 3;
            this.bonusLettre[1, 9] = 3;
            this.bonusLettre[2, 6] = 2;
            this.bonusLettre[2, 8] = 2;
            this.bonusLettre[3, 7] = 2;
            this.bonusLettre[3, 0] = 2;
            this.bonusLettre[3, 14] = 2;

            this.bonusLettre[5, 1] = 3;
            this.bonusLettre[5, 5] = 3; 
            this.bonusLettre[5, 9] = 3;
            this.bonusLettre[5, 13] = 3;

            this.bonusLettre[6, 2] = 2;
            this.bonusLettre[6, 6] = 2;
            this.bonusLettre[6, 8] = 2;
            this.bonusLettre[6, 12] = 2;

            this.bonusLettre[7, 3] = 2;
            this.bonusLettre[7, 11] = 2;

            this.bonusLettre[8, 2] = 2;
            this.bonusLettre[8, 6] = 2;
            this.bonusLettre[8, 8] = 2;
            this.bonusLettre[8, 12] = 2;

            this.bonusLettre[9, 1] = 3;
            this.bonusLettre[9, 5] = 3;
            this.bonusLettre[9, 9] = 3;
            this.bonusLettre[9, 13] = 3;

            this.bonusLettre[11, 0] = 2;
            this.bonusLettre[11, 7] = 2;
            this.bonusLettre[11, 14] = 2;

            this.bonusLettre[12, 6] = 2;
            this.bonusLettre[12, 8] = 2;

            this.bonusLettre[13, 5] = 3;
            this.bonusLettre[13, 9] = 3;

            this.bonusLettre[14, 3] = 2;
            this.bonusLettre[14, 11] = 2;
            //-----------BONUS MOTS-----------
            this.bonusMot[0, 0] = 3;
            this.bonusMot[0, 7] = 3;
            this.bonusMot[0, 14] = 3;

            this.bonusMot[7, 0] = 3;
            this.bonusMot[7, 14] = 3;

            this.bonusMot[14, 0] = 3;
            this.bonusMot[14, 7] = 3;
            this.bonusMot[14, 14] = 3;

            this.bonusMot[1, 1] = 2;
            this.bonusMot[1, 13] = 2;

            this.bonusMot[2, 2] = 2;
            this.bonusMot[2, 12] = 2;

            this.bonusMot[3, 3] = 2;
            this.bonusMot[3, 11] = 2;

            this.bonusMot[4, 4] = 2;
            this.bonusMot[4, 10] = 2;

            this.bonusMot[13, 1] = 2;
            this.bonusMot[13, 13] = 2;

            this.bonusMot[10, 4] = 2;
            this.bonusMot[10, 10] = 2;

            this.bonusMot[12, 2] = 2;
            this.bonusMot[12, 12] = 2;

            this.bonusMot[11, 3] = 2;
            this.bonusMot[11, 11] = 2;
            #endregion bonus
        }

        public string toString() //On affiche le plateau avec les lettres posées
        {
            string value = "";
            for (int i = 0; i < lettres.GetLength(0); i++)
            {
                value += "\n";
                for (int j = 0; j < lettres.GetLength(1); j++)
                {
                    value += "[" + lettres[i, j]+"] ";
                }
            }
            return value;
        }

        /// <summary>
        /// Fonction principale qui affiche le plateau en couleurs. C'est la plus grosse fonction qui permet d'afficher
        /// les cases en couleurs, bouger le mot, et afficher les options
        /// </summary>
        /// <param name="x">Coordonnées en x du plateau</param>
        /// <param name="y">Coordonnées en y du plateau</param>
        /// <param name="joueur">Instance du joueur qui joue</param>
        public void afficher(int x, int y, Joueur joueur)
        {
            DateTime dateDansSixMinutes = DateTime.Now.AddMinutes(Program.tempsMinuteur); //Créé un minuteur avec comme temps sélectionné en début
            char[,] choixEnCours = new char[15, 15]; //matrice comprenant le mot posé
            
            //Fonction pour remplir la matrice du choix en cours avec des 0
            void remplirChoixEnCours()
            {
                for (int i = 0; i < choixEnCours.GetLength(0); i++)
                {
                    for (int j = 0; j < choixEnCours.GetLength(1); j++)
                    {
                        choixEnCours[i, j] = '0';
                    }
                }
            }

            remplirChoixEnCours(); //On remplit la matrice choixencours de 0
            ConsoleKeyInfo key = new ConsoleKeyInfo(); //Permet de voir les touches que le joueur utilisent
            bool isDone = false; //Si le joueur a posé son mot
            while (isDone == false) //Tant que le mot n'est pas posé
            {
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(joueur.ToString());
                Console.WriteLine("Vous avez jusqu'à " + dateDansSixMinutes.Hour+":"+ ((dateDansSixMinutes.Minute<10)?"0"+ dateDansSixMinutes.Minute: dateDansSixMinutes.Minute) + " pour jouer.");
                Console.WriteLine("Pour jeter des jetons appuyez sur J.");
                Console.ResetColor();

                if (mot == "")
                {
                    demanderMot(); //On demande le mot
                }
                else
                {
                    remplirChoixEnCours();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" ");
                    Console.WriteLine("Mot sélectionné: " + mot + ". Pour annuler appuyer sur échap.");
                    Console.WriteLine("Pour changer le sens du mot appuyez sur la touche D.");
                    Console.ResetColor();
                    try //On essaye de poser dans une matrice de 15*15 le mot que le joueur a selectionné
                    {
                        if (sensHorizontal == true) //Pour le sens horizontal
                        {
                            for (int i = 0; i < mot.Length; i++)
                            {
                                choixEnCours[x, y + i] = mot[i];
                            }
                        }
                        else //Pour le sens vertical
                        {
                            for (int i = 0; i < mot.Length; i++)
                            {
                                choixEnCours[x + i, y] = mot[i];
                            }
                        }
                    }
                    catch { }
                }

                #region contour du haut du plateau
                for (int i = 0; i < this.bonusLettre.GetLength(0); i++) //Affiche les contours du haut du plateau plateau
                {
                    if (("" + this.bonusLettre[i, 0]).Length == 1)
                    {
                        Console.Write("==");
                    }
                    else
                    {
                        Console.Write("====");
                    }
                }
                #endregion

                #region couleurs des cases du plateau
                for (int i = 0; i < lettres.GetLength(0); i++)
                {
                    Console.WriteLine("");
                    for (int j = 0; j < lettres.GetLength(1); j++)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen; //Si la case n'a pas de bonus de mot ou de lettre
                        string value = "" + lettres[i, j]; //lettre sur la n-ième et j-ième case du plateau
                        if(lettres[i,j] == ' ' && choixEnCours[i,j] != '0') 
                        {
                            value = "" + choixEnCours[i, j]; //affiche la n-ième lettre du mot sélectionné par le joueur sur le plateau
                        }
                        if (this.bonusLettre[i, j] == 2) //Couleur de la case si le bonus d'une lettre est x2
                        {
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.Black;

                        }
                        else if (this.bonusLettre[i, j] == 3) //Couleur de la case si le bonus d'une lettre est x3
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }

                        else if (this.bonusMot[i, j] == 2) //Couleur de la case si le bonus d'un mot est x2
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                        }
                        else if (this.bonusMot[i, j] == 3) //Couleur de la case si le bonus d'un mot est x3
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                        }
                        
                        if (x == i && y == j) //Couleur de la case que le joueur vise/sélectionne/est-dessus en ce moment
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(value.PadRight(2)); //On rajoute un peu de padding pour que la case soit carré
                        Console.ResetColor();
                    }
                }
                #endregion

                #region contour du bas du plateau
                Console.WriteLine(" ");
                for (int i = 0; i < this.bonusLettre.GetLength(0); i++) //contour du bas du plateau
                {
                    if (("" + this.bonusLettre[i, 0]).Length == 1)
                    {
                        Console.Write("==");
                    }
                    else
                    {
                        Console.Write("====");
                    }
                }
                #endregion

                #region légende des couleurs des cases

                //légende des couleurs des cases
                Console.Write("\n\nLégende: ");

                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.Write("".PadRight(2));
                Console.ResetColor();
                Console.Write(" Lettre x2   ");

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("".PadRight(2));
                Console.ResetColor();
                Console.Write(" Lettre x3   ");

                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("".PadRight(2));
                Console.ResetColor();
                Console.Write(" Mot x2   ");

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("".PadRight(2));
                Console.ResetColor();
                Console.Write(" Mot x3   ");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                #endregion

                if (mot == "") //Si aucun mot n'a été sélectionné
                {
                    Console.Write(">> ");
                    mot = Console.ReadLine(); //On demande le mot que le joueur veut jouer
                    while(joueur.motUtilisable(mot.ToUpper()) == false) //Tant que le joueur n'a pas les lettres pour jouer le mot
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Vous n'avez pas les lettres pour ce mot. Appuyez sur une touche pour continuer.");
                        Console.ResetColor();
                        mot = Console.ReadLine(); //On lui demande de rejouer un mot
                        
                    }
                    key = new ConsoleKeyInfo();
                } else
                {
                    key = Console.ReadKey(true);
                }

                // Partie on bouge
                
                if (key.Key.ToString() == "DownArrow") //Fait baisser le mot d'une case
                {
                    x++;
                    if(x > 14)
                    {
                        x = 0;
                    }
                }
                else if (key.Key.ToString() == "UpArrow") //fait monter le mot d'une case
                {
                    x--;
                    if (x < 0)
                    {
                        x = 14;
                    }
                }
                else if (key.Key.ToString() == "RightArrow") //le mot va à droite
                {
                    y++;
                    if (y > 14)
                    {
                        y = 0;
                    }
                }
                else if (key.Key.ToString() == "LeftArrow") //le mot va à gauche
                {
                    y--;
                    if (y < 0)
                    {
                        y = 14;
                    }
                }
                else if (key.Key.ToString() == "Escape") //annule la sélection de choix du mot
                {
                    mot = "";
                    remplirChoixEnCours();
                                    }
                else if(key.Key.ToString() == "D") //change le sens du mot (horizontal ou vertical)
                {
                    sensHorizontal = !sensHorizontal;

                }
                else if (key.Key.ToString() == "M") //retourne au menu principal
                {
                    Utils.MenuPrincipal();

                }
                else if(key.Key.ToString() == "J") //permet de jeter des jetons et donc de passer son tour
                {
                    Console.WriteLine("Veuillez choisir les lettres que vous souhaitez supprimer, séparé par une virgule.");
                    Console.WriteLine("Par exemple: A,J,K");
                    string lettresAJeter = Console.ReadLine();
                    string[] lettresAJeterTab = lettresAJeter.Split(',');
                    foreach(string lettre in lettresAJeterTab)
                    {
                        try //Permet de jeter les jetons que le joueur possède. S'il marque un jeton qu'il n'a pas ça fait rien
                        {
                            joueur.Remove_Main_Courante(Program.sac.Info(Convert.ToChar(lettre.ToUpper())));

                        }catch
                        {}
                    }
                    while (joueur.MainCourante.Count < 7) //On remplit la main du joueur
                    {
                        joueur.Add_Main_Courante(Program.sac.Retire_Jeton(Program.r));
                    }
                    mot = "";
                    isDone = true; //tour terminé, il passe son tour
                    

                }
                else if (key.Key.ToString() == "Enter") //Le joueur valide le mot qu'il a sélectionné
                {
                    //Vérification characètre *
                    for(int i = 0;i<mot.Length;i++) //pour chaque joker que le joueur pose
                    {
                        if (mot[i] == '*') //On demande au joueur quelle lettre il veut utiliser pour le joker
                        {
                            Console.WriteLine("Veuillez dire quel caractère vous souhaitez utiliser pour ce mot.");
                            string character = Console.ReadLine();
                            mot = mot.Remove(i, 1).Insert(i, character.ToUpper());
                            Program.sac.Info('*').Retire_Un_Nombre();
                        }
                    }
                    char sens = sensHorizontal ? 'h' : 'v';
                    bool estPossible = Test_Plateau(mot, x, y, sens); //On test si le mot peut être posé
                    int resultMotProche = testMotProche(mot, x, y, sens);

                    if (resultMotProche != -1 || Program.dico.RechDichoRecursif(mot)) //On test si le mot appartient au dictionnaire
                    {                        
                        if (resultMotProche == -1 && estPossible == false) //Si le mot n'est pas posable
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Impossible de placer le mot. Vérifier son emplacement. Appuyez sur n'importe quelle touche...");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        else //Sinon
                        {
                            
                            DateTime date = DateTime.Now;
                            if (dateDansSixMinutes > date) { //On vérifie que le joueur n'a pas dépassé le temps imparti
                                for (int i = 0; i < mot.Length; i++)
                                {
                                    //On pose finalement les lettres du joueur sur le plateau de manière définitive
                                    if (sensHorizontal == true)
                                    {
                                        lettres[x, y + i] = mot[i]; 
                                    }
                                    else
                                    {
                                        lettres[x + i, y] = mot[i];
                                    }
                                }
                                isDone = true; //La partie est terminée
                                joueur.add_Mot(mot); //On ajoute le mot à la main du joueur

                                if(resultMotProche < 0)
                                {
                                    resultMotProche = 0;
                                }

                                joueur.Add_Score(calculerScoreMot(mot, x, y, sens)+resultMotProche); //On calcul le score du joueur

                                foreach (char lettre in mot)
                                {
                                    //On supprime les lettres que le joueur avait dans sa main
                                    joueur.Remove_Main_Courante(joueur.MainCourante.Find(r => r.Lettre == Char.ToUpper(lettre)));
                                }
                                for (int j = joueur.MainCourante.Count; j < 7; j++)
                                {
                                    //On remplit la main du joueur
                                    joueur.Add_Main_Courante(Program.sac.Retire_Jeton(Program.r));
                                }
                                mot = "";
                            } else //Si le minuteur est dépassé
                            {
                                isDone = true; //Le joueur passe son tour
                                mot = "";
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Temps écoulé. Appuyez sur n'importe quelle touche pour continuer.");
                                Console.ResetColor();
                                Console.ReadKey();

                            }

                        }
                    } else //Si le mot n'appartient pas au dictionnaire, on ré-exécute le début de sélection du mot
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ce mot n'appartient pas au dictionnaire. Appuyez sur n'importe quelle touche...");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                }
            }
        }

        //Demande le mot que le joueur veut poser
        public void demanderMot()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nEntrer le mot que vous souhaitez jouer, puis positionnez-le sur le plateau:");
            Console.ResetColor();
            
        }

        //Calculer le score du mot posé
        public int calculerScore(string mot, int ligne, int colonne, char direction)
        {
            int score = 0;
            for(int i = 0; i < mot.Length; i++) //Selectionne chaque lettre du mot
            {
                int tempScore = Program.sac.Info(mot[i]).Score; //Score de la lettre
                if(direction == 'h')
                {
                    tempScore = this.bonusLettre[ligne, colonne + i] * tempScore; //Multiplie au bonus de la case lettre
                    this.bonusLettre[ligne, colonne + i] = 1; //Rénitialise la case bonus
                } else
                {
                    tempScore = this.bonusLettre[ligne + i, colonne] * tempScore; //Multiplie au bonus de la case lettre
                    this.bonusLettre[ligne+i, colonne] = 1; //Rénitialise la case bonus
                }
                score += tempScore;
            }
            return score;
        }

        //Test si le mot peut être posé
        public bool Test_Plateau(string mot, int ligne, int colonne, char direction)
        {
            bool etat = false;
            int nombreDeMotsPoses = 0;
            foreach(Joueur joueur in Utils.joueurs)
            {
                nombreDeMotsPoses += joueur.MotsTrouves.Count; //Permet de vérifier si un mot a déjà été posé sur la case centrale
            }
            
            //Si le mot a déjà été posée sur la case centre
            if(nombreDeMotsPoses > 0)
            {
                for (int i = 0; i < mot.Length; i++)
                {
                    //On regarde que les cases sont vides OU que ce sont les mêmes lettres qui se traversent
                    if (direction == 'h')
                    {
                        if (lettres[ligne, colonne + i] != ' ' && mot[i] == lettres[ligne, colonne + i]) 
                        {
                            etat = true;
                        }
                        
                    }
                    else if (direction == 'v')
                    {
                        if (lettres[ligne + i, colonne] != ' ' && mot[i] == lettres[ligne + i, colonne])
                        {
                            etat = true;
                        }
                        
                    }
                }

            }
            else //Si aucun mot ne se trouve sur la case centrale (donc début de la partie)
            {
                if (direction == 'h')
                {
                    if (ligne == 7 && colonne + mot.Length >= 8) //Si le mot passe par la case centrale
                    {
                        etat = true; //Alors mot posable
                    }
                }
                else
                {
                    if (colonne == 7 && ligne + mot.Length >= 8) //Si le mot passe par la case centrale
                    {
                        etat = true; //Alors mot posable
                    }
                }
            }


            return etat;
        }

        //Calcul du score du mot avec le bonus de multiplicateur du mot
        public int calculerScoreMot(string mot, int ligne, int colonne, char direction)
        {
            int score = calculerScore(mot,ligne,colonne,direction); //On calcule d'abord le score avec le bonus de score
            for (int i = 0; i < mot.Length; i++) //Pour chaque lettre du mot
            {
                if (direction == 'h')
                {
                    score = this.bonusMot[ligne, colonne + i] * score; //On multiplie le score global par le score multiplicateur du mot
                    this.bonusMot[ligne, colonne + i] = 1; //On rénitialise la case
                }
                else
                {
                    score = this.bonusLettre[ligne + i, colonne] * score; //On multiplie le score global par le score multiplicateur du mot
                    this.bonusMot[ligne + i, colonne] = 1; //On rénitialise la case
                }
            }
            return score;

        }

        /// <summary>
        /// Cette fonction permet de tester si on peut poser un mot à côté d'un autre mot,
        /// et de calculer le score résultant de ce mot.
        /// </summary>
        /// <param name="mot">mot sélectionné</param>
        /// <param name="ligne">coordonnée x</param>
        /// <param name="colonne">coordonnée y</param>
        /// <param name="direction">direction du mot</param>
        /// <returns>
        /// -1: si le mot ne peut pas être placé ici
        /// [0;+infini[: score grâce aux mots adjacents (il suffit ensuite d'ajouter le score du mot de base)
        /// </returns>
        public int testMotProche(string mot, int ligne, int colonne, char direction)
        {
            int score = -1;
            if(direction == 'h') //Pour les mots horizontaux
            {
                for(int i = 0; i < mot.Length; i++)
                {
                    if(lettres[ligne+i,colonne] != ' ' || lettres[ligne-i,colonne] == ' ') //S'il y a une contuinité d'un mot au dessus ou en dessous
                    {
                        int compteur = 1;
                        List<char> nouveauMot = new List<char>(); //On créé une liste du grand mot
                        nouveauMot.Add(mot[i]);

                        while(lettres[ligne + i+compteur, colonne] != ' ') //On ajoute les lettres jusqu'à qu'il y ait du vide
                        {
                            nouveauMot.Insert(0, lettres[ligne + i + compteur, colonne]); //On ajoute les lettres avant la première lettre
                            compteur++;
                        }
                        compteur = 1;
                        while (lettres[ligne + i - compteur, colonne] != ' ') //On ajoute les lettres après la première lettre
                        {
                            nouveauMot.Add(lettres[ligne + i - compteur, colonne]);
                            compteur++;
                        }
                        nouveauMot.Reverse(); //On inverse le mot car le mot est inversé de base
                        string nouveauMotForme = "";
                        foreach(char lettre in nouveauMot)
                        {
                            nouveauMotForme += lettre; //On convert la liste en string
                        }
                        nouveauMotForme = nouveauMotForme.ToUpper(); //On met en majuscule

                        int compteurLigne = 0;
                        compteur = 1;
                        while (lettres[ligne + i -compteur, colonne] != ' ') //On cherche la position de la première lettre
                        {
                            compteurLigne = ligne - compteur;
                            compteur++;
                        }
                        if (Program.dico.RechDichoRecursif(nouveauMotForme)) //Si le mot appartient au dictionnaire
                        {
                            score = score+ calculerScoreMot(nouveauMotForme, compteurLigne, colonne, 'h'); //On ajoute le score du mot
                        } else
                        {
                            return -1; //On dit que le mot n'est pas posable
                        }                       
                    }
                }
            } else //Pour les mots verticaux
            //Le code ci-dessous est le même que celui d'au-dessus: pour le comprendre se référer aux commentaires d'au-dessus.
            {
                for (int i = 0; i < mot.Length; i++)
                {
                    if (lettres[ligne, colonne+i] != ' ' || lettres[ligne , colonne-i] == ' ')
                    {
                        int compteur = 1;
                        List<char> nouveauMot = new List<char>();
                        nouveauMot.Add(mot[i]);

                        while (lettres[ligne , colonne + i + compteur] != ' ')
                        {
                            nouveauMot.Insert(0, lettres[ligne , colonne + i + compteur]);
                            compteur++;
                        }
                        compteur = 1;
                        while (lettres[ligne , colonne + i - compteur] != ' ')
                        {
                            nouveauMot.Add(lettres[ligne , colonne + i - compteur]);
                            compteur++;
                        }
                        nouveauMot.Reverse();
                        string nouveauMotForme = "";
                        foreach (char lettre in nouveauMot)
                        {
                            nouveauMotForme += lettre;
                        }
                        nouveauMotForme = nouveauMotForme.ToUpper();

                        int compteurLigne = 0;
                        compteur = 1;
                        while (lettres[ligne, colonne + i - compteur] != ' ')
                        {
                            compteurLigne = colonne - compteur;
                            compteur++;
                        }
                        if (Program.dico.RechDichoRecursif(nouveauMotForme))
                        {
                            score = score + calculerScoreMot(nouveauMotForme, ligne, compteurLigne, 'v');
                        }
                        else
                        {
                            return -1; //On retourne faux
                        }
                    }
                }
            }
            Console.ReadKey();
            return score;
        }


        /// <summary>
        /// Permet de générer toutes les combinaisons possibles de mots.
        /// /!\ ATTENTION: L'IA ne fonctionne pas, cette fonction arrive juste à générer toutes
        /// les combinaisons de mots possibles, mais bloque sur un stack overflow pour vérifier si les mots
        /// sont dans le dictionnaire (plusisuers centaines de milliers de mots doivent être vérifiés).
        /// </summary>
        /// <param name="joueur">Joueur considéré comme l'IA</param>
        public void IA(Joueur joueur)
        {
            List<string> tousLesMots = new List<string>();
            List<char> lettres = new List<char>();

            foreach(Jeton j in joueur.MainCourante)
            {
                lettres.Add(j.Lettre);
            }

            #region longueur 7
            for (int i = 0; i < lettres.Count; i++)
            {
                for(int j = 0; j < lettres.Count; j++)
                {
                    for (int k = 0; k < lettres.Count; k++)
                    {
                        for (int l = 0; l < lettres.Count; l++)
                        {
                            for (int m = 0; m < lettres.Count; m++)
                            {
                                for (int n = 0; n < lettres.Count; n++)
                                {
                                    for (int o = 0; o < lettres.Count; o++)
                                    {
                                        string newMot = lettres[i] + "" + lettres[j] + "" + lettres[k] + "" + lettres[l] + "" + lettres[m] + "" + lettres[n] + "" + lettres[o];
                                        if (Program.dico.Mots[5].Contains(newMot))
                                        {
                                            tousLesMots.Add(newMot);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion //Ajoute les combinaisons de mots de longueurs 7

            #region longueur 6
            for (int i = 0; i < lettres.Count; i++)
            {
                for (int j = 0; j < lettres.Count; j++)
                {
                    for (int k = 0; k < lettres.Count; k++)
                    {
                        for (int l = 0; l < lettres.Count; l++)
                        {
                            for (int m = 0; m < lettres.Count; m++)
                            {
                                for (int n = 0; n < lettres.Count; n++)
                                {
                                    string newMot = lettres[i] + "" + lettres[j] + "" + lettres[k] + "" + lettres[l] + "" + lettres[m] + "" + lettres[n];
                                    if (Program.dico.Mots[4].Contains(newMot))
                                    {
                                        tousLesMots.Add(newMot);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion //etc...

            #region longueur 5
            for (int i = 0; i < lettres.Count; i++)
            {
                for (int j = 0; j < lettres.Count; j++)
                {
                    for (int k = 0; k < lettres.Count; k++)
                    {
                        for (int l = 0; l < lettres.Count; l++)
                        {
                            for (int m = 0; m < lettres.Count; m++)
                            {
                                string newMot = lettres[i] + "" + lettres[j] + "" + lettres[k] + "" + lettres[l] + "" + lettres[m];
                                if (Program.dico.Mots[3].Contains(newMot))
                                {
                                    tousLesMots.Add(newMot);
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region longueur 4
            for (int i = 0; i < lettres.Count; i++)
            {
                for (int j = 0; j < lettres.Count; j++)
                {
                    for (int k = 0; k < lettres.Count; k++)
                    {
                        for (int l = 0; l < lettres.Count; l++)
                        {
                            string newMot = lettres[i] + "" + lettres[j] + "" + lettres[k] + "" + lettres[l];
                            if (Program.dico.Mots[2].Contains(newMot))
                            {
                                tousLesMots.Add(newMot);
                            }
                        }
                    }
                }
            }
            #endregion

            #region longueur 3
            for (int i = 0; i < lettres.Count; i++)
            {
                for (int j = 0; j < lettres.Count; j++)
                {
                    for (int k = 0; k < lettres.Count; k++)
                    {
                        string newMot = lettres[i] + "" + lettres[j] + "" + lettres[k];
                        if (Program.dico.Mots[1].Contains(newMot))
                        {
                            tousLesMots.Add(newMot);
                        }
                    }
                }
            }
            #endregion

            #region longueur 2
            for (int i = 0; i < lettres.Count; i++)
            {
                for (int j = 0; j < lettres.Count; j++)
                {
                    string newMot = lettres[i] + "" + lettres[j];
                    if (Program.dico.Mots[0].Contains(newMot))
                    {
                        tousLesMots.Add(newMot);
                    }
                }
            }
            #endregion

            
            foreach(string mot in tousLesMots)
            {
                Console.Write(mot + " "); //Affiche tous les mots
            }
            Console.ReadKey();
        }
    }
}
