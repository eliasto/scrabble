using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    /* <summary>
     * Classe jeton qui contient un jeton.
     * </summary>
     */
    public class Jeton
    {
        private char lettre; //lettre du jeton
        private int score; //score du jeton
        private int nombreJ; //nombre de jetons identique

        public Jeton(char lettre, int score, int nombreJ)
        {
            this.lettre = lettre;
            this.score = score;
            this.nombreJ = nombreJ;
        }

        public char Lettre
        {
            get { return this.lettre; } //renvoie la lettre du jeton
        }

        public int NombreJ
        {
            get { return this.nombreJ; } //renvoie le nombre de jetons de ce type
        }

        public int Score
        {
            get { return this.score; } //renvoie le score du jeton
        }
        public void Retire_Un_Nombre() //retire un jeton de ce type dans le sac de jeton
        {
            if (this.nombreJ > 0)
            {
                this.nombreJ--; 
            }
            else
            {

            }
        }

        public override string ToString() //Affiche les infos sur un jeton
        {
            return "Jeton [" + this.lettre + "]: Score " + this.score + ". Il reste " + this.nombreJ + " jetons."; 
        }
    }
}
