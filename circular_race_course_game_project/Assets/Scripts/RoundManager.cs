using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.Commands.WkTree;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    private int roundCounter; // Counter to keep track of the current round
    private ObjectPlacementManager objectPlacementManager; // Reference to the ObjectPlacementManager
    public Text roundDisplay; // UI text element to display the current round
    public Text timerDisplay; // UI text element to display the elapsed time
    public int oilSpawnRound; // Round number when oil puddles should spawn
    private ReadConfigFile numberOfRounds; // Reference to the ReadConfigFile for the number of rounds
    private PrefabGenerator prefabGenerator; // Reference to the PrefabGenerator
    public static List<float> roundTimes; // List to store the times for each round

    private float elapsedTime; // Elapsed time for the current round

    // Start is called before the first frame update
    public void Start()
    {
        objectPlacementManager = FindObjectOfType<ObjectPlacementManager>();
        prefabGenerator = FindObjectOfType<PrefabGenerator>();
        numberOfRounds = FindObjectOfType<ReadConfigFile>();
        
        roundTimes = new List<float>();
        
        roundDisplay.text = "Round: 1/3";
        timerDisplay.text = "Time: 0s";

        oilSpawnRound = Random.Range(1, ReadConfigFile.rounds + 1); // Randomly determine the round for oil puddle spawn

        elapsedTime = 0;
    }

    // Update the timer display during gameplay
    private void UpdateTimerDisplay()
    {
        if (!PauseManager.gamePaused && CountDownManager.canDrive)
        {
            elapsedTime += Time.deltaTime;
            timerDisplay.text = "Time: " + (elapsedTime / 10).ToString("F2") + "s";
        }
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTimerDisplay();
    }

    // Method called when a round is finished
    public void RoundFinished()
    {
        roundCounter++; // Increment the round counter

        // Store the start time for the current round, excluding the first round
        if (roundCounter > 1 && roundCounter <= ReadConfigFile.rounds)
        {
            roundTimes.Add(elapsedTime);
        }

        // Update UI and initiate server connection for the next round
        if (roundCounter > 1 && roundCounter <= ReadConfigFile.rounds)
        {
            roundDisplay.text = "Round: " + roundCounter + "/3";

            objectPlacementManager.ServerConnect();
            
            elapsedTime = 0; // Reset elapsed time for the next round
        }

        // Set a flag to spawn oil puddles if the current round matches the randomly chosen round
        if (roundCounter == oilSpawnRound)
        {
            prefabGenerator.spawnOilPuddle = true;
        }

        // Load the end scene when all rounds are completed
        if (ReadConfigFile.rounds < roundCounter)
        {
            roundTimes.Add(elapsedTime); // Store the time for the last round
            SceneManager.LoadScene("EndScene");
        }
    }

    // Getter for roundCounter
    public int RoundCounter
    {
        get { return roundCounter; }
    }
}
