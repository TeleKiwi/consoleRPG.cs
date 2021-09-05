using System;
using System.Threading;
using System.Diagnostics;

namespace consoleRPG
{
    class Program
    {
        public static string playerName;
        public static string versionNo = "0.0.1";
        public static string tempVar; // used to store player input temporarily
        public static int playerHP;
        public static int playerMP;
        public static int playerXP = 0;
        public static int playerLVL = 1;
        
        static void Main(string[] args)
        {
            Console.Title = "consoleRPG.cs";

            // title screen
            Console.WriteLine("consoleRPG.cs");
            Console.WriteLine("version " + versionNo);
            Console.WriteLine("Press any key to begin.");
            Console.WriteLine("----------------------------");
            Console.ReadKey();

            // new game/load save 
            Console.Clear();
            Console.WriteLine("Would you like to save or load a new file?");
            Console.WriteLine("Type '1' to start a new game or '2' to enter a save key.");
            Console.WriteLine("----------------------------------------------------------");
            tempVar = Convert.ToString(Console.ReadLine());

            switch(tempVar)
            {
                case "1":
                {
                    NewGame();
                    break;
                }
                case "2":
                {
                    NewGame();
                    break; // will implement save/load system later, for now we pass into a new game
                }
                default:
                {
                    Console.WriteLine("Invalid input.");
                    break;
                }
            }
            Console.ReadKey(); // instaclose prevention
        }

        static void NewGame()
        {
            Console.Clear();
            Console.WriteLine("Hello there, young fellow");
            Console.WriteLine("I see you're trying to become a wizard, eh?");
            Console.WriteLine("Well, that's no problem! I can help you achieve your dreams!");
            Console.WriteLine("What was your name again? I must have forgotten.");
            Console.WriteLine("-----------------------------------------------------------------");
            playerName = Convert.ToString(Console.ReadLine());

            Console.Clear();
            Console.WriteLine("Ah, so it was " + playerName + ", yes? Silly me.");
            Console.WriteLine("Well, your journey is about to begin!");
            Console.WriteLine("I hope you have fun in your adventure, " + playerName + "! Farewell!");
            Console.WriteLine("Press any key to continue.");
            Console.WriteLine("------------------------------------------------------------------------");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Setting up...");
            playerXP = 0;
            playerMP = 15;
            playerLVL = 1;
            playerHP = 100;

            GameLoop();
        }

        static void GameLoop()
        {
            Console.Clear();
            Console.WriteLine(playerName);
            Console.WriteLine("XP = " + playerXP);
            Console.WriteLine("MP = " + playerMP);
            Console.WriteLine("LEVEL = " + playerLVL);
            Console.WriteLine("HP = " + playerHP);
            Console.WriteLine("1 - BATTLE        2 - ITEMS/MOVES");
            Console.WriteLine("3 - SAVE          4 - QUIT");
            Console.WriteLine("-----------------------------------");
            tempVar = Convert.ToString(Console.ReadLine());

            switch(tempVar)
            {
                case "1":
                {
                    BeginBattle();
                    break;
                }
                case "2":
                {
                    ViewItems();
                    break;
                }
                case "3":
                {
                    Save();
                    break;
                }
                case "4":
                {
                    Quit();
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid input.");
                    Thread.Sleep(2000);
                    GameLoop();
                    break;
                }
            }

            static void Quit()
            {
                Console.Clear();
                Console.WriteLine("Do you want to save before quitting?");
                Console.WriteLine("1 for yes, 2 for no.");
                Console.WriteLine("-------------------------------------");
                tempVar = Convert.ToString(Console.ReadLine());

                switch(tempVar)
                {
                    case "1":
                    {
                        Save();
                        break;
                    }
                    case "2":
                    {
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Invalid input.");
                        Thread.Sleep(2000);
                        Quit();
                        break;
                    }
                }

                Console.Clear();
                Console.WriteLine("Are you sure you want to quit?");
                Console.WriteLine("1 for yes, 2 for no.");
                Console.WriteLine("-------------------------------------");
                tempVar = Convert.ToString(Console.ReadLine());

                switch(tempVar)
                {
                    case "1":
                    {
                        Environment.Exit(0);
                        break;
                    }
                    case "2":
                    {
                        GameLoop();
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Invalid input.");
                        Thread.Sleep(2000);
                        Quit();
                        break;
                    }
                }
            }

        }
    }   

}
