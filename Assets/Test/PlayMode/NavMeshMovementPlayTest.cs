using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace UnityTests
{
    public class NavMeshMovementPlayTest
    {
        string SceneName = "Level 1",
               NavMeshTest = "TestNavMesh";

        Scene testScene;

        [UnitySetUp]
        public IEnumerator Initialize()
        {
            testScene = SceneManager.GetActiveScene();  /// Store test scene to recover after test is finished
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName));
        }

        [UnityTest]
        public IEnumerator NavSceneCanBeLoaded_ByName()
        {                        
            Assert.IsTrue(SceneManager.GetActiveScene().name == SceneName);
            yield return null;
        }

        [UnityTest]
        public IEnumerator AtLeast_OnePlayer_IsCreated()
        {            
            var player = GameObject.FindObjectOfType<PlayerController>();
            Assert.IsNotNull(player);
            yield return null;            
        }

        [UnityTest]
        public IEnumerator CanTakeRandomPointOnNavMesh()
        {
            yield return null;
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

            // Pick the first indice of a random triangle in the nav mesh
            int t = Random.Range(0, navMeshData.indices.Length - 3);

            // Select a random point on it
            Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
            point = Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

            Assert.AreNotEqual(Vector3.zero, point);           
        }

        [UnityTest]
        public IEnumerator NavMeshMover_PlayerReachesRandomNavMeshPoint()
        {

            yield return SceneManager.LoadSceneAsync(NavMeshTest, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(NavMeshTest));

            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
            // Pick the first indice of a random triangle in the nav mesh
            int t = Random.Range(0, navMeshData.indices.Length - 3);
            // Select a random point on it
            Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
            point = Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

            var player = GameObject.FindObjectOfType<PlayerController>();
            (player.UnitMover as ClickToMoveUnitMover).Agent.SetDestination(point);

            Time.timeScale = 1.0f; /// Increase the timeScale so the test executes quickly
            yield return new WaitForSeconds(5f);
            
            Assert.AreEqual(point.x, player.transform.position.z);

            Time.timeScale = 1.0f;
        }

        [UnityTearDown]
        public IEnumerator AfterEveryTest()
        {
            Time.timeScale = 1.0f;
            foreach (var go in GameObject.FindObjectsOfType<PlayerController>())
                Object.Destroy(go);

            SceneManager.SetActiveScene(testScene);
            yield return SceneManager.UnloadSceneAsync(SceneName);
        }
    }
}
