using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnityTests
{
    //public class UnitMovementTests
    //{
    //    //[UnityTest]
    //    //public IEnumerator MovesAlongXAxis_ByHorizontalInput_WithSpeed1_ChangeXPositionByOne()
    //    //{
    //    //    var input = Substitute.For<IPlayerInput>();
    //    //    input.Horizontal.Returns(1);

    //    //    var player = new GameObject().AddComponent<PlayerController>();
    //    //    player.InputScheme = input;//player.UnitMover.Init(input);
    //    //    player.AnimCtrl = null;
    //    //    player.MoveSpeed = 1f;

    //    //    var fixedTime = Time.fixedDeltaTime;
    //    //    yield return new WaitForSeconds(1.0f);

    //    //    Assert.AreEqual(fixedTime, player.transform.position.x, 0.00001f);
    //    //}

    //    /// <summary>
    //    /// Sometimes programmers want to intentionally throw errors to make sure others (and themselves) know that something has gone awry, 
    //    /// that a method received information that it wasn't expecting. This is a good place to test these type of errors to make sure 
    //    /// they actually prevent folks from going down a rabbit hole t of unnecessary debugging.
    //    /// https://cantina.co/writing-unit-tests-in-unity-part-1/
    //    /// </summary>
    //    //[Test]
    //    //public void CatchingErrors()
    //    //{
    //    //    /// Тест пройдет, так как Unity сделало ThrowException. Т.е. это проверка на то, сделал ли программист ThrowException
    //    //    GameObject gameObject = new GameObject("test");
    //    //    Assert.Throws<MissingComponentException>( () => gameObject.GetComponent<Rigidbody>().velocity = Vector3.one );            
    //    //}

    //    #region Examples UnityBlog.TDD.SophiaClarke.2018 
    //    ///https://blogs.unity3d.com/ru/2018/11/02/testing-test-driven-development-with-the-unity-test-runner/
    //    //[Test]
    //    //public void AtLeastOnePaddleIsSuccesfullyCreated()
    //    //{
    //    //    GameObject[] paddles = CreatePaddles();
    //    //    Assert.IsNotNull(paddles); /// Assert that the paddles object exists
    //    //}

    //    //[Test]
    //    //public void TwoPaddlesAreSuccesfullyCreated()
    //    //{
    //    //    GameObject[] paddles = CreatePaddles();
    //    //    Assert.AreEqual(2, paddles.Length); /// Assert that the number of paddles equals 2
    //    //}

    //    /// <summary>
    //    /// If I was to use deltaTime here, the test would be unstable because it can vary. I set up Time.captureFramerate, 
    //    /// or fixedDeltaTime, to make this predictable. 
    //    /// </summary>
    //    //[UnityTest]
    //    //public IEnumerator Paddle1StaysInUpperCameraBounds()
    //    // {            
    //    //     Time.timeScale = 20.0f; /// Increase the timeScale so the test executes quickly

    //    //     /// _setup is a member of the class TestSetup where I store the code for
    //    //     /// setting up the test scene (so that I don’t have a lot of copy-pasted code)
    //    //     Camera cam = _setup.CreateCameraForTest();

    //    //     GameObject[] paddles = _setup.CreatePaddlesForTest();

    //    //     float time = 0;
    //    //     while (time < 5)
    //    //     {
    //    //         paddles[0].GetComponent<Paddle>().RenderPaddle();
    //    //         paddles[0].GetComponent<Paddle>().MoveUpY("Paddle1");
    //    //         time += Time.fixedDeltaTime;
    //    //         yield return new WaitForFixedUpdate();
    //    //     }

    //    //     Time.timeScale = 1.0f;/// Reset timeScale

    //    //     /// Edge of paddle should not leave edge of screen (Camera.main.orthographicSize - paddle.transform.localScale.y /2) 
    //    //     /// is where the edge of the paddle touches the edge of the screen, 
    //    //     /// and 0.15 is the margin of error I gave it to wait for the next frame            
    //    //     Assert.LessOrEqual(paddles[0].transform.position.y, (Camera.main.orthographicSize - paddles[1].transform.localScale.y / 2) + 0.15f);
    //    // }
    //    #endregion

    //    //[TearDown]
    //    //public void AfterEveryTest()
    //    //{
    //    //    foreach (var go in GameObject.FindObjectsOfType<PlayerController>())
    //    //        Object.Destroy(go);
    //    //}
    //}
}
