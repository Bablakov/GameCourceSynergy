namespace GameCourse
{
    public class Elf : Enemy
    {
        public Elf(int levelPlayer) : base(levelPlayer)
        {
            base._stats.Damage = (int)(base._stats.Damage * 1.5f);
            base._stats.Shield = 30;
            base._stats.Name = "Elf";
        }
    }
}
