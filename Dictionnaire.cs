using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Projet_POO_Lombard
{
    public class Dictionnaire
    {
        private string langue;
        private string[][] dico;
        
        //Constructeur :

        public Dictionnaire(string langue)
        {
            this.langue = langue;
            string path = "MotsPossibles";
            if (langue == "Français") path += "FR.txt";      // Selon la langue, on change le chemin vers le dictionnaire avant de l'ouvrir
            else path +="EN.txt";
            this.dico = new string[14][];                  // Tableau de tableaux avec tous les mots pour chaque longueur
            string[] lines = File.ReadAllLines(path); // Création d'un tableau ou chaque ligne du fichier txt est un string
            int c = 0;
            for (int i = 1; i < dico.Length*2; i++)
            {
                if(i%2!=0)                           // On enlève les lignes inutiles (qui nous donnent le nombre de lettres de chaque mot)
                {
                    dico[c] = lines[i].Split(' ');              // On créée un sous-tableau avec tous les mots de même longueur
                    c++;
                }
            }
        }

        // Propriétés :

                                                                // On définit les propriétés en Get
        public string Langue
        {
            get { return this.langue;}
        }

        public string[][] Dico
        {
            get { return this.dico; }
        }

        //Méthodes :

        /// <summary>
        /// Affiche le contenu du Dictionnaire : Nombre de mots total et de chaque longueur
        /// </summary>     
        /// <returns>Chaîne de caractères contenant la description du Dictionnaire</returns>
        
        public override string ToString()
        {
            string res = "";
            if(langue=="Français")
            {
                res = "Langue du dictionnaire : " + this.langue + "\n\nNombre de mots par taille : ";
                for (int i = 0; i < dico.Length; i++)
                {
                    res += "\n" + (i + 2) + " lettres : " + dico[i].Length;         // On regarde combien de mots composent chaque sous-tableau.
                }
            }
            else
            {
                res = "Dictionary language : " + this.langue + "\n\nNumber of words per size : ";
                for (int i = 0; i < dico.Length; i++)
                {
                    res += "\n" + (i + 2) + " letters : " + dico[i].Length;         // Pareil en anglais
                }
            }
            return res;
        }

        /// <summary>
        /// Vérifie la présence ou non du mot dans le Dictionnaire
        /// </summary>
        /// <param name="mot">Mot à vérifier</param>
        /// <param name="start">Indice de départ, 0 par défaut</param>
        /// <param name="end">Indice de fin du tableau, par défaut le dernier indice du tableau</param>
        /// <returns>Un booléen qui définit la présence du mot</returns>

        public bool RechDichoRecursif(string mot,int start =0,int end =-1)    //On fait une recherche dichotomique récursive dans le sous-tableau de la longueur du mot recherché
        {
            if (end == -1) end = dico[mot.Length - 2].Length - 1;
            if (end < start) return false;
            int middle = (start + end) / 2;
            int res = String.Compare(mot, dico[mot.Length - 2][middle]);   //On compare les mots qui sont classés par ordre alphabétiques
            if (res == 0) return true;
            else if (res < 0) end = middle - 1;
            else start = middle + 1;
            return RechDichoRecursif(mot, start, end);
        }
    }
}
