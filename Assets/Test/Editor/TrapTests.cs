using NSubstitute;
using NUnit.Framework;
using UnityEditor.SceneManagement;

namespace UnityTests
{
    public class TrapTests
    {
        [Test]
        public void PlayerEntering_PlayerTargetedTrap_ReducesHealthByOne()
        {
            Trap trap = new Trap();
            ICharacter character = Substitute.For<ICharacter>();
            character.IsPlayer.Returns(true);

            trap.HandleCharacterEnetered(character, TrapTargetType.Player);

            Assert.AreEqual(-1f, character.Health);
        }

        [Test]
        public void NpcEntering_NpcTargetedTrap_ReducesHealthByOne()
        {
            Trap trap = new Trap();
            ICharacter character = Substitute.For<ICharacter>();

            trap.HandleCharacterEnetered(character, TrapTargetType.Npc);

            Assert.AreEqual(-1f, character.Health);
        }

        [SetUp]
        public void ResetScene()
        {
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
        }
    }
}
