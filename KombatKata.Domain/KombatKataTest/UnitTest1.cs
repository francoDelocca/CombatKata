using KombatKata.Domain.Domain;
using NUnit.Framework;

namespace KombatKataTest
{
    public class CharacterShould
    {
        private Character _character;
        [SetUp]
        public void SetUp()
        {
            _character = new();
        }

        [Test]
        public void StartWithDefaultHealth()
        {
            Assert.AreEqual(1000, _character.GetHealth());
        }

        [Test]
        public void StartLevelOne()
        {
            Assert.AreEqual(1, _character.GetLevel());
        }

        [Test]
        public void StartAlive()
        {
            Assert.AreEqual(true, _character.IsAlive());
        }

        [Test]
        public void DealDamageToCharacters()
        {
            _character.DealDamage(_character, 50);

            Assert.AreEqual(950, _character.GetHealth());
        }

        [Test]
        public void DieWhenTheReceivedDamageExceedsCurrentHealth()
        {
            _character.DealDamage(_character, _character.GetHealth() + 1);

            Assert.AreEqual(false, _character.IsAlive());
        }

        [Test]
        public void WhenTheReceivedDamageExceedsCurrentHealthThisBecomeZero()
        {
            _character.DealDamage(_character, _character.GetHealth() + 1);

            Assert.AreEqual(0, _character.GetHealth());
        }

        [Test]
        public void HealCharacters()
        {

            _character.DealDamage(_character, 100);
            _character.Heal(_character, 50);

            Assert.AreEqual(950, _character.GetHealth());
        }

        [Test]
        public void NotHealAboveDefaultHealth()
        {
            _character.Heal(_character, 1000);

            Assert.AreEqual(1000, _character.GetHealth());
        }
    }
}