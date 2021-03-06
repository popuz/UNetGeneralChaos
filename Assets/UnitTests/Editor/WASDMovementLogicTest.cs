﻿using NSubstitute;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UNetGeneralChaos.UnitTests
{
    public class WASDMovementLogicTest
    {
        private Transform _transform;
        private WASDUnitMover _mover;
        IPlayerInput _input;

        /// <summary>
        /// When running tests that create and destroy GameObjects, a good practice is to reset the environment to prevent 
        /// unintended side effects from previous tests. This is easily accomplished with the following code snippet.
        /// I tend to add this to each one of my tests files, even if I'm not creating GameObjects.
        /// If it’s always there I don't need to remember to add it when I do include a test that creates GameObjects.
        ///  https://cantina.co/writing-unit-tests-in-unity-part-1/
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene); /// ResetScene

            _transform = new GameObject().transform;
            _input = Substitute.For<IPlayerInput>();
            _mover = new WASDUnitMover(_transform, 1f);
            _mover.Init(_input);
        }

        [Test]
        public void IfNoDirectionWasProvided_TransformDoesntMove()
        {
            _mover.Tick();

            Assert.AreEqual(Vector3.zero, _transform.position);
            /// If we would like sertain precision then use this instead
            //Assert.AreEqual(expected: 1, _transform.position.x, delta: 0.01f);
            //Assert.AreEqual(expected: 1, _transform.position.y, delta: 0.01f);
            //Assert.AreEqual(expected: 1, _transform.position.z, delta: 0.01f);
        }

        [Test]
        public void IfSpeedIsEqualToZero_TransformDoesntMove()
        {
            _mover.Speed = 0f;            

            _mover.Tick();

            Assert.AreEqual(Vector3.zero, _transform.position);
        }

        [Test]
        public void MovesAlongXAxis_ByHorizontalInput_WithSpeedEqual1_ChangeXPositionByFixedDeltaTimeAmount()
        {
            _input.Horizontal.Returns(1f);

            _mover.Tick();

            Assert.AreEqual(Time.fixedDeltaTime, _transform.position.x, 0.0001f);
        }

        [TestCase(1f, 1f, 1f)]
        [TestCase(1f, 2f, 1f)]
        [TestCase(1f, 1f, 5f)]        
        public void MovesAlongXAxis_ByHorizontalInput_Speed_MoveTime(float hInput, float moveSpeed, float moveTime)
        {
            _input.Horizontal.Returns(hInput);
            _mover.Speed = moveSpeed;
            _mover.Tick(moveTime);

            Assert.AreEqual(expected: hInput* moveSpeed* moveTime, _transform.position.x, 0.0001f);
        }       

        [Test]
        public void MovesAlongZAxis_ByVerticalInput_WithSpeedEqual1_ChangeZPositionByFixedDeltaTimeAmount()
        {
            _input.Vertical.Returns(1f);

            _mover.Tick();

            Assert.AreEqual(Time.fixedDeltaTime, _transform.position.z, 0.0001f);
        }

        [TestCase(1f, 1f, 1f)]
        [TestCase(1f, 2f, 1f)]
        [TestCase(1f, 1f, 5f)]
        public void MovesAlongZAxis_ByVerticalInput_Speed_MoveTime(float vInput, float moveSpeed, float moveTime)
        {            
            _input.Vertical.Returns(vInput);
            _mover.Speed = moveSpeed;
            _mover.Tick(moveTime);

            Assert.AreEqual(expected: vInput * moveSpeed * moveTime, _transform.position.z, 0.0001f);
        }             
    }
}
