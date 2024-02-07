using GameCourse;

namespace GameCourse
{
    public class Item
    {
        public readonly string Name;
        public readonly int Cost;
        public readonly ItemSize Size;
        public readonly TypePlayer PlayerType;

        public Item()
        {
            Name = "Noname";
            Cost = 0;
            Size = ItemSize.NoSize;
        }
        public Item(string name, int cost, ItemSize size, TypePlayer playerType)
        {
            Name = name;
            Cost = cost;
            Size = size;
            PlayerType = playerType;
        }

        public virtual void Display()
        {
            Console.Write($"{Name} | Cost: {Cost} | ItemSize: {Size} | PlayerType {PlayerType} ");
        }
    }
    public enum ItemType
    {
        Weapon,
        Clothes,
        Other,
        NoType
    }

    public enum ItemSize
    {
        S,
        M,
        L,
        NoSize
    }
}
