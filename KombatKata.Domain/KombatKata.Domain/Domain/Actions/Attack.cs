using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KombatKata.Domain.Domain
{
    public class Attack
    {
        private const int DAMAGE_REDUCER_PERCENTAGE = 50;
        private const int DAMAGE_INCREASER_PERCENTAGE = 150;
        private const int DAMAGE_DEFAULT_PERCENTAGE = 100;

        private IRandom randomService;

        public Attack(IRandom randomService)
        {
            this.randomService = randomService;
        }

        public void Execute(Character target, Character attacker)
        {
            if (TargetIsAlly(target, attacker))
                return;

            if (AttackerIsInRange(target, attacker))
                return;

            target.ReceiveDamage(CalculateDamage(target, attacker));
        }

        private bool TargetIsAlly(Character target, Character attacker)
        {
            return attacker.Factions.IsAlly(target);
        }

        private bool AttackerIsInRange(Character target, Character attacker)
        {
            return target.GetMaxRange() > attacker.GetMaxRange();
        }

        private int CalculateDamage(Character target, Character attacker)
        {
            return CalculateDamageIfCritical(attacker) * GetDamagePercentage(target, attacker) / 100;
        }

        private int CalculateDamageIfCritical(Character attacker)
        {
            return IsCritical() ? attacker.GetDamage() * 2 : attacker.GetDamage();
        }

        private bool IsCritical()
        {
            return randomService.GetRandomValue() >= 80;
        }

        private int GetDamagePercentage(Character target, Character attacker)
        {
            if (TargetHasMoreLevelThanAttacker(target, attacker))
            {
                return DAMAGE_REDUCER_PERCENTAGE;
            }

            if (TargetHasLessLevelThanAttacker(target, attacker))
            {
                return DAMAGE_INCREASER_PERCENTAGE;
            }

            return DAMAGE_DEFAULT_PERCENTAGE;
        }

        private bool TargetHasMoreLevelThanAttacker(Character target, Character attacker)
        {
            return target.GetLevel() - attacker.GetLevel() > 0;
        }

        private bool TargetHasLessLevelThanAttacker(Character target, Character attacker)
        {
            return target.GetLevel() - attacker.GetLevel() < 0;
        }
    }
}
