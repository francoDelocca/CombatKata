using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KombatKata.Domain.Domain
{
    public class RandomServiceTest : IRandom
    {
        public int CriticalHit { get; set; }

        public RandomServiceTest(int criticalHit)
        {
            CriticalHit = criticalHit;
        }

        public int GetRandomValue()
        {
            return CriticalHit;
        }
    }
}
