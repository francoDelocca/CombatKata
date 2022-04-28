using System;

namespace KombatKata.Domain.Domain
{
    public class Health
    {
        private const int INITIAL_HEALTH = 1000;
        private const int MINIMUM_HEALTH = 0;

        public int Amount { get; private set; }
        public bool IsAlive { get; private set; }

        public Health()
        {
            Amount = INITIAL_HEALTH;
            IsAlive = true;
        }

        public void ReceiveDamage(int amount)
        {
            Amount = Math.Max(MINIMUM_HEALTH, Amount - amount);
            IsAlive = CheckIfIsAlive();
        }

        private bool CheckIfIsAlive()
        {
            return !(Amount <= MINIMUM_HEALTH);
        }

        public void ReceiveHeal(int amount)
        {
            Amount = Math.Min(INITIAL_HEALTH, Amount + amount);
        }
    }
}