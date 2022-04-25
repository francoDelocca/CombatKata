using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KombatKata.Domain.Domain
{
    public class RangedFighter : Character
    {
        private const int RANGED_FIGHTER_MAX_RANGE = 20;

        public override int GetMaxRange()
        {
            return RANGED_FIGHTER_MAX_RANGE;
        }
    }
}
