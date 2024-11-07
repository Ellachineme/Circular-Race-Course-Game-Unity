using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    public GameObject panel; // Reference to the GameObject that represents the pause menu panel

    public static bool gamePaused; // Static variable to track whether the game is paused or not

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false; // Set the initial state of the game to not paused
        panel.SetActive(false); // Deactivate the pause menu panel at the start
    }

    // Method to handle the pause button press
    public void PauseButtonPressed()
    {
        if (!gamePaused) // If the game is not paused
        {
            panel.SetActive(true); // Activate the pause menu panel
            gamePaused = true; // Set the game state to paused
            CountDownManager.canDrive = false; // Disable the ability to drive (assuming this is related to game controls)
        }
        else // If the game is already paused
        {
            panel.SetActive(false); // Deactivate the pause menu panel
            gamePaused = false; // Set the game state to not paused
            CountDownManager.canDrive = true; // Enable the ability to drive
        }
    }
}