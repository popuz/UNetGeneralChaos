using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace UNetGeneralChaos.IntegratedTests
{
    public class NavMeshMovementPlayTest
    {
        string SceneName = "TestNavMesh";

        Scene testScene;
        PlayerController player;
        Vector3 point;
        NavMeshUnitMover navMeshUnitMover;

        [UnitySetUp]
        public IEnumerator Initialize()
        {
            testScene = SceneManager.GetActiveScene();  /// Store test scene to recover after test is finished
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName));

            player = GameObject.FindObjectOfType<PlayerController>();
            navMeshUnitMover = player.UnitMover as NavMeshUnitMover;
            //GameObject player = Resources.Load("Prefabs/Player") as GameObject;

            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
            /// Pick the first indice of a random triangle in the nav mesh
            int t = Random.Range(0, navMeshData.indices.Length - 3);

            /// Select a random point on it
            point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
            point = Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);
        }

        [UnityTest]
        public IEnumerator NavScene_CanBeLoaded_ByName()
        {                        
            Assert.IsTrue(SceneManager.GetActiveScene().name == SceneName);
            yield return null;
        }

        [UnityTest]
        public IEnumerator AtLeast_OnePlayer_ExistsOnScene()
        {                                    
            Assert.IsNotNull(player);
            yield return null;            
        }

        [UnityTest]
        public IEnumerator PlayerInTestScene_IsMoved_ByNavMeshUnitMover()
        {            
            Assert.IsNotNull(navMeshUnitMover);
            yield return null;
        }

        [UnityTest]
        public IEnumerator RandomPointOnNavMesh_NotEqualZero()
        {           
            Assert.AreNotEqual(Vector3.zero, point);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ClickToMoveUnithMover_PlayerCanReachesRandomNavMeshPoint()
        {
            navMeshUnitMover.Agent.SetDestination(point);            
                        
            Time.timeScale = 40.0f; /// Increase the timeScale so the test executes quickly
            yield return new WaitForSeconds(0.1f* Time.timeScale);
            
            Assert.AreEqual(point.x, player.transform.position.x);
            Assert.AreEqual(point.z, player.transform.position.z);

            Time.timeScale = 1.0f;            
        }

        [UnityTearDown]
        public IEnumerator AfterEveryTest()
        {
            Time.timeScale = 1.0f;

            SceneManager.SetActiveScene(testScene);
            yield return SceneManager.UnloadSceneAsync(SceneName);
        }
    }
}
