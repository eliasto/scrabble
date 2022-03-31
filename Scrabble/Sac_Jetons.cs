using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    /* <summary>
     * Cette classe contient un sac de jetons contenant tous les types de jetons du jeu.
     * </summary>
     */
    public class Sac_Jetons
    {
        private List<Jeton> jetons;

        public Sac_Jetons() //Initialise le sac de jetons
        {
            this.jetons = new List<Jeton>(); //créé une liste de jetons
            try
            {
                string[] lines = File.ReadAllLines("Jetons.txt"); //Lis le fichier comprenant tous les types de jetons
                foreach (string line in lines) //pour chaque ligne
                {
                    string[] valeurs = line.Split(";"); //chaque ligne = un jeton, on supprime les ";" pour avoir des infos sur le jeton
                    Jeton jeton = new Jeton(Convert.ToChar(valeurs[0]), Convert.ToInt32(valeurs[1]), Convert.ToInt32(valeurs[2])); //on créé une nouvelle instance du jeton
                    this.jetons.Add(jeton); //ajoute ce jeton dans le sac de jeton
                }
            }
            catch (Exception f) //S'il y a une erreur
            {
                Console.WriteLine("Le fichier n'existe pas.");
                Console.WriteLine(f.Message);

            }
        }

        public List<Jeton> Jetons
        {
            get { return this.jetons; } //Affiche le sac de jetons
        }

        public Jeton Retire_Jeton(Random r) //Retire un jetons aléatoire
        {
            Jeton randomJeton = this.jetons[r.Next(0, this.Jetons.Count)]; //Prends un jetons
            while(randomJeton.NombreJ == 0) //Si le nombre de jetons de ce jeton est = 0
            {
                this.Jetons.Remove(randomJeton); //Supprime le jeton
                randomJeton = this.jetons[r.Next(0, this.Jetons.Count)]; //repioche un jeton
            }
            randomJeton.Retire_Un_Nombre(); //Supprime un nombreJ du jeton pioché
            return randomJeton;
        }

        public Jeton Info(char lettre)
        {
            return this.jetons.Find(r => r.Lettre == Char.ToUpper(lettre)); //Retourne un jeton avec comme lettre passé en paramètre
        }

        public int tailleSac() //retourne la taille du sac
        {
            int taille = 0;
            foreach(Jeton jeton in this.jetons) //ajoute chaque nombreJ à taille
            {
                taille += jeton.NombreJ;
            }
            return taille;
        }

        public override string ToString() //Retourn des infos sur le sac de jetons
        {
            string value = "";
            foreach(Jeton jeton in this.jetons)
            {
                value += "\n"+jeton.ToString(); //Affiche toutes les infos de chaque jeton
            }
            return value;
        }


    }
}
