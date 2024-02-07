namespace GameCourse
{
    public class ItemWeapon : Item
    {
        public readonly int Damage;

        public ItemWeapon()
        {
            new ItemWeapon("Weapon", 0, ItemSize.NoSize, TypePlayer.NoType, 0);
        }

        public ItemWeapon(string name, int cost, ItemSize size, TypePlayer playerType, int damage) : base(name, cost, size, playerType)
        {
            Damage = damage;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($" | Damage: {Damage} ");
        }
    }
}
