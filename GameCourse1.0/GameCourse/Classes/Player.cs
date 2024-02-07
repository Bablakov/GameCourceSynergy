using GameCourse;

namespace GameCourse
{
    public class Player : Unit
    {
        private int _gold;

        public TypePlayer Type;
        public int Level = 1;
        public Position PositionPlayer = new(1,1);
        private float[] PercentUpdate = new float[2] { 1, 1 };

        public bool IsAlive => Health > 0;
        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }
        
        public Player(string name, TypePlayer type, int gold = 0)
        {
            switch (type)
            {
                case TypePlayer.Magicion:
                    _stats = new CharacterStats(name, 100, 100, 200);
                    Type = TypePlayer.Magicion;
                    break;
                case TypePlayer.Swordsman:
                    _stats = new CharacterStats(name, 70, 20, 15, 35);
                    PercentUpdate[0] = 1.2f;
                    PercentUpdate[1] = 0.8f;
                    Type = TypePlayer.Swordsman;
                    break;
                case TypePlayer.Archer:
                    _stats = new CharacterStats(name, 20, 25, 10);
                    PercentUpdate[0] = 0.8f;
                    PercentUpdate[1] = 1.2f;
                    Type = TypePlayer.Archer;
                    break;
            }
            _gold = gold;
            Type = type;
            _spells.Add(new Spell(50, 3));
            _spells.Add(new Spell(100, 5));
            _spells.Add(new Spell(35, 1));
        }

        public Player(string name, int health, int damage)
        {
            _stats = new CharacterStats(name, health, damage, 20);
            _gold = 0;
        }

        public Player(string name, int health, int damage, int gold) : this(name, health, damage)
        {
            _gold = gold;
        }

        // Увеличение уровня
        public void Update()
        {
            Level += 1;
            base._stats.Health += (int)(10 * PercentUpdate[0]);
            base._stats.Damage += (int)(5 * PercentUpdate[1]);
        }

        // Вывод статистики по игроку
        public override void PrintStats()
        {
            Console.WriteLine($"{Name}: урон-{Damage}, здоровье-{Health}, броня-{Shield}," +
                $" качество брони-{ShQuality}, золота-{Gold}");
        }

        // Получения новой позиции
        public void NewPosition(Position pos)
        {
            PositionPlayer = pos;
        }
    }

    public enum TypePlayer
    {
        Magicion,
        Swordsman,
        Archer,
        NoType
    }
}
