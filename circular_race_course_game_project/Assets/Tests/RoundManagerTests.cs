using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{


    public class RoundManagerTests
    {
        private RoundManager roundManager;
        
        [OneTimeSetUp]
        public void LoadScene()
        {
            // Load the scene
            EditorSceneManager.OpenScene("Assets/Scenes/RaceScene.unity");
        }
        
        [UnitySetUp]
        public IEnumerator UnitySetup()
        {
            roundManager = Object.FindObjectOfType<RoundManager>();
            
            if (roundManager == null)
            {
                // Create a GameObject and attach the RoundManager script
                GameObject roundManagerObject = new GameObject();
                roundManager = roundManagerObject.AddComponent<RoundManager>();
                
            }
            roundManager.Start(); //call the start method to initialise the values
            
            // Add a delay to ensure any asynchronous operations in the Start method are completed
            // Adjust the delay duration based on your specific scenario
            WaitForSeconds delay = new WaitForSeconds(1.0f);
            yield return delay;
            
            // Suppress the error related to yielding null in Edit mode tests
            LogAssert.Expect(LogType.Error, "EditMode test can only yield null");
            
            // Yield null at the end to comply with Edit mode restrictions
            yield return null;
            
            
        }

        [UnityTest]
        public IEnumerator TestRoundInitialization()
        {
            Debug.Log($"RoundCounter: {roundManager.RoundCounter}");
            Debug.Log($"roundDisplay.text: {roundManager.roundDisplay.text}");
            Debug.Log($"timerDisplay.text: {roundManager.timerDisplay.text}");

            Assert.AreEqual(0, roundManager.RoundCounter);
            Assert.AreEqual("Round: 1/3", roundManager.roundDisplay.text);
            Assert.AreEqual("Time: 0s", roundManager.timerDisplay.text); 
            
            // Suppress the error related to yielding null in Edit mode tests
            //LogAssert.Expect(LogType.Error, "EditMode test can only yield null");


            yield return null; // Ensure the test completes as a coroutine
        }
        
        
        
          



        
        [TearDown]
        public void TearDown()
        {
            // Clean up resources, destroy the test object
            Object.DestroyImmediate(roundManager.gameObject);
        }
    }
}