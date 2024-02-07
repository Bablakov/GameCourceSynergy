using System.Net.WebSockets;
using System.Numerics;
using System.Security.Authentication;
using GameCourse;

namespace GameCourse
{
    public struct Equpment
    {
        public ItemWeapon Weapon;
        public ItemClothes Clothes;
        public ItemOther Other;

        public Equpment()
        {
            Weapon = new ItemWeapon();
            Clothes = new ItemClothes();
            Other = new ItemOther();
        }

        // Вывод информации о экипировки
        public void Print()
        {
            Console.WriteLine();
            PrintItem(Weapon);
            PrintItem(Clothes);
            PrintItem(Other);
            Console.WriteLine();
        }

        // Вывод информации о определённой элемента экипировки 
        private void PrintItem(Item item)
        {
            if (item.Size != ItemSize.NoSize)
                Console.Write($" | Weapon - {item.Name} | ");
            else if (item.Size != ItemSize.NoSize)
                Console.Write($" | Clothes - {item.Name} | ");
            else if (item.Size != ItemSize.NoSize)
                Console.Write($" | Other - {item.Name} | ");
            else
                Console.Write($" | NoItem | ");
        }
    }
}
