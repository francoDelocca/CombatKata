using System;
using System.Collections.Generic;
using System.Linq;

namespace KombatKata.Domain.Domain
{
    public class Factions
    {
        private List<Faction> factionsList;

        public Factions()
        {
            factionsList = new List<Faction>();
        }

        public List<Faction> GetFactions()
        {
            return factionsList;
        }

        public void Add(Faction faction)
        {
            factionsList.Add(faction);
        }

        public void Remove(Faction faction)
        {
            factionsList.Remove(faction);
        }

        public bool IsAlly(Character target)
        {
            return this.factionsList.Intersect(target.Factions.factionsList).Any();
        }
    }
}