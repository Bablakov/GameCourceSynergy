using GameCourse;

namespace GameCourse
{
    public interface IBattler
    {
        public CharacterStats Stats { get; }
        public string Name => Stats.Name;
        public int Damage => Stats.Damage;
        public int Health => Stats.Health;
        public int Shield => Stats.Shield;
        public float Dexterity => Stats.Dexterity;
        public int CriticalChance => Stats.CriticalChance;

        public void TakeDmg(string name, int dmg, int addCriticalChance = 0,  bool got = true, bool success = true);
    }
}
