namespace GameCourse
{
    public class ItemClothes : Item
    {
        public int Shield;
        public int ShieldQuality;
        public int Health;

        public ItemClothes()
        {
            new ItemClothes("Clothes", 0, ItemSize.NoSize, TypePlayer.NoType, 0, 0);
        }

        public ItemClothes(string name, int cost, ItemSize size, TypePlayer typePlayer, int shield, int shieldQuality) : base(name, cost, size, typePlayer)
        {
            Shield = shield;
            ShieldQuality = shieldQuality;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($" | Shield: {Shield} | ShieldQuality: {ShieldQuality}");
        }
    }
}
