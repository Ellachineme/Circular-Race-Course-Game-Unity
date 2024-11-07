using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

namespace Tests
{
    [TestFixture]
    public class CarControllerTests
    {
        private CarController carController;
        

        [OneTimeSetUp]
        public void LoadScene()
        {
            // Load the scene
            EditorSceneManager.OpenScene("Assets/Scenes/RaceScene.unity");
        }

        [SetUp]
        public void SetUp()
        {
            // Find the CarController in the scene
            carController = Object.FindObjectOfType<CarController>();
           

            if (carController == null)
            {
                // Instantiate the CarController if not found
                // Create a new GameObject named "carObject"
                GameObject carObject = new GameObject();
                // Add a CarController script component to the carObject
                // and store a reference to it in the carController variable
                carController = carObject.AddComponent<CarController>();
                // Add a Rigidbody2D component to the carObject
                // and store a reference to it in the CarController's CarRigidbody2D property
                carController.CarRigidbody2D = carObject.AddComponent<Rigidbody2D>();

            }
            

            // Set the roadTilemap field in the CarController
            carController.roadTilemap = new GameObject().AddComponent<Tilemap>();
        }

        [Test]
        public void CarMovesWhenArrowKeysPressed_Right()
        {
            // Simulate arrow key input (e.g., move right)
            carController.HandleInput();

            // Set input values to simulate movement to the right
            carController.CarRigidbody2D.velocity = new Vector2(1f, 0f);

            carController.MoveCar(); // Call MoveCar method

            // Check if the car has moved
            Assert.AreNotEqual(Vector2.zero, carController.CarRigidbody2D.velocity);
        }
        
        [Test]
        public void CarMovesWhenArrowKeysPressed_Left()
        {
            // Simulate arrow key input (e.g., move left)
            carController.HandleInput();
    
            // Set input values to simulate movement to the left
            carController.CarRigidbody2D.velocity = new Vector2(-1f, 0f);

            carController.MoveCar(); // Call MoveCar method

            // Check if the car has moved
            Assert.AreNotEqual(Vector2.zero, carController.CarRigidbody2D.velocity);
        }

        [Test]
        public void CarMovesWhenArrowKeysPressed_Up()
        {
            // Simulate arrow key input (e.g., move up)
            carController.HandleInput();

            // Set input values to simulate movement up
            carController.CarRigidbody2D.velocity = new Vector2(0f, 1f);

            carController.MoveCar(); // Call MoveCar method

            // Check if the car has moved
            Assert.AreNotEqual(Vector2.zero, carController.CarRigidbody2D.velocity);
        }
        //Optional test for Exercise 4
        [Test]
        public void CarMovesWhenArrowKeysPressed_Down()
        {
            // Simulate arrow key input (e.g., move down)
            carController.HandleInput();
    
            // Set input values to simulate movement down
            carController.CarRigidbody2D.velocity = new Vector2(0f, -1f);

            carController.MoveCar(); // Call MoveCar method

            // Check if the car has moved
            Assert.AreNotEqual(Vector2.zero, carController.CarRigidbody2D.velocity);
        }
        
        
        

        [TearDown]
        public void TearDown()
        {
            // Clean up after each test
            Object.DestroyImmediate(carController.gameObject);
            Object.DestroyImmediate(carController.roadTilemap.gameObject);
        }
    }
}
