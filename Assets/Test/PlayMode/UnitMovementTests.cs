using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace UnityTests
{
    public class UnitMovementTests
    {
        [UnityTest]
        public IEnumerator MovesAlongXAxis_ByHorizontalInput_WithSpeed1_ChangeXPositionByOne()
        {            
            var input = Substitute.For<IPlayerInput>();
            input.Horizontal.Returns(1);
            
            var player = new GameObject().AddComponent<PlayerController>();
            player.InputScheme = input;//player.UnitMover.Init(input);
            player.AnimCtrl = null;            
            player.Speed = 1f;

            var fixedTime = Time.fixedDeltaTime;
            yield return new WaitForSeconds(1.0f);

            Assert.AreEqual(fixedTime, player.transform.position.x, 0.00001f);
        }

        //[UnityTest] /// with changing TimeScale
        //public IEnumerator UnitReaches_TargetPoint_ByUsingUnitMover()
        //{            
        //    Time.timeScale = 20.0f;/// Increase the timeScale so the test executes quickly

        //    float time = 0;
        //    while (time < 5)
        //    {                
        //        //paddles[0].GetComponent<Paddle>().MoveUpY("Paddle1");
        //        time += Time.fixedDeltaTime;
        //        yield return new WaitForFixedUpdate();
        //    }

        //    Time.timeScale = 1.0f;/// Reset timeScale
        //}

        [TearDown]
        public void AfterEveryTest()
        {
            foreach (var go in GameObject.FindObjectsOfType<PlayerController>())
                Object.Destroy(go);
        }
    }
}
