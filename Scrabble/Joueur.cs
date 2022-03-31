using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    /* <summary>
     * Classe comportant les joueurs du jeu et les informations rattachées à eux.
     * </summary>
     */
    public class Joueur
    {
        private string nom;
        private int score;
        private List<string> motsTrouves; //mots trouvés
        private List<Jeton> mainCourante = new List<Jeton>(); //jetons dans sa main

        public int Score
        {
            get { return this.score; } //renvoie son score
        }
        public Joueur(string nom) //Initialise un nouveau joueur
        {
            if (nom != null && nom.Length > 0)
            {
                this.nom = nom;
                this.score = 0;
                this.motsTrouves = new List<string>();
            }
        }

        public string Nom
        {
            get { return this.nom; }
        } //renvoie son nom

        public List<Jeton> MainCourante
        {
            get { return this.mainCourante; } //renvoie les jetons dans sa main
        }

        public List<string> MotsTrouves
        {
            get { return this.motsTrouves; } //renvoie les mots trouvés
        }
        public void add_Mot(string mot) //Ajoute un nouveau mot dans "mots trouvés"
        {
            this.motsTrouves.Add(mot);
        }

        public bool motUtilisable(string mot) //Vérifie si le joueur possède les lettres pour poser ce mot
        {
            int compteur = 0;
            List<Jeton> mainCouranteTemp = this.mainCourante; //copie la liste de jetons
            for (int i = 0; i < mot.Length; i++) //fais une boucle qui passe n fois pour chaque jetons (où n est la longueur du mot)
            {
                for(int j = 0; j < mainCouranteTemp.Count; j++)
                {
                    if(mot[i] == mainCouranteTemp[j].Lettre) //la lettre du mot est dans la main courante 
                    {
                        j = mainCouranteTemp.Count + 1; //on quitte la boucle
                        compteur++; //on augmente un compteur pour voir si on a bien trouvés toutes les lettres
                    }
                }
            }
            bool etat = (compteur == mot.Length) ? true : false; //si le compteur vaut la longueur du mot, cela signifie qu'on a bien trouvés tous les jetons
            return etat;
        }
        public override string ToString() //Retourne des infos sur le joueur
        {
            string value = "Information sur le joueur (" + this.nom + "). Score: "+this.score+"." + " Mots trouvés: ";
            if(this.motsTrouves.Count != 0) //liste des mots qu'il a trouvé
            {
                for (int i = 0; i < this.motsTrouves.Count; i++)
                {
                    value = value + "\n   - " + this.motsTrouves[i];
                }
            } else
            {
                value += "aucun.";
            }
            value += "\nMain courante: ";
            foreach(Jeton jetons in this.mainCourante) //liste de ses jetons
            {
                value += jetons.Lettre + "["+jetons.Score+"] ";
            }
            return value;
        }

        public void Add_Score(int val) //Ajoute un score à son score
        {
            this.score = this.score + val;
        }

        public void Add_Main_Courante(Jeton monjeton) //Ajoute un jeton à sa main courante
        {
            this.mainCourante.Add(monjeton);
        }

        public void Remove_Main_Courante(Jeton monjeton) //Retire un jeton de sa main
        {
            if(monjeton != null)
            {
                this.mainCourante.Remove(monjeton);
                monjeton.Retire_Un_Nombre();
            }
        }


    }
}
