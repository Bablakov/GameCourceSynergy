using GameCourse;
using GameCourse.Architecture;
using System;
using System.Drawing;
using System.Reflection;

namespace GameCourse
{
    public sealed class Level
    {
        public readonly int ID;
        public Position? PlayerSpawn;

        private char[,] _map;
        private List<Enemy> _enemies = new List<Enemy>();
        private Boss _boss;

        public Boss BossLevel => _boss;

        public char[,] Map => _map;
        public List<Enemy> Enemies => _enemies;
        public bool Finished => _boss.Health <= 0;

        // Конструктор нашего класса
        public Level()
        {
            // Получаем нынешний уровень(его ID)
            ID = World.Levels.Count;
            _boss = new Boss();
            _map = MapReader.GetMaps();
            CreateEnemy();
            GenerateMap();
        }

        // Получения карты и дальнейшее расположение нужных объектов на ней
        public void GenerateMap()
        {
            List<Position> emptyCell = new List<Position>();
            char cell;

            for (int y = 0; y < _map.GetLength(0); y++)
            {
                for (int x = 0; x < _map.GetLength(1); x++)
                {
                    cell = _map[y, x];
                    if (cell == '@')
                    {
                        PlayerSpawn = new Position(x, y);
                        _map[y, x] = 'P';
                    }
                    else if (cell == ' ')
                    {
                        emptyCell.Add(new Position(x, y));
                    }
                }
            }

            for (int i = 0; i < _enemies.Count; i++)
            {
                ReplaceCell(emptyCell, MapPoint.Enemy);
            }

            ReplaceCell(emptyCell, MapPoint.Shop);

            for (int i = 0; i < new Random().Next(0, 10); i++)
            {
                ReplaceCell(emptyCell, MapPoint.Gold);
            }
        }

        // Замена пустых ячеек на нужные нам
        private void ReplaceCell(List<Position> list, MapPoint point)
        {
            char cell = GetMapPoint(point);
            int index = new Random().Next(0, list.Count);
            Position pos = list[index];
            if (_map[pos.Y, pos.X] == ' ')
                _map[pos.Y, pos.X] = cell;
            else
                ReplaceCell(list, point);
        }

        // Возвращение по MapPoint символа
        private static char GetMapPoint(MapPoint mapPoint) => mapPoint switch
        {
            MapPoint.None => ' ',
            MapPoint.Wall => '#',
            MapPoint.Player => 'P',
            MapPoint.Enemy => 'E',
            MapPoint.Boss => 'B',
            MapPoint.Gold => 'G',
            MapPoint.Shop => 'S',
            MapPoint.Door => 'D',
            _ => ' '
        };

        // Замена ячейки игрока
        public void ReplaceCellPlayer(Position pos)
        {
            Player player = Game.TakePlayer;
            _map[player.PositionPlayer.Y, player.PositionPlayer.X] = ' ';
            _map[pos.Y, pos.X] = 'P';
        }

        // Реализация сомого перемещения игрока
        private void MoveElement(Position pos)
        {
            Player player = Game.TakePlayer;
            ReplaceCellPlayer(pos);
            player.NewPosition(pos);
            GameDraw.DrawLevel(this);
        }

        // Метод возвращающий клетку в которую мы хотим переместиться и перемещение игрока
        public MapPoint Movement(Position newPosition)
        {
            char cell = _map[newPosition.Y, newPosition.X];
            if (cell == '#')
                return MapPoint.Wall;
            else if (cell == ' ')
            {
                MoveElement(newPosition);
                return MapPoint.None;
            }
            else if (cell == 'E')
            {
                MoveElement(newPosition);
                return MapPoint.Enemy;
            }
            else if (cell == 'G')
            {
                MoveElement(newPosition);
                return MapPoint.Gold;
            }
            else if (cell == 'S')
            {
                MoveElement(newPosition);
                return MapPoint.Shop;
            }
            else if (cell == 'B')
            {
                MoveElement(newPosition);
                return MapPoint.Boss;
            }
            else if (cell == 'D' && Finished)
            {
                MoveElement(newPosition);
                return MapPoint.Door;
            }
            return MapPoint.None;
        }

        // Метод для обозначения окончания уровня
        public void LvlFinish()
        {
            if (!Finished)
                return;

            Console.WriteLine("Уровень пройден");
            World.CurrentLevel += 1;
            Thread.Sleep(2000);
        }

        // Генерация врагов
        public void CreateEnemy()
        {
            for (int i = 0; i < new Random().Next(3, 5); i++)
            {
                switch (new Random().Next(0, 3))
                {
                    case 0:
                        _enemies.Add(new Dwarf(1));
                        break;
                    case 1:
                        _enemies.Add(new Elf(1));
                        break;
                    case 2:
                        _enemies.Add(new Orc(1));
                        break;
                }
            }
        }
    }

    public enum MapPoint
    {
        None,
        Wall, 
        Player,
        Enemy,
        Boss,
        Gold,
        Shop,
        Door
    }
}
