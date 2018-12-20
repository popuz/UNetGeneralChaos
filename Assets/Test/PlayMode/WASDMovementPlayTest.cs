using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace UNetGeneralChaos.IntegratedTests
{
    public class WASDMovementPlayTest     
    {        
        [UnityTest]
        public IEnumerator MovesAlongXAxis_ByHorizontalInput_WithSpeed1_ChangeXPositionByFiveWorldUnits_inFiveSeconds()
        {
            Time.timeScale = 40.0f; /// Increase the timeScale so the test executes quickly

            var _transform = new GameObject().transform;
            var _input = Substitute.For<IPlayerInput>();
            var _mover = new WASDUnitMover(_transform, 1f);
            _mover.Init(_input);

            _input.Horizontal.Returns(1f);

            float time = Time.fixedDeltaTime;
            while (time < 5)
            {
                _mover.Tick();
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Time.timeScale = 1.0f;/// Reset timeScale

            Assert.AreEqual(5f, _transform.position.x, 0.001f);
        }

        [TearDown]
        public void AfterEveryTest()
        {
            Time.timeScale = 1.0f;/// Reset timeScale
            /// If you create some Components, then clean them up
            //foreach (var go in GameObject.FindObjectsOfType<PlayerController>())   Object.Destroy(go);            
        }
    }
}
