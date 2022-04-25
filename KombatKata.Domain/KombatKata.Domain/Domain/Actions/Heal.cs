using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KombatKata.Domain.Domain
{
    public class Heal
    {
        public void Execute(Character target, Character healer, int amount)
        {
            if (IsSelfHeal(target, healer) || TargetIsAlly(target, healer))
                target.Health.ReceiveHeal(amount);
        }

        private bool TargetIsAlly(Character target, Character healer)
        {
            return healer.Factions.IsAlly(target);
        }

        private bool IsSelfHeal(Character target, Character healer)
        {
            return target.Equals(healer);
        }
    }
}
