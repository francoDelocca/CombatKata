using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KombatKata.Domain.Domain
{
    public class MeleeFighter : Character
    {
        private const int MELEE_FIGHTER_MAX_RANGE = 2;

        public MeleeFighter(int level = 1, IRandom randomService = null) : base(level, randomService)
        {
        }

        public override int GetMaxRange()
        {
            return MELEE_FIGHTER_MAX_RANGE;
        }
    }
}
