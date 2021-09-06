using System;
using System.Threading;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;

// encryption and decryption code from c-sharpcorner.com
// (i slightly modified it)

namespace consoleRPG
{
    class Player
    {
        public static string playerName;
        public static string versionNo = "0.0.3";
        public static string userPassword;
        public static string tempVar; // used to store player input temporarily
        public static int battleTurn;
        public static int tempInt;
        public static int playerHP;
        public static int playerMP;
        public static int playerXP = 0;
        public static int playerLVL = 1;
        public static int i;
        public static string context;
        public static string[] moves =
        {
            "Melee",
            "Spell",
            "Heal"
        };
        public static int[] damage =
        {
            5,
            7,
            -5
        };
        public static string[] items;
        public static string[] itemDescriptions;
        public static string[] itemDamage;

        static void Main(string[] args)
        {
            Console.Title = "consoleRPG.cs";

            TitleScreen();
        }

        static void TitleScreen()
        {
            // title screen
            Console.Clear();
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
            tempVar = Convert.ToString(Console.ReadKey());

            switch (tempVar)
            {
                case "1":
                    {
                        NewGame();
                        break;
                    }
                case "2":
                    {
                        Load();
                        break;
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
            Console.WriteLine("Hello there, young fellow!");
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
            tempVar = Convert.ToString(Console.ReadKey());

            switch (tempVar)
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
                        context = "main menu";
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
        }

        static void BeginBattle()
        {
            Enemy.GenEnemy();
            Console.WriteLine("You encountered a level " + Enemy.enemyLVL + " " + Enemy.enemyType + "!");
            Thread.Sleep(5000);
            Console.Clear();
            MainBattleLoop();
        }

        static void ViewItems()
        {
            Console.Clear();

            for (i = 0; i <= moves.Length; i++)
            {
                if (moves[i] == "Heal")
                {
                    Console.WriteLine("Heal - Heals " + Math.Abs(damage[i]) + " health.");
                }
                else
                {
                    Console.WriteLine(moves[i] + " - Deals " + damage[i] + " damage.");
                }
            }

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            for (i = 0; i <= items.Length; i++)
            {
                Console.WriteLine(items[i] + " - " + itemDescriptions[i]);
            }

            Console.WriteLine("Press any key to exit.");
            Console.WriteLine("-------------------------------------------------");
            Console.ReadKey();
            switch (context)
            {
                case "main menu":
                    {
                        GameLoop();
                        break;
                    }
                case "attack menu":
                    {
                        MainBattleLoop();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Couldn't find a place to send you back to. Sending you to title screen.");
                        Thread.Sleep(5000);
                        Console.Clear();
                        TitleScreen();
                        break;
                    }

            }


        }

        static void Save()
        {
            Console.Clear();
            Console.WriteLine("Generating your password....");
            Console.WriteLine("------------------------------");
            Cryptography.Encrypt(Convert.ToString(playerName), "stqm3-9tm64-193kk");
            userPassword = userPassword + Cryptography.result;
            Cryptography.result = "";
            Cryptography.Encrypt(Convert.ToString(playerXP), "4m1ii-93641-mqhjl");
            userPassword = userPassword + Cryptography.result;
            Cryptography.result = "";
            Cryptography.Encrypt(Convert.ToString(playerHP), "331jk-loinf-5t1ja");
            userPassword = userPassword + Cryptography.result;
            Cryptography.result = "";
            Cryptography.Encrypt(Convert.ToString(playerMP), "kja81-9knag-pq1tb");
            userPassword = userPassword + Cryptography.result;
            Cryptography.result = "";
            Cryptography.Encrypt(Convert.ToString(playerLVL), "nhgya-muiy1-g1n7k");
            userPassword = userPassword + Cryptography.result;
            Cryptography.result = "";

            Console.Clear();
            Console.WriteLine("Here's your password.");
            Console.WriteLine(userPassword);
            Console.WriteLine("Don't forget it! Press any key to continue.");
            Console.WriteLine("----------------------------------------------");
            Console.ReadKey();

            Console.Clear();
            switch (context)
            {
                case "quit":
                    {
                        QuitConformation();
                        break;
                    }
                case "main menu":
                    {
                        GameLoop();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Couldn't find a place to send you back to. Sending you to title screen.");
                        Thread.Sleep(5000);
                        Console.Clear();
                        TitleScreen();
                        break;
                    }
            }

        }

        static void Load()
        {
            Console.Clear();
            Console.WriteLine("Please present your password.");
            Console.WriteLine("----------------------------------");
            userPassword = Convert.ToString(Console.ReadLine());

            /* todo; code in decrypting.
            for now we send the player back to the title screen.
            */

            Console.Clear();
            Console.WriteLine("Sorry, but loading is currently unfinished. Please start a new file.");
            Console.WriteLine("---------------------------------------------------------------------");
            Thread.Sleep(5000);
            TitleScreen();

        }

        public static void MainBattleLoop()
        {
            Console.Clear();

            if (battleTurn == 0)
            {
                Console.WriteLine(playerName + ": " + playerHP + "HP, " + playerMP + "MP, LEVEL" + playerLVL);
                Console.WriteLine(Enemy.enemyType + ": " + Enemy.enemyHP + "HP, " + Enemy.enemyMP + "MP, LEVEL" + Enemy.enemyLVL);
                Console.WriteLine("1 - ATTACKS      2 - HEAL");
                Console.WriteLine("3 - ITEMS        4 - RUN AWAY");
                Console.WriteLine("----------------------------------------------------------------------------------------------");
                tempVar = Convert.ToString(Console.ReadLine());

                switch (tempVar)
                {
                    case "1":
                        {
                            InitAttackMenu();
                            break;
                        }
                    case "2":
                        {
                            Attacks.Heal();
                            break;
                        }
                    case "3":
                        {
                            context = "attack menu";
                            ViewItems();
                            break;
                        }
                    case "4":
                        {
                            RunAway();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid input. Try again.");
                            Thread.Sleep(5000);
                            MainBattleLoop();
                            break;
                        }
                }
            }
            else
            {
                tempInt = Enemy.enemyR.Next(0, 9);

                switch (tempInt)
                {
                    case 1 or 2 or 3:
                        {
                            Attacks.EnemyMeleeAttack();
                            break;
                        }
                    case 4 or 5 or 6:
                        {
                            Attacks.EnemySpellAttack();
                            break;
                        }
                    case 7 or 8 or 9:
                        {
                            Attacks.EnemyHeal();
                            break;
                        }
                }
            }

        }


        static void Quit()
        {
            Console.Clear();
            Console.WriteLine("Do you want to save before quitting?");
            Console.WriteLine("1 for yes, 2 for no.");
            Console.WriteLine("-------------------------------------");
            tempVar = Convert.ToString(Console.ReadKey());

            switch (tempVar)
            {
                case "1":
                    {
                        context = "quit";
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

            QuitConformation(); // split this up so we can return to this if we decide to save
        }

        public static void QuitConformation()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to quit?");
            Console.WriteLine("1 for yes, 2 for no.");
            Console.WriteLine("-------------------------------------");
            tempVar = Convert.ToString(Console.ReadKey());

            switch (tempVar)
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

        public static void InitAttackMenu()
        {
            // todo
        }

        public static void RunAway()
        {
            Console.Clear();

            tempInt = Enemy.enemyR.Next(0, 10);

            if (tempInt <= 4) // failed to run away
            {
                Console.WriteLine("You failed to run away!");
                Console.WriteLine("-------------------------");
                Thread.Sleep(5000);
                battleTurn = 1;
                MainBattleLoop();
            }
            else
            {
                Console.WriteLine("You got away safely!");
                Console.WriteLine("---------------------");
                Thread.Sleep(5000);
                Player.battleTurn = 0;
                GameLoop();

            }

        }

    }

    class Enemy
    {
        public static string enemyType;
        public static Random enemyR = new Random();
        public static int enemyTempVar;
        public static int enemyLVL;
        public static int enemyHP;
        public static int enemyMP;
        public static int enemyTypeAmount = possibleEnemyTypes.Length;
        public static string[] possibleEnemyTypes =
        {
            "Goba",
            "Shiny Goba",
            "Qubon",
            "Shiny Qubon",
            "Menena",
            "Shiny Menena",
        };
        public static int[] minEnemyLevels =
        {
            1,
            11,
            16,
            23,
            45,
            61

        };
        public static int[] maxEnemyLevels =
        {
            5,
            15,
            25,
            35,
            65,
            85
        };

        public static void GenEnemy()
        {
            while (enemyLVL! > Player.playerLVL)
            {
                // this generates the enemy type
                enemyTempVar = enemyR.Next(1, enemyTypeAmount);
                enemyType = possibleEnemyTypes[enemyTempVar];

                // and this generates the enemy's level
                enemyLVL = enemyR.Next(minEnemyLevels[enemyTempVar], maxEnemyLevels[enemyTempVar]);
            }
        }

    }

    public class Cryptography
    {
        public static string result;

        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            result = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            return result;
        }
        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            result = UTF8Encoding.UTF8.GetString(resultArray);
            return result;
        }
    }

    public class Attacks
    {
        public static void Heal()
        {
            Console.Clear();
            Console.WriteLine(Player.playerName + " used Heal!");
            if (Player.playerHP == 100)
            {
                Console.WriteLine("But, they already had 100HP!");
            }
            else
            {
                if (Player.playerHP >= 96)
                {
                    Player.playerHP += (100 - Player.playerHP); // prevents overflowing
                    Console.WriteLine("They healed " + (100 - Player.playerHP) + "HP!");
                }
                else
                {
                    Player.playerHP += 5;
                    Console.WriteLine("They healed 5HP!");
                }
            }
            Console.WriteLine("--------------------------------------------------");
            Thread.Sleep(5000);
            Console.Clear();
            Player.battleTurn = 1;
            Player.MainBattleLoop();
        }

        public static void MeleeAttack()
        {
            // todo
        }

        public static void SpellAttack()
        {
            // todo
        }

        public static void Item()
        {
            // todo
        }

        public static void EnemyHeal()
        {
            Console.Clear();
            Console.WriteLine(Enemy.enemyType + " used Heal!");
            if (Enemy.enemyHP == 100)
            {
                Console.WriteLine("But, they already had 100HP!");
            }
            else
            {
                if (Enemy.enemyHP >= 96)
                {
                    Enemy.enemyHP += (100 - Enemy.enemyHP); // prevents overflowing
                    Console.WriteLine("They healed " + (100 - Enemy.enemyHP) + "HP!");
                }
                else
                {
                    Enemy.enemyHP += 5;
                    Console.WriteLine("They healed 5HP!");
                }
            }
            Console.WriteLine("--------------------------------------------------");
            Thread.Sleep(5000);
            Console.Clear();
            Player.battleTurn = 0;
            Player.MainBattleLoop();
        }

        public static void EnemyMeleeAttack()
        {
            // todo
        }

        public static void EnemySpellAttack()
        {
            // todo
        }
    }
}
