using KombatKata.Domain.Domain;
using NUnit.Framework;
using System.Linq;

namespace KombatKataTest
{
    public class CharacterTest
    {
        private const int INITIAL_HEALTH = 1000;
        private const int INITIAL_LEVEL = 1;
        private const int CHARACTER_DAMAGE = 50;
        private const int CHARACTER_HEAL = 25;
        private const int MEDIUM_LEVEL = 5;
        private const int HIGH_LEVEL = 10;

        private const int NO_CRITICAL_HIT = 50;
        private const int CRITICAL_HIT = 90;

        private Character _character;
        private Character _characterCriticalHit;
        private Character _dummyCharacter;
        private Character _dummyCharacterHighLevel;
        private Character _dummyCharacterHighestLevel;
        private MeleeFighter _meleeFighter;
        private RangedFighter _rangedFighter;

        private IRandom randomServiceTest;

        [SetUp]
        public void SetUp()
        {
            randomServiceTest = new RandomServiceTest(NO_CRITICAL_HIT);
            _character = new(INITIAL_LEVEL, randomServiceTest);
            _characterCriticalHit = new Character(INITIAL_LEVEL, new RandomServiceTest(CRITICAL_HIT));
            _meleeFighter = new(INITIAL_LEVEL, randomServiceTest);
            _rangedFighter = new(INITIAL_LEVEL, randomServiceTest);
            _dummyCharacter = new(INITIAL_LEVEL, randomServiceTest);
            _dummyCharacterHighLevel = new(MEDIUM_LEVEL, randomServiceTest);
            _dummyCharacterHighestLevel = new(HIGH_LEVEL, randomServiceTest);
        }

        [Test]
        public void CharacterShouldStartWithDefaultHealth()
        {
            Assert.AreEqual(INITIAL_HEALTH, _character.GetHealth());
        }

        [Test]
        public void CharacterShouldStartLevelOne()
        {
            Assert.AreEqual(INITIAL_LEVEL, _character.GetLevel());
        }

        [Test]
        public void CharacterShouldStartAlive()
        {
            Assert.AreEqual(true, _character.IsAlive());
        }

        [Test]
        public void CharacterShouldDealDamageToCharacters()
        {
            var expectedHeal = INITIAL_HEALTH - CHARACTER_DAMAGE;

            _character.DealDamage(_dummyCharacter);

            Assert.AreEqual(expectedHeal, _dummyCharacter.GetHealth());
        }

        [Test]
        public void CharacterShouldDieWhenTheReceivedDamageExceedsCurrentHealth()
        {
            CharacterShouldDealEnoughDamageToDie();

            Assert.AreEqual(false, _dummyCharacter.IsAlive());
        }

        private void CharacterShouldDealEnoughDamageToDie()
        {
            for (int i = 0; i < 100; i++)
            {
                _character.DealDamage(_dummyCharacter);
            }
        }

        [Test]
        public void CharacterShouldWhenTheReceivedDamageExceedsCurrentHealthThisBecomeZero()
        {
            CharacterShouldDealEnoughDamageToDie();

            Assert.AreEqual(0, _dummyCharacter.GetHealth());
        }

        [Test]
        public void CharacterShouldHeal()
        {
            var expectedHeal = INITIAL_HEALTH - CHARACTER_DAMAGE + CHARACTER_HEAL;

            _dummyCharacter.DealDamage(_character);
            _character.DoHeal(_character);

            Assert.AreEqual(expectedHeal, _character.GetHealth());
        }

        [Test]
        public void CharacterShouldNotHealAboveDefaultHealth()
        {
            _character.DoHeal(_character);

            Assert.AreEqual(INITIAL_HEALTH, _character.GetHealth());
        }

        [Test]
        public void CharacterShouldDealDamageToEnemiesNotToHimself()
        {
            _character.DealDamage(_character);

            Assert.AreEqual(INITIAL_HEALTH, _character.GetHealth());
        }

        [Test]
        public void CharacterShouldHealHimselfButNotHisEnemies()
        {
            _character.DealDamage(_dummyCharacter);
            _character.DoHeal(_dummyCharacter);

            Assert.AreEqual(INITIAL_HEALTH - CHARACTER_DAMAGE, _dummyCharacter.GetHealth());
        }

        [Test]
        public void CharacterShouldDealLessDamageWhenTargetIsFiveOrMoreLevelsAbove()
        {
            _character.DealDamage(_dummyCharacterHighLevel);

            Assert.AreEqual(975, _dummyCharacterHighLevel.GetHealth());
        }

        [Test]
        public void CharacterShouldDealMoreDamageWhenTargetIsFiveOrMoreLevelsBelow()
        {
            _dummyCharacterHighestLevel.DealDamage(_dummyCharacterHighLevel);

            Assert.AreEqual(925, _dummyCharacterHighLevel.GetHealth());
        }

        [Test]
        public void CharacterShouldHaveAnAttackRange()
        {
            Assert.AreEqual(0, _character.GetMaxRange());
        }

        [Test]
        public void MeleeFightersShouldHaveARangeOfTwo()
        {
            Assert.AreEqual(2, _meleeFighter.GetMaxRange());
        }

        [Test]
        public void RangedFightersShouldHaveARangeOfTwenty()
        {
            Assert.AreEqual(20, _rangedFighter.GetMaxRange());
        }

        [Test]
        public void CharactersShouldNotDealDamageWhenTheyAreNotInRange()
        {
            _meleeFighter.DealDamage(_rangedFighter);

            Assert.AreEqual(INITIAL_HEALTH, _rangedFighter.GetHealth());
        }

        [Test]
        public void CharactersMustBeInRangeToDealDamage()
        {
            _rangedFighter.DealDamage(_meleeFighter);

            Assert.AreEqual(INITIAL_HEALTH - _rangedFighter.GetDamage(), _meleeFighter.GetHealth());
        }

        [Test]
        public void NewlyCreatedCharactersBelongToNoFaction()
        {
            Assert.AreEqual(0, _character.Factions.GetFactions().Count);
        }

        [Test]
        public void CharacterShouldJoinAFaction()
        {
            Faction faction = new Faction();

            _character.JoinFaction(faction);

            Assert.AreEqual(faction, _character.Factions.GetFactions().FirstOrDefault());
        }

        [Test]
        public void CharacterShouldLeaveAFaction()
        {
            Faction faction = new Faction();

            _character.JoinFaction(faction);
            _character.LeaveFaction(faction);

            Assert.AreEqual(null, _character.Factions.GetFactions().FirstOrDefault());
        }

        [Test]
        public void CharactersThatBelongsToTheSameFactionShouldBeConsideredAllies()
        {
            Faction faction = new Faction();

            _character.JoinFaction(faction);
            _dummyCharacter.JoinFaction(faction);

            Assert.AreEqual(true, _character.Factions.IsAlly(_dummyCharacter));
        }

        [Test]
        public void CharacterAlliesCannontDealDamageToOneAnother()
        {
            Faction faction = new Faction();

            _character.JoinFaction(faction);
            _dummyCharacter.JoinFaction(faction);

            _character.DealDamage(_dummyCharacter);

            Assert.AreEqual(INITIAL_HEALTH, _dummyCharacter.GetHealth());
        }

        [Test]
        public void CharacterAlliesCanHealOneAnother()
        {
            Faction faction = new Faction();

            _character.JoinFaction(faction);
            _dummyCharacter.JoinFaction(faction);
            _dummyCharacterHighLevel.DealDamage(_dummyCharacter);
            _character.DoHeal(_dummyCharacter);

            var expectedHealth = INITIAL_HEALTH - (CHARACTER_DAMAGE + (CHARACTER_DAMAGE / 2)) + CHARACTER_HEAL;

            Assert.AreEqual(expectedHealth, _dummyCharacter.GetHealth());

        }

        [Test]
        public void CharacterShouldDealCriticalDamage()
        {
            _characterCriticalHit.DealDamage(_dummyCharacter);

            Assert.AreEqual(INITIAL_HEALTH - (CHARACTER_DAMAGE * 2), _dummyCharacter.GetHealth());
        }
    }
}