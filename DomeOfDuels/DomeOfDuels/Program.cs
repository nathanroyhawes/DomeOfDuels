using System;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Reflection.Emit;

namespace DomeOfDuels
{
    internal class Program
    {

        static void Main(string[] args)

        {
            Player player = new Player(20, 20, 6, 6);
            Item item;
           
            Hashtable itemTable = new Hashtable();

            Item item1 = new Item(1, "SWORD", +10, 20);
            Item item2 = new Item(2, "FISTS", 5, 55);
            Item item3 = new Item(3, "BOW & ARROW", 20, 35);

            itemTable.Add(item1.Id, item1);
            itemTable.Add(item2.Id, item2);
            itemTable.Add(item3.Id, item3);

            Item storedItem1 = (Item)itemTable[item1.Id];

 
            //offer weapon option and assign value to player attack
            Console.WriteLine("CHOOSE YOUR WEAPON: ");
            Console.WriteLine("_______________________________");
            Console.WriteLine("");
            foreach (Item value in itemTable.Values)
            {
                Console.WriteLine("Item Name:{0}", value.Name);
                Console.WriteLine("Attack Modifier:{0}", value.AttackMod);
                Console.WriteLine("Value in Gold:{0}", value.Gold);
                Console.WriteLine("_______________________________");
            }
            int weaponchoice = int.Parse(Console.ReadLine());

            if (weaponchoice == 1)
            {
                Player player1 = new Player(20, 20, 6, 6);

                Console.WriteLine("You have chosen the SWORD!");

            }
            else if (weaponchoice == 2)
            {
                Player player1 = new Player(20, 20, 6, 6);
                player1.PlayerAttack += item2.AttackMod;
                Console.WriteLine("You have chosen your FISTS!");
            }
            else if (weaponchoice == 3) 
            {
                Player player1 = new Player(20, 20, 6, 6);
                player1.PlayerAttack += item3.AttackMod;
                Console.WriteLine("You have chosen the Bow & Arrow!");
            }

            // Create table of enemies
            Hashtable enemyTable = new Hashtable();

            Enemy enemy1 = new Enemy(0, "Carl the Killer", 5, 15, 15, 4);
            Enemy enemy2 = new Enemy(1, "Louise the Loser", 7, 20, 20, 5);
            Enemy enemy3 = new Enemy(2, "Veronica the Victorious", 10, 30, 30, 7);

            enemyTable.Add(enemy1.EnemyId, enemy1);
            enemyTable.Add(enemy2.EnemyId, enemy2);
            enemyTable.Add(enemy3.EnemyId, enemy3);

            Enemy storedEnemy1 = (Enemy)enemyTable[enemy1.EnemyId];
            StartCombat(enemy1, player, item1);

        }
        static void StartCombat(Enemy enemy, Player player, Item item)
        {
            
            Random random = new Random();
            //allow for specific object to be used in the combat senario

            while (player.PlayerCurrentHP > 0 && enemy.EnemyCurrentHp > 0)
            {
                Console.WriteLine("--- YOUR TURN ---");
                Console.WriteLine("Enter 'a' to Attack or 'h' to Heal");
                var userinput = Console.ReadLine();

                //add try again message for any invalid input
                if (userinput == null)
                {
                    Console.WriteLine("try again");
                }

                else if (userinput == "a")
                {
                    enemy.EnemyCurrentHp -= (player.PlayerAttack + item.AttackMod);
                    Console.WriteLine("YOU deal " + (player.PlayerAttack + item.AttackMod) + " damage!");
                    Console.WriteLine(enemy.EnemyName + "HP: " + enemy.EnemyCurrentHp);
                    Console.WriteLine("_____________________");
                }
                else if (userinput == "h")
                {
                    player.PlayerCurrentHP += player.HealAmt;

                    Console.WriteLine("You heal " + player.PlayerCurrentHP + " points!");
                    Console.WriteLine("YOUR HP: " + player.PlayerCurrentHP);
                    Console.WriteLine("_____________________");
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
                        Console.WriteLine("YOUR HP: " + player.PlayerCurrentHP);
                        Console.WriteLine("_____________________");
                    }

                    else
                    {
                        enemy.EnemyCurrentHp += enemy.HealAmt;
                        Console.WriteLine(enemy.EnemyName + "heals themself!");
                        Console.WriteLine(enemy.EnemyName + "HP: " + enemy.EnemyCurrentHp);
                        Console.WriteLine("_____________________");
                    }

                }


            }

            if (player.PlayerCurrentHP <= 0)
            {
                Console.WriteLine("*-_- YOU HAVE PERISHED! -_-*");
            }
            else if (enemy.EnemyCurrentHp <= 0)
            {

                Console.WriteLine("***-- FOE DEFEATED! --***");
            }


        }
    }
}