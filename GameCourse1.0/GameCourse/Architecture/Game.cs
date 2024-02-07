using GameCourse;
using System.Reflection.Emit;

namespace GameCourse
{
    public static class Game
    {
        private static Player _player;

        public static Player TakePlayer => _player;
        public static bool FinishedGame = false;
        static Game()
        {
            Init();
        }

        // Реализация гемплея
        public static void GameProcess()
        {
            Console.WriteLine("Игра началась");
            Thread.Sleep(1000);
            while (!FinishedGame)
            {
                bool levelFinished = false;
                Level level = World.TakeLevel;
                MapPoint mapPoint = MapPoint.None;
                _player.PositionPlayer = level.PlayerSpawn != null ? level.PlayerSpawn : new Position(1, 1);
                GameDraw.DrawLevel(level);
                while (_player.IsAlive && !levelFinished)
                {
                    mapPoint = GetCell(level);

                    switch (mapPoint)
                    {
                        case MapPoint.Gold:
                            int gold = new Random().Next(5, 15);
                            Console.WriteLine($"Вы получили {gold} золота");
                            _player.Gold += gold;
                            break;
                        case MapPoint.Shop:
                            Shop.OpenShop();
                            GameDraw.DrawLevel(level);
                            break;
                        case MapPoint.Enemy:
                            Battle.Fight(level.Enemies[new Random().Next(0, level.Enemies.Count)]);
                            GameDraw.DrawLevel(level);
                            break;
                        case MapPoint.Boss:
                            Battle.Fight(level.BossLevel);
                            GameDraw.DrawLevel(level);
                            break;
                        case MapPoint.Door:
                            levelFinished = true;
                            break;
                    }
                }
                if (!_player.IsAlive || World.CurrentLevel == World.Levels.Count)
                    break;
                level.LvlFinish();
            }
        }
        
        // Передвижение и получения ячейки для дальнейшего взаимодействия
        private static MapPoint GetCell(Level level)
        {
            ConsoleKeyInfo inputKey = Console.ReadKey();
            switch (inputKey.Key)
            {
                case ConsoleKey.W:
                    return level.Movement(new Position(_player.PositionPlayer.X, _player.PositionPlayer.Y - 1));
                case ConsoleKey.S:
                    return level.Movement(new Position(_player.PositionPlayer.X, _player.PositionPlayer.Y + 1));
                case ConsoleKey.A:
                    return level.Movement(new Position(_player.PositionPlayer.X - 1, _player.PositionPlayer.Y));
                case ConsoleKey.D:
                    return level.Movement(new Position(_player.PositionPlayer.X + 1, _player.PositionPlayer.Y));
                default:
                    return MapPoint.Wall;
            }
        }

        // Создание персонажа
        public static void Init(int gold = 0)
        {
            string? name;
            int select;

            Console.WriteLine("Давайте создадим персонажа");

            do
            {
                Console.WriteLine("Введите имя персонажа: ");
                name = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(name)) ;

            do
            {
                Console.Write($"Выберите класс персонажа (0-маг, 1-мечник, 2-лучник): ");
                select = GetOption();
                Console.WriteLine();
            }
            while (select > 2 || select < 0) ;

            _player = new Player(name, (TypePlayer)select, 0);
            _player.PrintStats();
        }

        // Получения число от 0 до 9 
        public static int GetOption()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
            }
            while ((int)key.Key < 47 && (int)key.Key > 58);
            return Convert.ToInt32(key.KeyChar.ToString());
        }

        /*public static void PrintMenu()
        {
            Console.Clear();
            _player.PrintStats();

            int select;
            do
            {
                Console.WriteLine("1-Магазин, 2-Инвентарь, 3-Выход");
                select = GetOption();
                if (select == 1)
                    Shop.OpenShop();
                else if (select == 2)
                    _player.Equpment.Print();
            }
            while (select != 3);
        }*/

        /*public static int Gameplay(Level level)
        {
            var random = new Random();
            int select;
            while (enemies.Count > 0)
            {
                select = SelectEnemy(enemies);
                if (Battle.Fight(enemies[select]))
                {
                    Console.WriteLine("Вы победили!");
                    _player.Gold += random.Next(3, 15);
                    _player.PrintStats();
                    enemies.Remove(enemies[select]);
                    continue;
                }
                else
                {
                    Console.WriteLine("Вы проиграли! Хотите начать занаво?(1-да, 2-нет): ");
                    select = GetOption();
                    Console.WriteLine();
                    if (select == 1)
                        return 1;
                    else
                    {
                        return 2;
                    }
                }
            }
            return 0;
        }*/

        /*public static List<Enemy> CreateEnemy(int level)
        {
            List<Enemy> enemies = new List<Enemy>();

            for (int i = 0; i < new Random().Next(3, 5); i++)
            {
                switch(new Random().Next(0,2))
                {
                    case 0:
                        enemies.Add(new Dwarf(level));
                        break;
                    case 1:
                        enemies.Add(new Elf(level));
                        break;
                    case 2:
                        enemies.Add(new Orc(level));
                        break;
                }

            }

            return enemies;
        }*/

        /*public static int SelectEnemy(List<Enemy> enemies)
        {
            int element;
            
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].PrintStats(i);

            do
            {
                Console.WriteLine("Выбирите врага из списка предложенных: ");
                element = GetOption();
            } 
            while (element < -1 && element > enemies.Count);
            
            return element;
        }*/

    }
}
