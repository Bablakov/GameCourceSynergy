using GameCourse;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;


namespace GameCourse
{
    public class Unit : IBattler
    {
        protected CharacterStats _stats;
        protected Equpment _equpment;
        protected List<Spell> _spells = new();

        public CharacterStats Stats => _stats;
        public Equpment Equpment => _equpment;
        public string Name => _stats.Name;
        public int Damage => _stats.Damage;
        public int Health => _stats.Health;
        public int Shield => _stats.Shield;
        public int ShQuality => _stats.ShieldQuality;
        public float Dexterity => _stats.Dexterity;
        public int CriticalChance => _stats.CriticalChance;

        public Unit()
        {
            _stats = new CharacterStats("Unit", 120, 5, 100);
            _equpment = new Equpment();
        }
        public Unit(string name)
        {
            _stats = new CharacterStats(name, 120, 5, 100);
            _equpment = new Equpment();
        }

        public Unit(CharacterStats stats)
        {
            _stats = stats;
            _equpment = new Equpment();
        }

        // Получения урона
        public void TakeDmg(string name, int dmg, int addCriticalChance = 0, bool got = true, bool success = true)
        {
            if(!got)
            {
                Console.WriteLine($"{name} промахнулся");
            }
            else if (success)
            {
                int upDmg = 1;
                if (CriticalChance + addCriticalChance > new Random().Next(0, 100))
                {
                    Console.WriteLine($"{name} ударил с критом");
                    upDmg *= 2;
                }
                int damage = (int)( upDmg * dmg * new Random().Next(80, 120) / 100);
                Console.WriteLine($"{name} нанёс {damage} урона {Name}");
                Thread.Sleep(1500);
                if (Shield > 0)
                {
                    _stats.Shield -= damage * ShQuality / 100;
                    damage -= damage * ShQuality / 100;
                    if(Shield < 0)
                    {
                        damage -= Shield;
                        _stats.Shield = 0;
                    }
                }
                _stats.Health -= damage;
                _stats.Dexterity += 0.1f;
            }
            else
            {
                Console.WriteLine($"{name} увернулся");
                Thread.Sleep(1500);
                _stats.Dexterity = _stats.MinDexterity;
            }
        }

        // Вывод статов
        public virtual void PrintStats()
        {
            Console.WriteLine($"{Name}: урон-{Damage}, здоровье-{_stats.Health}, брони-{Shield}");
        }

        // Подсчитывания урона магией
        public int CalculateSpellDmg()
        {
            int rnd = new Random().Next(0, _spells.Count);
            if (!_spells[rnd].Cooldown)
                return _spells[rnd].Use();
            else
            {
                foreach (Spell spell in _spells)
                {
                    if (!spell.Cooldown)
                        return spell.Use();
                }
            }

            Console.WriteLine("Все заклинания на перезарядке");
            Thread.Sleep(2000);
            return 0;
        }

        // Получении экипировки
        public void AddItem(Item item)
        {
            if (item is ItemWeapon weapon)
                _equpment.Weapon = weapon;
            else if (item is ItemClothes clothes)
                _equpment.Clothes = clothes;
            else if (item is ItemOther other)
                _equpment.Other = other;
        }
    }
}
