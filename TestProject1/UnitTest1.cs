using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scrabble
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test la fonction Add_Score de la classe Joueur.cs
        /// </summary>
        [TestMethod]
        public void Add_Score()
        {
            Joueur joueur = new Joueur("Elias");
            joueur.Add_Score(8);
            Assert.AreEqual(8, joueur.Score);
        }

        /// <summary>
        /// Test la fonction RechDichoRecursif de la classe Dictionnaire.cs
        /// </summary>
        [TestMethod]
        public void RechDichoRecursif()
        {
        Dictionnaire dico = new Dictionnaire("français", "Dictionnaire.txt"); //Génère un dictionnaire de mots
        Assert.AreEqual(false, dico.RechDichoRecursif("vivelescrabble"));
        }

        /// <summary>
        /// Test la foncton TailleSac de la classe Sac_Jetons.cs
        /// </summary>
        [TestMethod]
        public void TailleSac()
        {
            Sac_Jetons sac = new Sac_Jetons(); //Génère un sac de jetons via le fichier jetons.txt
            Assert.AreEqual(102, sac.tailleSac());
        }

        /// <summary>
        /// Test la foncton calculerScore de la classe Plateau.cs
        /// </summary>
        [TestMethod]
        public void calculerScore()
        {
            Plateau plateau = new Plateau(); //Génère un plateau
            Assert.AreEqual(6, plateau.calculerScore("elias",7,7,'h'));
        }

        /// <summary>
        /// Test la foncton motUtilisable de la classe Joueur.cs
        /// </summary>
        [TestMethod]
        public void motUtilisable()
        {
            Joueur joueur = new Joueur("Elias");
            Jeton jeton = new Jeton('E', 3, 1);
            joueur.Add_Main_Courante(jeton);
            Assert.AreEqual(true, joueur.motUtilisable("E"));
            Assert.AreEqual(false, joueur.motUtilisable("ELIAS"));
        }
    }
}
