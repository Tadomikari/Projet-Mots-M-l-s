using System;
using System.IO;
using System.Xml;

namespace Projet_POO_Lombard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Affichage :

            Console.WriteLine(" █   █ ██▀ █   ▄▀▀ ▄▀▄ █▄ ▄█ ██▀\r\n ▀▄▀▄▀ █▄▄ █▄▄ ▀▄▄ ▀▄▀ █ ▀ █ █▄▄\r\n");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("");
            int res=0;
            string langue = "";
            while (res!=1 && res!=2)
            {
                Console.WriteLine("       _                                                       _                                       \r\n      ( )                                                     (_ )                                     \r\n   ___| |__    _     _    ___   __    _   _   _   _   _ _ __   | |   _ _  ___    __    _ _   __    __  \r\n / ___)  _  \\/ _ \\ / _ \\/  __)/ __ \\ ( ) ( )/ _ \\( ) ( )  __)  | | / _  )  _  \\/ _  \\/ _  )/ _  \\/ __ \\\r\n( (___| | | | (_) ) (_) )__  \\  ___/ | (_) | (_) ) (_) | |     | |( (_| | ( ) | (_) | (_| | (_) |  ___/\r\n \\____)_) (_)\\___/ \\___/(____/\\____)  \\__  |\\___/ \\___/(_)    (___)\\__ _)_) (_)\\__  |\\__ _)\\__  |\\____)\r\n                                     ( )_| |                                  ( )_) |     ( )_) |      \r\n                                      \\___/                                    \\___/       \\___/       \r\n");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1) French \n\n2) English");  //On demande le langage à l'utilisateur
                Console.WriteLine("");
                string? rep = Console.ReadLine();
                if (int.TryParse(rep, out res)) Console.Clear();
                else
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Entrée invalide");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
            if(res==1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("░░░▀░█▀▀░█░▒█░░░█▀▄░█▀▀░░░█▀▄▀█░▄▀▀▄░▀█▀░█▀▀░░░█▀▄▀█░█▀▀░█░░█▀▀░█▀▀\r\n░░░█░█▀▀░█░▒█░░░█░█░█▀▀░░░█░▀░█░█░░█░░█░░▀▀▄░░░█░▀░█░█▀▀░█░░█▀▀░▀▀▄\r\n░█▄█░▀▀▀░░▀▀▀░░░▀▀░░▀▀▀░░░▀░░▒▀░░▀▀░░░▀░░▀▀▀░░░▀░░▒▀░▀▀▀░▀▀░▀▀▀░▀▀▀\r\n");
                Console.ForegroundColor= ConsoleColor.White;
                langue = "Français";
                Console.WriteLine();
                Console.WriteLine("Voulez-vous charger une sauvegarde ?(Y/N)");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("░█▀▄▀█░░▀░░█░█░█▀▀░█▀▄░░░█░░░█░▄▀▀▄░█▀▀▄░█▀▄░█▀▀░░░█▀▀▀░█▀▀▄░█▀▄▀█░█▀▀\r\n░█░▀░█░░█▀░▄▀▄░█▀▀░█░█░░░▀▄█▄▀░█░░█░█▄▄▀░█░█░▀▀▄░░░█░▀▄░█▄▄█░█░▀░█░█▀▀\r\n░▀░░▒▀░▀▀▀░▀░▀░▀▀▀░▀▀░░░░░▀░▀░░░▀▀░░▀░▀▀░▀▀░░▀▀▀░░░▀▀▀▀░▀░░▀░▀░░▒▀░▀▀▀\r\n");
                Console.ForegroundColor=ConsoleColor.White;
                langue = "Anglais";
                Console.WriteLine();
                Console.WriteLine("Do you want to load a save ?(Y/N)");
            }
            Console.WriteLine();
            Dictionnaire dico = new Dictionnaire(langue);
            string? testsave="";
            string? nom1 = "";
            string? nom2 = "";
            while (testsave!="Y" && testsave!="N")
            {
                Console.WriteLine();
                testsave = Console.ReadLine();
                if (testsave != "Y" && testsave != "N")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Entrée invalide");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            if (testsave == "Y")  // Cas de la sauvegarde
            {
                Joueur j1 = new Joueur(nom1);
                Joueur j2 = new Joueur(nom2);
                Jeu jeu = new Jeu(dico, j1, j2);
                jeu.LoadSave();
            }
            else // Cas du plateau automatique
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                if (langue != "Français")
                {
                    Console.WriteLine(" ██▀ █▄ █ ▀█▀ ██▀ █▀▄   █▀▄ █   ▄▀▄ ▀▄▀ ██▀ █▀▄   ▄█   █▄ █ ▄▀▄ █▄ ▄█ ██▀\r\n █▄▄ █ ▀█  █  █▄▄ █▀▄   █▀  █▄▄ █▀█  █  █▄▄ █▀▄    █   █ ▀█ █▀█ █ ▀ █ █▄▄\r\n");
                }
                else Console.WriteLine("░▒█▀▀▀░█▀▀▄░▀█▀░█▀▀▄░█▀▀░▀▀█░░░█░░█▀▀░░░█▀▀▄░▄▀▀▄░█▀▄▀█░░░█▀▄░█░▒█░░░░░▀░▄▀▀▄░█░▒█░█▀▀░█░▒█░█▀▀▄░░░▄█░\r\n░▒█▀▀▀░█░▒█░░█░░█▄▄▀░█▀▀░▄▀▒░░░█░░█▀▀░░░█░▒█░█░░█░█░▀░█░░░█░█░█░▒█░░░░░█░█░░█░█░▒█░█▀▀░█░▒█░█▄▄▀░░░░█▒\r\n░▒█▄▄▄░▀░░▀░░▀░░▀░▀▀░▀▀▀░▀▀▀░░░▀▀░▀▀▀░░░▀░░▀░░▀▀░░▀░░▒▀░░░▀▀░░░▀▀▀░░░█▄█░░▀▀░░░▀▀▀░▀▀▀░░▀▀▀░▀░▀▀░░░▄█▄\r\n");
                nom1 = Console.ReadLine();
                Joueur j1 = new Joueur(nom1);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                if (langue != "Français")  Console.WriteLine(" ██▀ █▄ █ ▀█▀ ██▀ █▀▄   █▀▄ █   ▄▀▄ ▀▄▀ ██▀ █▀▄   ▀█   █▄ █ ▄▀▄ █▄ ▄█ ██▀\r\n █▄▄ █ ▀█  █  █▄▄ █▀▄   █▀  █▄▄ █▀█  █  █▄▄ █▀▄   █▄   █ ▀█ █▀█ █ ▀ █ █▄▄\r\n");
                else Console.WriteLine("░▒█▀▀▀░█▀▀▄░▀█▀░█▀▀▄░█▀▀░▀▀█░░░█░░█▀▀░░░█▀▀▄░▄▀▀▄░█▀▄▀█░░░█▀▄░█░▒█░░░░░▀░▄▀▀▄░█░▒█░█▀▀░█░▒█░█▀▀▄░░░█▀█\r\n░▒█▀▀▀░█░▒█░░█░░█▄▄▀░█▀▀░▄▀▒░░░█░░█▀▀░░░█░▒█░█░░█░█░▀░█░░░█░█░█░▒█░░░░░█░█░░█░█░▒█░█▀▀░█░▒█░█▄▄▀░░░▒▄▀\r\n░▒█▄▄▄░▀░░▀░░▀░░▀░▀▀░▀▀▀░▀▀▀░░░▀▀░▀▀▀░░░▀░░▀░░▀▀░░▀░░▒▀░░░▀▀░░░▀▀▀░░░█▄█░░▀▀░░░▀▀▀░▀▀▀░░▀▀▀░▀░▀▀░░░█▄▄\r\n");
                nom2 = Console.ReadLine();
                Joueur j2 = new Joueur(nom2);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                if (langue == "Français") Console.WriteLine("De combien de temps dispose le joueur à chaque plateau ? (en min)");
                else Console.WriteLine("How much time does the player have on each board ? (in minutes)");
                int time = 0;
                while (time<=0)
                {
                    try
                    {
                        time = Convert.ToInt32(Console.ReadLine()) * 60;
                        if (time <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            if (langue == "Français") Console.WriteLine("Erreur. Veuillez entrer un nombre entier positif.");
                            else Console.WriteLine("Error. Please enter a positive integer.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (langue == "Français") Console.WriteLine("Erreur. Veuillez entrer un nombre entier positif.");
                        else Console.WriteLine("Error. Please enter a positive integer.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.Clear();
                Jeu jeu = new Jeu(dico, j1, j2);
                Console.WriteLine();
                string? gamemode = "";
                while (gamemode != "Y" && gamemode != "N")
                {
                    if (langue == "Français")
                    {
                        Console.Clear();
                        Console.ForegroundColor= ConsoleColor.Blue;
                        Console.WriteLine("░▒█▀▄▀█░▄▀▀▄░█▀▄░█▀▀░░░█▀▄░█▀▀░░░░░▀░█▀▀░█░▒█\r\n░▒█▒█▒█░█░░█░█░█░█▀▀░░░█░█░█▀▀░░░░░█░█▀▀░█░▒█\r\n░▒█░░▒█░░▀▀░░▀▀░░▀▀▀░░░▀▀░░▀▀▀░░░█▄█░▀▀▀░░▀▀▀\r\n");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Voulez vous jouez avec des plateaux générés automatiquements ? (Y/N)");
                        Console.WriteLine("Sinon : vérifiez que vos fichiers sont nommées correctement et placés au bon endroit (lire la documentation d'utilisation)");
                        if (gamemode != "Y" && gamemode != "N") Console.WriteLine();
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("░▒█▀▀█░█▀▀▄░█▀▄▀█░█▀▀░░░█▀▄▀█░▄▀▀▄░█▀▄░█▀▀\r\n░▒█░▄▄░█▄▄█░█░▀░█░█▀▀░░░█░▀░█░█░░█░█░█░█▀▀\r\n░▒█▄▄▀░▀░░▀░▀░░▒▀░▀▀▀░░░▀░░▒▀░░▀▀░░▀▀░░▀▀▀\r\n");
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Do you want to play with automatically generated boards? (Y/N)");
                        Console.WriteLine("If no : make sure your files are named correctly and placed in the right place (read the user documentation)");
                        Console.WriteLine("\nWarning : manually generated boards might be in french ! (Just create an english one !)");
                        if (gamemode != "Y" && gamemode != "N") Console.WriteLine("");
                    }
                    Console.WriteLine("");
                    gamemode = Console.ReadLine();
                }
                if (gamemode == "Y") 
                {
                    gamemode = "Auto";
                }
                else gamemode = "Fichier";
                /*jeu.Partie(time, 1, gamemode);
                Thread.Sleep(1500);
                jeu.Partie(time, 2, gamemode);
                Thread.Sleep(1500);
                jeu.Partie(time, 3, gamemode);
                Thread.Sleep(1500);
                jeu.Partie(time, 4, gamemode);
                Thread.Sleep(1500);*/
                jeu.Partie(time, 5, gamemode);
            }      
        }
    }
}