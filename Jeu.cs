using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projet_POO_Lombard
{
    public class Jeu
    {
        private Dictionnaire dico;
        private Plateau[] liste_plateaux;
        private Joueur j1;
        private Joueur j2;

        // Constructeur :

        public Jeu(Dictionnaire dico,Joueur j1, Joueur j2) // On a seulement besoin du dico et des joueurs
        {
            this.dico = dico;
            liste_plateaux = new Plateau[5]; // Car 5 parties, et l'on ajoutera les plateaux au fur et à mesure
            this.j1 = j1;
            this.j2 = j2;
        }

        // Propriétés :

        public Dictionnaire Dico
        {
            get { return dico; }
        }
        public Plateau[] Liste_Plateaux
        {
            get { return liste_plateaux; }
        }

        public Joueur J1
        {
            get { return j1; }
        }

        public Joueur J2
        {
            get { return j2;}
        }

        // Méthodes :

        /// <summary>
        /// Lance une partie de diffculté donnée entre deux joueurs
        /// </summary>
        /// <param name="temps">Temps accordé à chaque joueur pour un plateau</param>
        /// <param name="stage">Difficulté de la partie</param>
        /// <param name="gamemode">Mode de jeu (Plateau généré aléatoirement ou bien a partir d'un ficher/sauvegarde)</param>

        public void Partie(int temps,int stage,string gamemode)
        {
            Plateau plateau = new Plateau(gamemode,stage,dico);
            liste_plateaux[stage - 1] = plateau;
            Regex regex = new Regex("[a-zA-Z]+(\\s(\\d\\s)+)[a-zA-Z]+"); //Regex pour vérifier l'entrée utilisateur
            DateTime start = DateTime.Now;
            start = start.AddMinutes(temps/60);
            DateTime test = DateTime.Now;
            TimeSpan intervalle = start-test;  // Différence de temps entre les deux dates;
            int nbmotstrouves = 0;
            while (intervalle.Minutes>=0 && intervalle.Seconds>=0 && nbmotstrouves< plateau.nbMots())  //Tant qu'il reste du temps et que tout les mots n'ont pas été trouvés
            {
                Console.Clear();
                Console.WriteLine(plateau.ToString(j1));  // Affichage du plateau de jeu
                if (dico.Langue == "Français") Console.WriteLine("Il reste " + intervalle.Minutes + " min et " + intervalle.Seconds + " s."); // Affichage du temps restant
                else Console.WriteLine(intervalle.Minutes + " min and " + intervalle.Seconds + " s remaining.");
                if (dico.Langue == "Français") Console.WriteLine("\nAu tour de "+j1.Nom +" : \nExemple de saisie : ETOILE 1 4 S (mot,ligne,colonne,direction)");
                else Console.WriteLine("\n"+j1.Nom +"'s turn : \nInput example: ETOILE 1 4 S (word,row,column,direction)");
                if (dico.Langue == "Français") Console.WriteLine("\nRéponse : ");
                else Console.WriteLine("\nAnswer : ");
                string? rep = Console.ReadLine();
                if (regex.IsMatch(rep))
                {
                    string[] res = rep.Split(" ");
                    if(plateau.Test_Plateau(res[0], Convert.ToInt32(res[1]), Convert.ToInt32(res[2]), res[3]))
                    {
                        if (j1.PasEncoreTrouve(res[0]))
                        {
                            nbmotstrouves++;
                            j1.Add_Mot(res[0]);
                            j1.Add_Score(res[0].Length, stage - 1);
                            if (dico.Langue == "Français") Console.WriteLine("Le mot " + res[0] + " a été trouvé ! +" + res[0].Length + " points pour " + j1.Nom + " !");
                            else Console.WriteLine("The word " + res[0] + " has been found ! +" + res[0].Length + " points for " + j1.Nom + " !");
                        }
                        else
                        {
                            if (dico.Langue == "Français") Console.WriteLine("Le mot " + res[0] + " a déjà été trouvé !");
                            else Console.WriteLine("The word " + res[0] + " has already been found !");
                        }
                    }
                }
                else
                {
                    if (dico.Langue == "Français") Console.WriteLine("Entrée incorrecte.");
                    else Console.WriteLine("Input error.");
                }
                Thread.Sleep(1500);
                test = DateTime.Now;
                intervalle = start - test;
            }
            if (dico.Langue == "Français") Console.WriteLine("\nFin du temps ! Au tour de " + j2.Nom + " !");
            else Console.WriteLine("\nTime's up ! "+j2.Nom + "'s turn !");
            Thread.Sleep(1500);
            plateau = new Plateau(gamemode, stage, dico);
            start = DateTime.Now;
            start = start.AddMinutes(temps / 60);
            test = DateTime.Now;
            intervalle = start - test;  // Différence de temps entre les deux dates;
            nbmotstrouves = 0;
            while (intervalle.Minutes >= 0 && intervalle.Seconds >= 0 && nbmotstrouves < plateau.nbMots())  // Tant qu'il reste du temps et que tout les mots n'ont pas été trouvés
            {
                Console.Clear();
                Console.WriteLine(plateau.ToString(j2));  // Affichage du plateau de jeu
                if (dico.Langue == "Français") Console.WriteLine("Il reste " + intervalle.Minutes + " min et " + intervalle.Seconds + " s."); // Affichage du temps restant
                else Console.WriteLine(intervalle.Minutes + " min and " + intervalle.Seconds + " s remaining.");
                if (dico.Langue == "Français") Console.WriteLine("\nAu tour de " + j2.Nom + " : \nExemple de saisie : ETOILE 1 4 S (mot,ligne,colonne,direction)");
                else Console.WriteLine("\n" + j2.Nom + " turn : \nInput example: ETOILE 1 4 S (word,row,column,direction)");
                if (dico.Langue == "Français") Console.WriteLine("\nRéponse :");
                else Console.WriteLine("\nAnswer: ");
                string? rep = Console.ReadLine();
                if (regex.IsMatch(rep))
                {
                    string[] res = rep.Split(" ");
                    if (plateau.Test_Plateau(res[0], Convert.ToInt32(res[1]), Convert.ToInt32(res[2]), res[3]))
                    {
                        if (j2.PasEncoreTrouve(res[0]))
                        {
                            nbmotstrouves++;
                            j2.Add_Mot(res[0]);
                            j2.Add_Score(res[0].Length, stage - 1);
                            if (dico.Langue == "Français") Console.WriteLine("Le mot " + res[0] + " a été trouvé ! +" + res[0].Length + " points pour " + j2.Nom + " !");
                            else Console.WriteLine("The word " + res[0] + " has been found ! +" + res[0].Length + " points for " + j2.Nom + " !");
                        }
                        else
                        {
                            if (dico.Langue == "Français") Console.WriteLine("Le mot " + res[0] + " a déjà été trouvé !");
                            else Console.WriteLine("The word " + res[0] + " has already been found !");
                        }
                    }
                }
                else
                {
                    if (dico.Langue == "Français") Console.WriteLine("Entrée incorrecte.");
                    else Console.WriteLine("Wrong input.");
                }
                Thread.Sleep(1500);
                test = DateTime.Now;
                intervalle = start - test;
            }
            Console.Clear();
            if (dico.Langue == "Français") Console.WriteLine("\nFin de la manche "+stage+" !\n\nRésultats : ");
            else Console.WriteLine("\nEnd of the round " + stage + " !\n\nScores : ");
            Console.WriteLine(j1.ToString());
            Console.WriteLine("");
            Console.WriteLine(j2.ToString());
            if (stage == 5)
                {
                Console.WriteLine("");
                if(j1.ScoreTotal() == j2.ScoreTotal())
                {
                    if (dico.Langue == "Français") Console.WriteLine("Egalité !");
                    else Console.WriteLine("Tie !" + j1.Nom);
                }
                else if (j1.ScoreTotal() > j2.ScoreTotal())
                {
                    if (dico.Langue == "Français") Console.WriteLine("Fin du jeu ! Le gagnant est : " + j1.Nom);
                    else Console.WriteLine("End of the game ! The winner is : " + j1.Nom);
                }
                else
                {
                    if (dico.Langue == "Français") Console.WriteLine("Fin du jeu ! Le gagnant est : " + j2.Nom);
                    else Console.WriteLine("End of the game ! The winner is : " + j2.Nom);
                }
                  Thread.Sleep(3500);
                }
            else
            {
                if (dico.Langue == "Français") Console.WriteLine("\nVoulez-vous sauvegarder la partie pour la continuer plus tard ? (Y/N)");
                else Console.WriteLine("\nDo you want to save the game to pursue it later ? (Y/N)");
                string? testsave = "";
                while (testsave != "Y" && testsave != "N")
                {
                    Console.WriteLine("");
                    testsave = Console.ReadLine();
                    if (testsave != "Y" || testsave != "N") Console.WriteLine("");
                }
                if (testsave == "Y")
                {
                    Save(temps,stage,gamemode);
                    Thread.Sleep(1500);
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// Sauvegarde le jeu entier actuel dans un fichier texte
        /// </summary>
        /// <param name="temps">Temps accordé à chaque joueur pour un plateau</param>
        /// <param name="level">Difficulté de la partie au moment de la sauvegarde</param>
        /// <param name="gamemode">Mode de jeu (Plateau généré aléatoirement ou bien à partir d'un ficher/sauvegarde)</param>

        public void Save(int temps,int level,string gamemode)
        {
            if (!File.Exists("Save.txt"))
            {
                string[] lines = new string[7]; // On créée un tableau de string contenant les données de sauvegarde
                if (j1.Mot_trouve != null && j1.Mot_trouve.Length > 0 && j1.Score_plateau != null && j1.Score_plateau.Length > 0)
                {
                    lines[0] = j1.Nom;
                    for (int i = 0; i < j1.Score_plateau.Length; i++)
                    {
                        if (i != j1.Score_plateau.Length - 1) lines[1] += j1.Score_plateau[i] + ";";        // On ajoute les infos du joueur 1
                        else lines[1] += j1.Score_plateau[i];
                    }
                    for (int i = 0; i < j1.Mot_trouve.Length; i++)
                    {
                        if (i != j1.Mot_trouve.Length - 1) lines[2] += j1.Mot_trouve[i] + ";";
                        else lines[2] += j1.Mot_trouve[i];
                    }
                    lines[3] = j2.Nom;
                    for (int i = 0; i < j2.Score_plateau.Length; i++)
                    {
                        if (i != j2.Score_plateau.Length - 1) lines[4] += j2.Score_plateau[i] + ";";        // On ajoute les infos du joueur 2
                        else lines[4] += j2.Score_plateau[i];
                    }
                    for (int i = 0; i < j2.Mot_trouve.Length; i++)
                    {
                        if (i != j2.Mot_trouve.Length - 1) lines[5] += j2.Mot_trouve[i] + ";";
                        else lines[5] += j2.Mot_trouve[i];
                    }
                    lines[6] = Convert.ToString(temps) + ";" + Convert.ToString(level) + ";" + gamemode;
                    File.WriteAllLinesAsync("Save.txt", lines);      // On écrit le fichier de sauvegarde
                    Console.WriteLine("Sauvegarde réussie !");
                }
                else Console.WriteLine("Les données sont erronées : impossible de sauvegarder.");
            }
            else
            {
                Console.WriteLine("Il existe déjà une sauvegarde, voulez-vous l'écraser ? (Y/N)");
                string? testsave = "";
                while (testsave != "Y" && testsave != "N")
                {
                    Console.WriteLine("");
                    testsave = Console.ReadLine();
                    if (testsave != "Y" && testsave != "N") Console.WriteLine("");
                }
                if (testsave == "Y")
                {
                    string[] lines = new string[7]; // On créée un tableau de string contenant les données de sauvegarde
                    if (j1.Mot_trouve != null && j1.Mot_trouve.Length > 0 && j1.Score_plateau != null && j1.Score_plateau.Length > 0)
                    {
                        lines[0] = j1.Nom;
                        for (int i = 0; i < j1.Score_plateau.Length; i++)
                        {
                            if (i != j1.Score_plateau.Length - 1) lines[1] += j1.Score_plateau[i] + ";";        // On ajoute les infos du joueur 1
                            else lines[1] += j1.Score_plateau[i];
                        }
                        for (int i = 0; i < j1.Mot_trouve.Length; i++)
                        {
                            if (i != j1.Mot_trouve.Length - 1) lines[2] += j1.Mot_trouve[i] + ";";
                            else lines[2] += j1.Mot_trouve[i];
                        }
                        lines[3] = j2.Nom;
                        for (int i = 0; i < j2.Score_plateau.Length; i++)
                        {
                            if (i != j2.Score_plateau.Length - 1) lines[4] += j2.Score_plateau[i] + ";";        // On ajoute les infos du joueur 2
                            else lines[4] += j2.Score_plateau[i];
                        }
                        for (int i = 0; i < j2.Mot_trouve.Length; i++)
                        {
                            if (i != j2.Mot_trouve.Length - 1) lines[5] += j2.Mot_trouve[i] + ";";
                            else lines[5] += j2.Mot_trouve[i];
                        }
                        lines[6] = Convert.ToString(temps) + ";" + Convert.ToString(level) + ";" + gamemode;
                        File.WriteAllLinesAsync("Save.txt", lines);      // On écrit le fichier de sauvegarde
                        Console.WriteLine("Sauvegarde réussie !");
                    }
                    else Console.WriteLine("Les données sont erronées : impossible de sauvegarder.");
                }
                else Console.WriteLine("Sauvegarde annulée");
            }
            
        }

        /// <summary>
        /// Charge une sauvegarde de jeu à partir d'un fichier texte
        /// </summary>

        public void LoadSave()
        {
            if (File.Exists("Save.txt"))
            {
                string[] lines = File.ReadAllLines("Save.txt");   // Lecture du fichier complet
                string n1 = lines[0];      // On isole la première ligne, il s'agit du nom du j1
                string n2 = lines[3];      // On isole la quatrième ligne, il s'agit du nom du j2
                string[] s1 = lines[1].Split(";");       // Pareil pour le score
                string[] s2 = lines[4].Split(";");       
                string[] m1 = lines[2].Split(";");       
                string[] m2 = lines[5].Split(";");       // Pareil pour les mots trouvés
                string[] parameters = lines[6].Split(";"); // Pareil pour les paramètres de jeu
                int time = Convert.ToInt32(parameters[0]);
                int level = Convert.ToInt32(parameters[1]);
                string gamemode = parameters[2];
                this.j1 = new Joueur(n1);
                this.j2 = new Joueur(n2);
                for(int i =0;i<s1.Length;i++)
                {
                    j1.Add_Score(Convert.ToInt32(s1[i]), i); // Ajout du score de j1
                }
                for (int i = 0; i < s2.Length; i++)
                {
                    j2.Add_Score(Convert.ToInt32(s2[i]), i); // Ajout du score de j2
                }
                for (int i = 0; i < m1.Length; i++)
                {
                    j1.Add_Mot(m1[i]); // Ajout des mots trouvés par le j1
                }
                for (int i = 0; i < m2.Length; i++)
                {
                    j2.Add_Mot(m2[i]); // Ajout des mots trouvés par le j2
                }
                Console.WriteLine("");
                Console.WriteLine("La partie continue au niveau "+(level+1)+" !");
                Thread.Sleep(1500);
                switch(level) // On continue selon le niveau auquel on s'est arrêté.
                {
                    case 1:
                        {
                            Partie(time, 2, gamemode);
                            Thread.Sleep(1500);
                            Partie(time, 3, gamemode);
                            Thread.Sleep(1500);
                            Partie(time, 4, gamemode);
                            Thread.Sleep(1500);
                            Partie(time, 5, gamemode);
                            break;
                        }
                    case 2:
                        {
                            Partie(time, 3, gamemode);
                            Thread.Sleep(1500);
                            Partie(time, 4, gamemode);
                            Thread.Sleep(1500);
                            Partie(time, 5, gamemode);
                            break;
                        }
                    case 3:
                        {
                            Partie(time, 4, gamemode);
                            Thread.Sleep(1500);
                            Partie(time, 5, gamemode);
                            break;
                        }
                    case 4:
                        {
                            Partie(time, 5, gamemode);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Erreur dans le chargement de la sauvegarde.");
                            break;
                        }
                }
            }
            else Console.WriteLine("La sauvegarde n'existe pas.");
        }
    }
}
