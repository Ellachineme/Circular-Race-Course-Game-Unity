using UnityEngine;

// Serializable class to represent the structure of the game configuration
[System.Serializable]
public class GameConfig
{
    // Configuration properties
    public int noOfRounds;
    public int noOfCoinsForMoreSpeed;
    public int noOfCoinsLoss;
    public int noOfOilSpills;
    public int speedLossTime;
}

// MonoBehaviour class responsible for reading and loading the game configuration
public class ReadConfigFile : MonoBehaviour
{
    // Serialized field to specify the JSON file in the Unity Editor
    [SerializeField] public TextAsset jsonFile;

    // Static variables to store configuration values accessible from other classes
    public static int rounds;
    public static int coinsForMoreSpeed;
    public static int coinsLoss;
    public static int oilSpills;
    public static int speedLossTime;

    // Path to the JSON file (assumes the file is in the "Resources" folder)
    public string jsonFilePath = "configFile";

    // Start method called when the script is first loaded
    void Start()
    {
        // Load the game configuration
        LoadConfig();
    }

    // Method to load the game configuration from the JSON file
    public void LoadConfig()
    {
        try
        {
            // Load the JSON file from Resources based on the specified path
            jsonFile = Resources.Load<TextAsset>(jsonFilePath);

            if (jsonFile == null)
            {
                // Log an error if the JSON file is not found
                Debug.LogError($"JSON file not found at path: {jsonFilePath}");
                return;
            }

            // Attempt to deserialize the JSON file into the GameConfig object
            GameConfig config = JsonUtility.FromJson<GameConfig>(jsonFile.text);

            // Access configuration properties and assign them to static variables
            rounds = config.noOfRounds;
            coinsForMoreSpeed = config.noOfCoinsForMoreSpeed;
            coinsLoss = config.noOfCoinsLoss;
            oilSpills = config.noOfOilSpills;
            speedLossTime = config.speedLossTime;

            // Log the loaded configuration values for debugging purposes
            Debug.Log(
                $"Rounds: {rounds}, Coins for More Speed: {coinsForMoreSpeed}, Coins Loss: {coinsLoss}, Oil Spills: {oilSpills}, Speed Loss Time: {speedLossTime}");
        }
        catch (System.Exception e)
        {
            // Handle the exception if there is an error loading the configuration
            Debug.LogError($"Error loading configuration: {e.Message}");

            // Optionally, set default values or take other actions in case of an error
            rounds = 0;
            coinsForMoreSpeed = 0;
        }
    }
}
