using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Tests
{
    public class ReadConfigFileTests
    {
        private ReadConfigFile configReader1;
        private ReadConfigFile configReader2;

        [SetUp]
        public void Setup()
        {
            // Create a GameObject and attach the ReadConfigFile script
            GameObject testObject1 = new GameObject();
            configReader1 = testObject1.AddComponent<ReadConfigFile>();

            GameObject testObject2 = new GameObject();
            configReader2 = testObject2.AddComponent<ReadConfigFile>();

        }

        [Test]
        public void LoadValidConfigFile()
        {
            // Assign a valid JSON file to the script's jsonFile field
            configReader1.jsonFile = Resources.Load<TextAsset>("validConfigFile");

            // Start the script
            configReader1.LoadConfig();

            // Assert that the values have been loaded correctly
            Assert.AreEqual(3, ReadConfigFile.rounds);
            Assert.AreEqual(3, ReadConfigFile.coinsForMoreSpeed);
            Assert.AreEqual(3, ReadConfigFile.coinsLoss);
            Assert.AreEqual(1, ReadConfigFile.oilSpills);
            Assert.AreEqual(2000, ReadConfigFile.speedLossTime);
        }

        [Test]
        public void LoadInvalidConfigFile()
        {
            // Assign an invalid JSON file to the script's jsonFile field
            configReader2.jsonFile = Resources.Load<TextAsset>("invalidConfigFile");

            // Use LogAssert.Expect to capture the expected error message in the log
            LogAssert.Expect(LogType.Error, "Error loading configuration: JSON parse error: Missing a name for object member.");

            // Start the script and allow it to throw the exception
            configReader2.LoadConfig();
           
            // Assert that the values remain at their default (0) because the file is invalid
            Assert.AreEqual(0, ReadConfigFile.rounds);
            Assert.AreEqual(0, ReadConfigFile.coinsForMoreSpeed);
            Assert.AreEqual(0, ReadConfigFile.coinsLoss);
            Assert.AreEqual(0, ReadConfigFile.oilSpills);
            Assert.AreEqual(0, ReadConfigFile.speedLossTime);
        }


        [TearDown]
        public void TearDown()
        {
            // Clean up resources, destroy the test object
            Object.DestroyImmediate(configReader1.gameObject);
            Object.DestroyImmediate(configReader2.gameObject);
        }
    }
}