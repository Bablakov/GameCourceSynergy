namespace GameCourse
{
    public class Dwarf : Enemy
    {
        public Dwarf(int levelPlayer) : base(levelPlayer)
        {
            base._stats.Health = (int)(base._stats.Health * 0.7f);
            base._stats.ShieldQuality = 40;
            base._stats.Shield = (int)(base._stats.Shield * 1.3f);
            base._stats.Name = "Dwarf";
        }
    }
}
