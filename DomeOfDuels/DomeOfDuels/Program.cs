using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

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
            Player player = new Player(playerCurrentHP: 40, playerMaxHP: 40, playerAttack: 5, healAmt: 6, numberOfVictories: 0);

            //offer weapon option and assign value to player attack
            

            Console.WriteLine("CHOOSE YOUR WEAPON: ");
            Console.WriteLine("_______________________________");
            Console.WriteLine("");
            foreach (Item value in itemTable.Values)
            {
                Console.WriteLine("ENTER {0}: ", value.Id);
                Console.WriteLine("Item Name:{0}", value.Name);
                Console.WriteLine("Attack Modifier:{0}", value.AttackMod);
                Console.WriteLine("Value in Gold:{0}", value.Gold);
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

                case 3: Console.WriteLine("You have chosen the BOW & ARROW!");
                    break;

                default:
                    throw new ArgumentException();
            }

            var item = (Item)itemTable[weaponchoice];


            // Create table of enemies
            Hashtable enemyTable = new Hashtable();

            Enemy enemy1 = new Enemy(enemyId: 0, enemyName: "Carl the Killer", enemyAttack: 5, enemyCurrentHp: 30, enemyMaxHp: 30, healAmt: 4);
            Enemy enemy2 = new Enemy(enemyId: 1, enemyName: "Louise the Loser",enemyAttack: 7, enemyCurrentHp: 35, enemyMaxHp: 35, healAmt: 5);
            Enemy enemy3 = new Enemy(enemyId: 2, enemyName: "Veronica the Victorious", enemyAttack: 10, enemyCurrentHp: 40, enemyMaxHp: 40, healAmt: 7);

            enemyTable.Add(enemy1.EnemyId, enemy1);
            enemyTable.Add(enemy2.EnemyId, enemy2);
            enemyTable.Add(enemy3.EnemyId, enemy3);

            Enemy storedEnemy1 = (Enemy)enemyTable[enemy1.EnemyId];
            StartCombat(enemy1, player, item);



            Console.WriteLine("Would you like to move to the next round?");
            Console.WriteLine("enter 'y' for Yes or 'n' for No.");
            var playAgain = Console.ReadLine();
            
            if (playAgain == "y")
            {
                StartCombat(enemy2, player, item);
                
            }
            else
            {
                Console.WriteLine("THANK YOU FOR PLAYING!");
                
            }

        }


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

                //add try again message for any invalid input
                if (userinput != "a" && userinput != "h")
                {
                    Console.WriteLine("Try again!");
                    continue;
                }

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
                        Console.WriteLine(enemy.EnemyName + " deals " + enemy.EnemyAttack + "damage!");
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
            var victory = enemy.EnemyCurrentHp <= 0;
            var defeat = player.PlayerCurrentHP <= 0;
           
            if (defeat)
            {
                Console.WriteLine("*-_- YOU HAVE PERISHED! -_-*");
            }
            else if (victory)
            {
                Console.WriteLine("***-- FOE DEFEATED! --***");
                player.NumberOfVictories++;
            }

            Console.WriteLine("You have won " + player.NumberOfVictories + " time(s)!");



        }







    }
}