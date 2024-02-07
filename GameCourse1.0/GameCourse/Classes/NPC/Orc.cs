namespace GameCourse
{
    public class Orc : Enemy
    {
        public Orc(int levelPlayer) : base(levelPlayer)
        {
            base._stats.Health = (int)(base._stats.Health * 1.5f);
            base._stats.Damage = (int)(base._stats.Damage * 0.7f);
            base._stats.Name = "Orc";
        }
    }
}
