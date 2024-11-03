using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_POO_Lombard
{
    public class Joueur
    {
        private string nom;
        private string[] mot_trouve;
        private int[] score_plateau;

        // Constructeur :

        public Joueur(string nom)
        {
            this.nom = nom;   // Nom du joueur
            this.mot_trouve = new string[90]; // Valeur maximale du nombre de mots à trouver
            this.score_plateau = new int[5]; // Valeur maximale du nombre de plateaux
        }

        // Propriétés :

        public string Nom
        {
            get { return this.nom; }
        }

        public string[] Mot_trouve
        {
            get { return this.mot_trouve; }  // On utilise seulement des get, des méthodes seront utilisées pour ajouter au score et les mots trouvés.
        }

        public int[] Score_plateau
        {
            get { return this.score_plateau; }
        }

        // Méthodes :

        /// <summary>
        /// Ajoute un mot à la liste des mots trouvés
        /// </summary>
        /// <param name="mot">Mot à ajouter</param>

        public void Add_Mot(string mot)
        {
            int c = 0;                  // La taille du tableau est le nombre de mots maximum à trouver (90), donc le tableau ne peut pas "déborder".
            while(mot_trouve[c] != null && mot_trouve[c] != "")
            {
                c++;
            }
            mot_trouve[c] = mot;  // On ajoute le mot à la suite dans le tableau
        }

        /// <summary>
        /// Vérifie si un mot a déjà été trouvé par le joueur
        /// </summary>
        /// <param name="mot">Mot à vérifier</param>

        public bool PasEncoreTrouve(string mot)
        {
            bool res = true;
            for(int i =0; i < mot_trouve.Length; i++)
            {
                if (mot_trouve[i] == mot)
                {
                    res = false;
                }
            }
            return res;
        }

        /// <summary>
        /// Affiche la description du joueur
        /// </summary>     
        /// <returns>Chaine de caractères contenant la description du Joueur</returns>

        public override string ToString()
        {
            int total = 0;
            string res = "Joueur : " + this.nom + "\nListe des mots trouvés : \n";  // Affichage nom joueur
            for (int i = 0; i < this.mot_trouve.Length; i++)
            {
                if (mot_trouve[i] != null) res += mot_trouve[i] + "\n";          // Affichage liste des mots trouvés par le joueur
            }
            res += "\nScore : ";
            for (int i=0; i < this.score_plateau.Length; i++)
            {
                res += "\nNiveau " + (i + 1) + " : " + Score_plateau[i];           // Affichage du score du joueur à chaque plateau
                total += score_plateau[i];
            }
            res += "\nTotal : " + total;                      // Affichage du score total
            return res; 
        }

        /// <summary>
        /// Ajoute une valeur au score du joueur
        /// </summary>
        /// <param name="val">Montant du score à ajouter</param>
        /// <param name="level">Niveau de difficulté pour lequel les points doivent être ajoutés</param>

        public void Add_Score(int val,int level)
        {
            score_plateau[level] += val;  // On ajoute le score du joueur au plateau correspondant
        }

        /// <summary>
        /// Retourne le score total d'un joueur, tout niveaux confondus
        /// </summary>
        
        public int ScoreTotal()
        {
            int res = 0;
            for (int i = 0; i < this.score_plateau.Length; i++)
            {
                res += score_plateau[i];
            }
            return res;
        }
    }
}
