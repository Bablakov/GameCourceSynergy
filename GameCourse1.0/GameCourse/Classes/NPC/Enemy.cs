using GameCourse;

namespace GameCourse
{
    public abstract class Enemy : Unit
    {
        public Enemy()
        {
            string name = "Enemy";
            int health = 30;
            int damage = 5;
            _stats = new CharacterStats(name, health, damage, 5);
        }
        public Enemy(int lvlPlayer)
        {
            Random rnd = new Random();
            string name = "Enemy";
            int damage = 1 * rnd.Next(5, lvlPlayer * 5);
            int health = 5 * rnd.Next(5, lvlPlayer * 5);
            int shield = 2 * rnd.Next(5, lvlPlayer * 5);

            _stats = new CharacterStats(name, health, damage, shield);
            base._equpment = new Equpment();
            GenerateEqupment();
            CreateSpellList();
        }

        public Enemy(string name, int health, int damage)
        {
            _stats = new CharacterStats(name, health, damage, 10);
        }

        // Случайная генерация элемента экипировки
        private void GenerateEqupment()
        {
            Item item;
            if (new Random().Next(0, 4) == 0)
            {
                item = Shop.EnemyGenerateQupment();
                AddItem(item);
            }
        }
        
        // Создания списка заклинаний
        protected void CreateSpellList()
        {
            var rnd = new Random();
            for (int i = 0; i < rnd.Next(2, 6); i++)
            {
                _spells.Add(Spell.GenerateSpell());
            }
        }
    }
}
