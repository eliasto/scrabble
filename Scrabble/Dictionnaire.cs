using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrabble
{
    /* <summary>
     * Classe dictionnaire: comporte une liste de liste: chaque liste comporte un nombre de mots de même longueurs.
     * Cela permet de vérifier si un mot appartient bien au dictionnaire. Par exemple le mot ELIAS est de longueur 5.
     * On cherche donc dans chaque liste le premier mot de longueur 5 puis on parcourt la liste pour trouver ce mot.
     * 
     * Le dictionnaire est créé via un fichier dictionnaire.txt comprenant une liste de mot.
     * </summary>
     */
    public class Dictionnaire
    {
        private List<List<string>> mots; //dictionnaire
        private string langue; //langue

        public List<List<string>> Mots
        {
            get { return this.mots; } //renvoie le dictionnaire
        }
        public Dictionnaire(string langue, string nomDuFichier)
        {
            this.mots = new List<List<string>>(); //créé une nouvelle liste
            this.langue = langue;
            List<string> tempMots = new List<string>(); //liste de longueurs définies temporaires

            try
            {
                string[] lines = File.ReadAllLines(nomDuFichier); //on lit le fichier
                foreach (string line in lines)
                {
                    if (line.Length >= 3) //si la ligne n'est pas un nombre
                    {
                        tempMots = new List<string>();
                        foreach (string mot in line.Split(" ")) //on supprime les espaces
                        {
                            tempMots.Add(mot); //on lit chaque mot et on l'ajoute à une liste temporaire
                        }
                        this.mots.Add(tempMots); //on ajoute cette liste dans la grande liste temporaire
                    }

                }
                this.mots.Add(tempMots); //on ajoute la liste temporaire dans la liste finale
            }
            catch (Exception f) //Si une erreur est rencontrée
            {
                Console.WriteLine("Erreur sur la lecture du fichier:");
                Console.WriteLine(f.Message);
                Console.ReadKey();

            }
        }

        public override string ToString() //Renvoie des infos sur le dictionnaire
        {
            string value = "Dictionnaire: "+this.langue;
            foreach(List<string> mot in this.mots)
            {
                value += "\nLongueur " + mot[0].Length + " lettres: Il y a "+mot.Count+" mots."; //Renvoie des infos sur chaque List de la grande Llist
            }
            
            return value;
        }

        public bool RechDichoRecursif(string mot, int n = 0, int i =0) //Recherche si un mot existe
        {
            if(mot == null || mot.Length < 2)
            {
                return false;
            }
            if(this.mots[i][0].Length == mot.Length) //Si la longueur du mot existe dans une liste
            {
                if(n> this.mots[i].Count-1) //Si l'index de recherche est supérieur à la longueur de la liste
                {
                    return false; //le mot n'est pas dans la liste: retourne faux
                }
                if(this.mots[i][n].ToUpper() == mot.ToUpper()) //Sinon on met les deux mots qu'on vérifie en majuscule
                {
                    return true; //s'ils sont égales
                } else
                {
                    return this.RechDichoRecursif(mot, n+1, i); //sinon on augmente l'index de recherche
                }
            }
            else
            {
                return this.RechDichoRecursif(mot, n, i+1); //on augmente l'index de la liste où on effectue la recherche
            }
        }
    }
}
