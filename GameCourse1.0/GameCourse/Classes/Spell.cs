using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCourse
{
    public class Spell
    {
        private int _damage;
        private int _cooldown;
        private int _currentCD = 0;

        public int Damage => _damage;
        public bool Cooldown => _currentCD > 0;

        public Spell(int damage, int cooldown)
        {
            _damage = damage;
            _cooldown = cooldown;
        }

        // Использование магии
        public int Use()
        {
            Thread.Sleep(2000);

            _currentCD = _cooldown;
            return _damage;
        }

        // Случайная генерация магии
        public static Spell GenerateSpell()
        {
            var rnd = new Random();
            int dmg = rnd.Next(10, 75);
            int cd = rnd.Next(2, 5);
            return new Spell(dmg, cd);
        }
    }
}
