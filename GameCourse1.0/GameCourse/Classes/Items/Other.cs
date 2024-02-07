using static System.Net.Mime.MediaTypeNames;

namespace GameCourse
{
    public class ItemOther : Item
    {
        public int Damage;
        public int Shield;
        public int ShieldQuality;

        public ItemOther()
        {
            new ItemOther("Other", 0, ItemSize.NoSize, TypePlayer.NoType, 0, 0, 0);
        }

        public ItemOther(string name, int cost, ItemSize size, TypePlayer playerType, int damage, int shield, int shieldQuality) : base(name, cost, size, playerType)
        {
            Damage = damage;
            Shield = shield;
            ShieldQuality = shieldQuality;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($" | Damage: {Damage} | Shield: {Shield} | ShieldQuality: {ShieldQuality}");
        }
    }
}
