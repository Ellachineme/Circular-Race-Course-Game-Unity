using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

namespace Tests
{
    [TestFixture]
    public class ObjectPlacementTests
    {
        private PrefabGenerator prefabGenerator;

        [OneTimeSetUp]
        public void LoadScene()
        {
            // Load the scene
            EditorSceneManager.OpenScene("Assets/Scenes/RaceScene.unity");
        }

        [SetUp]
        public void SetUp()
        {
            //find the prefabgenerator in the scene
            prefabGenerator = Object.FindObjectOfType<PrefabGenerator>();

            if (prefabGenerator == null)
            {
                // Instantiate the PrefabGenerator if not found
                GameObject prefabGeneratorObject = new GameObject();
                prefabGenerator = prefabGeneratorObject.AddComponent<PrefabGenerator>();
                prefabGenerator.roadTilemap = new GameObject().AddComponent<Tilemap>();
            }
        }

        [Test]
        public void ObjectsArePlacedAppropriately()
        {
            // Call the method that generates objects
            prefabGenerator.GenerateObjects();

            // Get the position of an object that was generated
            Vector3 objectPosition = prefabGenerator.GetRandomCell();

            // Check if the object is placed within track boundaries
            Assert.IsTrue(prefabGenerator.IsPositionWithinTrackBoundaries(objectPosition));
        }
        


        
        [TearDown]
        public void TearDown()
        {
            // Clean up after each test
            Object.DestroyImmediate(prefabGenerator.gameObject);
        }
    }
}