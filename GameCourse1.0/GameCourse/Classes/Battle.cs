using GameCourse;
using System.Collections.Generic;

namespace GameCourse
{
    public class Battle
    {
        // Сам процесс битвы
        public static bool Fight(Enemy fighter)
        {
            Console.Clear();
            var player = Game.TakePlayer;
            bool status = false;

            int pDmg;
            int fDmg;
            bool got = true;
            int crit = 0;

            while (true)
            {
                pDmg = player.Damage;
                fDmg = fighter.Damage;
                // Удары игрока
                if (player.Health > 0)
                {
                    if (PlayerChoose())
                    {
                        PointImpactPlayer(out got, out crit);
                        pDmg = player.CalculateSpellDmg();
                        fighter.TakeDmg(player.Name, pDmg, crit, got);
                    }
                    else if (new Random().NextSingle() > fighter.Dexterity)
                    {
                        pDmg *= SkillCheck();
                        PointImpactPlayer(out got, out crit);
                        fighter.TakeDmg(player.Name, pDmg, crit, got);
                    }
                    else
                        fighter.TakeDmg(fighter.Name, 0, 0, false);
                }
                else
                {
                    Console.WriteLine("Игрок проиграл");
                    Thread.Sleep(1500);
                    break;
                }
                // Удары врага
                if (fighter.Health > 0)
                {
                    var rnd = new Random().Next(1, 3);
                    if (rnd == 1)
                    {
                        PointImpactEnemy(out got, out crit);   
                        fDmg = fighter.CalculateSpellDmg();
                        player.TakeDmg(fighter.Name, fDmg, crit, got);
                    }
                    else if (new Random().NextSingle() > player.Dexterity)
                    {
                        PointImpactEnemy(out got, out crit);
                        player.TakeDmg(fighter.Name, fDmg, crit, got);
                    }
                    else
                        player.TakeDmg(player.Name, 0, 0, false);
                }
                else
                {
                    Console.WriteLine("Противник проиграл");
                    Thread.Sleep(1500);
                    status = true;
                    break;
                }
                // Выведение статов если игрок и враг живы
                if (fighter.Health > 0 && player.Health > 0)
                {
                    Console.WriteLine($"У игрока {player.Health} здоровья, у противника {fighter.Health} здоровья");
                    Thread.Sleep(1500);
                }
            }
            return status;
        }

        // Увеличение урона если игрок успеет нажать клавишу
        private static int SkillCheck()
        {
            int delay = 0;
            int delayTime = 2000;
            ConsoleKey key = (ConsoleKey)new Random().Next(65, 90);
            Console.WriteLine($"Нажмите кнопку для сильной атаки {key}");

            do
            {
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey().Key == key)
                        return 2;
                    else
                        return 1;
                }

                Thread.Sleep(50);
                delay += 50;
            }
            while (delay < delayTime);

            return 1;
        }

        // Выбор способа удара
        private static bool PlayerChoose()
        {
            while (true)
            {
                Console.WriteLine("Нажми 1 чтобы ударить с руки | Нажми 2 чтобы использовать заклинание");
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.D1)
                    return false;
                else if (key == ConsoleKey.D2)
                    return true;

                Console.Clear();
                Console.WriteLine("\nВы нажали неверную кнопку");
            }
        }

        // Выбор места удара
        public static void PointImpactPlayer(out bool mistake, out int criticalChance)
        {
            Random rnd = new Random();
            string? select;
            criticalChance = 0;
            while (true)
            {
                Console.WriteLine("В какое место вы хотите ударить(body, head, hand, leg)");
                select = Console.ReadLine();
                if (select == "body")
                {
                    mistake = (rnd.Next(0, 10) < 9);
                    criticalChance = 20;
                    break;
                }
                else if (select == "head")
                {
                    mistake = (rnd.Next(0, 5) < 5);
                    criticalChance = 60;
                    break;
                }
                else if (select == "hand")
                {
                    mistake = (rnd.Next(0, 20) < 20);
                    criticalChance = 5;
                    break;
                }
                else if (select == "leg")
                {
                    mistake = true;
                    break;
                }
                else
                    Console.WriteLine("\nВы не правильно ввели строку");
            }
        }
        
        // Случайный выбор места удара у противника
        public static void PointImpactEnemy(out bool mistake, out int criticalChance)
        {
            Random rnd = new Random();
            int select = rnd.Next(0,3);
            criticalChance = 0;
            while (true)
            {
                if (select == 0)
                {
                    mistake = (rnd.Next(0, 10) < 9);
                    criticalChance = 20;
                    Console.WriteLine("Противник бъёт в тело");
                    break;
                }
                else if (select == 1)
                {
                    mistake = (rnd.Next(0, 5) < 5);
                    criticalChance = 60;
                    Console.WriteLine("Противник бъёт в голову");
                    break;
                }
                else if (select == 2)
                {
                    mistake = (rnd.Next(0, 20) < 20);
                    Console.WriteLine("Противник бъёт в руку");
                    criticalChance = 0;
                    break;
                }
                else if (select == 3)
                {
                    mistake = true;
                    Console.WriteLine("Противник бъёт в ногу");
                    break;
                }
            }
        }
    }
}
