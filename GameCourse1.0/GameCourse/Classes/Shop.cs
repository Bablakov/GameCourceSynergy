using GameCourse;

namespace GameCourse
{
    public static class Shop
    {
        private static Player _player = Game.TakePlayer;
        private static List<Item> _weapon = new List<Item>();
        private static List<Item> _clothes = new List<Item>();
        private static List<Item> _other = new List<Item>();

        static Shop()
        {
            Init();
        }

        private static void Init()
        {
            GenerateItems();
        }

        // Случайная генерация экипировки всех типов
        private static void GenerateItems()
        {
            _weapon.Clear();
            _clothes.Clear();
            _other.Clear();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _weapon.Add(GenerateItem(ItemType.Weapon, j));
                    _clothes.Add(GenerateItem(ItemType.Clothes, j));
                    _other.Add(GenerateItem(ItemType.Other, j));
                }
            }
        }

        // Случайная генерация экипировки определённого типа
        public static Item GenerateItem(ItemType type, int place)
        {
            Random rnd = new Random();
            ItemSize size = (ItemSize)rnd.Next(0, 2);
            TypePlayer playerType = (TypePlayer)rnd.Next(0, 2);
            int cost = rnd.Next(10, 50);
            string name;
            switch(type)
            {
                case ItemType.Weapon:
                    name = "Weapon" + place.ToString();
                    int dmg = rnd.Next(5, 30);
                    return (Item)(new ItemWeapon(name, cost, size, playerType, dmg));
                case ItemType.Clothes:
                    name = "Clothes" + place.ToString();
                    int shield = rnd.Next(5, 40);
                    int shieldQuality = rnd.Next(10, 60);
                    return (Item)(new ItemClothes(name, cost, size, playerType, shield, shieldQuality));
                case ItemType.Other:
                    name = "Other" + place.ToString();
                    int sh = rnd.Next(5, 40);
                    int shQuality = rnd.Next(10, 60);
                    int damage = rnd.Next(5, 30);
                    return (Item)(new ItemOther(name, cost, size, playerType, damage, sh, shQuality));
            }
            return new Item();
        }

        // Открытия магазина
        public static void OpenShop()
        {
            Menu();
        }

        // Выбор тип покупаемой экипировки 
        private static void Menu()
        {
            Console.WriteLine("Что вы хотите приобрести(weapon, clothes, other, exit): ");
            string? select;
            List<Item> list;
            while (true)
            {
                select = Console.ReadLine();
                if (select == "weapon")
                {
                    list = _weapon;
                    break;
                }
                else if (select == "clothes")
                {
                    list = _clothes;
                    break;
                }
                else if (select == "other")
                {
                    list = _other;
                    break;
                }
                else if (select == "exit")
                    return;
                else
                    Console.WriteLine("Вы не правильно ввели строку");
            }
            DisplayItems(list);
            ShopInteract(list);
        }

        // Получения числа от пользователя и непосредственно покупка экипировки
        private static void ShopInteract(List<Item> list)
        {
            Console.WriteLine("Для выхода нажмите Z или введите ID предмета для покупки");

            var key = Console.ReadKey();

            if ((int)key.Key > 47 && (int)key.Key < 58)
            {
                string test = key.KeyChar.ToString();
                BuyItem(int.Parse(test), list);
            }
            else if (key.Key == ConsoleKey.Z)
            {
                Console.Clear();
                return;
            }
            else
            {
                Menu();
            }
        }

        // Отображение имеющегося товара
        private static void DisplayItems(List<Item> list)
        {
            Console.Clear();
            Console.WriteLine("Вот список товара:");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write($"{i} - ");
                list[i].Display();
            }
        }

        // Процесс покупки и получение экипировки
        private static void BuyItem(int id, List<Item> list)
        {
            Item item = list[id];
            var player = Game.TakePlayer;

            if (item.Cost > player.Gold)
            {
                Console.WriteLine("Не хватило денег");
                Menu();
            }
            else if (item.PlayerType != _player.Type)
            {
                Console.WriteLine("Оружие не на данный тип персонажа");
                Menu();
            }
            else
            {
                player.Gold -= item.Cost;
                player.AddItem(item);
                list.RemoveAt(id);
                Console.WriteLine("Успешная покупка");
                Thread.Sleep(1500);
                Menu();
            }
        }

        // Случайная генерация экипировки для врага
        public static Item EnemyGenerateQupment()
        {
            switch(new Random().Next(0, 2))
            {
                case 0:
                    return (Item)_weapon[new Random().Next(0, _weapon.Count - 1)];
                case 1:
                    return (Item)_clothes[new Random().Next(0, _clothes.Count - 1)];
                case 2:
                    return (Item)_other[new Random().Next(0, _other.Count - 1)];
            }
            return (Item)_weapon[0];
        }

        /*public static void Shopping()
        {
            Player player = Game.Player;
            Console.WriteLine("Добро пожаловать в магазин! Выбирите себе товар");
            for (int i = 0; i < _shopList.Count; i++)
                _shopList[i].PrintStats(i);
            Console.WriteLine($"{_shopList.Count} - выход из магазина");

            while (true)
            {
                int select = Convert.ToInt32(Console.ReadLine());
                
                if (select == _shopList.Count)
                    break;
                
                else if (select < _shopList.Count && select > -1)
                {
                    if (_shopList[select].TypeEquipment.Contains(player.Type))
                    {
                        if (player.Gold >= _shopList[select].Cost)
                        {
                            player.TakeInventory(_shopList[select]);
                            break;
                        }
                        else
                            Console.WriteLine("У вас не хватает денег!");
                    }
                    else
                        Console.WriteLine("Это оружие не поддерживает тип персонажа");
                }
            }
            Console.WriteLine("Спасибо за покупки!");
        }*/

        /*public static void IssueGunRandom(Enemy enemy)
        {
            GenerateEquipment();
            var random = new Random();
            if (random.Next(0, 10) == 0)
                enemy.TakeInventory(_shopList[random.Next(0, _shopList.Count)]);
            if (random.Next(0, 5) == 0)
                enemy.TakeInventory(_shopList[random.Next(0, _shopList.Count)]);
            if (random.Next(0, 8) == 0)
                enemy.TakeInventory(_shopList[random.Next(0, _shopList.Count)]);
        }

        public static void GenerateEquipment()
        {
            for (int i = 0; i < new Random().Next(1, 10); i++)
            {
                _shopList.Add(CreateEquipment(ItemType.Weapon, i));
            }
            for (int i = 0; i < new Random().Next(1, 10); i++)
            {
                _shopList.Add(CreateEquipment(ItemType.Armor, i));
            }
            for (int i = 0; i < new Random().Next(1,10); i++)
            {
                _shopList.Add(CreateEquipment(ItemType.Accessory, i));
            }
        }

        public static Item CreateEquipment(ItemType itemType, int place)
        {
            Random rnd = new Random();
            
            string name = itemType.ToString() + place.ToString();
            ItemSize itemSize = (ItemSize)rnd.Next(0,2);
            float damage = 0;
            float protectQuality = rnd.Next(105, 150);
            float protect = rnd.Next(10, 100);
            int cost = rnd.Next(10, 50);
            TypePlayer typePlayer;
            List<TypePlayer> typeEquipment = new List<TypePlayer>();

            if (itemType == ItemType.Weapon)
                damage = rnd.Next(10, 30);

            for (int i = 0; i < rnd.Next(1,3); i++)
            {
                typePlayer = (TypePlayer)rnd.Next(0, 3);
                if (!typeEquipment.Contains(typePlayer))
                    typeEquipment.Add(typePlayer);
            }

            return new Item(name, itemType, itemSize, damage, protectQuality, protect, cost, typeEquipment);
        }*/
    }
}
