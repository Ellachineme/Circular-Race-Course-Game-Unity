using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace
using System.Linq; // For LINQ methods

public class InventoryControlText : MonoBehaviour
{
    private List<PlayersItem> displayNames; // List to hold the display names
    private List<PlayersItem> displayValues; // List to hold the display values

    [SerializeField] private GameObject buttonTemplate; // Template for the buttons
    [SerializeField] private GridLayoutGroup gridGroup; // GridLayoutGroup to adjust the column count

    // Start is called before the first frame update
    void Start()
    {
        displayNames = new List<PlayersItem>(); // Initialize the display names list
        displayValues = new List<PlayersItem>(); // Initialize the display values list

        // Simulated JSON data string
        string jsonData = @"{
            ""messageType"": ""GAME_END"",
            ""winner"": ""1913af0d-5ce8-4a55-8267-8c4a0cd955b2"",
            ""globalStats"": [
                { ""displayName"": ""Total Killed Enemies"", ""displayValue"": ""450"" },
                { ""displayName"": ""Earned Gold"", ""displayValue"": ""400"" },
                { ""displayName"": ""Win Condition"", ""displayValue"": ""Last surviving Player"" }
            ]
        }";

        // Parse the JSON data
        GameEndData gameEndData = JsonUtility.FromJson<GameEndData>(jsonData);

        // Populate the lists with parsed data
        PopulateInventory(gameEndData);

        // Generate the inventory UI
        GenInventory();
    }

    // Populate the lists with data from JSON
    void PopulateInventory(GameEndData gameEndData)
    {
        foreach (var stat in gameEndData.globalStats)
        {
            // Add display name item (first row)
            PlayersItem displayNameItem = new PlayersItem
            {
                itemText = stat.displayName,
                isBold = true
            };
            displayNames.Add(displayNameItem);

            // Add display value item (second row)
            PlayersItem displayValueItem = new PlayersItem
            {
                itemText = stat.displayValue,
                isBold = false
            };
            displayValues.Add(displayValueItem);
        }
    }

    // Generate the inventory UI
    void GenInventory()
    {
        // Ensure the number of columns in the grid is set to the number of globalStats
        gridGroup.constraintCount = displayNames.Count;

        // Create the first row with display names
        foreach (PlayersItem newItem in displayNames)
        {
            GameObject newButton = Instantiate(buttonTemplate) as GameObject; // Instantiate a new button from the template
            newButton.SetActive(true); // Activate the button

            TMP_Text buttonText = newButton.GetComponent<InventoryText>().SetText(newItem.itemText); // Set the text on the button

            // Make the text bold
            buttonText.fontStyle = FontStyles.Bold;

            newButton.transform.SetParent(buttonTemplate.transform.parent, false); // Set the parent of the button to the template's parent
        }

        // Create a row break by adding an empty GameObject
        GameObject rowBreak = new GameObject("RowBreak");
        rowBreak.transform.SetParent(buttonTemplate.transform.parent, false);

        // Create the second row with display values
        foreach (PlayersItem newItem in displayValues)
        {
            GameObject newButton = Instantiate(buttonTemplate) as GameObject; // Instantiate a new button from the template
            newButton.SetActive(true); // Activate the button

            TMP_Text buttonText = newButton.GetComponent<InventoryText>().SetText(newItem.itemText); // Set the text on the button

            // Ensure the text is not bold
            buttonText.fontStyle = FontStyles.Normal;

            newButton.transform.SetParent(buttonTemplate.transform.parent, false); // Set the parent of the button to the template's parent
        }
    }

    // Class to represent an item in the player's inventory
    public class PlayersItem
    {
        public string itemText; // Text of the item
        public bool isBold; // Whether the text should be bold
    }
}

[System.Serializable]
public class GlobalStat
{
    public string displayName;
    public string displayValue;
}

[System.Serializable]
public class GameEndData
{
    public string messageType;
    public string winner;
    public List<GlobalStat> globalStats;
}
