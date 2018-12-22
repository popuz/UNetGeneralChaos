using NSubstitute;
using NUnit.Framework;
using UNetGeneralChaos;

namespace TutorialsUnitTest
{
    public class TrapTests
    {
        Trap _trap;
        ICharacter _character;

        [SetUp]
        public void Initialize()
        {            
            _trap = new Trap();
            _character = Substitute.For<ICharacter>();            
        }

        [Test]
        public void PlayerEntering_PlayerTargetedTrap_ReducesHealthByOne()
        {            
            _character.IsPlayer.Returns(true);

            _trap.HandleCharacterEnetered(_character, TrapTargetType.Player);

            Assert.AreEqual(-1f, _character.Health);
        }

        [Test]
        public void NpcEntering_NpcTargetedTrap_ReducesHealthByOne()
        {            
            _trap.HandleCharacterEnetered(_character, TrapTargetType.Npc);

            Assert.AreEqual(-1f, _character.Health);
        }
    }
}
