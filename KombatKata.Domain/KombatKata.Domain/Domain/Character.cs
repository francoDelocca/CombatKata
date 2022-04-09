using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KombatKata.Domain.Domain
{
    public class Character
    {
        private const int START_HEALTH = 1000;

        private int _health;
        private bool _isAlive;

        public Character()
        {
            _health = START_HEALTH;
            _isAlive = true;
        }

        public int GetHealth()
        {
            return _health;
        }

        public int GetLevel()
        {
            return 1;
        }

        public bool IsAlive()
        {
            return _isAlive;
        }

        private void ReceiveDamage(int amount)
        {
            _health = _health - amount <= 0 ? 0 : _health - amount;

            _isAlive = _health <= 0 ? false : true;
        }

        public void DealDamage(Character character, int amount)
        {
            character.ReceiveDamage(amount);
        }

        private void ReceiveHeal(int amount)
        {
            _health = _health + amount >= START_HEALTH ? START_HEALTH : _health + amount;
        }

        public void Heal(Character character, int amount)
        {
            character.ReceiveHeal(amount);
        }
    }
}