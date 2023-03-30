using System;
using System.Collections;
using System.Linq;

namespace DomeOfDuels
{
    internal class Program

    {
        public static Hashtable itemTable = new Hashtable()
        {
            {1, new Item(1, "SWORD", 10, 20)},
            {2, new Item(2, "FISTS", 5, 55)},
            {3, new Item(3, "BOW & ARROW", 20, 35)}
        };

        static void Main(string[] args)

        {
            //Player stats
            Player player = new Player(playerCurrentHP: 100, playerMaxHP: 100, playerAttack: 5, healAmt: 6, numberOfVictories: 0, playerName:"{0}");

            GetName(player);
            //Introduction to the game
            Console.WriteLine("");
            Console.WriteLine("In the year 30,000,000 C.E. the only source of entertainment are one-on-one duels...");
            Console.WriteLine("A new challenger approaches to claim the the title of 'CHAMPION' ");
            Console.WriteLine("");
            Console.WriteLine("RISE " + player.PlayerName + "...");
            Console.WriteLine("");
            Console.Write("PRESS 'ENTER' TO CONTINUE >");
            Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("WELCOME TO THE DOME OF DUELS!");
            Console.WriteLine("");
            Console.WriteLine("The rules are simple:");
            Console.WriteLine("-Choose your weapon-");
            Console.WriteLine("-Fighters will take turns-");
            Console.WriteLine("-Fight until you drop! (or get tired of dueling)-");
            Console.WriteLine("");
            Console.WriteLine("");


            //Offer user weapon options, player's choice will be added to player attack score
            Console.WriteLine("CHOOSE YOUR WEAPON: ");
            Console.WriteLine("_______________________________");
            Console.WriteLine("");
            foreach (Item value in itemTable.Values)
            {
                Console.WriteLine("ENTER {0}: ", value.Id);
                Console.WriteLine("Item Name:{0}", value.Name);
                Console.WriteLine("Attack Modifier:{0}", value.AttackMod);
                Console.WriteLine("_______________________________");
            }
            int weaponchoice = int.Parse(Console.ReadLine());

            
            switch (weaponchoice)
            {
                case 1:
                    Console.WriteLine("You have chosen the SWORD!");
                    break;

                case 2:
                    Console.WriteLine("You have chosen your FISTS!");
                    break;

                case 3:
                    Console.WriteLine("You have chosen the BOW & ARROW!");
                    break;

                default:
                    throw new ArgumentException();
            }

            var item = (Item)itemTable[weaponchoice];


            // Create table of enemies
            Hashtable enemyTable = new Hashtable();

            Enemy enemy1 = new Enemy(enemyId: 0, enemyName: "Carl the Killer", enemyAttack: 25, enemyCurrentHp: 26, enemyMaxHp: 30, healAmt: 4);
            Enemy enemy2 = new Enemy(enemyId: 1, enemyName: "Louise the Loser",enemyAttack: 5, enemyCurrentHp: 35, enemyMaxHp: 35, healAmt: 10);
            Enemy enemy3 = new Enemy(enemyId: 2, enemyName: "Veronica the Victorious", enemyAttack: 15, enemyCurrentHp: 40, enemyMaxHp: 40, healAmt: 7);

            enemyTable.Add(enemy1.EnemyId, enemy1);
            enemyTable.Add(enemy2.EnemyId, enemy2);
            enemyTable.Add(enemy3.EnemyId, enemy3);

            Console.WriteLine("***************************************************");
            Console.WriteLine("Today you will be up against any of these fighters:");
            Console.WriteLine("");

            //organize enemies by attack power using LINQ
            var enemies = enemyTable.Values.Cast<Enemy>();
            enemies = enemies.OrderBy(x => x.EnemyAttack).ToList();

            foreach(Enemy enemy in enemies)
            {
                Console.WriteLine("Name: {0}", enemy.EnemyName);
                Console.WriteLine("Attack Power: {0}", enemy.EnemyAttack);
                Console.WriteLine("");
            }


            Console.WriteLine("Press any key to continue to battle!");
            Console.WriteLine("");
            Random spawnEnemy = new Random();
            var playAgain = Console.ReadLine();

            //master loop for player to play unlimited rounds, with randomized enemies
            do
            {

                var nextBattle = spawnEnemy.Next(0, 3);
                if (nextBattle == 0)
                {
                    enemy1.EnemyCurrentHp = 30;
                    Console.WriteLine("");
                    Console.WriteLine("Your next opponent will be 'Carl the Killer!'");
                    StartCombat(enemy1, player, item);
                    Console.WriteLine("Would you like to play again?");
                    Console.WriteLine("Enter 'y' for yes, or 'n' for no");
                    playAgain = Console.ReadLine();
                }
                if (nextBattle == 1)
                {
                    enemy2.EnemyCurrentHp = 35;
                    Console.WriteLine("");
                    Console.WriteLine("Your next opponent will be 'Louise the Loser!'");
                    StartCombat(enemy2, player, item);
                    Console.WriteLine("Would you like to play again?");
                    Console.WriteLine("Enter 'y' for yes, or 'n' for no");
                    playAgain = Console.ReadLine();

                }
                else if (nextBattle == 2)
                {
                    enemy3.EnemyCurrentHp = 40;
                    Console.WriteLine("");
                    Console.WriteLine("Your next opponent will be 'Veronica the Victorious!'");
                    StartCombat(enemy3, player, item);
                    Console.WriteLine("Would you like to play again?");
                    Console.WriteLine("Enter 'y' for yes, or 'n' for no");
                    playAgain = Console.ReadLine();
                }
            }while(playAgain == "y");
            
        }
        //method to call the combat scenario when needed
        static void StartCombat(Enemy enemy, Player player, Item item)
        {
            
            Random random = new Random();
            //allow for specific object to be used in the combat senario

            while (player.PlayerCurrentHP > 0 && enemy.EnemyCurrentHp > 0)
            {
                Console.WriteLine("");
                Console.WriteLine("--- YOUR TURN ---");
                Console.WriteLine("Enter 'a' to Attack or 'h' to Heal");
                var userinput = Console.ReadLine();

                // try again message for any invalid input
                if (userinput != "a" && userinput != "h")
                {
                    Console.WriteLine("Try again!");
                    continue;
                }
                // read player input and act accordingly
                if (userinput == "a")
                {
                    var damage = (player.PlayerAttack + item.AttackMod);
                    enemy.EnemyCurrentHp -= damage;
                    Console.WriteLine("YOU deal " + damage + " damage!");
                    Console.WriteLine("");
                    Console.WriteLine(enemy.EnemyName + "'s HP: " + enemy.EnemyCurrentHp);
                    Console.WriteLine("_____________________");
                    Console.WriteLine("");
                }
                else if (userinput == "h")
                {
                    player.PlayerCurrentHP += player.HealAmt;

                    Console.WriteLine("You heal " + player.PlayerCurrentHP + " points!");
                    Console.WriteLine("");
                    Console.WriteLine("YOUR HP: " + player.PlayerCurrentHP);
                    Console.WriteLine("_____________________");
                    Console.WriteLine("");
                }

                //enemy's turn

                if (enemy.EnemyCurrentHp > 0)
                {
                    
                    Console.WriteLine("--- " + enemy.EnemyName + "'s turn ---");
                    int enemychoice = random.Next(0, 2);

                    if (enemychoice == 0)
                    {

                        player.PlayerCurrentHP -= enemy.EnemyAttack;
                        Console.WriteLine(enemy.EnemyName + " deals " + enemy.EnemyAttack + " damage!");
                        Console.WriteLine("");
                        Console.WriteLine("YOUR HP: " + player.PlayerCurrentHP);
                        Console.WriteLine("_____________________");
                        Console.WriteLine("");
                    }

                    else
                    {
                        enemy.EnemyCurrentHp += enemy.HealAmt;
                        Console.WriteLine(enemy.EnemyName + " heals themself!");
                        Console.WriteLine("");
                        Console.WriteLine(enemy.EnemyName + "'s HP: " + enemy.EnemyCurrentHp);
                        Console.WriteLine("_____________________");
                        Console.WriteLine("");
                    }

                }


            }
            // give player feedback on battle, prints number of victories to console
            var victory = enemy.EnemyCurrentHp <= 0;
            var defeat = player.PlayerCurrentHP <= 0;

            if (defeat)
            {
              Console.WriteLine("*-_- YOU HAVE PERISHED! -_-*");
              Console.WriteLine("");
            }
            else if (victory)
            {
              Console.WriteLine("***-- FOE DEFEATED! --***");
              Console.WriteLine("");
              player.NumberOfVictories++;
            }
            Console.WriteLine("");
            Console.WriteLine(player.PlayerName + ".... you have won " + player.NumberOfVictories + " time(s)!");


        }
        //gets player name
        static void GetName(Player player)
        {
            Console.WriteLine("Please enter your name...");
            player.PlayerName = Console.ReadLine();
        }
    }
}