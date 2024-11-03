using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Projet_POO_Lombard
{
    public class Plateau
    {
        private char[,] mat_plateau;
        private int difficulté;
        private string[] mots_recherche;
        private Dictionnaire dico;
        private Random r = new Random();

        // Constructeur :

        public Plateau(string mode_génération, int difficulté, Dictionnaire dico)
        {
            this.dico = dico;
            if (mode_génération == "Auto")               // On créée automatiquement le plateau au niveau demandé
            {
                switch (difficulté)
                {
                    case 1:
                        {
                            this.difficulté = 1;
                            Generer1();
                            break;
                        }
                    case 2:
                        {
                            this.difficulté = 2;
                            Generer2();
                            break;
                        }
                    case 3:
                        {
                            this.difficulté = 3;
                            Generer3();                // Génération des plateaux au niveau voulu
                            break;
                        }
                    case 4:
                        {
                            this.difficulté = 4;
                            Generer4();
                            break;
                        }
                    case 5:
                        {
                            this.difficulté = 5;
                            Generer5();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Erreur lors de l'affectation du niveau");   // En cas d'erreur, on l'affiche.
                            break;
                        }
                }
            }
            else ToRead("PlateauNiveau" + difficulté + ".csv");    // On appelle la fonction de lecture de fichier afin d'initisaliser le plateau.
        }                                                        // Pour utiliser un plateau différent, le renommer PlateauNiveauX et le placer dans la solution /bin/Debug/Net6.0.


        // Propriétés :

        public char[,] Mat_Plateau
        {
            get { return this.mat_plateau; }
        }

        public int Difficulté
        {
            get { return this.difficulté; }            // On définit les propriétés en Get
        }

        public string[] Mots_Recherche
        {
            get { return this.mots_recherche; }
        }

        //Méthodes :

        /// <summary>
        /// Affiche la description du plateau
        /// </summary>     
        /// <returns>Chaine de caractères contenant la description du plateau</returns>

        public string ToString(Joueur joueur)
        {
            string res = "";
            if(dico.Langue=="Français")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Difficulté : " + this.difficulté);
                Console.ForegroundColor= ConsoleColor.White;
                Console.WriteLine("\nMots à trouver : \n");            // Affichage difficulté
                for (int i = 0; i < this.mots_recherche.Length; i++)
                {
                    if (!joueur.PasEncoreTrouve(mots_recherche[i])) Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(mots_recherche[i]);                                                         // Affichage mots à trouver
                    Console.ForegroundColor= ConsoleColor.White;
                }
                Console.WriteLine();
                Console.WriteLine("Tableau : ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Difficulty : " + this.difficulté);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nWords to find :\n");            // Affichage difficulté
                for (int i = 0; i < this.mots_recherche.Length; i++)
                {
                    if (!joueur.PasEncoreTrouve(mots_recherche[i])) Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(mots_recherche[i]);                                                         // Affichage mots à trouver
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
                Console.WriteLine("Board : ");
            }

            res += "  | ";
            for (int l = 0; l < mat_plateau.GetLength(1); l++)    // Première ligne avec les numéros de colonnes
            {
                if (l <= 8) res += l + 1 + " | ";
                else res += l + 1 + "| ";
            }
            res += "\n--";
            for (int k = 0; k < mat_plateau.GetLength(1); k++)    // Lignes de séparation
            {
                res += "+---";
            }
            res += "+\n";
            for (int i = 0; i < mat_plateau.GetLength(0); i++)
            {
                if (i <= 8) res += i + 1 + " | ";
                else res += i + 1 + "| ";
                for (int j = 0; j < mat_plateau.GetLength(1); j++)
                {
                    res += mat_plateau[i, j] + " | ";                                // On sépare les caractères par " | " pour donner un tableau plus aéré.
                }
                res += "\n--";
                for (int k = 0; k < mat_plateau.GetLength(1); k++)
                {
                    res += "+---";
                }
                res += "+\n";
            }
            return res;
        }

        /// <summary>
        /// Sauvegarde le plateau dans un fichier texte
        /// </summary>
        /// <param name="nomfile">Nom du fichier dans lequel inscrire le tableau</param>

        public void ToFile(string nomfile)
        {
            if (mat_plateau != null && mat_plateau.Length > 0 && mots_recherche != null && mots_recherche.Length > 0)
            {
                string[] lines = new string[mat_plateau.GetLength(0) + 2]; // On créée un tableau de string de la hauteur du plateau + une ligne de paramètres + une ligne des mots à trouver.
                lines[0] = difficulté + ";" + mat_plateau.GetLength(0) + ";" + mat_plateau.GetLength(1) + ";" + mots_recherche.Length;  //On ajoute à la première ligne les paramètres du tableau
                for (int i = 0; i < mots_recherche.Length; i++)
                {
                    if (i != mots_recherche.Length - 1) lines[1] += mots_recherche[i] + ";";        // On ajoute à la seconde ligne les mots à trouver
                    else lines[1] += mots_recherche[i];
                }
                for (int i = 0; i < mat_plateau.GetLength(0); i++)
                {
                    for (int z = 0; z < mat_plateau.GetLength(1); z++)
                    {                                                 // On ajoute les lignes du tableau
                        if (i != mat_plateau.GetLength(1) - 1) lines[i + 2] += mat_plateau[i, z] + ";";       
                        else lines[i + 2] += mat_plateau[i, z];
                    }
                }
                File.WriteAllLinesAsync(nomfile + ".csv", lines);      // On écrit le fichier
                Console.WriteLine("Le fichier " + nomfile + ".csv à été exporté avec succès");
            }
            else Console.WriteLine("Le tableau est erroné : impossible de l'exporter en tant que fichier");  //En cas d'erreur, on l'affiche.
        }

        /// <summary>
        /// Charge la sauvegarde d'un plateau à partir d'un fichier texte
        /// </summary>
        /// <param name="nomfile">Nom du fichier depuis lequel le plateau doit être chargé</param>

        public void ToRead(string nomfile)
        {
            if(File.Exists(nomfile))
            {
                string[] lines = File.ReadAllLines(nomfile);   // Lecture du fichier complet
                string[] settings = lines[0].Split(";");       // On isole la première ligne, il s'agit des paramètres
                this.difficulté = Convert.ToInt32(settings[0]); // On assigne la difficulté en lisant la valeur en indice 1
                this.mat_plateau = new char[Convert.ToInt32(settings[1]), Convert.ToInt32(settings[2])];   // On initialise la matrice
                this.mots_recherche = new string[Convert.ToInt32(settings[3])]; // On initialise le tableau des mots à chercher

                string[] mots = lines[1].Split(";"); // On isole la ligne 2, il s'agit de tous les mots à trouver
                for (int i = 0; i < mots.Length; i++)
                {
                    this.mots_recherche[i] = mots[i];     // On copie les mots dans notre variable d'instance
                }

                for (int i = 0; i < mat_plateau.GetLength(0); i++)  // Les lignes restantes représentent le plateau, on copie donc les valeurs dans la matrice.
                {
                    string[] mat = lines[i + 2].Split(";");
                    for (int z = 0; z < mat_plateau.GetLength(1); z++)
                    {
                        mat_plateau[i, z] = Convert.ToChar(mat[z]);
                    }
                }
            }
            else Console.WriteLine("Le fichier "+nomfile+" n'existe pas ! passage au niveau suivant");
        }

        /// <summary>
        /// Teste si un mot peut être inséré dans le plateau sans dépasser du tableau ni modifier un mot déja placé
        /// </summary>
        /// <param name="mot">Mot à vérifier</param>
        /// <param name="ligne">Nombre de ligne du plateau</param>
        /// <param name="colonne">Nombre de colonne du plateau</param>
        /// <param name="direction">Direction du mot à vérifier</param>

        public bool Test_Plateau(string mot, int ligne, int colonne, string direction)
        {
            bool res = true;
            if ( mot !=null && mot !="" && dico.RechDichoRecursif(mot) && colonne > 0 && ligne > 0 && ligne <= mat_plateau.GetLength(0) && colonne <= mat_plateau.GetLength(1))   // On vérifie que le mot appartient bien au dictionnaire choisi et si on est dans les limites de la matrice
            {
                switch (direction)        // Selon la direction du mot, on vérifie si il est bien placé sur le plateau
                {
                    case "N":
                        {
                            if (ligne - mot.Length >= 0) // On vérifie que le mot rentre dans la matrice
                            {
                                int c = 0;
                                for (int i = ligne; i > ligne - mot.Length; i--)
                                {
                                    if (mat_plateau[i - 1, colonne - 1] != ' ' && mat_plateau[i - 1, colonne - 1] != mot[c]) res = false; // On vérifie que les lettres déjà placées correspondent
                                    else c++;
                                }
                            }
                            else res = false;
                            break;
                        }
                    case "S":
                        {
                            if (ligne + mot.Length - 1 <= mat_plateau.GetLength(0)) // On vérifie si on est bien dans les limites de la matrice
                            {
                                for (int i = ligne; i < ligne + mot.Length; i++)
                                {
                                    if (mat_plateau[i - 1, colonne - 1] != ' ' && mat_plateau[i - 1, colonne - 1] != mot[i - ligne]) res = false; // On vérifie que les lettres déja placées correspondent
                                }
                            }
                            else res = false;
                            break;
                        }
                    case "E":
                        {
                            if (colonne + mot.Length - 1 <= mat_plateau.GetLength(1)) //On vérifie si on est dans les limites de la matrice
                            {
                                for (int i = colonne; i < colonne + mot.Length; i++)
                                {
                                    if (mat_plateau[ligne - 1, i - 1] != ' ' && mat_plateau[ligne - 1, i - 1] != mot[i - colonne]) res = false; //On vérifie que les lettres déja placées correspondent
                                }
                            }
                            else res = false;
                            break;
                        }
                    case "O":
                        {
                            if (colonne - mot.Length >= 0) //On vérifie que le mot rentre dans la matrice
                            {
                                int c = 0;
                                for (int i = colonne; i > colonne - mot.Length; i--)
                                {
                                    if (mat_plateau[ligne - 1, i - 1] != ' ' && mat_plateau[ligne - 1, i - 1] != mot[c]) res = false; //On vérifie que les lettres déja placées correspondent
                                    else c++;
                                }
                            }
                            else res = false;
                            break;
                        }
                    case "NE":
                        {
                            if (colonne + mot.Length - 1 <= mat_plateau.GetLength(1) && ligne - mot.Length >= 0) //On vérifie que le mot rentre dans la matrice
                            {
                                for (int i = 0; i < mot.Length; i++)
                                {
                                    if (mat_plateau[ligne - i - 1, colonne + i - 1] != ' ' && mat_plateau[ligne - i - 1, colonne + i - 1] != mot[i]) res = false; //On vérifie que les lettres déja placées correspondent
                                }
                            }
                            else res = false;
                            break;
                        }
                    case "NO":
                        {
                            if (colonne - mot.Length >= 0 && ligne - mot.Length >= 0) //On vérifie que le mot rentre dans la matrice
                            {
                                for (int i = 0; i < mot.Length; i++)
                                {
                                    if (mat_plateau[ligne - i - 1, colonne - i - 1] != ' ' && mat_plateau[ligne - i - 1, colonne - i - 1] != mot[i]) res = false; //On vérifie que les lettres déja placées correspondent
                                }
                            }
                            else res = false;
                            break;
                        }
                    case "SE":
                        {
                            if (colonne + mot.Length - 1 <= mat_plateau.GetLength(1) && ligne + mot.Length - 1 <= mat_plateau.GetLength(0)) //On vérifie que le mot rentre dans la matrice
                            {
                                for (int i = 0; i < mot.Length; i++)
                                {
                                    if (mat_plateau[ligne + i - 1, colonne + i - 1] != ' ' && mat_plateau[ligne + i - 1, colonne + i - 1] != mot[i]) res = false; //On vérifie que les lettres déja placées correspondent
                                }
                            }
                            else res = false;
                            break;
                        }
                    case "SO":
                        {
                            if (colonne - mot.Length >= 0 && ligne + mot.Length - 1 <= mat_plateau.GetLength(0)) //On vérifie que le mot rentre dans la matrice
                            {
                                for (int i = 0; i < mot.Length; i++)
                                {
                                    if (mat_plateau[ligne + i - 1, colonne - i - 1] != ' ' && mat_plateau[ligne + i - 1, colonne - i - 1] != mot[i]) res = false; //On vérifie que les lettres déja placées correspondent
                                }
                            }
                            else res = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Direction invalide."); //En cas d'erreur, on l'affiche
                            break;
                        }
                }
            }
            else res = false;
            return res;
        }


        /////////////////////////////////////////////////////////
        //Méthodes de génération des plateaux de chaque niveaux :
        /////////////////////////////////////////////////////////

        /// <summary>
        /// Remplissage des cases restantes du plateau avec des lettres aléatoires
        /// </summary>

        private void RemplirPlateau()
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                for (int i = 0; i < mat_plateau.GetLength(0); i++)
                {
                    for (int z = 0; z < mat_plateau.GetLength(1); z++)
                    {
                        int indice = r.Next(0, 26);
                        if (mat_plateau[i, z] == ' ') mat_plateau[i, z] = letters[indice];  // Permet de remplir les cases vides du plateau
                    }
                }
        }

        //Méthode pour placer le mot si son emplacement est valide :

        /// <summary>
        /// Place un mot dans le plateau si la position est valide
        /// </summary>
        /// <param name="mot">Mot à placer</param>
        /// <param name="ligne">Nombre de ligneS du plateau</param>
        /// <param name="colonne">Nombre de colonneS du plateau</param>
        /// <param name="direction">Direction du mot à placer</param>
        
        private void PlacerMot(string mot, int ligne, int colonne, string direction)
        {
            switch (direction)        // Choix de la direction du mot
            {
                case "N":
                    {
                        int c = 0;
                            for (int i = ligne; i > ligne - mot.Length; i--)
                            {
                                if (mat_plateau[i - 1, colonne - 1] == ' ') mat_plateau[i - 1, colonne - 1]=mot[c]; // On place le mot dans la bonne direction
                                c++;
                            }
                        break;
                    }
                case "S":
                    {
                            for (int i = ligne; i < ligne + mot.Length; i++)
                            {
                                if (mat_plateau[i - 1, colonne - 1] == ' ') mat_plateau[i - 1, colonne - 1]= mot[i - ligne]; 
                            }
                        break;
                    }
                case "E":
                    {
                            for (int i = colonne; i < colonne + mot.Length; i++)
                            {
                                if (mat_plateau[ligne - 1, i - 1] == ' ') mat_plateau[ligne - 1, i - 1]= mot[i - colonne]; 
                            }
                        break;
                    }
                case "O":
                    {
                            int c = 0;
                            for (int i = colonne; i > colonne - mot.Length; i--)
                            {
                                if (mat_plateau[ligne - 1, i - 1] == ' ') mat_plateau[ligne - 1, i - 1]= mot[c]; 
                                c++;
                            }
                        break;
                    }
                case "NE":
                    {
                            for (int i = 0; i < mot.Length; i++)
                            {
                                if (mat_plateau[ligne - i - 1, colonne + i - 1] == ' ') mat_plateau[ligne - i - 1, colonne + i - 1] = mot[i]; 
                            }
                        break;
                    }
                case "NO":
                    {
                            for (int i = 0; i < mot.Length; i++)
                            {
                                if (mat_plateau[ligne - i - 1, colonne - i - 1] == ' ') mat_plateau[ligne - i - 1, colonne - i - 1] = mot[i]; 
                            }
                        break;
                    }
                case "SE":
                    {
                            for (int i = 0; i < mot.Length; i++)
                            {
                                if (mat_plateau[ligne + i - 1, colonne + i - 1] == ' ') mat_plateau[ligne + i - 1, colonne + i - 1] = mot[i]; 
                            }
                        break;
                    }
                case "SO":
                    {
                            for (int i = 0; i < mot.Length; i++)
                            {
                                if (mat_plateau[ligne + i - 1, colonne - i - 1] == ' ') mat_plateau[ligne + i - 1, colonne - i - 1] = mot[i]; 
                            }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Direction invalide"); // En cas d'erreur, on l'affiche
                        break;
                    }
            }
        }

        /// <summary>
        /// Créer un plateau de difficulté 1
        /// </summary>

        private void Generer1()
        {
            this.mat_plateau = new char[7, 6];
            this.mots_recherche = new string[8];   // On crée nos éléments en fonction de la difficulté
            for(int i =0;i<mat_plateau.GetLength(0);i++)
            {
                for(int z=0;z<mat_plateau.GetLength(1);z++)
                {
                    mat_plateau[i,z] = ' ';                // On remplit la plateau de valeur vides
                }
            }
            int c = 0;
            while (c<8)   // Tant que le nombre de mots à trouver n'est pas atteint, on génère le tableau
             {
                int taille = r.Next(1,8);  // On génère un mot de longueur aléatoire (par rapport à la taille du tableau)
                int indice_mot = r.Next(0, dico.Dico[taille].Length);  // On choisit le mot aléatoire dans le tableau des mots de même longueur
                string mot = dico.Dico[taille][indice_mot]; 
                int ligne = r.Next(1, mat_plateau.GetLength(0)); //On génère une ligne aléatoirement
                int colonne = r.Next(1, mat_plateau.GetLength(1)); //On génère une colonne aléatoirement
                int indice_direction = r.Next(0,2);  //On détermine une direction aléatoire
                string direction="";
                switch (indice_direction)
                {
                    case 0:
                        {
                            direction = "E";           //Niveau 1: On positionne les mots selon seulement 2 directions
                            break;
                        }
                    case 1:
                        {
                            direction = "S";
                            break;
                        }
                }
                if (Test_Plateau(mot,ligne,colonne,direction))
                {
                    PlacerMot(mot, ligne, colonne, direction); //Si conditions remplies : on ajoute le mot au plateau et à la liste de mots à trouver
                    mots_recherche[c] = mot;
                    c++;
                }
            }
            RemplirPlateau();
        }

        /// <summary>
        /// Créer un plateau de difficulté 2
        /// </summary>

        private void Generer2()
        {
            this.mat_plateau = new char[8, 7];
            this.mots_recherche = new string[13];   //On créée nos éléments en fonction de la difficulté
            for (int i = 0; i < mat_plateau.GetLength(0); i++)
            {
                for (int z = 0; z < mat_plateau.GetLength(1); z++)
                {
                    mat_plateau[i, z] = ' ';                //On remplit le plateau de valeurs vides
                }
            }
            int c = 0;
            while (c < 13)   //Tant que le nombre de mots à trouver n'est pas atteint,on génère le tableau
            {
                int taille = r.Next(1, 13);  //On génère un mot de longueur aléatoire (par rapport à la taille du tableau)
                int indice_mot = r.Next(0, dico.Dico[taille].Length);  //On choisit le mot aléatoire dans le tableau des mots de même longueurs
                string mot = dico.Dico[taille][indice_mot];
                int ligne = r.Next(1, mat_plateau.GetLength(0)); //On génère une ligne aléatoirement
                int colonne = r.Next(1, mat_plateau.GetLength(1)); //On génère une colonne aléatoirement
                int indice_direction = r.Next(0, 4);  //On détermine une direction aléatoire
                string direction = "";
                switch (indice_direction)
                {
                    case 0:
                        {
                            direction = "E";           //Niveau 2: On positionne les mots selon 4 directions
                            break;
                        }
                    case 1:
                        {
                            direction = "S";
                            break;
                        }
                    case 2:
                        {
                            direction = "O";
                            break;
                        }
                    case 3:
                        {
                            direction = "N";
                            break;
                        }
                }
                if (Test_Plateau(mot, ligne, colonne, direction))
                {
                    PlacerMot(mot, ligne, colonne, direction); // Si conditions remplies : on ajoute le mot au plateau et à la liste de mots à trouver
                    mots_recherche[c] = mot;
                    c++;
                }
            }
            RemplirPlateau();
        }

        /// <summary>
        /// Créer un plateau de difficulté 3
        /// </summary>

        private void Generer3()
        {
            this.mat_plateau = new char[10, 9];
            this.mots_recherche = new string[18];
            for (int i = 0; i < mat_plateau.GetLength(0); i++)
            {
                for (int z = 0; z < mat_plateau.GetLength(1); z++)
                {
                    mat_plateau[i, z] = ' ';
                }
            }
            int c = 0;
            while (c < 18)   // Tant que le nombre de mots à trouver n'est pas atteint,on génère le tableau
            {
                int taille = r.Next(1, 14);  // On génère un mot de longueur aléatoire (par rapport à la taille du tableau)
                int indice_mot = r.Next(0, dico.Dico[taille].Length);  // On choisit le mot aléatoire dans le tableau des mots de même longueur
                string mot = dico.Dico[taille][indice_mot];
                int ligne = r.Next(1, mat_plateau.GetLength(0)); // On génère une ligne aléatoirement
                int colonne = r.Next(1, mat_plateau.GetLength(1)); // On génère une colonne aléatoirement
                int indice_direction = r.Next(0, 6);  // On détermine une direction aléatoire
                string direction = "";
                switch (indice_direction)
                {
                    case 0:
                        {
                            direction = "E";           //Niveau 3: On positionne les mots selon 6 directions
                            break;
                        }
                    case 1:
                        {
                            direction = "S";
                            break;
                        }
                    case 2:
                        {
                            direction = "O";
                            break;
                        }
                    case 3:
                        {
                            direction = "N";
                            break;
                        }
                    case 4:
                        {
                            direction = "NE";
                            break;
                        }
                    case 5:
                        {
                            direction = "SO";
                            break;
                        }
                }
                if (Test_Plateau(mot, ligne, colonne, direction))
                {
                    PlacerMot(mot, ligne, colonne, direction); //Si conditions remplies : on ajoute le mot au plateau et à la liste de mots à trouver
                    mots_recherche[c] = mot;
                    c++;
                }
            }
            RemplirPlateau();
        }

        /// <summary>
        /// Créer un plateau de difficulté 4
        /// </summary>

        private void Generer4()
        {
            this.mat_plateau = new char[11,11];
            this.mots_recherche = new string[23];
            for (int i = 0; i < mat_plateau.GetLength(0); i++)
            {
                for (int z = 0; z < mat_plateau.GetLength(1); z++)
                {
                    mat_plateau[i, z] = ' ';
                }
            }
            int c = 0;
            while (c < 23)   //Tant que le nombre de mots à trouver n'est pas atteint,on génère le tableau
            {
                int taille = r.Next(1, 14);  //On génére un mot de longueur aléatoire (par rapport à la taille du tableau)
                int indice_mot = r.Next(0, dico.Dico[taille].Length);  //On choisit le mot aléatoire dans le tableau des mots de même longueur
                string mot = dico.Dico[taille][indice_mot];
                int ligne = r.Next(1, mat_plateau.GetLength(0)); //On génère une ligne aléatoirement
                int colonne = r.Next(1, mat_plateau.GetLength(1)); //On génère une colonne aléatoirement
                int indice_direction = r.Next(0, 8);  //On détermine une direction aléatoire
                string direction = "";
                switch (indice_direction)
                {
                    case 0:
                        {
                            direction = "E";           //Niveau 4: On positionne les mots selon 8 directions
                            break;
                        }
                    case 1:
                        {
                            direction = "S";
                            break;
                        }
                    case 2:
                        {
                            direction = "O";
                            break;
                        }
                    case 3:
                        {
                            direction = "N";
                            break;
                        }
                    case 4:
                        {
                            direction = "NE";
                            break;
                        }
                    case 5:
                        {
                            direction = "SO";
                            break;
                        }
                    case 6:
                        {
                            direction = "NO";
                            break;
                        }
                    case 7:
                        {
                            direction = "SE";
                            break;
                        }
                }
                if (Test_Plateau(mot, ligne, colonne, direction))
                {
                    PlacerMot(mot, ligne, colonne, direction); //Si conditions remplies : on ajoute le mot au plateau et à la liste de mots à trouver
                    mots_recherche[c] = mot;
                    c++;
                }
            }
            RemplirPlateau();
        }

        /// <summary>
        /// Créer un plateau de difficulté 5
        /// </summary>

        private void Generer5()
        {
            this.mat_plateau = new char[13, 13];
            this.mots_recherche = new string[28];
            for (int i = 0; i < mat_plateau.GetLength(0); i++)
            {
                for (int z = 0; z < mat_plateau.GetLength(1); z++)
                {
                    mat_plateau[i, z] = ' ';
                }
            }
            int c = 0;
            while (c < 28)   //Tant que le nombre de mots à trouver n'est pas atteint,on génère le tableau
            {
                int taille = r.Next(1, 14);  //On génère un mot de longueur aléatoire (par rapport à la taille du tableau)
                int indice_mot = r.Next(0, dico.Dico[taille].Length);  //On choisit le mot aléatoire dans le tableau des mots de même longueur
                string mot = dico.Dico[taille][indice_mot];
                int ligne = r.Next(1, mat_plateau.GetLength(0)); //On génère une ligne aléatoirement
                int colonne = r.Next(1, mat_plateau.GetLength(1)); //On génère une colonne aléatoirement
                int indice_direction = r.Next(0, 8);  //On détermine une direction aléatoire
                string direction = "";
                switch (indice_direction)
                {
                    case 0:
                        {
                            direction = "E";           //Niveau 5: On positionne les mots selon 8 directions
                            break;                     //diagonales de bas en haut : faire SE à l'envers revient à faire NO, or étant aléatoire, similaire au niveau 4 mais avec plus de mots et sur un plus grand plateau
                        }
                    case 1:
                        {
                            direction = "S";
                            break;
                        }
                    case 2:
                        {
                            direction = "O";
                            break;
                        }
                    case 3:
                        {
                            direction = "N";
                            break;
                        }
                    case 4:
                        {
                            direction = "NE";
                            break;
                        }
                    case 5:
                        {
                            direction = "SO";
                            break;
                        }
                    case 6:
                        {
                            direction = "NO";
                            break;
                        }
                    case 7:
                        {
                            direction = "SE";
                            break;
                        }
                }
                if (Test_Plateau(mot, ligne, colonne, direction))
                {
                    PlacerMot(mot, ligne, colonne, direction); //Si conditions remplies : on ajoute le mot au plateau et à la liste de mots à trouver
                    mots_recherche[c] = mot;
                    c++;
                }
            }
            RemplirPlateau();
        }

        /// <summary>
        /// Retourne le nombre de mots à trouver selon la difficulté
        /// </summary>
 
        public int nbMots()
        {
            int res = 0;
            switch(this.difficulté)
            {
                case 1:
                    {
                        res = 8;
                        break;
                    }
                case 2:
                    {
                        res = 13;
                        break;
                    }
                case 3:
                    {
                        res = 18;
                        break;
                    }
                case 4:
                    {
                        res = 23;
                        break;
                    }
                case 5:
                    {
                        res = 28;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Error Plateau nbMots"); ;
                        break;
                    }
            }
            return res;
        }
    }
}
