using GameCourse.Architecture;

namespace GameCourse
{
    public static class World
    {
        private static List<Level> _levels = new();
        public static List<Level> Levels => _levels;

        private static int _currentLevel = 0;
        public static int CurrentLevel
        {
            get { return _currentLevel; }
            set
            {
                if (value == _levels.Count)
                {
                    Console.WriteLine("Вы прошли игру");
                    Game.FinishedGame = true;
                    return;
                }
                else if (_levels[value].Finished)
                    return;
                else
                    _currentLevel = value;
            }
        }
        public static bool GameOver => _currentLevel == Levels.Count;
        public static Level TakeLevel => _levels[CurrentLevel];

        // Генерация уровней и добавления их в наш класс
        public static void GenerateLevel()
        {
            for(int i = 0; i < MapReader.CountMap; i++)
            {
                _levels.Add(new Level());
            }
        }
    }
}
