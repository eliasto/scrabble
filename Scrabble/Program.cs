using System;
using System.Collections.Generic;

namespace Scrabble
{
    public class Program
    {
        /* <summary>
         * Classe principale: le but est de simuler une partie de scrabble.
         * Cette classe gère l'ordre des joueurs, l'initialisation des différentes classes (plateau, sac de jetons, jetons, joueurs,
         * dictionnaire, miuteur.
         * La classe affiche aussi le menu et permet de sélectionner les différentes options de jeu.
         * </summary>
         * */

        public static Random r = new Random(); //Génère un nombre d'aléatoire
        public static Sac_Jetons sac = new Sac_Jetons(); //Génère un sac de jetons via le fichier jetons.txt
        public static Plateau plateau = new Plateau(); //Génère un plateau
        public static Queue<Joueur> ordreDeJeu = new Queue<Joueur>(); //Ajoute les joueurs dans une queue où chaque joueur est rajouté au début dès qu'il quitte la queue
        public static Dictionnaire dico = new Dictionnaire("français", "Dictionnaire.txt"); //Génère un dictionnaire de mots
        public static int tempsMinuteur = 0; //Initialisation de la variable du minuteur

        static public void Main() 
        {
            if (Utils.isStart == false) //Si la partie n'a pas commencé
               {
                   Utils.MenuPrincipal(); //Afficher le menu principal
               }

               while (Utils.isStart) //Sinon
               {
                   while(tempsMinuteur < 1) //Si le minuteur n'est pas mis
                {
                    Console.WriteLine("Veuillez entrer le temps du minuteur entre chaque tour: ");
                    tempsMinuteur = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                }
                   if(sac.tailleSac() > 0) //Si la taille du sac n'est pas vide
                {
                    Joueur joueur = ordreDeJeu.Dequeue(); //On prend le premier joueur de la file
                    ordreDeJeu.Enqueue(joueur); //On le réajoute
                    plateau.afficher(7, 7, joueur); //Le joueur joue
                } else //Si la partie est terminé, on affiche les scores
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Partie terminé !");
                    Console.ResetColor();
                    Console.WriteLine(" ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Etat actuel de la partie: "); //Affiche l'état actuel de la partie
                    string joueurTexte = Utils.joueurs.Count == 0 ? "joueur" : "joueurs";
                    Console.WriteLine("Il y a " + Utils.joueurs.Count + " " + joueurTexte + ".");
                    if (Utils.joueurs.Count != 0)
                    {
                        foreach (Joueur joueur in Utils.joueurs)
                        {
                            Console.WriteLine("  - " + joueur.Nom + ", score: " + joueur.Score); //Montre le score de chaque joueurs
                        }
                    }
                    Console.ResetColor();
                }
               }
    }
}
}
