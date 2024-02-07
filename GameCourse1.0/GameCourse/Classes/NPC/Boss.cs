namespace GameCourse
{
    public class Boss : Enemy
    {
        public Boss() : base(1)
        {
            base._stats.Health = 10;
            base._stats.ShieldQuality = 40;
            base._stats.Shield = 100;
            base._stats.Name = "Boss";
        }
    }
}
