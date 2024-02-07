namespace GameCourse
{
    public static class GameDraw
    {
        // Отрисовка интерфейса и карты
        public static void DrawLevel(Level level)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            DrawUpUI();
            DrawMap(level);
            DrawBottomUI();
        }

        // Отрисовка верхней части интерфейса
        private static void DrawUpUI()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Вы находитесь на уровне {World.CurrentLevel + 1} из {World.Levels.Count}");
        }

        // Отрисовка нижней части интерфейса
        private static void DrawBottomUI()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            var player = Game.TakePlayer;
            Console.WriteLine($"{player.Name} - HP: {player.Health} | Gold: {player.Gold} | {player.Damage}");
            Console.WriteLine("WASD - передвижение");
            Console.WriteLine("E - Враги | B - Босс | S - Магазин | G - Золото | D - Проход на следующий уровень");
        }

        // Отрисовка карты
        private static void DrawMap(Level level)
        {
            char[,] map = level.Map;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    char cell = map[y, x];
                    if (cell == '#')
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else if (cell == 'G')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (cell == 'E')
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (cell == 'B')
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    else if (cell == 'S')
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    else if (cell == 'D')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(cell);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
