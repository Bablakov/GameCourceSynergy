using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace GameCourse.Architecture
{
    public static class MapReader
    {
        private static int _sizeX = 22;
        private static int _sizeY = 7;

        private static int _currentX = 0;
        private static int _currentY = 0;

        private static int _currentArrayY = 0;

        /*private static int _maxSizeY = 23;*/
        private static int _maxSizeX = 22;

        private static char[][,] _maps = new char[3][,];
        private static int _currentMap = 0;
        private static readonly int _countMap = 3;
        
        public static int CountMap => _countMap;

        static MapReader()
        {
            Init();
        }

        // Считываем карты из файла и добавляем в массив
        private static void Init()
        {
            var fileMap = File.ReadAllLines("maps.txt");
            for (int i = 0; i < _countMap; i++)
            {
                _maps[i] = new char[_sizeY, _sizeX];
                for (int y = _currentArrayY; y < _sizeY; y++, _currentY++)
                {
                    for (int x = _currentX; x < _maxSizeX; x++)
                    {
                        _maps[i][y, x] = fileMap[_currentY][x];
                    }
                }
                _currentY += 1;
            }
            
        }

        // Считывание карты из вне из массива
        public static char[,] GetMaps()
        {
            char[,] map = _maps[_currentMap];
            if (_currentMap == _countMap)
            {
                Array.Copy(_maps[_currentMap], map, _maps[_currentMap].Length);
                return map;
            }
            else
            {
                Array.Copy(_maps[_currentMap], map, _maps[_currentMap].Length);
                _currentMap++;
                return map;
            }
        }
    }
}
