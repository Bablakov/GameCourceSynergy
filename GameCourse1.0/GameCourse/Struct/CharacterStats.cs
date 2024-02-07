using System.Reflection.PortableExecutable;

namespace GameCourse
{
    public struct CharacterStats
    {
        private const int _maxSQuality = 80;
        private float _dexterity;

        public string Name;
        public int Health;
        public int Shield;
        public int ShieldQuality;
        public int Damage;
        public int CriticalChance;
  
        public float Dexterity
        {
            get => _dexterity;
            set
            {
                if (value > MaxDexterity)
                    _dexterity = MaxDexterity;
                else
                    _dexterity = value;
            }
        }

        public const float MaxDexterity = 0.8f;
        public readonly float MinDexterity = 0.1f;

        public CharacterStats(string name, int health, int damage, 
                             int shield, int shieldQuality = 20, float dexterity = 0.1f)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Shield = shield;

            if (shieldQuality < _maxSQuality)
                ShieldQuality = shieldQuality;
            else
                ShieldQuality = _maxSQuality;

            if (dexterity > MaxDexterity)
                _dexterity = MaxDexterity;
            else
                _dexterity = dexterity;
        }
    }
}
