using GameCourse.Architecture;
using System;
using System.Collections.Generic;

namespace GameCourse
{
    class Program
    {
        static void Main()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            World.GenerateLevel();

            Game.GameProcess();
        }
    }
}