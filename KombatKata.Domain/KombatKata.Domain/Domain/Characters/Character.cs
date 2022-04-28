using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KombatKata.Domain.Domain
{
    public class Character
    {
        private const int INITIAL_LEVEL = 1;
        private const int CHARACTER_DAMAGE = 50;
        private const int CHARACTER_HEAL = 25;
        private const int CHARACTER_RANGE = 0;

        private int _level;
        private int _damage;
        private int _heal;

        private Attack Attack { get; set; }
        private Heal Heal { get; set; }
        public Health Health { get; private set; }
        public Factions Factions { get; private set; }

        public Character(int level = INITIAL_LEVEL, IRandom randomService = null)
        {
            _damage = CHARACTER_DAMAGE;
            _heal = CHARACTER_HEAL;
            _level = level;

            Attack = new Attack(randomService);
            Heal = new Heal();
            Health = new Health();
            Factions = new Factions();

        }

        public int GetDamage()
        {
            return _damage;
        }

        public int GetHealth()
        {
            return Health.Amount;
        }

        public int GetLevel()
        {
            return _level;
        }

        public bool IsAlive()
        {
            return Health.IsAlive;
        }

        public void DealDamage(Character target)
        {
            if (target != this)
                Attack.Execute(target, this);
        }

        public void DoHeal(Character target)
        {
            Heal.Execute(target, this, _heal);
        }

        public virtual int GetMaxRange()
        {
            return CHARACTER_RANGE;
        }

        public void JoinFaction(Faction faction)
        {
            Factions.Add(faction);
        }

        public void LeaveFaction(Faction faction)
        {
            Factions.Remove(faction);
        }

        public void ReceiveDamage(int value)
        {
            Health.ReceiveDamage(value);
        }
    }
}